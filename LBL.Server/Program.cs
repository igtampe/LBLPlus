using System;
using System.Collections.Generic;
using Igtampe.Switchboard.Server;
using System.Windows.Forms;
using System.Reflection;
using System.Drawing;

namespace Igtampe.LBL.Server {
    static class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            SwitchboardConfiguration Config = new SwitchboardConfiguration {
                ServerName = "LBL+ Server",
                ServerVersion = "1.0",
                DefaultIP = "127.0.0.1",
                DefaultPort = 909,
                AllowAnonymousDefault = true,
                MultiLoginDefault = true,
                DefaultWelcome = "Bonjour. Welcome to the server.",
                ServerExtensions = GetExtensions
            };

            Launcher.Launch("LBL+ Server",Icon.ExtractAssociatedIcon(Assembly.GetExecutingAssembly().Location),Config);
        }

        public static List<SwitchboardExtension> GetExtensions() {
            List<SwitchboardExtension> extensions = new List<SwitchboardExtension> {new LBLExtension()};
            return extensions;
        }

    }
}
