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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace Loan_issue.Report
{
    public partial class rptAllCustomer : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public rptAllCustomer()
        {
            InitializeComponent();
        }

        private void rptAllCustomer_Load(object sender, EventArgs e)
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






            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String AllCus = @"SELECT [Cus_ID],[FristName],[LastName],[MobileNo],[Fax],[Telephone],[Address],[NIC],[Email],[Country],[Remark] FROM [Customer_Details]";

            SqlDataAdapter dscmd = new SqlDataAdapter(AllCus, cnn);
            DataSet1 ds = new DataSet1();
            dscmd.Fill(ds);




            //view the christtal report
            AllCustomer rpt = new AllCustomer();
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            crptAllcustomer.ReportSource = rpt;
            crptAllcustomer.Refresh();
            cnn.Close();
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

       
    }
}
