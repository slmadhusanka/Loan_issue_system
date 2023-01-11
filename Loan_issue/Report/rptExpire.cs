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
using System.Configuration;
using CrystalDecisions.CrystalReports.Engine;

namespace Loan_issue.Report
{
    public partial class rptExpire : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        String Filter="";
        public rptExpire()
        {
            InitializeComponent();
            LgUser.Text = User_ID.UserID;
            LgDisplayName.Text = User_ID.userName;
        }

        public void concatSQLquery()
        {
            #region Filter Pay methode wise......................................................

            Filter = "select Loan_ID,Pay_Method,LoanIssueDate,LoanPeriad,LoanEndDate,TotalLoanAmount,Balance,AlreadyPaid from New_Loan_Details where 1=1 ";

            if (radioButton1.Checked == true)
            {
                Filter += " AND LoanEndDate <='" + dateTimePicker1.Text + "'  ";
                
            }
           


            Filter.Replace("1=1 AND ", "");
            Filter.Replace(" WHERE 1=1 ", "");
           // MessageBox.Show(Filter);
            #endregion
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {

        }
        DataRow r;
        
        private void simpleButton1_Click(object sender, EventArgs e)
        {

            

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
            SqlCommand cmm=new SqlCommand(Filter,cnn);
            SqlDataReader dr=cmm.ExecuteReader(CommandBehavior.CloseConnection);

            while(dr.Read())
            {
               
                  

               

                   SqlConnection cnn1 = new SqlConnection(IMS);
                   cnn1.Open();
                   String slectLoan1 = "select top 1   Cus_ID,Paid,balance from Payment_Add where Loan_ID='" + dr[0].ToString() + "'  order by Auto_id desc ";
                   SqlCommand cmm1 = new SqlCommand(slectLoan1, cnn1);
                   SqlDataReader dr1 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);

                   while (dr1.Read())
                   {

                       if (Double.Parse(dr1[2].ToString()) != 0)
                       {
                           r = t.NewRow();

                       r["CusID"] = dr1[0].ToString();
                       r["Paid"] = dr1[1].ToString();
                       r["LastBalance"] = dr1[2].ToString();


                       r["LoanID"] = dr[0].ToString();
                       r["Paymenthod"] = dr[1].ToString();
                       r["LoanIssuDate"] = (DateTime.Parse(dr[2].ToString())).ToShortDateString();
                       r["LoanPeriad"] = dr[3].ToString();
                       r["LoanEndDate"] = (DateTime.Parse(dr[4].ToString())).ToShortDateString();
                       r["TotalLoanAmount"] = dr[5].ToString();
                       r["AlreadyPaid"] = dr[7].ToString();
                       r["Balance"] = dr[6].ToString();

                       t.Rows.Add(r);
                       }
                      
                      
                      
                   }


               
               

                
            }
             expir rpt = new expir();
             TextObject date;

            #region text change Status wise---------------------------------------------------------------------
            if (rpt.ReportDefinition.ReportObjects["Text20"] != null)
            {
                date = (TextObject)rpt.ReportDefinition.ReportObjects["Text20"];
                date.Text = dateTimePicker1.Text;

            }
            #endregion



            cnn.Close();
            //   view the christtal report
           
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            viewreportExpire.ReportSource = rpt;
            viewreportExpire.Refresh();
            cnn.Close();
        }

        private void rptExpire_Load(object sender, EventArgs e)
        {

        }
    }
}
