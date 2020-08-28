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
            LoginBW.RunWorkerAsync();

        }

        private void LoginBW_DoWork(object sender,System.ComponentModel.DoWorkEventArgs e) {
            if(Connection.Connect()) {Result = Connection.Login(Username,Password);}
        }



    }
}
 