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
using System.Configuration;

namespace Loan_issue
{
    public partial class ApprovePaymentDoc : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public ApprovePaymentDoc()
        {
           

            InitializeComponent();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Load loan to grideview.................................


           PnlLoanSerch.Visible = true;


//            dataGridView1.Rows.Clear();
//            SqlConnection cnn = new SqlConnection(IMS);
//            cnn.Open();
//            string SELPYT = @"SELECT   distinct    New_Loan_Details.Loan_ID,   New_Loan_Details.Book_No,Payment_Add.Payment_ID,Payment_Add.[user],New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
//                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
//                         New_Loan_Details.Balance, New_Loan_Details.IssueBy, New_Loan_Details.LoanAmount_Day, New_Loan_Details.loanRate_Day, 
//                         New_Loan_Details.LoanTotal_day, New_Loan_Details.TotalLoanAmount,Customer_Details.FristName, Customer_Details.LastName,Customer_Details.NIC, Customer_Details.Telephone,Payment_Add.Balance.Auto_id
//                        FROM New_Loan_Details inner JOIN Payment_Add  ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID inner join Customer_Details on Customer_Details.Cus_ID= Payment_Add.Cus_ID where Payment_Add.Balance!='0' and  Payment_Add.Status!='Approved'  ";

//            SqlCommand cmm = new SqlCommand(SELPYT, cnn);
//            SqlDataReader dr = cmm.ExecuteReader();
//            while (dr.Read() == true)
//            {
//                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21]);
//            }


           SqlConnection cnn = new SqlConnection(IMS);
           cnn.Open();
           string SELpyt = @"SELECT  distinct New_Loan_Details.Loan_ID,   New_Loan_Details.Book_No,Payment_Add.Payment_ID,Payment_Add.[user],New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
                         New_Loan_Details.Balance, New_Loan_Details.IssueBy, New_Loan_Details.LoanAmount_Day, New_Loan_Details.loanRate_Day, 
                         New_Loan_Details.LoanTotal_day, New_Loan_Details.TotalLoanAmount,Customer_Details.FristName, Customer_Details.LastName,Customer_Details.NIC, Customer_Details.Telephone,Payment_Add.Auto_id,Payment_Add.Amount,Payment_Add.Paid,Payment_Add.Balance,Payment_Add.dayPaid
                        FROM New_Loan_Details inner JOIN Payment_Add  ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID inner join Customer_Details on Customer_Details.Cus_ID= Payment_Add.Cus_ID where Payment_Add.Status!='Approved' and Payment_Add.Balance!='0' and Payment_Add.[Status]!='Openning' ";

