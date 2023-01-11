using CrystalDecisions.CrystalReports.Engine;
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

namespace Loan_issue.Report
{
    public partial class rptCompletedLoan : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public rptCompletedLoan()
        {
            InitializeComponent();
            LgUser.Text = User_ID.UserID;
            LgDisplayName.Text = User_ID.userName;
            
        }
        String loanId;
        private void rptCompletedLoan_Load(object sender, EventArgs e)
        {
           
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (radioButton1.Checked == false && radioButton2.Checked == false)
            {
                MessageBox.Show("Please selected Loan Type...", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

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

            CompletedLoad rpt = new CompletedLoad();

            #region if condition-------------------------------------------------------------------------------
            TextObject Completed,current;
            if (radioButton1.Checked == true)
            {
                #region text change Completed---------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                {
                    Completed = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                    Completed.Text = "Completed Loan";

                }
                #endregion

            }

            if (radioButton2.Checked == true)
            {
                #region text change Current---------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                {
                    current = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                    current.Text = "Current Loan";

                }
                #endregion
            }
            #endregion



            if (radioButton1.Checked == true)
            {
                #region completed loan----------------------------------------------------------

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                String CompletedLoan = @"SELECT  New_Loan_Details.Loan_ID, New_Loan_Details.Book_No, New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, 
                         New_Loan_Details.LoanPeriad, New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate,New_Loan_Details.TotalLoanAmount, New_Loan_Details.AlreadyPaid, New_Loan_Details.Balance, New_Loan_Details.IssueBy
                         FROM            New_Loan_Details INNER JOIN Payment_Add ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID where Payment_Add.Balance='0' order by New_Loan_Details.Loan_ID asc ";
                SqlDataAdapter dscmd = new SqlDataAdapter(CompletedLoan, cnn);
                DataSet1 ds = new DataSet1();
                dscmd.Fill(ds);

                //view the christtal report
                
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                ViwerCompletedLoan.ReportSource = rpt;
                ViwerCompletedLoan.Refresh();
                cnn.Close();
            }

                #endregion


            if (radioButton2.Checked == true)
            {
                #region Current ------------------------------------------------------------

                DataSet1 ds = new DataSet1();

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open();
                String LoadLoanid = "select Loan_ID from New_Loan_Details";
                SqlCommand cmm1 = new SqlCommand(LoadLoanid, cnn1);
                SqlDataReader dr1 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);
                while (dr1.Read() == true)
                {
                    loanId = dr1[0].ToString();
                   // MessageBox.Show(loanId);
                    #region select top balance Loan ID wise-------------------------------------------------------------------------------

                    SqlConnection cnn2 = new SqlConnection(IMS);
                    cnn2.Open();
                    String PayID2 = @"select top 1 balance,Auto_id from Payment_Add where Loan_ID='" + loanId + "' order by Auto_id desc";
                    SqlCommand cmm2 = new SqlCommand(PayID2, cnn2);
                    SqlDataReader dr5 = cmm2.ExecuteReader(CommandBehavior.CloseConnection);

                    #endregion

                    while (dr5.Read())
                    {
                        String Balance = dr5[0].ToString();
                        String Autoidd = dr5[1].ToString();

                       // MessageBox.Show(Balance);
                        if (((Double.Parse(Balance)) != 0))
                        {
                            SqlConnection cnn3 = new SqlConnection(IMS);
                            cnn3.Open();
                            String CompletedLoan2 = @"SELECT   Payment_Add.Auto_id,New_Loan_Details.Loan_ID, New_Loan_Details.Book_No, New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, 
                         New_Loan_Details.LoanPeriad, New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate,New_Loan_Details.TotalLoanAmount, New_Loan_Details.AlreadyPaid, New_Loan_Details.Balance, New_Loan_Details.IssueBy
                         FROM            New_Loan_Details INNER JOIN Payment_Add ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID where Payment_Add.Auto_id='" + Autoidd + "' ";
                            SqlCommand cmm3 = new SqlCommand(CompletedLoan2, cnn3);
                            SqlDataReader dr3 = cmm3.ExecuteReader(CommandBehavior.CloseConnection);
                            while (dr3.Read()) 
                            {
                               String Autoid = dr3[0].ToString();
                                 
                                SqlConnection cnn = new SqlConnection(IMS);
                                cnn.Open();
                                String CompletedLoan = @"SELECT   New_Loan_Details.Loan_ID, New_Loan_Details.Book_No, New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, 
                         New_Loan_Details.LoanPeriad, New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate,New_Loan_Details.TotalLoanAmount, New_Loan_Details.AlreadyPaid, New_Loan_Details.Balance, New_Loan_Details.IssueBy
                         FROM            New_Loan_Details INNER JOIN Payment_Add ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID where Payment_Add.Auto_id='" + Autoid + "' ";
                                SqlDataAdapter dscmd = new SqlDataAdapter(CompletedLoan, cnn);

                                dscmd.Fill(ds);
                               // MessageBox.Show(CompletedLoan);
                            }

                        }
                    }

                    
                }
                //view the christtal report
                //CompletedLoad rpt = new CompletedLoad();
                rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
                ViwerCompletedLoan.ReportSource = rpt;
                ViwerCompletedLoan.Refresh();
                //cnn.Close();
                #endregion
            }

            
        }
    }
}
