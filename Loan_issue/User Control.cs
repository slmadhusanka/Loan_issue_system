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
//using Excel = Microsoft.Office.Interop.Excel;


namespace Loan_issue
{
    public partial class User_Control : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public User_Control()
        {
            InitializeComponent();
            LgDisplayName.Text = User_ID.userName;
            LgUser.Text = User_ID.UserID;
            LoadUser();
            desable();
            if (cmbUserID.SelectedIndex == -1)
            {
                desable();
                simpleButton1.Enabled = false;
            }
            if (cmbUserID.SelectedIndex != -1)
            {
                enable();
                simpleButton1.Enabled = true;
            }
        }
        public void uncheck()
        {
            #region checkbox uncheck..................................

            chbApprovePayment.Checked = false;
            chbBackUp.Checked = false;
            chbLoanIssue.Checked = false;
            chbNewCusto.Checked = false;
           
            chbUserControl1.Checked = false;
            chbUserProfile.Checked = false;
            
            rbtCompletedLoan.Checked = false;
            rbtDailyCollection.Checked = false;
            rbtExpierDetails.Checked = false;
            chbNewLoan.Checked = false;
           
            rbtPayMethod.Checked = false;
            rtbAllCustomer.Checked = false;
            cmbUserID.SelectedIndex = -1;
            label6.Text = "--";
            #endregion
        }

        public void enable()
        {
            #region checkboxs Enable..................................\

            chbApprovePayment.Enabled = true;
            chbBackUp.Enabled = true;
            chbLoanIssue.Enabled = true;
            chbNewCusto.Enabled = true;
            
            chbUserControl1.Enabled = true;
            chbUserProfile.Enabled = true;
           
            rbtCompletedLoan.Enabled = true;
            rbtDailyCollection.Enabled = true;
            rbtExpierDetails.Enabled = true;
            chbNewLoan.Enabled = true;
            
            rbtPayMethod.Enabled = true;
            rtbAllCustomer.Enabled = true;

            #endregion

        }

        public void desable()
        {
            #region checkbox Desable..................................

            chbApprovePayment.Enabled = false;
            chbBackUp.Enabled = false;
            chbLoanIssue.Enabled = false;
            chbNewCusto.Enabled = false;
            chbNewLoan.Enabled = false;
            chbUserControl1.Enabled = false;
            chbUserProfile.Enabled = false;
            
            rbtCompletedLoan.Enabled = false;
            rbtDailyCollection.Enabled = false;
            rbtExpierDetails.Enabled = false;
            
            
            rbtPayMethod.Enabled = false;
            rtbAllCustomer.Enabled = false;
            #endregion
        }


        public void LoadUser()
        {
            #region load User id....................................................................
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String UserID = "select UserCode,FirstName,LastName from UserProfile";
                SqlCommand cmm = new SqlCommand(UserID, cnn);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                cmbUserID.Items.Clear();
                while (dr.Read())
                {
                    cmbUserID.Items.Add(dr[0].ToString());
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            #endregion
        }



        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }
        String ApprovePay, Backup, loanIssue, newCustomer, NewLoan, userControl, userProfile, RAllCuss,  ACompletedLoan, AdailyCollection, Aexpier, APaymethode;
        private void simpleButton1_Click(object sender, EventArgs e)
        {


            #region if Condition..................................................................

            if (chbApprovePayment.Checked == true)
            {
                ApprovePay = "1";
            }
            if (chbApprovePayment.Checked == false)
            {
                ApprovePay = "0";
            }
            //-------------------------------------------------------------------------------------
            if (chbBackUp.Checked == true)
            {
                Backup = "1";
            }
            if (chbBackUp.Checked == false)
            {
                Backup = "0";
            }
            //------------------------------------------
            if (chbLoanIssue.Checked == true)
            {
                loanIssue = "1";
            }
            if (chbLoanIssue.Checked == false)
            {
                loanIssue = "0";
            }
            //------------------------------------------
            if (chbNewCusto.Checked == true)
            {
                newCustomer = "1";
            }
            if (chbNewCusto.Checked == false)
            {
                newCustomer = "0";
            }
            //------------------------------------------
            if (chbNewLoan.Checked == true)
            {
                NewLoan = "1";
            }
            if (chbNewLoan.Checked == false)
            {
                NewLoan = "0";
            }
            //------------------------------------------
            if (chbUserControl1.Checked == true)
            {
                userControl = "1";
            }
            if (chbUserControl1.Checked == false)
            {
                userControl = "0";
            }
            //------------------------------------------
            if (chbUserProfile.Checked == true)
            {
                userProfile = "1";
            }
            if (chbUserProfile.Checked == false)
            {
                userProfile = "0";
            }
            //------------------------------------------
            if (rtbAllCustomer.Checked == true)
            {
                RAllCuss = "1";
            }
            if (rtbAllCustomer.Checked == false)
            {
                RAllCuss = "0";
            }
            //------------------------------------------
            
            if (rbtCompletedLoan.Checked == true)
            {
                ACompletedLoan = "1";
            }
            if (rbtCompletedLoan.Checked == false)
            {
                ACompletedLoan = "0";
            }
            //------------------------------------------
            if (rbtDailyCollection.Checked == true)
            {
                AdailyCollection = "1";
            }
            if (rbtDailyCollection.Checked == false)
            {
                AdailyCollection = "0";
            }
            //------------------------------------------
            if (rbtExpierDetails.Checked == true)
            {
                Aexpier = "1";
            }
            if (rbtExpierDetails.Checked == false)
            {
                Aexpier = "0";
            }
            //------------------------------------------
           
           
            if (rbtPayMethod.Checked == true)
            {
                APaymethode = "1";
            }
            if (rbtPayMethod.Checked == false)
            {
                APaymethode = "0";
            }
            #endregion

            #region UpDate user Setting.................................................................
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String UpdateSetting = @"UPDATE  User_Setting set AllCustomerRPT='" + RAllCuss + "', CompletedLoanRPT='" + ACompletedLoan + "', DailyCollectionRPT='" + AdailyCollection + "', ExpireRPT='" + Aexpier + "', PaymethodRPT='" + APaymethode + "', ApprovePayment='" + ApprovePay + "', BackUpSave='" + Backup + "', LoanIssue='" + loanIssue + "', NewCustomer='" + newCustomer + "', NewLoan='" + NewLoan + "',UserControl='" + userControl + "', UserProfile='" + userProfile + "' where  UserId ='" + cmbUserID.SelectedItem + "' ";
                SqlCommand cmm = new SqlCommand(UpdateSetting, cnn);
                cmm.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            MessageBox.Show("Update Completed", "Update", MessageBoxButtons.OK, MessageBoxIcon.Information);
            #endregion

            LoadUser();
            uncheck();
            desable();
            //----------------------------------------------------------
            if (cmbUserID.SelectedIndex == -1)
            {
                desable();
                simpleButton1.Enabled = false;
            }
            if (cmbUserID.SelectedIndex != -1)
            {
                enable();
                simpleButton1.Enabled = true;
            }
        }

