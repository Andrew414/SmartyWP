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

        static CommonHelper inst = null;

        private CommonHelper() { }
        public static CommonHelper GetInstanse()
        {
            if (inst == null) inst = new CommonHelper();
            return inst;
        }
    }
}
