using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    public class HouseServer : IServer
    {
        public HouseServer()
        {

        }
        
        /*private static HouseServer instance = null;

        public static HouseServer GetInstance()
        {
            if (instance == null)
            {
                instance = new HouseServer();
            }
            return instance;
        }*/

        public bool LoadData()
        {
            return true;
        }

        public void SetupServer(Dictionary<String, String> parameters)
        {
            
        }

        public bool IsServerValid()
        {
            return false;
        }

        public string GetState(string id)
        {
            return "";
        }

        public string SetState(string id, string state)
        {
            return "";
        }
    }
}
