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
using System.Data.SqlClient;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;


namespace Loan_issue.Report
{
    public partial class rptPaymentAdd : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public String PaymentAdd = "";
        public rptPaymentAdd()
        {
            InitializeComponent();

        }

        private void rptPaymentAdd_Load(object sender, EventArgs e)
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

            Report.PaymentAdd rpt1 = new Report.PaymentAdd();

            TextObject Approved, ApprovedDate,ApprovedBySign,dot;



            if (rpt1.ReportDefinition.ReportObjects["CopyDetails"] != null)
            {

                Approved = (TextObject)rpt1.ReportDefinition.ReportObjects["CopyDetails"];
                Approved.Text = "";

            }

            if (rpt1.ReportDefinition.ReportObjects["Text4"] != null)
            {
                ApprovedDate = (TextObject)rpt1.ReportDefinition.ReportObjects["Text4"];
                ApprovedDate.Text = "";
            }
            if (rpt1.ReportDefinition.ReportObjects["Text27"] != null)
            {
                ApprovedBySign = (TextObject)rpt1.ReportDefinition.ReportObjects["Text27"];
                ApprovedBySign.Text = "";
            }

            if (rpt1.ReportDefinition.ReportObjects["Text26"] != null)
            {
                dot = (TextObject)rpt1.ReportDefinition.ReportObjects["Text26"];
                dot.Text = "";
            }
            


            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String PayADD = @"SELECT Payment_Add.Payment_ID, Payment_Add.Loan_ID, Payment_Add.Cus_ID, Payment_Add.Amount, Payment_Add.Paid, Payment_Add.Balance, 
                         Payment_Add.Status, Payment_Add.dayPaid, Payment_Add.Difference, Payment_Add.timeStamp, Payment_Add.[user], Payment_id_Doc.ToatalForPaymentID, 
                         Payment_id_Doc.PaymentConformDate, Payment_id_Doc.Approved_By
                         FROM Payment_Add INNER JOIN
                         Payment_id_Doc ON Payment_Add.Payment_ID = Payment_id_Doc.Payment_id where  Payment_Add.Payment_ID='" + PaymentAdd + "'";

            SqlDataAdapter dscmd = new SqlDataAdapter(PayADD, cnn);
            DataSet1 ds = new DataSet1();
            dscmd.Fill(ds);

            


            //view the christtal report
         //  PaymentAdd rpt = new PaymentAdd();
            rpt1.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            ViewrptPaymentAdd.ReportSource = rpt1;
            ViewrptPaymentAdd.Refresh();
            cnn.Close();


        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
