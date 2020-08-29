using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    public partial class MainForm:Form {

        private LBLConnection Connection;
        
        private Stack<String> Path;
        private String PathAsString;

        private LBLDirectory CurrentDirectory;

        private ServerContactForm DirectoryUpdateForm;

        public MainForm(LBLConnection Connection) {
            InitializeComponent();
            Icon = Resources.LBL_Standalone;

            this.Connection = Connection;
            if(!Connection.Connected) { throw new ArgumentException("Connection has to be connected!"); }

            Path = new Stack<string>();

        }

        private void MainForm_Shown(object sender,EventArgs e) {UpdateDirectory();}

        private void FolderListbox_SelectedIndexChanged(object sender,EventArgs e) {
            //Actually do nothing. Have users have to double click folders to access them.
        }

        private void FolderListBox_DoubleClick(object sender,EventArgs e) {
            //Go into the Folder
            if(FolderListBox.SelectedItem != null) {
                Path.Push(FolderListBox.SelectedItem.ToString());
                DownloadBTN.Enabled = false;
                UpdateDirectory();
            }
        }

        private void UpLevelBTN_Click(object sender,EventArgs e) {
            if(Path.Count != 0) {
                Path.Pop();
                UpdateDirectory();
            }
        }

        private void RootBTN_Click(object sender,EventArgs e) {
            Path.Clear();
            UpdateDirectory();
        }

        private void UploadBTN_Click(object sender,EventArgs e) {
            OpenFileDialog Dialog = new OpenFileDialog {
                Title = "Pick a file to upload",
                CheckFileExists = true,
                Multiselect = true
            };

            if(Dialog.ShowDialog() == DialogResult.OK) {
                foreach(string File in Dialog.FileNames) {

                    //get just the file name
                    String[] Path = File.Split('\\');
                    string Filename = Path[Path.Length - 1]; //last split from \ is the filename

                    if(CurrentDirectory.Files.Contains(Filename)) {
                        switch(MessageBox.Show("File " + Filename + " already exists in this directory. Overwrite?","LBL",MessageBoxButtons.YesNoCancel,MessageBoxIcon.Question)) {
                            case DialogResult.Yes:
                                UploadForm Uploader = new UploadForm(PathAsString + Filename,File,true,Connection);
                                Uploader.ShowDialog();
                                break;
                            case DialogResult.No:
                                UploadForm Uploader1 = new UploadForm(PathAsString + Filename,File,false,Connection);
                                Uploader1.ShowDialog();
                                break;
                            default:
                                break;
                        }
                    } else { 
                        UploadForm Uploader = new UploadForm(PathAsString + Filename,File,false,Connection);
                        Uploader.ShowDialog();
                    }


                }
            }

            UpdateDirectory();
        }

        private void DownloadBTN_Click(object sender,EventArgs e) {
            if(FilesListBox.SelectedItems.Count == 0) {
                MessageBox.Show("Select some files to download first!","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            SaveFileDialog Dialog = new SaveFileDialog() {
                Title = "Pick a filename and location to download this file",
                CheckPathExists = true
            };

            foreach(string File in FilesListBox.SelectedItems) {
                Dialog.FileName = File;
                Dialog.Title = "Where to save " + File + "?";
                if(Dialog.ShowDialog() == DialogResult.OK) {
                    DownloadForm downloader = new DownloadForm(PathAsString + File,Dialog.FileName,Connection);
                    downloader.ShowDialog();
                }
            }

        }

        private void FilesListBox_SelectedIndexChanged(object sender,EventArgs e) {DownloadBTN.Enabled = true;}

        public void UpdateDirectory() {

            Enabled = false;

            RootBTN.Enabled = false;
            UpLevelBTN.Enabled = false;

            //Get the directory header.
            PathAsString="";
            if(Path.Count != 0) {

                //get a stack in reverse order
                Stack<string> tempstack = new Stack<string>();
                foreach(String PathPortion in Path) {tempstack.Push(PathPortion);}

                PathAsString = String.Join("\\",tempstack)+"\\";
                RootBTN.Enabled = true;
                UpLevelBTN.Enabled = true;
            }
            PathAsString = "\\" + PathAsString;

            //Set title
            Text = "LBL Browser [LBL:\\\\" + Connection.IP + PathAsString + "]";

            //Setup the ServerContactForm
            DirectoryUpdateForm = new ServerContactForm("Retrieving directory " + PathAsString,"Please wait...");
            DirectoryUpdateForm.Show();

            //Start the Backgroundworker.
            UpdateDirectoryBW.RunWorkerAsync();

        }

        private void UpdateDirectoryBW_DoWork(object sender,DoWorkEventArgs e) {
            try {CurrentDirectory = Connection.GetDirectory(PathAsString);} 
            catch(Exception) {throw;}
        }

        private void UpdateDirectoryBW_RunWorkerCompleted(object sender,RunWorkerCompletedEventArgs e) {

            Enabled = true;
            DirectoryUpdateForm.Close();
            DirectoryUpdateForm.Dispose();

            if(e.Error is FileNotFoundException) { 
                MessageBox.Show("Folder " + PathAsString + " not found!","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                Path.Pop();
                UpdateDirectory();
                return;
            }

            if(e.Error != null) {
                if(e.Error is AuthenticationException) { MessageBox.Show("Not enough permissions to use the server","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
                if(e.Error is InvalidOperationException) { MessageBox.Show("Server doesn't have the LBL Extension","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }
                if(e.Error is TimeoutException) { MessageBox.Show("Server timed out! Disconnect and reconnect.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); }

                Close();
                return;
            }

            //Populate the listview
            FolderListBox.Items.Clear();
            FilesListBox.Items.Clear();

            //Populate Folders
            if(CurrentDirectory.Folders != null) {foreach(string Folder in CurrentDirectory.Folders) { FolderListBox.Items.Add(Folder); }}


            //Populate Files
            if(CurrentDirectory.Files != null) { foreach(string File in CurrentDirectory.Files) { FilesListBox.Items.Add(File); } }


        }

        private void DisconnectBTN_Click(object sender,EventArgs e) {Close();}

        private void FilesListBox_DoubleClick(object sender,EventArgs e) { DownloadBTN_Click(sender,e);}
    }
}
