using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    class DummyServer : IServer
    {
        private DummyServer()
        {

        }
        private static DummyServer instance = null;

        public static DummyServer GetInstance()
        {
            if (instance == null)
            {
                instance = new DummyServer();
            }
            return instance;
        }

        public void LoadData()
        {

        }

        public void SetupServer(Dictionary<String, String> parameters)
        {

        }

        public bool IsServerKnown()
        {
            return System.Windows.MessageBoxResult.Cancel == System.Windows.MessageBox.Show(
                "Do you want to set up the server manually?", 
                "Question", 
                System.Windows.MessageBoxButton.OKCancel
            );
        }
    }
}