        private void cmbUserID_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbUserID.SelectedIndex == -1)
            {
                desable();
                simpleButton1.Enabled = false;
            }
            if (cmbUserID.SelectedIndex != -1)
            {
                enable();
                simpleButton1.Enabled = true;
            }

            #region load user Name....................................................................
            try
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String UserID = "select UserCode,FirstName,LastName from UserProfile where UserCode='" + cmbUserID.SelectedItem + "'";

                SqlCommand cmm = new SqlCommand(UserID, cnn);
                SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr.Read())
                {
                    label6.Text = dr[1].ToString();

                }
            #endregion

                #region select user Control.......................................................................

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String UserID1 = @"SELECT   AllCustomerRPT, CompletedLoanRPT, DailyCollectionRPT, ExpireRPT, PaymethodRPT, ApprovePayment, BackUpSave, 
                      LoanIssue, NewCustomer, NewLoan, UserControl, UserProfile FROM User_Setting where UserId='" + cmbUserID.SelectedItem + "' ";
                SqlCommand cmm1 = new SqlCommand(UserID1, cnn1);
                SqlDataReader dr1 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);
                if (dr1.Read())
                {
                    #region if condition.....................................................................



                    if (dr1[0].ToString() == "1")
                    {
                        rtbAllCustomer.Checked = true;
                    }
                    if (dr1[0].ToString() == "0")
                    {
                        rtbAllCustomer.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                   
                    if (dr1[1].ToString() == "1")
                    {
                        rbtCompletedLoan.Checked = true;
                    }
                    if (dr1[1].ToString() == "0")
                    {
                        rbtCompletedLoan.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[2].ToString() == "1")
                    {
                        rbtDailyCollection.Checked = true;
                    }
                    if (dr1[2].ToString() == "0")
                    {
                        rbtDailyCollection.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[3].ToString() == "1")
                    {
                        rbtExpierDetails.Checked = true;
                    }
                    if (dr1[3].ToString() == "0")
                    {
                        rbtExpierDetails.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                   
                    if (dr1[4].ToString() == "1")
                    {
                        rbtPayMethod.Checked = true;
                    }
                    if (dr1[4].ToString() == "0")
                    {
                        rbtPayMethod.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[5].ToString() == "1")
                    {
                        chbApprovePayment.Checked = true;
                    }
                    if (dr1[5].ToString() == "0")
                    {
                        chbApprovePayment.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[6].ToString() == "1")
                    {
                        chbBackUp.Checked = true;
                    }
                    if (dr1[6].ToString() == "0")
                    {
                        chbBackUp.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[7].ToString() == "1")
                    {
                        chbLoanIssue.Checked = true;
                    }
                    if (dr1[7].ToString() == "0")
                    {
                        chbLoanIssue.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[8].ToString() == "1")
                    {
                        chbNewCusto.Checked = true;
                    }
                    if (dr1[8].ToString() == "0")
                    {
                        chbNewCusto.Checked = false;
                    }
                    //---------------------------------------------------------------------------------
                    if (dr1[9].ToString() == "1")
                    {
                        chbNewLoan.Checked = true;
                    }
                    if (dr1[9].ToString() == "0")
                    {
                        chbNewLoan.Checked = false;
                    }
                    //---------------------------------------------------------------------------------


                    if (dr1[10].ToString() == "1")
                    {

                        chbUserControl1.Checked = true;

                    }
                    if (dr1[10].ToString() == "0")
                    {
                        chbUserControl1.Checked = false;
                    }
                    //---------------------------------------------------------------------------------

                    if (dr1[11].ToString() == "1")
                    {
                        chbUserProfile.Checked = true;

                    }
                    if (dr1[11].ToString() == "0")
                    {
                        chbUserProfile.Checked = false;
                    }

                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
                #endregion
        }

        private void User_Control_Load(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e, Control ctrl)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {
            uncheck();
            desable();
        }

        private void label15_Click(object sender, EventArgs e)
        {
        }

        private void chbBackUp_CheckedChanged(object sender, EventArgs e)
        {

        }


    }

}
