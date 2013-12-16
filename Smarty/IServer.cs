using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    public interface IServer
    {
        bool LoadData();
        void SetupServer(Dictionary<String, String> parameters);
        bool IsServerValid();
        string GetState(string id);
        string SetState(string id, string state);
    }
}
