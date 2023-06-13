using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GsoRetinalCameraSaver
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Thread.Sleep(10 * 1000);
            _frm = new FrmMain();
            Application.Run(_frm);
        }
        static FrmMain _frm;

        public static void ScanDrive()
        {
            //var loc = @"C:\test\";
            var loc = "R:\\";

            foreach (var _ in Directory.EnumerateFiles(loc))
            {    
                if (_.EndsWith(".db")) continue;

               // var wnd = new FrmNewFile(_);
                //wnd.TopMost = true;
                //wnd.ShowDialog(_frm);
            }
        }
    }
}
