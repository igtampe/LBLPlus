using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.ComponentModel;
using System.Threading;
using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    
    /// <summary>Form that can handle the entire process of downloading a file from an LBL Server</summary>
    public class DownloadForm:ServerContactForm {

        //------------------------------[Variables]------------------------------

        /// <summary>Local file path and name</summary>
        protected string LocalFile;

        /// <summary>Remote file path and name</summary>
        protected string RemoteFile;

        /// <summary>Transfer Handler that actually handles the transfer</summary>
        protected LBLClientTransferHandler TransferHandler;

        //------------------------------[Constructor]------------------------------

        /// <summary>Creates a download form. Download will start as soon as the form is launched.</summary>
        /// <param name="RemoteFile"></param>
        /// <param name="LocalFile"></param>
        /// <param name="Connection"></param>
        public DownloadForm(string RemoteFile, string LocalFile, LBLConnection Connection) : base("Downloading File",RemoteFile) {
            TransferHandler = new LBLClientTransferHandler(Connection);
            this.LocalFile = LocalFile;
            this.RemoteFile = RemoteFile;
            Image.Image = Resources.FileIn;

            Text = "Downloading " + RemoteFile;
            CancelBTN.Enabled = true;

            MainProgBar.Style = ProgressBarStyle.Continuous;
        }

        //------------------------------[The one button]------------------------------

        /// <summary>Cancels the transfer by just closing the window</summary>
        protected override void CancelBTN_Click(object sender,EventArgs e) {Close();}

        //------------------------------[Close handler]------------------------------

        /// <summary>Cancel the transfer and wait for it to close.</summary>
        protected override void ServerContactForm_FormClosing(object sender,FormClosingEventArgs e) {
            
            //Cancel the transfer
            TransferHandler.Cancel();

            //Wait for the transfer to finish up.
            while(TransferHandler.Busy) { }
        }

        //------------------------------[BackgroundWorker]------------------------------

        /// <summary>Actually starts the download and displays progress.</summary>
        protected override void MainBWorker_DoWork(object sender,DoWorkEventArgs e) {
            TransferHandler.DownloadAsync(RemoteFile,LocalFile);

            while(TransferHandler.Busy) {
                MainBWorker.ReportProgress(Convert.ToInt32(TransferHandler.Progress * 100));
                Thread.Sleep(100); 
            }
        }

        /// <summary>Displays the progress</summary>
        protected override void MainBWorker_ProgressChanged(object sender,ProgressChangedEventArgs e) {
            SubtitleLabel.Text = RemoteFile + " " + TransferHandler.LinesProcessed + "/" + TransferHandler.LinesTotal + " Lines processed (" + e.ProgressPercentage + "%)";
            MainProgBar.Value = e.ProgressPercentage;
        }

        /// <summary>Once this is done, close the form</summary>
        protected override void MainBWorker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {Close();}

    }
}
