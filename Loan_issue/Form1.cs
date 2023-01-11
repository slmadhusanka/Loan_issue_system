using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;


namespace Loan_issue
{
    public partial class Form1 : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string UserName = "";
        public string UPassword = "";
        // public string UserID;
        public string UserDisplayName = "";

        public Form1()
        {
            InitializeComponent();
        }

        public void logintoform()
        {

            #region Load the User Administrator=======================================================================

            if (LgUserName.Text == "Jude123" && LgPassWord.Text == "DivyaaZMHo123")
            {
                MainForm mf = new MainForm();

                //ID and Displayname Pass to the Form
                mf.LgUser.Text = "USR0000";
                mf.LgDisplayName.Text = "SystemAdmin";
                mf.Visible = true;
                this.Hide();

                return;
            }

            // Load the User profile Database=======================================================================
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            //=====================================================================================================================
            string SelectUsers = @"SELECT  UserCode, DisplayOn, Passwod, UserName FROM UserProfile WHERE AtiveDeactive='1' AND UserName='" + LgUserName.Text + "' AND Passwod='" + LgPassWord.Text + "'";
            SqlCommand cmd = new SqlCommand(SelectUsers, Conn);

            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (dr.Read())
            {
                UserName = dr[3].ToString();
                UPassword = dr[2].ToString();
                string UserID1 = dr[0].ToString();
                UserDisplayName = dr[1].ToString();

                MainForm mf = new MainForm();

                //ID and Displayname Pass to the Form
                mf.LgUser.Text = UserID1;
                mf.LgDisplayName.Text = UserDisplayName;

                //pass value to class------------------
                User_ID.UserID = UserID1;
                User_ID.userName = UserDisplayName;

               // mf.UserDisplayName = UserDisplayName;
                mf.Visible = true;

                this.Hide();

            }

            else
            {
                MessageBox.Show("Please enter correct details and try again.", "Error Login", MessageBoxButtons.OK, MessageBoxIcon.Error);
                LgUserName.Focus();
            }

            #endregion =====================================================================================================

        }


        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LogOkBtn_Click(object sender, EventArgs e)
        {
            logintoform();
        }

        private void LogExitBtn_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void LgUserName_KeyDown(object sender, KeyEventArgs e)
        {

            if (e.KeyValue == 13)
            {
                LgPassWord.Focus();
            }
        }

        private void LgPassWord_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                logintoform();
            }
        }

        private void Form1_Activated(object sender, EventArgs e)
        {
            LgUserName.Focus();
        }
    }
}