           dataGridView1.Rows.Clear();
           SqlCommand cmm = new SqlCommand(SELpyt, cnn);
           SqlDataReader dr = cmm.ExecuteReader();
           while (dr.Read() == true)
           {
               dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21], dr[22], dr[23], dr[24], dr[25], dr[26]);
           }


            #endregion
        }

        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            #region Search Ppayment id to grideview.................................

           // PnlLoanSerch.Visible = true;


            
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            string SELpyt = @"SELECT  distinct New_Loan_Details.Loan_ID,   New_Loan_Details.Book_No,Payment_Add.Payment_ID,Payment_Add.[user],New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
                         New_Loan_Details.Balance, New_Loan_Details.IssueBy, New_Loan_Details.LoanAmount_Day, New_Loan_Details.loanRate_Day, 
                         New_Loan_Details.LoanTotal_day, New_Loan_Details.TotalLoanAmount,Customer_Details.FristName, Customer_Details.LastName,Customer_Details.NIC, Customer_Details.Telephone,Payment_Add.Auto_id,Payment_Add.Amount,Payment_Add.Paid,Payment_Add.Balance,Payment_Add.dayPaid
                        FROM New_Loan_Details inner JOIN Payment_Add  ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID inner join Customer_Details on Customer_Details.Cus_ID= Payment_Add.Cus_ID where Payment_Add.Status!='Approved' AND Payment_Add.Balance!='0' AND (New_Loan_Details.Loan_ID like '%" + txtSearch.Text + "%' or  New_Loan_Details.Book_No like '%" + txtSearch.Text + "%' or New_Loan_Details.Cust_ID like '%" + txtSearch.Text + "%'or Customer_Details.FristName like '%" + txtSearch.Text + "%' or Customer_Details.LastName like '%" + txtSearch.Text + "%' or Customer_Details.NIC like '%" + txtSearch.Text + "%' or Customer_Details.Telephone like '%" + txtSearch.Text + "%' or Payment_Add.Payment_ID like '%" + txtSearch.Text + "%' ) ";

            dataGridView1.Rows.Clear();
            SqlCommand cmm = new SqlCommand(SELpyt, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read() == true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19], dr[20], dr[21],dr[22]);
            }
            #endregion
        }

        private void ApprovePaymentDoc_Load(object sender, EventArgs e)
        {
            //user name--------------------------------------------------------
            LgDisplayName.Text = User_ID.userName;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlLoanSerch.Visible = false;
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {

            listView1.Items.Clear();
            checkEdit1.Checked = false;

            DataGridViewRow dr;
            dr = dataGridView1.SelectedRows[0];

            lblPaymentID.Text = dr.Cells[2].Value.ToString();
            lbluserid.Text = dr.Cells[3].Value.ToString();

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String PaymentSelect = @"SELECT [Payment_Add].[Payment_ID],[Payment_Add].[Loan_ID],[Payment_Add].[Cus_ID],New_Loan_Details.[Book_No],[Payment_Add].[dayPaid],[Payment_Add].[timeStamp],[Payment_Add].[user],[Payment_Add]. [Auto_id],Payment_id_Doc.ToatalForPaymentID
  FROM [Payment_Add]inner join New_Loan_Details on [Payment_Add].Loan_ID=New_Loan_Details.Loan_ID left join Payment_id_Doc on [Payment_Add].Payment_ID=Payment_id_Doc.Payment_id
 where Payment_Add. Payment_ID='" + lblPaymentID.Text + "'";
            SqlCommand cmm = new SqlCommand(PaymentSelect,cnn);
            SqlDataReader dr2 = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            while(dr2.Read())
            {

                ListViewItem li = new ListViewItem(dr2[1].ToString());
                li.SubItems.Add(dr2[2].ToString());
                li.SubItems.Add(dr2[3].ToString());
                
                li.SubItems.Add(dr2[5].ToString());
                li.SubItems.Add(dr2[4].ToString());
                li.SubItems.Add(dr2[7].ToString());


                txtThisAmount.Text = dr2[8].ToString();

                listView1.Items.Add(li);

                
            }


           



            PnlLoanSerch.Visible = false;


           
        }

        private void txtThisAmount_TextChanged(object sender, EventArgs e)
        {

        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("Insert details in to this Form ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }


            if(checkEdit1.Checked==false)
            {
                MessageBox.Show("Please Select 'Approved This Payment Document'.. ", "Message", MessageBoxButtons.OKCancel, MessageBoxIcon.Information);
                return;
            }

            for (int a = 0 ; a < listView1.Items.Count ; a++)
            {
                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String UpdatePayment = "update Payment_Add set Status ='" + "Approved" + "' where Auto_id='" + listView1.Items[a].SubItems[5].Text + "' ";
                SqlCommand cmm = new SqlCommand(UpdatePayment, cnn);
                cmm.ExecuteNonQuery();
            }




            SqlConnection cnn1 = new SqlConnection(IMS);
            cnn1.Open();
            String UpdatePaymentDoc = "update Payment_id_Doc set Status='" + "Approved" + "',PaymentConformDate='" + DateTime.Now.ToString() + "',Approved_By='" + LgUser.Text + "' where Payment_id='" + lblPaymentID.Text + "' ";
            SqlCommand cmm1 = new SqlCommand(UpdatePaymentDoc, cnn1);
            cmm1.ExecuteNonQuery();


            MessageBox.Show("Update Successfull....... ", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);

            //Cristal report---------------------------------------------------------------

            Report.rptApprovedDoc Approvedoc = new Report.rptApprovedDoc();
            Approvedoc.Approved = lblPaymentID.Text;
            Approvedoc.Visible = true;


            listView1.Items.Clear();
            lblPaymentID.Text = "--";
            lbluserid.Text = "--";
            checkEdit1.Checked = false;
            txtThisAmount.Text = "0.00";
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            listView1.Items.Clear();
            lblPaymentID.Text = "--";
            lbluserid.Text = "--";
            checkEdit1.Checked = false;
            txtThisAmount.Text = "0.00";
        }
    }
}
