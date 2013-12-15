using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    public interface IServer
    {
        void LoadData();
        void SetupServer(Dictionary<String, String> parameters);
        bool IsServerKnown();
    }
}
