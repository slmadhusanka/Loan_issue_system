using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loan_issue
{
    public partial class UserProfile : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public UserProfile()
        {
            InitializeComponent();
            GenerateJOBNumbe();
            LgUser.Text = User_ID.UserID;
            LgDisplayName.Text = User_ID.userName;
        }
        public void GenerateJOBNumbe()
        {
            #region New User Number...........................................
            try
            {
                SqlConnection Conn = new SqlConnection(IMS);
                Conn.Open();


                //=====================================================================================================================
                string sql = "SELECT UserCode FROM UserProfile";
                SqlCommand cmd = new SqlCommand(sql, Conn);
                SqlDataReader dr = cmd.ExecuteReader();

                //=====================================================================================================================
                if (!dr.Read())
                {
                    txtCashCode.Text = "USR1001";
                    // PassInvoiceNumber.Text = "INV1001";

                    cmd.Dispose();
                    dr.Close();

                }

                else
                {

                    cmd.Dispose();
                    dr.Close();

                    string sql1 = " SELECT TOP 1 UserCode FROM UserProfile order by UserCode DESC";
                    SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                    SqlDataReader dr7 = cmd1.ExecuteReader();

                    while (dr7.Read())
                    {
                        string no;
                        no = dr7[0].ToString();

                        string ItemNumOnly = no.Substring(3);

                        no = (Convert.ToInt32(ItemNumOnly) + 1).ToString();

                        txtCashCode.Text = "USR" + no;
                        // PassInvoiceNumber.Text = "INV" + no;

                    }
                    cmd1.Dispose();
                    dr7.Close();

                }
                Conn.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        #region Clear...........................................
        public void Clear()
        {
            //txtCashCode.Clear();
            txtCashName.Clear();
            txtlastName.Clear();
            txtDislayName.Clear();
            txtUserAddr.Clear();
            txttelUse.Clear();
            txtUserEMail.Clear();
            txtPassword.Clear();
            txtConfirmPs.Clear();
            cmbUerLeve.Items.Add("");
            TxtUserName.Text = "";
            checkBox1.Checked = false;

        }
        #endregion

        public void User_Role_Insert()
        {

            SqlConnection con = new SqlConnection(IMS);
            con.Open();

            string insertUser = "";

            if (cmbUerLeve.Text == "Admin" || cmbUerLeve.Text == "Chairman")
            {
                insertUser = @"insert into User_Setting( UserId, AllCustomerRPT, CompletedLoanRPT, DailyCollectionRPT, ExpireRPT, PaymethodRPT, ApprovePayment, BackUpSave, 
                      LoanIssue, NewCustomer, NewLoan, UserControl, UserProfile) values('" + txtCashCode.Text + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "','" + "1" + "')";

            }

            else
            {

                insertUser = @"insert into User_Setting ( UserId, AllCustomerRPT, CompletedLoanRPT, DailyCollectionRPT, ExpireRPT, PaymethodRPT, ApprovePayment, BackUpSave, 
                      LoanIssue, NewCustomer, NewLoan, UserControl, UserProfile) values('" + txtCashCode.Text + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "0" + "','" + "1" + "','" + "1" + "','" + "0" + "','" + "0" + "')";


            }


            SqlCommand cmd = new SqlCommand(insertUser, con);
            cmd.ExecuteNonQuery();


        }

        public void UserName_ck()
        {

            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();
            string checkId = "Select UserName from UserProfile WHERE UserName= '" + TxtUserName.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(checkId, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {
                MessageBox.Show("User Name Also Available.. please try another Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                TxtUserName.Focus();
                return;
            }
        }

        // slect value in list view===================================================================================================================================

        public void selectListview()
        {

            try
            {


                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectListView = "SELECT UserCode, FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive FROM UserProfile WHERE AtiveDeactive='1'";
                SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                lstPublic.Items.Clear();
                while (dr.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(dr[0].ToString());


                    //li.SubItems.Add(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());


                    lstPublic.Items.Add(li);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }

        }

        
        private void btnSave_Click(object sender, EventArgs e)
        {
            if (txtCashName.Text == "")
            {
                MessageBox.Show("Please Enter First Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtCashName.Focus();
                return;
            }
            if (txtlastName.Text == "")
            {
                MessageBox.Show("Please Enter Last Name.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtlastName.Focus();
                return;
            }
            if (txtDislayName.Text == "")
            {
                MessageBox.Show("Please Enter Dispal On System.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtDislayName.Focus();
                return;
            }
            if (txtUserAddr.Text == "")
            {
                MessageBox.Show("Please Enter Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserAddr.Focus();
                return;
            }
            if (txttelUse.Text == "")
            {
                MessageBox.Show("Please Enter Phone Number.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txttelUse.Focus();
                return;
            }
            if (txtUserEMail.Text == "")
            {
                MessageBox.Show("Please Enter E-Mail Address.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtUserEMail.Focus();
                return;
            }

            if (txtConfirmPs.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Please Enter correct password.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtPassword.Focus();
                return;
            }
            if (cmbUerLeve.Text == "")
            {
                MessageBox.Show("Please Enter Acting Role.", "Warning", MessageBoxButtons.OK, MessageBoxIcon.Information);
                cmbUerLeve.Focus();
                return;
            }



            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();
            string checkId = "Select UserCode from UserProfile WHERE UserCode= '" + txtCashCode.Text + "' ";
            SqlCommand cmd1 = new SqlCommand(checkId, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();



            if (!dr1.Read())
            {
                //insert user informations to the DB.....
                SqlConnection con = new SqlConnection(IMS);
                con.Open();
                string insertUser = "INSERT INTO UserProfile( UserCode , FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive,CreatedBy )Values('" + txtCashCode.Text + "','" + txtCashName.Text + "','" + txtlastName.Text + "','" + txtDislayName.Text + "','" + txtUserAddr.Text + "','" + txttelUse.Text + "','" + txtUserEMail.Text + "','" + txtPassword.Text + "','" + TxtUserName.Text + "','" + cmbUerLeve.Text + "','1','" + LgUser.Text + "')";
                SqlCommand cmd = new SqlCommand(insertUser, con);
                cmd.ExecuteNonQuery();

                //add user controle settings
                User_Role_Insert();

                MessageBox.Show("Successfully saved.", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                //GenerateJOBNumbe();
                Clear();
                GenerateJOBNumbe();

                selectListview();


                if (con.State == ConnectionState.Open)
                {
                    con.Close();
                }
            }

                //UPdate user================================================================================================================================================
                else
                {

                    if (checkBox1.Checked == true)
                    {
                        #region Deactivate-------------------------
                        DialogResult result = MessageBox.Show("Are You Sure Deactivate?", "Message ", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                        if (result == DialogResult.OK)
                        {
                            cmd1.Dispose();
                            dr1.Close();

                            SqlConnection con4 = new SqlConnection(IMS);
                            con4.Open();

                            string UserUpdate1 = "UPDATE  UserProfile SET AtiveDeactive='0' WHERE UserCode='" + txtCashCode.Text + "'";

                            SqlCommand cmd31 = new SqlCommand(UserUpdate1, con4);
                            cmd31.ExecuteNonQuery();

                            MessageBox.Show("Successfully Deactivate the User.", "Deactivated", MessageBoxButtons.OK, MessageBoxIcon.Information);

                            Clear();
                            selectListview();

                            btnSave.Text = "Save";


                            checkBox1.Visible = false;
                            GenerateJOBNumbe();
                        }

                        #endregion
                    }
                    if (checkBox1.Checked == false)
                    {
                        #region Update------------------------

                        cmd1.Dispose();
                        dr1.Close();

                        SqlConnection con3 = new SqlConnection(IMS);
                        con3.Open();
                       
                        string UserUpdate = "UPDATE  UserProfile SET UserCode ='" + txtCashCode.Text + "' ,FirstName ='" + txtCashName.Text + "' ,LastName ='" + txtlastName.Text + "' ,DisplayOn ='" + txtDislayName.Text + "' ,Addre ='" + txtUserAddr.Text + "' ,TPNum ='" + txttelUse.Text + "' ,EmailAddress ='" + txtUserEMail.Text + "' ,Passwod ='" + txtPassword.Text + "' ,UserName ='" + TxtUserName.Text + "'  ,ActingRole ='" + cmbUerLeve.Text + "',AtiveDeactive='1' WHERE UserCode='" + txtCashCode.Text + "'";

                        SqlCommand cmd3 = new SqlCommand(UserUpdate, con3);
                        cmd3.ExecuteNonQuery();

                        MessageBox.Show("Successfully Updated the User Details.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        btnSave.Enabled = true;
                        cmbUerLeve.SelectedIndex = -1;
                        Clear();
                        selectListview();

                        btnSave.Text = "Save";


                        checkBox1.Visible = false;
                        GenerateJOBNumbe();

                        #endregion
                    }

                }



            }



       

        private void lstPublic_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ListViewItem item = lstPublic.SelectedItems[0];
            txtCashCode.Text = item.SubItems[0].Text;
            txtCashName.Text = item.SubItems[1].Text;
            txtlastName.Text = item.SubItems[2].Text;
            txtDislayName.Text = item.SubItems[3].Text;
            txtUserAddr.Text = item.SubItems[4].Text;
            txttelUse.Text = item.SubItems[5].Text;
            txtUserEMail.Text = item.SubItems[6].Text;
            txtPassword.Text = item.SubItems[7].Text;
            TxtUserName.Text = item.SubItems[8].Text;
            cmbUerLeve.Text = item.SubItems[9].Text;

            if (item.SubItems[10].Text == "0")
            {
                checkBox1.Checked = true;
            }

            if (item.SubItems[10].Text == "1")
            {
                checkBox1.Checked = false;
            }

            txtConfirmPs.Text = "";

            checkBox1.Visible = true;
            btnSave.Text = "Update";
            
        }

        private void button7_Click(object sender, EventArgs e)
        {
            Clear();
            GenerateJOBNumbe();
            btnSave.Enabled = true;
            btnSave.Text = "Save";
            cmbUerLeve.SelectedIndex = -1;
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
           
        }

        private void TxtUserName_Leave(object sender, EventArgs e)
        {
            UserName_ck();
        }

        private void UserProfile_Load(object sender, EventArgs e)
        {
            checkBox1.Visible = false;
            selectListview();
            GenerateJOBNumbe();
        }

        private void chbAllView_CheckedChanged(object sender, EventArgs e)
        {
            if (chbAllView.Checked == true)
            {
                SqlConnection con1 = new SqlConnection(IMS);
                con1.Open();
                string selectListView = "SELECT UserCode, FirstName,LastName,DisplayOn,Addre,TPNum,EmailAddress,Passwod,UserName,ActingRole,AtiveDeactive FROM UserProfile ";
                SqlCommand cmd1 = new SqlCommand(selectListView, con1);
                SqlDataReader dr = cmd1.ExecuteReader(CommandBehavior.CloseConnection);
                lstPublic.Items.Clear();
                while (dr.Read() == true)
                {

                    ListViewItem li;
                    li = new ListViewItem(dr[0].ToString());


                    //li.SubItems.Add(dr[0].ToString());
                    li.SubItems.Add(dr[1].ToString());
                    li.SubItems.Add(dr[2].ToString());
                    li.SubItems.Add(dr[3].ToString());
                    li.SubItems.Add(dr[4].ToString());
                    li.SubItems.Add(dr[5].ToString());
                    li.SubItems.Add(dr[6].ToString());
                    li.SubItems.Add(dr[7].ToString());
                    li.SubItems.Add(dr[8].ToString());
                    li.SubItems.Add(dr[9].ToString());
                    li.SubItems.Add(dr[10].ToString());


                    lstPublic.Items.Add(li);
                }

            }
            if (chbAllView.Checked == false)
            {
                lstPublic.Items.Clear();
                selectListview();


            }
        }
    }
}