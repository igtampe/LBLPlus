using Igtampe.LBL.GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    public partial class MainForm:Form {
        public MainForm() {
            InitializeComponent();
            Icon = Resources.LBL_Standalone;

            
        }

        private void MainForm_Load(object sender,EventArgs e) {
            UpLevelBTN.Enabled = false;
        }

        private void FolderListbox_SelectedIndexChanged(object sender,EventArgs e) {
            //Update the Files

        }

        private void FolderListBox_DoubleClick(object sender,EventArgs e) {
            //Go into the Folder
        }

        private void UpLevelBTN_Click(object sender,EventArgs e) {
            //Go Up One Level
        }

        private void RootBTN_Click(object sender,EventArgs e) {
            //Go to root level
        }

        private void UploadBTN_Click(object sender,EventArgs e) {
            //Pick a file and upload it.
        }

        private void DownloadBTN_Click(object sender,EventArgs e) {
            //Pick a place to save it and download it.
        }

        private void FilesListBox_SelectedIndexChanged(object sender,EventArgs e) {DownloadBTN.Enabled = true;}
    }
}
