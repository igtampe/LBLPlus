using System;
using System.IO;
using Igtampe.Switchboard.Server;
using System.Collections.Generic;

namespace Igtampe.LBL.Server {
    /// <summary>Holds the LBL+ Extension</summary>
    public class LBLExtension:SwitchboardExtension {

        /// <summary>Default settings with 0 as permission level, and the LBL Folder in the current directory as the root directory for filesharing </summary>
        private static readonly string[] DefaultSettings = { "0~2~LBL\\" };

        /// <summary>Required permission level to access LBL</summary>
        private int DownloadPLevel = 0;
        private int UploadPLevel = 2;
        private string RootDir = "LBL\\";

        private readonly Dictionary<int,LBLTransfer> Transfers;

        /// <summary>Creates and initializes an LBL</summary>
        public LBLExtension():base("LBL+","1.0") {

            Transfers = new Dictionary<int,LBLTransfer>();

            //Write default 
            if(!File.Exists("LBL.cfg")) { File.WriteAllLines("LBL.cfg",DefaultSettings); }

            try {
                string[] Settings = File.ReadAllLines("LBL.CFG")[0].Split('~');
                DownloadPLevel = int.Parse(Settings[0]);
                UploadPLevel = int.Parse(Settings[1]);
                RootDir = Settings[2];

                //Make sure that if the rootdir doesn't exist, create it.
                if(!Directory.Exists(RootDir)) {Directory.CreateDirectory(RootDir);}

            } catch(Exception e) { Console.WriteLine(e.Message + "\n\n" + e.StackTrace); }


        }

        public override string Help() {
            return "LBLPlus Extension Version 1.0\n" +
                "\n (All Commands have the prefix LBL and are separated by ~) \n\n" +
                "DOWNLOAD~(FILE)    | Starts a download transfer to retrieve that file. Returns transfer ID.\n" +
                "UPLOAD~(FILE)      | Starts an upload transfer to append text to that file. Returns transfer ID \n" +
                "OVERWRITE~(FILE)   | Starts an upload transfer to overwrite text to that file. Returns transfer ID\n"+
                "APPEND~(ID)~(LINE) | Appends a line of text to the file on that transfer.\n" +
                "REQUEST~(ID)       | Requests the next line of text from the file on that transfer.\n" +
                "CLOSE~(ID)         | Closes Transfer with that ID\n" +
                "DIR~(SUBDIR)       | Sends a comma separated list of the files available on the server in the\n" +
                "                   | given subdirectory (as \\directory)\n" +
                "PING               | Ping the LBLPlus Extension\n" +
                "\n" +
                "Results:\n" +
                "(text)             | Normal result for some commands\n" +
                "LBL.OK             | Normal result for some commands\n" +
                "LBL.N              | Not enough permission level to execute.\n" +
                "LBL.A              | Argument Exception \n" +
                "LBL.E:(E):(Stack)  | Unhandled exception with stacktrace \n";
        }

        public override string Parse(ref SwitchboardUser User,string Command) {
            if(!Command.ToUpper().StartsWith("LBL:")) { return null; }

            string[] CommandSplit = Command.Split('~');
            try {
                switch(CommandSplit[1].ToUpper()) {
                    case "DOWNLOAD":
                        if(!User.CanExecute(DownloadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        return CreateTransfer(LBLTransfer.LBLTransferType.Send,false,CommandSplit[2]).ToString();
                    case "UPLOAD":
                        if(!User.CanExecute(UploadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        return CreateTransfer(LBLTransfer.LBLTransferType.Receive,false,CommandSplit[2]).ToString();
                    case "OVERWRITE":
                        if(!User.CanExecute(UploadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        return CreateTransfer(LBLTransfer.LBLTransferType.Receive,true,CommandSplit[2]).ToString();
                    case "APPEND":
                        if(!User.CanExecute(UploadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length < 4) { return "LBL.A"; }
                        if(int.TryParse(CommandSplit[2],out int AppendID)) { return "LBL.A"; }
                        Transfers[AppendID].Receive(CommandSplit[3]);
                        return "LBL.OK";
                    case "REQUEST":
                        if(!User.CanExecute(DownloadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length < 3) { return "LBL.A"; }
                        if(int.TryParse(CommandSplit[2],out int RequestID)) { return "LBL.A"; }
                        return Transfers[RequestID].Send();
                    case "CLOSE":
                        if(!User.CanExecute(DownloadPLevel)) { return "LBL.N"; }
                        if(int.TryParse(CommandSplit[2],out int CloseID)) { return "LBL.A"; }

                        Transfers[CloseID].Close();
                        Transfers.Remove(CloseID);

                        return "LBL.OK";

                    case "DIR":
                        //get files
                        String[] Files = Directory.GetFiles(RootDir + CommandSplit[2]?.ToString());

                        //replace prefix
                        for(int i = 0; i < Files.Length; i++) { Files[i] = Files[i].Replace(RootDir + CommandSplit[2]?.ToString(),""); }

                        return String.Join(",",Files);

                    case "PING":
                        return "PONG";
                    default:
                        return null;
                }

            } catch(Exception E) {
                Console.WriteLine(E.Message + "\n\n" + E.StackTrace);
                return "LBL.E:" + E.Message + ":" + E.StackTrace;
            }
        }

        /// <summary>Verifies the list of transfers to see if this file is busy</summary>
        /// <returns>True if the file is being dealt with by a transfer, false otherwise.</returns>
        public bool FileBusy(string Filename) {
            foreach(LBLTransfer transfer in Transfers.Values) {if(transfer.Filename.ToUpper() == Filename.ToUpper()) { return true; }}
            return false;
        }

        /// <summary>Generates an ID for a new LBL Transfer</summary>
        /// <returns>A random number from 0 to 99999</returns>
        public static int GenerateID() {return new Random().Next(0,99999);}

        /// <summary>Creates a transfer</summary>
        /// <param name="Type"></param>
        /// <param name="Overwrite"></param>
        /// <param name="Filename"></param>
        /// <returns>ID of the new transfer</returns>
        public int CreateTransfer(LBLTransfer.LBLTransferType Type,bool Overwrite,String Filename) {
            int ID;
            do { ID = GenerateID(); } while(Transfers.ContainsKey(ID));

            LBLTransfer Transfer = new LBLTransfer(ID,RootDir,Filename,Overwrite,Type);
            Transfers.Add(ID,Transfer);

            return ID;
        }

        public override void Settings() {
            Forms.SettingsForm Form = new Forms.SettingsForm(DownloadPLevel,UploadPLevel,RootDir);
            Form.ShowDialog();
            if(Form.DialogResult == System.Windows.Forms.DialogResult.OK) {
                //retrieve data
                DownloadPLevel = Form.DownloadPLevel;
                UploadPLevel = Form.UploadPLevel;
                RootDir = Form.RootDir;

                //save settings
                SaveSettings();
            }
        }

        /// <summary>Saves LBL's Settings.</summary>
        public void SaveSettings() {

            File.Delete("LBL.CFG");
            File.WriteAllText("LBL.cfg",DownloadPLevel.ToString() +"~" + UploadPLevel.ToString() + "~"+ RootDir);

        }


    }
}
