using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Authentication;
using System.Threading;
using System.Windows.Forms;

namespace Igtampe.LBL.Client {

    /// <summary>Transfer Handler that can Download/Upload files via LBL if given an LBL Connection.</summary>
    public class LBLClientTransferHandler {

        //------------------------------[Variables]------------------------------

        /// <summary>private property used to store Busy</summary>
        private bool busy;

        /// <summary>Indicates whether this transfer handler is busy.</summary>
        public bool Busy {
            get { return busy; }
            set {
                busy = value;

                //if we were just made not busy, cancellations should not be pending.
                if(!busy) { CancellationPending = false; }
            }
        }

        /// <summary>Indicates when a cancellation is pending</summary>
        private bool CancellationPending = false;

        /// <summary>Progress for the current operation</summary>
        public double Progress { get; private set; }

        /// <summary>Lines Processed (sent or received depending on the current operation)</summary>
        public int LinesProcessed { get; private set; } = 0;
        
        /// <summary>Lines to be processed in total (Sent or received depending on the current operation)</summary>
        public int LinesTotal { get; private set; } = 0;

        /// <summary>Connection used for transfers</summary>
        protected LBLConnection Connection;

        //------------------------------[Constructor]------------------------------

        /// <summary>Creates an LBLClientTransferHandler</summary>
        /// <param name="Connection"></param>
        public LBLClientTransferHandler(LBLConnection Connection) {
            this.Connection = Connection;
            Progress = 0.0;
            Busy = false;
        }

        //------------------------------[Operations]------------------------------

        /// <summary>Downloads a file from the server</summary>
        /// <param name="RemoteFilename"></param>
        /// <param name="LocalFilename"></param>
        public void Download(string RemoteFilename, string LocalFilename) {

            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }

            //Make sure we're busy now
            Busy = true;

            //Set up the transfer and temporary list holder
            List<String> Lines = new List<string>();
            int[] DownloadInfo = Connection.OpenDownload(RemoteFilename);

            int ID = DownloadInfo[0];
            int Linecount = DownloadInfo[1];

            LinesTotal = Linecount; ;

            //Actually download this cosa
            for( int i = 0; i < Linecount; i++) {
                if(CancellationPending) {Connection.Close(ID); busy = false; return; }

                LinesProcessed = i;

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

        /// <summary>Uploads a file to the server</summary>
        /// <param name="RemoteFilename"></param>
        /// <param name="LocalFilename"></param>
        /// <param name="Overwrite"></param>
        public void Upload(string RemoteFilename,string LocalFilename,bool Overwrite) {

            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }

            //Make sure we're busy now
            Busy = true;

            //Set up the transfer and load the file into memory
            string[] AllLines = File.ReadAllLines(LocalFilename);
            int ID= Connection.OpenUpload(RemoteFilename,Overwrite);
            int Linecount = AllLines.Length;
            int i = 0;

            LinesTotal = Linecount;

            //Actually Upload the cosa
            foreach(string Line in AllLines) {
                if(CancellationPending) { Connection.Close(ID); busy = false;  return; }

                LinesProcessed = i;

                Progress = (i + 0.0) / (Linecount + 0.0); //gotta make sure that the progress is updated
                Connection.Append(ID,Line);
                i++;
            }

            //Close the transfer
            Connection.Close(ID);

            //make us no longer be busy.
            Busy = false;

        }

        //------------------------------[Async Thread Handlers]------------------------------

        /// <summary>Starts/handles Async exceptions for download</summary>
        private void Download(object obj) {
            string[] Split = obj.ToString().Split('|');
            busy = false;
            try { Download(Split[0],Split[1]); } 
            catch(AuthenticationException) { MessageBox.Show("User has no permission to download files","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(InvalidOperationException) { MessageBox.Show("Connection busy or not connected","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(FileNotFoundException) { MessageBox.Show("File not found.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(IOException) { MessageBox.Show("File is Busy","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(TimeoutException) { MessageBox.Show("Connection timed out! Probably disconnect and reconnect.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); } 
            catch(Exception E) { MessageBox.Show("Unhandled Exception: \n\n" + E.Message,"LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); } 
            finally { Busy = false; }
        }

        /// <summary>Starts/handles async exceptions for upload</summary>
        /// <param name="obj"></param>
        private void Upload(object obj) {
            string[] Split = obj.ToString().Split('|');
            busy = false;
            try { Upload(Split[0],Split[1],bool.Parse(Split[2])); }
            catch(AuthenticationException) { MessageBox.Show("User has no permission to upload files","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(InvalidOperationException) { MessageBox.Show("Connection busy or not connected","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(FileNotFoundException) { MessageBox.Show("File not found.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(IOException) { MessageBox.Show("File is Busy","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
            catch(TimeoutException) { MessageBox.Show("Connection timed out! Probably disconnect and reconnect.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); } 
            catch(Exception E) { MessageBox.Show("Unhandled Exception: \n\n" + E.Message,"LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); } 
            finally { Busy = false; }
        }

        //------------------------------[Async Thread starters]------------------------------

        /// <summary>Starts an Asynchronous Download Transfer</summary>
        /// <param name="RemoteFilename"></param>
        /// <param name="LocalFilename"></param>
        public void DownloadAsync(string RemoteFilename,string LocalFilename) {
            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }
            Busy = true;
            Thread DownloadThread = new Thread(Download);
            DownloadThread.Start(RemoteFilename+"|"+LocalFilename);
        }

        /// <summary>Starts an Asynchronous Upload Transfer</summary>
        /// <param name="RemoteFilename"></param>
        /// <param name="LocalFilename"></param>
        /// <param name="Overwrite"></param>
        public void UploadAsync(string RemoteFilename,string LocalFilename,bool Overwrite) {
            if(Busy) { throw new InvalidOperationException("Transfer handler is busy!"); }
            Busy = true;
            Thread UploadThread = new Thread(Upload);
            UploadThread.Start(RemoteFilename + "|" + LocalFilename + "|" + Overwrite.ToString());
        }

        //------------------------------[Cancel]------------------------------

        /// <summary>Sends a cancel notice to a currently running async operation</summary>
        public void Cancel() {if(Busy) { CancellationPending = true; }}


    }
}
