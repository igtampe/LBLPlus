using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Igtampe.LBL.Client {
    public class LBLClientTransferHandler {

        /// <summary>Indicates whether this transfer handler is busy.</summary>
        public bool Busy { get; protected set; }

        /// <summary>Progress for the current operation</summary>
        public double Progress { get; private set; }

        /// <summary>Connection used for transfers</summary>
        protected LBLConnection Connection;

        public LBLClientTransferHandler(LBLConnection Connection) {
            this.Connection = Connection;
            Progress = 0.0;
            Busy = false;
        }

        public void Download(string RemoteFilename, string LocalFilename) {

            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }

            //Make sure we're busy now
            Busy = true;

            //Set up the transfer and temporary list holder
            List<String> Lines = new List<string>();
            int[] DownloadInfo = Connection.OpenDownload(RemoteFilename);

            int ID = DownloadInfo[0];
            int Linecount = DownloadInfo[1];

            //Actually download this cosa
            for(int i = 0; i == Linecount; i++) {
                Progress = (i+0.0) / (Linecount+0.0); //gotta make sure that the progress is updated
                try { Lines.Add(Connection.Request(ID)); } 
                catch(EndOfStreamException) { break; } //In case we go over for some reason
                catch(Exception) { throw; }
            }

            //Close the transfer
            Connection.Close(ID);

            //Save all text
            if(File.Exists(LocalFilename)) { File.Delete(LocalFilename); }
            File.WriteAllLines(LocalFilename,Lines);

            //make us no longer be busy.
            Busy = false;
        }

        public void Upload(string RemoteFilename,string LocalFilename,bool Overwrite) {

            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }

            //Make sure we're busy now
            Busy = true;

            //Set up the transfer and load the file into memory
            string[] AllLines = File.ReadAllLines(LocalFilename);
            int ID= Connection.OpenUpload(RemoteFilename,Overwrite);
            int Linecount = AllLines.Length;
            int i = 0;

            //Actually Upload the cosa
            foreach(string Line in AllLines) {
                Progress = (i + 0.0) / (Linecount + 0.0); //gotta make sure that the progress is updated
                Connection.Append(ID,Line);
                i++;
            }

            //Close the transfer
            Connection.Close(ID);

            //make us no longer be busy.
            Busy = false;

        }

        private void Download(object obj) {
            string[] Split = obj.ToString().Split('|');
            Download(Split[0],Split[1]);
        }
        private void Upload(object obj) {
            string[] Split = obj.ToString().Split('|');
            Upload(Split[0],Split[1],bool.Parse(Split[2]));
        }


        public void DownloadAsync(string RemoteFilename,string LocalFilename) {
            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }
            Busy = true;
            Thread DownloadThread = new Thread(Download);
            DownloadThread.Start(RemoteFilename+"|"+LocalFilename);
        }

        public void UploadAsync(string RemoteFilename,string LocalFilename,bool Overwrite) {
            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }
            Busy = true;
            Thread UploadThread = new Thread(Download);
            UploadThread.Start(RemoteFilename + "|" + LocalFilename + "|" + Overwrite.ToString());
        }



    }
}
