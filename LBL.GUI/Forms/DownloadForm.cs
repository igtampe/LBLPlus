using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    public class DownloadForm:ServerContactForm {

        protected string LocalFile;
        protected string RemoteFile;
        protected LBLClientTransferHandler TransferHandler;

        public DownloadForm(string RemoteFile, string LocalFile, LBLConnection Connection) : base("Downloading File",RemoteFile) {
            TransferHandler = new LBLClientTransferHandler(Connection);
            this.LocalFile = LocalFile;
            this.RemoteFile = RemoteFile;
            Image.Image = Resources.FileIn;

            Text = "Downloading " + RemoteFile;
            CancelBTN.Enabled = true;

            MainProgBar.Style = ProgressBarStyle.Continuous;
        }

        protected override void CancelBTN_Click(object sender,EventArgs e) {Close();}

        protected override void ServerContactForm_FormClosing(object sender,FormClosingEventArgs e) {
            
            //Cancel the transfer
            TransferHandler.Cancel();

            //Wait for the transfer to finish up.
            while(TransferHandler.Busy) { }
        }

        protected override void MainBWorker_DoWork(object sender,DoWorkEventArgs e) {
            TransferHandler.DownloadAsync(RemoteFile,LocalFile);

            while(TransferHandler.Busy) {
                MainBWorker.ReportProgress(Convert.ToInt32(TransferHandler.Progress * 100));
                Thread.Sleep(100); 
            }

        }

        protected override void MainBWorker_ProgressChanged(object sender,ProgressChangedEventArgs e) {
            SubtitleLabel.Text = RemoteFile + " " + TransferHandler.LinesProcessed + "/" + TransferHandler.LinesTotal + " Lines processed (" + e.ProgressPercentage + "%)";
            MainProgBar.Value = e.ProgressPercentage;
        }

        protected override void MainBWorker_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {Close();}


    }
}
