using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.ComponentModel;
using System.Threading;

namespace Igtampe.LBL.GUI.Forms {
    public class UploadForm:DownloadForm {

        protected bool Overwrite;

        public UploadForm(string RemoteFile,string LocalFile,bool Overwrite,LBLConnection Connection):base(RemoteFile,LocalFile,Connection) {

            HeaderLabel.Text = "Uploading File";


            this.Overwrite = Overwrite;
            Text = "Uploading " + LocalFile;
            Image.Image = Resources.FileOut;
        }

        protected override void MainBWorker_DoWork(object sender,DoWorkEventArgs e) {
            TransferHandler.UploadAsync(RemoteFile,LocalFile,Overwrite);

            while(TransferHandler.Busy) {
                MainBWorker.ReportProgress(Convert.ToInt32(TransferHandler.Progress * 100));
                Thread.Sleep(100);
            }

        }


    }
}
