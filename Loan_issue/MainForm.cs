using DevExpress.XtraEditors;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
//using Loan_issue.Report

namespace Loan_issue
{
    public partial class MainForm : Form
    {

        UserSetting UserCont = new UserSetting();
        public MainForm()
        {
            InitializeComponent();
            timer1.Start();

            //TxtDateTime.Text = DateTime.Now.ToString();
        }

        public  void userSetting()
        {
             SqlDataReader dr = UserCont.User_Setting();
             if (dr.Read())
             {
                 //Allcustomer R
                 if (dr[0].ToString() == "0")
                 {
                     tileItem8.Enabled = false;
                 }
                 if (dr[0].ToString() == "1")
                 {
                     tileItem8.Enabled = true;
                 }

                 //Completed Loan R
                 if (dr[1].ToString() == "0")
                 {
                     tileItem11.Enabled = false;
                 }
                 if (dr[1].ToString() == "1")
                 {
                     tileItem11.Enabled = true;
                 }

                 //Daily Collection R
                 if (dr[2].ToString() == "0")
                 {
                     tileItem12.Enabled = false;
                 }
                 if (dr[2].ToString() == "1")
                 {
                     tileItem12.Enabled = true;
                 }

                 //Expired R
                 if (dr[3].ToString() == "0")
                 {
                     tileItem10.Enabled = false;
                 }
                 if (dr[3].ToString() == "1")
                 {
                     tileItem10.Enabled = true;
                 }

                 //Pay Method R
                 if (dr[4].ToString() == "0")
                 {
                     tileItem13.Enabled = false;
                 }
                 if (dr[4].ToString() == "1")
                 {
                     tileItem13.Enabled = true;
                 }

                 //Approv Payment

                 if (dr[5].ToString() == "0")
                 {
                     tileItem4.Enabled = false;
                 }
                 if (dr[5].ToString() == "1")
                 {
                     tileItem4.Enabled = true;
                 }


                 //Back Up
                 if (dr[6].ToString() == "0")
                 {
                     tileItem9.Enabled = false;
                 }
                 if (dr[6].ToString() == "1")
                 {
                     tileItem9.Enabled = true;
                 }

                 //Loan Issue
                 if (dr[7].ToString() == "0")
                 {
                     tileItem2.Enabled = false;
                 }
                 if (dr[7].ToString() == "1")
                 {
                     tileItem2.Enabled = true;
                 }


                 //New Customer
                 if (dr[8].ToString() == "0")
                 {
                     tileItem3.Enabled = false;
                 }
                 if (dr[8].ToString() == "1")
                 {
                     tileItem3.Enabled = true;
                 }


                 //New Loan
                 if (dr[9].ToString() == "0")
                 {
                     tileItem1.Enabled = false;
                 }
                 if (dr[9].ToString() == "1")
                 {
                     tileItem1.Enabled = true;
                 }

                 //User Control
                 if (dr[10].ToString() == "0")
                 {
                     tileItem7.Enabled = false;
                 }
                 if (dr[10].ToString() == "1")
                 {
                     tileItem7.Enabled = true;
                 }

                 //user Profile
                 if (dr[11].ToString() == "0")
                 {
                     tileItem6.Enabled = false;
                 }
                 if (dr[11].ToString() == "1")
                 {
                     tileItem6.Enabled = true;
                 }
             }
        }

        private void pictureBox5_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            Form1 fm1 = new Form1();
            fm1.Show();

            this.Close();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            DateTime DT = DateTime.Now;
            //DateTime Date=DateTime.

            this.TxtDateTime.Text = DT.ToShortTimeString();
            this.Txt_date.Text = DT.ToLongDateString();
        }

        private void tileItem2_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //loan payment adding..................

            Loan_Issue cuscrP = new Loan_Issue();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            userSetting();
        }

        private void tileItem3_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //New Customer------------

            New_Customer cuscrP = new New_Customer();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem6_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //New User------------

            UserProfile cuscrP = new UserProfile();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem1_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //New User------------

            New_Loan_Type cuscrP = new New_Loan_Type();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem4_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {

            //New Approve Payment------------

            ApprovePaymentDoc cuscrP = new ApprovePaymentDoc();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem8_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //New Approve Payment------------

            Report.rptAllCustomer cuscrP = new Report.rptAllCustomer();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem12_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            //New Approve Payment------------

            Report.Daily_Collection cuscrP = new Report.Daily_Collection();

            cuscrP.LgDisplayName.Text = LgDisplayName.Text;
            cuscrP.LgUser.Text = LgUser.Text;

            cuscrP.Show();
        }

        private void tileItem9_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Back_UP_Form bkf = new Back_UP_Form();
            bkf.LgDisplayName.Text = LgDisplayName.Text;
            bkf.LgUser.Text = LgUser.Text;

            bkf.Show();

        }

        private void tileItem11_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report.rptCompletedLoan compltedLoan = new Report.rptCompletedLoan();

            compltedLoan.LgDisplayName.Text = LgDisplayName.Text;
            compltedLoan.LgUser.Text = LgUser.Text;

            compltedLoan.Show();
        }

        private void tileItem10_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report.rptExpire expirLoan = new Report.rptExpire();

            expirLoan.LgDisplayName.Text = LgDisplayName.Text;
            expirLoan.LgUser.Text = LgUser.Text;

            expirLoan.Show();
        }

        private void tileItem13_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            Report.rptPayMethode paymethode = new Report.rptPayMethode();

            paymethode.LgDisplayName.Text = LgDisplayName.Text;
            paymethode.LgUser.Text = LgUser.Text;

            paymethode.Show();

        }

        private void tileItem7_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
            User_Control control = new User_Control();

            control.LgDisplayName.Text = LgDisplayName.Text;
            control.LgUser.Text = LgUser.Text;

            control.Show();
        }

        private void tileItem14_ItemClick(object sender, DevExpress.XtraEditors.TileItemEventArgs e)
        {
           
        }
    }
}
