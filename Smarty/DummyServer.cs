using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Windows;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Smarty
{
    class DummyServer : IServer
    {
        CommonHelper helper;
        DummyInternetHelper inter;

        public DummyServer()
        {
            helper = CommonHelper.GetInstanse();
            inter = new DummyInternetHelper();
        }
        /*
        private static DummyServer instance = null;

        public static DummyServer GetInstance()
        {
            if (instance == null)
            {
                instance = new DummyServer();
            }
            return instance;
        }*/

        public bool LoadData()
        {
            try
            {
                helper.model.Devices.Clear();

                helper.model.DevCoords.Clear();

                string smap = inter.DownloadFile(Connector.GETMAP);
                JObject map = JObject.Parse(smap);
                helper.model.housemap = map["floors"].First.First.ToString();

                Dictionary<string, DeviceItem> devices = new Dictionary<string, DeviceItem>();

                foreach (var i in map["devices"])
                {
                    //string str = i..ElementAt(0).ToString();
                    //dirty hack

                    string str = i.ToString().Replace("\n", "").Replace("\r", "").Replace(" ", "")
                        .Replace("\t", "").Replace("\"", "").Split(new char[] { ':' }).First();

                    devices.Add(str,new DeviceItem() { Floor = "1" });
                    string scoords = i.First.ToString();
                    int[] coords = scoords.Split(new char[] {','}).Select(x => int.Parse(x)).ToArray();
                    helper.model.DevCoords.Add(str, new Coords() { Floor=coords[0], X = coords[1], Y=coords[2] });
                }

                string devs = inter.DownloadFile(Connector.DEVICEINFO);

                JObject devmap = JObject.Parse(devs);

                foreach (var i in devmap)
                {
                    DeviceItem item = devices[i.Key];
                    item.ID = i.Key;
                    item.Name = i.Value["name"].ToString();
                    item.Type = i.Value["type"].ToString();

                    item.Floor = helper.model.DevCoords[item.ID].Floor.ToString();

                    item.LineMain = i.Value["name"].ToString();

                    string devstate = inter.DownloadFile(Connector.GETSTATE + "/" + item.ID);
                    JObject state = JObject.Parse(devstate);

                    if (state["result"].ToString() == "1")
                    {
                        item.State = state[(item.Type == "light") ? "state" : "temperature"].ToString();
                        item.LineMore = (item.Type == "light") ? (item.State == "1" ? "Включена" : "Выключена") : (item.State + " °C");
                    } 
                    else
                    {
                        item.State = "error";
                        item.LineMore = "Невозможно загрузить данные";
                    }

                    

                    devices[i.Key] = item;
                }

                foreach (var i in devices)
                {
                    helper.model.Devices.Add(i.Value);
                }

                helper.mainpage.demomode = true;

                return true;

            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message);
                return false;
            }
        }

        public void SetupServer(Dictionary<String, String> parameters)
        {

        }

        public bool IsServerValid()
        {
            try
            {
                string checkserver = inter.DownloadFile(Connector.HOUSEINFO);

                JObject json = JObject.Parse(checkserver);

                return json["smartyname"] != null;
            }
            catch (Exception)
            {
                return false;
            }
            /*
            return System.Windows.MessageBoxResult.Cancel == System.Windows.MessageBox.Show(
                "Do you want to set up the server manually?", 
                "Question", 
                System.Windows.MessageBoxButton.OKCancel
            );*/
        }
    }
}
