using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;

namespace Loan_issue.Report
{
    public partial class rptNew_LoanDetails : Form
    {
        String IMS=ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public string NewloanPrint = "";

        public rptNew_LoanDetails()
        {
            InitializeComponent();
        }

        private void New_LoanDetails_Load(object sender, EventArgs e)
        {

            LgUser.Text = User_ID.UserID;
            LgDisplayName.Text = User_ID.userName;

             string totalTables = "";


            //select the print datatables number----------------
            SqlConnection conx = new SqlConnection(IMS);
            conx.Open();

            string ReSelecttableNumbers = @"SELECT RptNumbers FROM RptNumbers";
            SqlCommand cmdx = new SqlCommand(ReSelecttableNumbers, conx);
            SqlDataReader drx = cmdx.ExecuteReader(CommandBehavior.CloseConnection);

            if (drx.Read() == true)
            {
                totalTables = drx[0].ToString();
            }

            if (conx.State == ConnectionState.Open)
            {
                conx.Close();
                drx.Close();
            }
            //....................................................................................................




            SqlConnection cnn=new SqlConnection(IMS);
            cnn.Open();
            String Newloan=@"SELECT  New_Loan_Details.Loan_ID, New_Loan_Details.Book_No, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
                         New_Loan_Details.Balance, New_Loan_Details.TotalLoanAmount, New_Loan_Details.LoanTotal_day, New_Loan_Details.loanRate_Day, 
                         New_Loan_Details.LoanAmount_Day, New_Loan_Details.TimeStamp, New_Loan_Details.IssueBy,  New_Loan_Details.Cust_ID, Customer_Details.FristName, 
                         Customer_Details.LastName, Customer_Details.MobileNo, Customer_Details.Telephone, Customer_Details.Address, Customer_Details.NIC, 
                         Customer_Details.Email, Customer_Details.Country, Customer_Details.Remark
                        FROM            Customer_Details INNER JOIN
                         New_Loan_Details ON Customer_Details.Cus_ID = New_Loan_Details.Cust_ID where New_Loan_Details.Loan_ID='"+NewloanPrint+"' order by New_Loan_Details.Loan_ID desc";

            SqlDataAdapter dscmd = new SqlDataAdapter(Newloan, cnn);
            DataSet1 ds = new DataSet1();
            dscmd.Fill(ds);



             //view the christtal report
            NewLoanDetails rpt = new NewLoanDetails();
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            ViewcrptNewLoanDetails.ReportSource = rpt;
            ViewcrptNewLoanDetails.Refresh();
            cnn.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
