using System;
using System.Windows.Forms;

namespace Igtampe.LBL.Server.Forms {

    /// <summary>LBL Settings Form</summary>
    public partial class SettingsForm:Form {

        /// <summary>Download Permission Level set in this settings window.</summary>
        public int DownloadPLevel => Decimal.ToInt32(DownloadPLevelSelector.Value);

        /// <summary>Upload permission level set in this settings window.</summary>
        public int UploadPLevel => Decimal.ToInt32(UploadPLevelSelector.Value);

        /// <summary>Root directory set in this settings window.</summary>
        public String RootDir => LBLDirectoryBox.Text;

        /// <summary>Creates a settings window configured with the specified settings.</summary>
        /// <param name="DownloadPLevel"></param>
        /// <param name="UploadPLevel"></param>
        /// <param name="RootDir"></param>
        public SettingsForm(int DownloadPLevel,int UploadPLevel,String RootDir) {
            InitializeComponent();
            DialogResult = DialogResult.Cancel;

            //Load the settings into the controls
            DownloadPLevelSelector.Value = DownloadPLevel;
            UploadPLevelSelector.Value = UploadPLevel;
            LBLDirectoryBox.Text = RootDir;

            //Set settings for the directory selector.
            LBLDirectorySelector.SelectedPath = RootDir;
            LBLDirectorySelector.Description = "Select the Root Directory for this LBL Server";
            LBLDirectorySelector.ShowNewFolderButton = true;
        }

        /// <summary>Click OK to OK</summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OKBtn_Click(object sender,EventArgs e) {
            DialogResult = DialogResult.OK;
            Close();
        }

        /// <summary>Lets you pick a folder as your root folder for the </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void BrowseBTN_Click(object sender,EventArgs e) {if(LBLDirectorySelector.ShowDialog()==DialogResult.OK){LBLDirectoryBox.Text = LBLDirectorySelector.SelectedPath;}}
    }
}
