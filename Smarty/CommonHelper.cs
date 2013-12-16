using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Smarty
{
    public class CommonHelper
    {
        public MainPage mainpage = null;
        public SetupPage setuppage = null;
        public MainViewModel model = null;
        public BasicSetup info = null;

        static CommonHelper inst = null;

        private CommonHelper() { }
        public static CommonHelper GetInstanse()
        {
            if (inst == null) inst = new CommonHelper();
            return inst;
        }

        public void ExitApp()
        {
            throw new Exception("killing the app");
        }
    }
}
