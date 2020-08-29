using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {

    /// <summary>Base for windows shown when contacting the server</summary>
    public partial class ServerContactForm:Form {

        /// <summary>Creates a ServerContactForm</summary>
        public ServerContactForm(string Header, string Subtitle) {
            InitializeComponent();

            HeaderLabel.Text = Header;
            SubtitleLabel.Text = Subtitle;

            CancelBTN.Enabled = false;
            MainProgBar.Style = ProgressBarStyle.Marquee;
            
        }

        /// <summary>Runs the backgroundworker as soon as the form is shown</summary>
        protected virtual void ServerContactForm_Shown(object sender,System.EventArgs e) { MainBWorker.RunWorkerAsync(); }

        //------------------------------[Empty functions for other windows to override]------------------------------

        //Buttons/closing
        protected virtual void CancelBTN_Click(object sender,System.EventArgs e) {}
        protected virtual void ServerContactForm_FormClosing(object sender,FormClosingEventArgs e) {}

        // Background worker
        protected virtual void MainBWorker_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e) {}
        protected virtual void MainBWorker_ProgressChanged(object sender,System.ComponentModel.ProgressChangedEventArgs e) {}
        protected virtual void MainBWorker_RunWorkerCompleted(object sender,System.ComponentModel.RunWorkerCompletedEventArgs e) {}


    }
}
