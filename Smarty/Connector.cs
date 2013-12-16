using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    public class Connector
    {
        public static string DEVICEINFO = "/devices";
        public static string HOUSEINFO = "/houseinfo";
        public static string GETMAP = "/getmap";
        public static string GETSTATE = "/getstate";
        public static string SETSTATE = "/setstate";
        
        public bool Connect(string address)
        {
            return false;
        }
        public Dictionary<string, string> ProcessRequest(string request)
        {
            return null;
        }
    }
}
