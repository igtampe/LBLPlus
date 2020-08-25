using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Igtampe.Switchboard.Server;

namespace Igtampe.LBL.Server {
    /// <summary>Holds the LBL+ Extension</summary>
    public class LBLExtension:SwitchboardExtension {

        /// <summary>Default settings with 0 as permission level, and the LBL Folder in the current directory as the root directory for filesharing </summary>
        private static readonly string[] DefaultSettings = { "0:LBL\\" };

        /// <summary>Required permission level to access LBL</summary>
        private int PermissionLevel = 0;
        private string RootDir = "LBL\\";

        /// <summary>Creates and initializes an LBL</summary>
        public LBLExtension():base("LBL+","1.0") {

            //Write default 
            if(!File.Exists("LBL.cfg")) { File.WriteAllLines("LBL.cfg",DefaultSettings); }

            try {
                string[] Settings = File.ReadAllLines("LBL.CFG")[0].Split(':');
                PermissionLevel = int.Parse(Settings[0]);
                RootDir = Settings[1];

                //Make sure that if the rootdir doesn't exist, create it.
                if(!Directory.Exists(RootDir)) {Directory.CreateDirectory(RootDir);}

            } catch(Exception e) { Console.WriteLine(e.Message + "\n\n" + e.StackTrace); }


        }

        public override string Help() {
            throw new NotImplementedException();
        }

        public override string Parse(ref SwitchboardUser User,string Command) {
            throw new NotImplementedException();
        }
    }
}
