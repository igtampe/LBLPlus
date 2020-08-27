using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    public partial class ServerContactForm:Form {

        public ServerContactForm(string Header, string Subtitle) {
            InitializeComponent();

            HeaderLabel.Text = Header;
            SubtitleLabel.Text = Subtitle;

            CancelBTN.Enabled = false;
            MainProgBar.Style = ProgressBarStyle.Marquee;
            
        }

        protected virtual void CancelBTN_Click(object sender,System.EventArgs e) {}
        protected virtual void ServerContactForm_FormClosing(object sender,FormClosingEventArgs e) {}

        protected virtual void MainBWorker_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e) {}
        protected virtual void MainBWorker_ProgressChanged(object sender,System.ComponentModel.ProgressChangedEventArgs e) {}
        protected virtual void MainBWorker_RunWorkerCompleted(object sender,System.ComponentModel.RunWorkerCompletedEventArgs e) {}

        protected virtual void ServerContactForm_Shown(object sender,System.EventArgs e) {MainBWorker.RunWorkerAsync();}

    }
}
