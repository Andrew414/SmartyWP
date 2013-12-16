using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    class DummyInternetHelper : IInternetHelper
    {
        static string HOUSEINFO = "{ \"smartyname\" : \"Demo server\", \"version\" : \"0.9b\" }";
        static string DEVICEINFO = "{ \"0\" : { \"type\" : \"light\", \"name\" : \"лампа в спальне\" }," +
            " \"1\" : { \"type\" : \"thermometer\", \"name\" : \"термометр в зале\" }," +
            " \"2\" : { \"type\" : \"light\", \"name\" : \"лампочка на кухне\" } }";
        static string HOUSEMAP = "{ \"floors\" : { \"floor1\" : \"/demomap.png\" }," +
            " \"devices\" : { \"0\" : \"1,650,170\", \"1\" : \"1,280,150\", \"2\" : \"1,400,500\"}}";
     

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

            return "";
        }
    }
}
