using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace stocktake
{
    static class Program
    {
        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            // Application.Run(new scanbar());
            //    Application.Run(new rpt());
            //  Application.Run(new bindInvMan());
              Application.Run(new Inventory());
          //  Application.Run(new test());

        }
    }
}
