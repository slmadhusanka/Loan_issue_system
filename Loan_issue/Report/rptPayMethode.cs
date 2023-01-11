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
    public partial class rptPayMethode : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        String Filter = "";
        String Filter2 = "";
        public rptPayMethode()
        {
            InitializeComponent();
            rbtAllPayMethod.Checked = true;
            LgUser.Text = User_ID.UserID;
            LgDisplayName.Text = User_ID.userName;
        }

        public void concatSQLquery()
        {
            #region Filter Pay methode wise......................................................

            Filter = "Select Loan_ID,Pay_Method,LoanIssueDate,LoanPeriad,LoanEndDate,TotalLoanAmount,AlreadyPaid,Balance from New_Loan_Details where 1=1 ";

            if (rbtByPayMethod.Checked == true)
            {
                Filter += " AND Pay_Method='" + cmbPayMethod.Text + "' ";
                
            }
          


            Filter.Replace("1=1 AND ", "");
            Filter.Replace(" WHERE 1=1 ", "");
            // MessageBox.Show(Filter);
            #endregion
        }

       


        DataRow r;
        private void button1_Click(object sender, EventArgs e)
        {
            #region pass value to crystal report................................................

            #region check combo box.......................................................
            if (rbtByPayMethod.Checked==true)
            {
                if(cmbPayMethod.SelectedIndex==-1 )
                {
                    MessageBox.Show("Please Select Pay Method....", "Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    cmbPayMethod.Focus();
                    return;
                }
            #endregion

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


           


            DataSet1 ds = new DataSet1();
            DataTable t = ds.Tables.Add("PayMethod");
            t.Columns.Add("LoanID", Type.GetType("System.String"));
            t.Columns.Add("Paymenthod", Type.GetType("System.String"));
            t.Columns.Add("LoanIssuDate", Type.GetType("System.String"));
            t.Columns.Add("LoanPeriad", Type.GetType("System.String"));
            t.Columns.Add("LoanEndDate", Type.GetType("System.String"));
            t.Columns.Add("TotalLoanAmount", Type.GetType("System.String"));
            t.Columns.Add("AlreadyPaid", Type.GetType("System.String"));
            t.Columns.Add("Balance", Type.GetType("System.String"));
            t.Columns.Add("CusID", Type.GetType("System.String"));
            t.Columns.Add("Paid", Type.GetType("System.String"));
            t.Columns.Add("LastBalance", Type.GetType("System.String"));
           

           

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            concatSQLquery();
          //  String PaymentAdd = "Select Loan_ID,Pay_Method,LoanIssueDate,LoanPeriad,LoanEndDate,TotalLoanAmount,AlreadyPaid,Balance from New_Loan_Details";
            SqlCommand cmm = new SqlCommand(Filter, cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            
            

            while (dr.Read())
            {
               


                r = t.NewRow();

              
                
                r["LoanID"] = dr[0].ToString();
                r["Paymenthod"] = dr[1].ToString();
                r["LoanIssuDate"] = (DateTime.Parse(dr[2].ToString())).ToShortDateString();
                r["LoanPeriad"] = dr[3].ToString();
                r["LoanEndDate"] = (DateTime.Parse(dr[4].ToString())).ToShortDateString();
                r["TotalLoanAmount"] = dr[5].ToString();
                r["AlreadyPaid"] = dr[6].ToString();
                r["Balance"] = dr[7].ToString();


              
               // ---------------------------------------------------------------------------------------------------

                SqlConnection cnn1 = new SqlConnection(IMS);
                cnn1.Open(); 
               String PaymentAdd2 = "select top 1 Cus_ID,Paid,Balance from Payment_Add where Loan_ID='" + dr[0].ToString() + "' order by Auto_id desc ";
               SqlCommand cmm1 = new SqlCommand(PaymentAdd2, cnn1);
                SqlDataReader dr1 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);
               
                while (dr1.Read())
                {
                     
                    r["CusID"] = dr1[0].ToString();
                    r["Paid"] = dr1[1].ToString();
                    r["LastBalance"] = dr1[2].ToString();

                }
               
              
                    t.Rows.Add(r);
               
                 }
            cnn.Close();

            //PayMethod rpt = new PayMethod();
            //SqlConnection cnn2 = new SqlConnection(IMS);
            //cnn2.Open();
            //concatSQLquery();
            //SqlDataAdapter dscmd = new SqlDataAdapter(Filter, cnn2);
            //DataSet1 ds2 = new DataSet1();
            //dscmd.Fill(ds2);
            //PayMethodPayAdd


            PayMethod rpt = new PayMethod();

            TextObject allPayMe,Day,Week,Month,all;

            if (rbtAllPayMethod.Checked == true)
            {
                #region text change Customer wise---------------------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    allPayMe = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    allPayMe.Text = "ALL";

                }

                if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                {
                    all = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                    all.Text = "  'All' Pay Method Wise......";

                }
                #endregion
               
            }
            if (rbtByPayMethod.Checked == true)
            {
                #region text change Pay Methode wise---------------------------------------------------------------------

                if(cmbPayMethod.Text=="Day")

                {
                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                    {
                        Day = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                        Day.Text = "Day";

                     }
                if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                {
                    Day = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                    Day.Text = "  'Day' Pay Method Wise......";

                }
             }

                if (cmbPayMethod.Text == "Week")
                {
                    if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                    {
                        Week = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                        Week.Text = "Week";

                    }

                    if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                    {
                        Day = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                        Day.Text = "  'Week' Pay Method Wise......";

                    }
                }

                if (cmbPayMethod.Text == "Months")
                {
                    if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                    {
                        Month = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                        Month.Text = "Months";

                    }

                    if (rpt.ReportDefinition.ReportObjects["Text19"] != null)
                    {
                        Day = (TextObject)rpt.ReportDefinition.ReportObjects["Text19"];
                        Day.Text = "  'Months' Pay Method Wise......";

                    }
                }
                #endregion

            }


         //   view the christtal report
            
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            crystViwerPaymethode.ReportSource = rpt;
            crystViwerPaymethode.Refresh();
            cnn.Close();

            #endregion
        }

        private void rbtAllPayMethod_CheckedChanged(object sender, EventArgs e)
        {
            cmbPayMethod.SelectedIndex = -1;



        }

        private void rbtByPayMethod_CheckedChanged(object sender, EventArgs e)
        {
            if (rbtByPayMethod.Checked == true)
            {
                cmbPayMethod.Enabled = true;
            }

            if (rbtByPayMethod.Checked == false)
            {
                cmbPayMethod.Enabled = false;
            }
        }
    }
}
