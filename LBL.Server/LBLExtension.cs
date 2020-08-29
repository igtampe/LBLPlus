using System;
using System.IO;
using Igtampe.Switchboard.Server;
using System.Collections.Concurrent;

namespace Igtampe.LBL.Server {

    /// <summary>Holds the LBL+ Extension</summary>
    public class LBLExtension:SwitchboardExtension {

        //------------------------------[Variables]------------------------------

        /// <summary>Default settings with 0 as permission level, and the LBL Folder in the current directory as the root directory for filesharing </summary>
        private static readonly string[] DefaultSettings = { "0~2~LBL\\" };

        /// <summary>Required permission level to access LBL</summary>
        private int DownloadPLevel = 0;
        private int UploadPLevel = 2;
        private string RootDir = "LBL\\";

        /// <summary>Dictionary of all transfers</summary>
        private readonly ConcurrentDictionary<int,LBLServerTransfer> Transfers;

        //------------------------------[Constructor]------------------------------

        /// <summary>Creates and initializes an LBL</summary>
        public LBLExtension():base("LBL+","1.0") {
            Transfers = new ConcurrentDictionary<int,LBLServerTransfer>();

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

        //------------------------------[Switchboard Functions]------------------------------

        public override string Help() {
            return "LBLPlus Extension Version 1.0\n" +
                "\n (All Commands have the prefix LBL and are separated by ~) \n\n" +
                "DOWNLOAD~(FILE)    | Starts a download transfer to retrieve that file. Returns transfer ID and linecount, separated by a :\n" +
                "UPLOAD~(FILE)      | Starts an upload transfer to append text to that file. Returns transfer ID \n" +
                "OVERWRITE~(FILE)   | Starts an upload transfer to overwrite text to that file. Returns transfer ID\n"+
                "APPEND~(ID)~(LINE) | Appends a line of text to the file on that transfer.\n" +
                "REQUEST~(ID)       | Requests the next line of text from the file on that transfer.\n" +
                "CLOSE~(ID)         | Closes Transfer with that ID\n" +
                "DIR~(SUBDIR)       | Sends a combination of two comma separated lists separated by a tilde (~).\n" +
                "                   | The first is of all directories, and the second is of all files. Give a\n" +
                "                   | as \\directory\\\n" +
                "PING               | Ping the LBLPlus Extension\n" +
                "\n" +
                "Results:\n\n" +
                "(text)             | Normal result for some commands\n" +
                "LBL.OK             | Normal result for some commands\n" +
                "LBL.EMPTY          | Empty directory/line\n" +
                "LBL.BUSY           | File is busy. \n" +
                "LBL.PLSCLOSE       | Reached end of file from download request. Close the request already!\n" +
                "LBL.NOTFOUND       | File/directory/transfer not found.\n" +
                "LBL.N              | Not enough permission level to execute.\n" +
                "LBL.A              | Argument Exception \n" +
                "LBL.E:(E):(Stack)  | Unhandled exception with stacktrace \n";
        }

        public override string Parse(ref SwitchboardUser User,string Command) {
            if(!Command.ToUpper().StartsWith("LBL~")) { return null; }

            string[] CommandSplit = Command.Split('~');
            try {
                switch(CommandSplit[1].ToUpper()) {
                    case "DOWNLOAD":
                        //Covering "\.." to avoid having people get out of the root directory.
                        if(!User.CanExecute(DownloadPLevel)||CommandSplit[2].Contains("\\..")) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        if(!File.Exists(RootDir + CommandSplit[2])) { return "LBL.NOTFOUND"; }
                        return CreateTransfer(LBLServerTransfer.LBLTransferType.Send,false,CommandSplit[2]);
                    case "UPLOAD":
                        if(!User.CanExecute(UploadPLevel) || CommandSplit[2].Contains("\\..")) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        if(FileBusy(CommandSplit[2],out int BusyID)) { return BusyID.ToString() ; }
                        return CreateTransfer(LBLServerTransfer.LBLTransferType.Receive,false,CommandSplit[2]);
                    case "OVERWRITE":
                        //Especially important here!!
                        if(!User.CanExecute(UploadPLevel) || CommandSplit[2].Contains("\\..")) { return "LBL.N"; }
                        if(CommandSplit.Length != 3) { return "LBL.A"; }
                        if(FileBusy(CommandSplit[2],out int doot)) { return "LBL.BUSY"; }
                        return CreateTransfer(LBLServerTransfer.LBLTransferType.Receive,true,CommandSplit[2]);
                    case "APPEND":
                        if(!User.CanExecute(UploadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length < 4) { return "LBL.A"; }
                        if(!int.TryParse(CommandSplit[2],out int AppendID)) { return "LBL.A"; }
                        if(!Transfers.ContainsKey(AppendID)) { return "LBL.NOTFOUND"; }

                        Transfers[AppendID].Receive(CommandSplit[3]);
                        return "LBL.OK";
                    case "REQUEST":
                        if(!User.CanExecute(DownloadPLevel)) { return "LBL.N"; }
                        if(CommandSplit.Length < 3) { return "LBL.A"; }
                        if(!int.TryParse(CommandSplit[2],out int RequestID)) { return "LBL.A"; }
                        if(!Transfers.ContainsKey(RequestID)) { return "LBL.NOTFOUND"; }

                        string Line = Transfers[RequestID].Send();
                        if(string.IsNullOrEmpty(Line)) { return "LBL.EMPTY"; } else { return Line; }
                    case "CLOSE":
                        if(!User.CanExecute(DownloadPLevel)) { return "LBL.N"; }
                        if(!int.TryParse(CommandSplit[2],out int CloseID)) { return "LBL.A"; }
                        if(!Transfers.ContainsKey(CloseID)) { return "LBL.NOTFOUND"; }

                        Transfers[CloseID].Close();
                        Transfers.TryRemove(CloseID,out LBLServerTransfer D);

                        return "LBL.OK";

                    case "DIR":
                        if(CommandSplit.Length != 3) { return "LBL.A"; }

                        if(!Directory.Exists(RootDir + CommandSplit[2]?.ToString())) { return "LBL.NOTFOUND"; }

                        //Get dirs
                        string[] Directories = Directory.GetDirectories(RootDir + CommandSplit[2]?.ToString());

                        //get files
                        string[] Files = Directory.GetFiles(RootDir + CommandSplit[2]?.ToString());

                        //empty directories will return just "~"
                        //if(Files.Length == 0) { return "LBL.EMPTY"; }

                        //replace prefix
                        for(int i = 0; i < Files.Length; i++) { Files[i] = Files[i].Replace(RootDir + CommandSplit[2]?.ToString(),""); }
                        for(int i = 0; i < Directories.Length; i++) { Directories[i] = Directories[i].Replace(RootDir + CommandSplit[2]?.ToString(),""); }

                        return String.Join("~",String.Join(",",Directories),String.Join(",",Files));

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

        //------------------------------[Internal Functions]------------------------------

        /// <summary>Verifies the list of transfers to see if this file is busy</summary>
        /// <returns>True if the file is being dealt with by a transfer, false otherwise.</returns>
        private bool FileBusy(string Filename, out int TransferID) {
            foreach(LBLServerTransfer transfer in Transfers.Values) {
                if(transfer.Filename.ToUpper() == Filename.ToUpper()) {
                    TransferID = transfer.ID;
                    return true; 
                }
            }
            TransferID = -1;
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
        public string CreateTransfer(LBLServerTransfer.LBLTransferType Type,bool Overwrite,String Filename) {
            int ID;
            do { ID = GenerateID(); } while(Transfers.ContainsKey(ID)); //Generate IDs until there's an ID that isn't there yet

            //Create the actual transfer and add it to the map
            LBLServerTransfer Transfer = new LBLServerTransfer(ID,RootDir,Filename,Overwrite,Type);
            Transfers.TryAdd(ID,Transfer);

            //Return the needed info
            if(Type == LBLServerTransfer.LBLTransferType.Send) { return ID.ToString() + ":" + Transfer.LineCount; }
            return ID.ToString();
        }

        //------------------------------[Settings Functions]------------------------------

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
