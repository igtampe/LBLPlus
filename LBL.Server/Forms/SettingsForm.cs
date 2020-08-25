using System;
using System.Windows.Forms;

namespace Igtampe.LBL.Server.Forms {
    public partial class SettingsForm:Form {

        public int DownloadPLevel => Decimal.ToInt32(DownloadPLevelSelector.Value);
        public int UploadPLevel => Decimal.ToInt32(UploadPLevelSelector.Value);
        public String RootDir => LBLDirectoryBox.Text;

        public SettingsForm(int DownloadPLevel,int UploadPLevel,String RootDir) {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;

            DownloadPLevelSelector.Value = DownloadPLevel;
            UploadPLevelSelector.Value = UploadPLevel;
            LBLDirectoryBox.Text = RootDir;

            LBLDirectorySelector.SelectedPath = RootDir;
            LBLDirectorySelector.Description = "Select the Root Directory for this LBL Server";
            LBLDirectorySelector.ShowNewFolderButton = true;

        }

        private void OKBtn_Click(object sender,EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        private void BrowseBTN_Click(object sender,EventArgs e) {if(LBLDirectorySelector.ShowDialog()==DialogResult.OK){LBLDirectoryBox.Text = LBLDirectorySelector.SelectedPath;}}
    }
}
