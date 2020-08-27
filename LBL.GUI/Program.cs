using System;
using System.Windows.Forms;
using Igtampe.LBL.GUI.Forms;

namespace Igtampe.LBL.GUI {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new LoginForm());
        }
    }
}
