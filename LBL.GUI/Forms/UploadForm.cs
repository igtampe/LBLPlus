using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.ComponentModel;
using System.Threading;

namespace Igtampe.LBL.GUI.Forms {

    /// <summary>Form that can handle the entire process of uploading a file from an LBL Server</summary>
    public class UploadForm:DownloadForm {

        /// <summary>Specifies if this connection is going to overwrite the remote file</summary>
        protected bool Overwrite;

        /// <summary>Creates an upload form. Upload will start as soon as the form is shown.</summary>
        /// <param name="RemoteFile"></param>
        /// <param name="LocalFile"></param>
        /// <param name="Overwrite"></param>
        /// <param name="Connection"></param>
        public UploadForm(string RemoteFile,string LocalFile,bool Overwrite,LBLConnection Connection):base(RemoteFile,LocalFile,Connection) {

            HeaderLabel.Text = "Uploading File";

            this.Overwrite = Overwrite;
            Text = "Uploading " + LocalFile;
            Image.Image = Resources.FileOut;
        }

        /// <summary>Starts and shows progress for an upload.</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected override void MainBWorker_DoWork(object sender,DoWorkEventArgs e) {
            TransferHandler.UploadAsync(RemoteFile,LocalFile,Overwrite);

            while(TransferHandler.Busy) {
                MainBWorker.ReportProgress(Convert.ToInt32(TransferHandler.Progress * 100));
                Thread.Sleep(100);
            }

        }


    }
}
