using System;
using System.IO;
using System.Security.Authentication;
using Igtampe.Switchboard.Common;

namespace Igtampe.LBL.Client {

    public class LBLConnection:SwitchboardClient {

        public LBLConnection(String IP, int Port):base(IP,Port) {}

        /// <summary>Returns a directory of all subdirectories and files in the server at the specified subdirectory</summary>
        /// <param name="Subdirectory"></param>
        /// <returns></returns>
        public LBLDirectory GetDirectory(string Subdirectory) {return new LBLDirectory(LBLSendReceive("DIR~" + Subdirectory));}

        /// <summary>Opens a download transfer on the server to download the specified file</summary>
        /// <param name="Filename"></param>
        /// <returns>Returns an array of 2 integers. The first is the ID of the transfer. The second is the number of lines in the file.</returns>
        public int[] OpenDownload(String Filename) {
            string[] ServerReturn = LBLSendReceive("DOWNLOAD~" + Filename).Split(':');
            int[] ReturnArray = { int.Parse(ServerReturn[0]),int.Parse(ServerReturn[1]) };
            return ReturnArray;
        }

        /// <summary>Opens an upload transfer on the server to upload the specified file </summary>
        /// <param name="Filename"></param>
        /// <param name="Overwrite">If true, deletes the already existing file</param>
        /// <returns>
        /// if the file isn't already being written to, a new transfer ID. <br></br>
        /// If the file is already being written to and overwrite is false, returns the ID of the pre-existing transfer. <br></br>
        /// If the file is already being written to and overwrite is true, throws an exception.
        /// </returns>
        public int OpenUpload(String Filename,bool Overwrite) {
            string ServerReturn;
            if(Overwrite) { ServerReturn = LBLSendReceive("OVERWRITE~" + Filename); } else { ServerReturn = LBLSendReceive("UPLOAD~" + Filename); }
            return int.Parse(ServerReturn);
        }

        /// <summary>Appends a line to the transfer</summary>
        /// <param name="ID"></param>
        /// <param name="Line"></param>
        /// <returns>True if the line was appended</returns>
        public bool Append(int ID,string Line) {return LBLSendReceive("APPEND~" + ID + "~" + Line) == "LBL.OK";}
        
        /// <summary>Requests a line from the transfer</summary>
        /// <param name="ID"></param>
        /// <returns>A line of text from the transfer</returns>
        public string Request(int ID) {return LBLSendReceive("REQUEST~" + ID);}

        /// <summary>Closes a transfer</summary>
        /// <param name="ID"></param>
        /// <returns>True if the transfer was closed, false otherwise</returns>
        public bool Close(int ID) {return LBLSendReceive("CLOSE~" + ID)=="LBL.OK";}

        /// <summary>Pings the server</summary>
        /// <returns>TRUE if the extension exists, false otherwise</returns>
        public bool Ping() {
            try {return LBLSendReceive("PING") == "PONG";} 
            catch(InvalidOperationException) {return false;} 
            catch(Exception) { throw; }
        }

        /// <summary>Handles LBL Send-Receive calls, and handles potential errors</summary>
        /// <param name="Send"></param>
        /// <returns>If everything is ok, returns the server return.</returns>
        private string LBLSendReceive(string Send) {
            string ServerReturn = SendReceive("LBL~" + Send);

            if(ServerReturn.StartsWith("Could not parse command")) { throw new InvalidOperationException("Server does not have the LBL Extension"); }
            if(ServerReturn.StartsWith("LBL.E:")) { throw new Exception(ServerReturn.Remove(0,6)); }

            switch(ServerReturn) {
                case "LBL.N":
                case "You're unauthorized to run any other commands.":
                    throw new AuthenticationException("User does not have permission to run this command");
                case "LBL.A":
                    throw new InvalidOperationException("Arguments for this call were incorrect");
                case "LBL.NOTFOUND":
                    throw new FileNotFoundException("File or Directory not found on the server");
                case "LBL.BUSY":
                    throw new IOException("File is busy");
                case "LBL.PLSCLOSE":
                    throw new EndOfStreamException("End of file reached! Close the stream!");
                case "LBL.EMPTY":
                    return "";
                default:
                    return ServerReturn;
            }
        }


    }
}
