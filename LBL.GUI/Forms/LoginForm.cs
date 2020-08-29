using Igtampe.LBL.GUI.Properties;
using System.Windows.Forms;
using Igtampe.LBL.Client;
using Igtampe.Switchboard.Common;

namespace Igtampe.LBL.GUI.Forms {

    /// <summary>Login Form that starts the program</summary>
    public partial class LoginForm:Form {

        //------------------------------[Variables]------------------------------

        /// <summary>Form that shows up when its time to login and contact the server</summary>
        private ServerContactForm Form;

        /// <summary>Connection that will be passed on to basically everything</summary>
        private LBLConnection Connection;

        /// <summary>Variable that'll hold the result for the BWorker</summary>
        private SwitchboardClient.LoginResult Result;

        /// <summary>Username holder for the background worker</summary>
        private string Username;

        /// <summary>Password holder for the Background Worker</summary>
        private string Password;

        //------------------------------[Constructor]------------------------------

        /// <summary>Constructs the LoginForm and starts the show!</summary>
        public LoginForm() {
            InitializeComponent();
            Icon = Resources.LBL_Standalone;
        }

        //------------------------------[Button]------------------------------

        /// <summary>Starts a Login Request</summary>
        private void LoginButton_Click(object sender,System.EventArgs e) {
            Enabled = false;
            Form = new ServerContactForm("Logging in to the server","Please wait...");

            try {Connection = new LBLConnection(IPBox.Text,int.Parse(PortBox.Text));} 
            catch(System.Exception) {MessageBox.Show("Unable to parse " + PortBox.Text + " to an integer","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error); return;}

            Username = UsernameTXB.Text;
            Password = PasswordTXB.Text;
            Form.Show();
            LoginBW.RunWorkerAsync();

        }

        //------------------------------[Background Worker]------------------------------

        /// <summary>Connects and logs in</summary>
        private void LoginBW_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e) {
            Result = SwitchboardClient.LoginResult.INVALID;
            if(Connection.Connect()) {Result = Connection.Login(Username,Password);}
        }

        /// <summary>Interprets the results from the background worker</summary>
        private void LoginBW_RunWorkerCompleted(object sender,System.ComponentModel.RunWorkerCompletedEventArgs e) {
            Form.Close();
            Enabled = true;

            //Check if we're connected
            if(!Connection.Connected) {
                MessageBox.Show("Could not connect to the server!","LBL",MessageBoxButtons.OK,MessageBoxIcon.Error);
                return;
            }

            //Switch on login result.
            switch(Result) {
                case SwitchboardClient.LoginResult.SUCCESS:
                    //We've successfully connected. Launch the de-esta cosa

                    //Launch the mainform
                    MainForm Haha = new MainForm(Connection);
                    Hide();
                    Haha.ShowDialog();
                    Show();
                    Connection.Close();

                    break;
                case SwitchboardClient.LoginResult.INVALID:
                    Connection.Close();
                    MessageBox.Show("Invalid Login Credentials","LBL",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    break;
                case SwitchboardClient.LoginResult.ALREADY:
                    Connection.Close();
                    //These shouldn't happen
                    MessageBox.Show("?","This isn't supposed to happen",MessageBoxButtons.OK,MessageBoxIcon.Question);
                    break;
                case SwitchboardClient.LoginResult.OTHERLOCALE:
                    Connection.Close();
                    MessageBox.Show("Already logged in on another client.","LBL",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                    break;
                default:
                    Connection.Close();
                    MessageBox.Show("?","This isn't supposed to happen",MessageBoxButtons.OK,MessageBoxIcon.Question);
                    break;
            }

        }
    }
}
 