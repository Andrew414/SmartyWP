using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    class DummyInternetHelper : IInternetHelper
    {
        Random rand = new Random(DateTime.Now.Millisecond);

        static string HOUSEINFO = "{ \"smartyname\" : \"Demo server\", \"version\" : \"0.9b\" }";
        static string DEVICEINFO = "{ \"0\" : { \"type\" : \"light\", \"name\" : \"лампа в спальне\" }," +
            " \"1\" : { \"type\" : \"thermometer\", \"name\" : \"термометр в зале\" }," +
            " \"2\" : { \"type\" : \"light\", \"name\" : \"лампочка на кухне\" } }";
        static string HOUSEMAP = "{ \"floors\" : { \"floor1\" : \"/demomap.png\" }," +
            " \"devices\" : { \"0\" : \"1,650,170\", \"1\" : \"1,280,150\", \"2\" : \"1,400,500\"}}";

        static string GETSTATE_TEMP_TEMPLATE = "{\"result\": \"1\", \"temperature\": \"$TEMP$\"}";
        static string GETSTATE_LAMP_TEMPLATE = "{\"result\": \"1\", \"state\": \"$STATE$\"}";

        static string SETSTATE_TEMPLATE = "{\"result\": \"$RES$\"}";


        bool bedroomlamp = false;
        bool kitchenlamp = true;

        public string DownloadFile(string url)
        {
            if (url == Connector.HOUSEINFO)
            {
                return HOUSEINFO;
            }

            if (url == Connector.GETMAP)
            {
                return HOUSEMAP;
            }

            if (url == Connector.DEVICEINFO)
            {
                return DEVICEINFO;
            }

            if (url == Connector.GETSTATE + "/1")
            {
                return GETSTATE_TEMP_TEMPLATE.Replace("$TEMP$", (rand.Next(3) + 20).ToString());
            }

            if (url == Connector.GETSTATE + "/0")
            {
                return GETSTATE_LAMP_TEMPLATE.Replace("$STATE$", bedroomlamp ? "1" : "0");
            }

            if (url == Connector.GETSTATE + "/2")
            {
                return GETSTATE_LAMP_TEMPLATE.Replace("$STATE$", kitchenlamp ? "1" : "0");
            }

            if (url.Contains(Connector.SETSTATE + "/1"))
            {
                return SETSTATE_TEMPLATE.Replace("$RES$", "0");
            }

            if (url.Contains(Connector.SETSTATE + "/0"))
            {
                var parsed = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                bool newstate = parsed.Last() == "1";
                bedroomlamp = newstate;
                return SETSTATE_TEMPLATE.Replace("$RES$", "1");
            }

            if (url.Contains(Connector.SETSTATE + "/2"))
            {
                var parsed = url.Split(new char[] { '/' }, StringSplitOptions.RemoveEmptyEntries);
                bool newstate = parsed.Last() == "1";
                kitchenlamp = newstate;
                return SETSTATE_TEMPLATE.Replace("$RES$", "1");
            }

            return "";
        }
    }
}
