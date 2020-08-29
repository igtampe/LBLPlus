using Igtampe.LBL.Client;
using Igtampe.LBL.GUI.Properties;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Security.Authentication;
using System.Windows.Forms;

namespace Igtampe.LBL.GUI.Forms {
    
    /// <summary>Main Form where everything is done</summary>
    public partial class MainForm:Form {

        //------------------------------[Variables]------------------------------

        /// <summary>Connection that runs the show</summary>
        private LBLConnection Connection;
        
        /// <summary>Stack that represents the path the server is in</summary>
        private Stack<String> Path;

        /// <summary>Path as string for access from the server (and display locally)</summary>
        private String PathAsString;

        /// <summary>Current directory</summary>
        private LBLDirectory CurrentDirectory;

        /// <summary>Form displayed when the server is reloading the directory.</summary>
        private ServerContactForm DirectoryUpdateForm;

        //------------------------------[Constructor]------------------------------

        /// <summary>Creates a main form that'll use the specified connection to run the show</summary>
        /// <param name="Connection"></param>
        public MainForm(LBLConnection Connection) {
            InitializeComponent();
            Icon = Resources.LBL_Standalone;

            this.Connection = Connection;
            if(!Connection.Connected) { throw new ArgumentException("Connection has to be connected!"); }

            Path = new Stack<string>();
        }

        //------------------------------[Shown]------------------------------

        /// <summary>Updates the directory as soon as the form is shown</summary>
        private void MainForm_Shown(object sender,EventArgs e) {UpdateDirectory();}

        //------------------------------[Buttons/clicks]------------------------------

        /// <summary>Moves to the double-clicked folder</summary>
        private void FolderListBox_DoubleClick(object sender,EventArgs e) {
            if(FolderListBox.SelectedItem != null) {
                Path.Push(FolderListBox.SelectedItem.ToString());
                DownloadBTN.Enabled = false;
                UpdateDirectory();
            }
        }

        /// <summary>Enable the download button once a file is selected</summary>
        private void FilesListBox_SelectedIndexChanged(object sender,EventArgs e) { DownloadBTN.Enabled = true; }

        /// <summary>Initiate a download when double clicking a file</summary>
        private void FilesListBox_DoubleClick(object sender,EventArgs e) { DownloadBTN_Click(sender,e); }

        /// <summary>Disconnect by closing the window. Loginform closes the connection when this form closes</summary>
        private void DisconnectBTN_Click(object sender,EventArgs e) { Close(); }

        /// <summary>Go up one level, if possible</summary>
        private void UpLevelBTN_Click(object sender,EventArgs e) {
            if(Path.Count != 0) {
                Path.Pop();
                UpdateDirectory();
            }
        }

        /// <summary>Go up to the root</summary>
        private void RootBTN_Click(object sender,EventArgs e) {
            Path.Clear();
            UpdateDirectory();
        }

        /// <summary>Pick File(s) to upload and then upload them using UploadForms</summary>
        private void UploadBTN_Click(object sender,EventArgs e) {
            
            //Create the openfile dialog
            OpenFileDialog Dialog = new OpenFileDialog {
                Title = "Pick a file to upload",
                CheckFileExists = true,
                Multiselect = true
            };

            //If yes, then upload each file.
            if(Dialog.ShowDialog() == DialogResult.OK) {
                foreach(string File in Dialog.FileNames) {

                    //get just the file name
                    String[] Path = File.Split('\\');
                    string Filename = Path[Path.Length - 1]; //last split from \ is the filename

                    //If there's a file there, ask whether or not to overwrite it.
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
                        //Just upload it.
                        UploadForm Uploader = new UploadForm(PathAsString + Filename,File,false,Connection);
                        Uploader.ShowDialog();
                    }


                }
            }

            UpdateDirectory();
        }

        /// <summary>Pick where to save each file and then download it.</summary>
        private void DownloadBTN_Click(object sender,EventArgs e) {

            //Make sure a file is selected
            if(FilesListBox.SelectedItems.Count == 0) {
                MessageBox.Show("Select some files to download first!","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            //Create the save-file dialog
            SaveFileDialog Dialog = new SaveFileDialog() {
                Title = "Pick a filename and location to download this file",
                CheckPathExists = true
            };

            //For each file, pick where to save it, then download it.
            foreach(string File in FilesListBox.SelectedItems) {
                Dialog.FileName = File;
                Dialog.Title = "Where to save " + File + "?";
                if(Dialog.ShowDialog() == DialogResult.OK) {
                    DownloadForm downloader = new DownloadForm(PathAsString + File,Dialog.FileName,Connection);
                    downloader.ShowDialog();
                }
            }

        }

        //------------------------------[Functions]------------------------------

        /// <summary>Prepares for updating the directory, then launch the backgroundworker that handles the rest</summary>
        public void UpdateDirectory() {

            //disable stuff
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
                
                //If we're here, theres folders to go up to, so enable these buttons again for later.
                RootBTN.Enabled = true;
                UpLevelBTN.Enabled = true;
            }

            //We add this here because requesting the root still has to be sent as \
            PathAsString = "\\" + PathAsString;

            //Set title
            Text = "LBL Browser [LBL:\\\\" + Connection.IP + PathAsString + "]";

            //Setup the ServerContactForm
            DirectoryUpdateForm = new ServerContactForm("Retrieving directory " + PathAsString,"Please wait...");
            DirectoryUpdateForm.Show();

            //Start the Backgroundworker.
            UpdateDirectoryBW.RunWorkerAsync();

        }

        //------------------------------[Backgroundworker]------------------------------

        /// <summary>Gets the directory from the server</summary>
        private void UpdateDirectoryBW_DoWork(object sender,DoWorkEventArgs e) {
            try {CurrentDirectory = Connection.GetDirectory(PathAsString);} 
            catch(Exception) {throw;}
        }

        /// <summary>Handles errors after the background worker and populates the listview with the result from the server.</summary>
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

    }
}
