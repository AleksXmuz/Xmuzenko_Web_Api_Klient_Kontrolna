using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Xmuzenko_Web_Api_Klient_Kontrolna.Forms;

namespace Xmuzenko_Web_Api_Klient_Kontrolna
{
    static class Program
    {
        public const string Url = "http://localhost:34562/api/";
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.SetHighDpiMode(HighDpiMode.SystemAware);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new TaskList());
        }
    }
}
