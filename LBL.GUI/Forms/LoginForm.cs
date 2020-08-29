using Igtampe.LBL.GUI.Properties;
using System.Windows.Forms;
using Igtampe.LBL.Client;
using Igtampe.Switchboard.Common;

namespace Igtampe.LBL.GUI.Forms {
    public partial class LoginForm:Form {

        private ServerContactForm Form;
        private LBLConnection Connection;
        private SwitchboardClient.LoginResult Result;

        private string Username;
        private string Password;

        public LoginForm() {
            InitializeComponent();
            Icon = Resources.LBL_Standalone;
        }

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

        private void LoginBW_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e) {
            Result = SwitchboardClient.LoginResult.INVALID;
            if(Connection.Connect()) {Result = Connection.Login(Username,Password);}
        }

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
 