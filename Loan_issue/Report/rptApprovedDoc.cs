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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;


namespace Loan_issue.Report
{
    public partial class rptApprovedDoc : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        public String Approved = "";

        public String report_heading = "";

        public string Doc_Number = "";

        public rptApprovedDoc()
        {
            InitializeComponent();
        }

        private void rptApprovedDoc_Load(object sender, EventArgs e)
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

            TextObject CopyDetails,userSign,userdot;


            if (rpt1.ReportDefinition.ReportObjects["Text2"] != null)
            {
                CopyDetails = (TextObject)rpt1.ReportDefinition.ReportObjects["Text2"];
                CopyDetails.Text = "Payment Confirom Details";

            }

            if (rpt1.ReportDefinition.ReportObjects["Text5"] != null)
            {
                userdot = (TextObject)rpt1.ReportDefinition.ReportObjects["Text5"];
                userdot.Text = "";

            }

            if (rpt1.ReportDefinition.ReportObjects["Text6"] != null)
            {
                userSign = (TextObject)rpt1.ReportDefinition.ReportObjects["Text6"];
                userSign.Text = "Payment Colle. By :";

            }



            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String PayADD = @"SELECT Payment_Add.Payment_ID, Payment_Add.Loan_ID, Payment_Add.Cus_ID, Payment_Add.Amount, Payment_Add.Paid, Payment_Add.Balance, 
                         Payment_Add.Status, Payment_Add.dayPaid, Payment_Add.Difference, Payment_Add.timeStamp, Payment_Add.[user], Payment_id_Doc.ToatalForPaymentID, 
                         Payment_id_Doc.PaymentConformDate, Payment_id_Doc.Approved_By
                         FROM Payment_Add INNER JOIN
                         Payment_id_Doc ON Payment_Add.Payment_ID = Payment_id_Doc.Payment_id where  Payment_Add.Payment_ID='" + Approved + "'";


            SqlDataAdapter dscmd = new SqlDataAdapter(PayADD, cnn);
            DataSet1 ds = new DataSet1();
            dscmd.Fill(ds);




            //view the christtal report
            
            rpt1.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            ViwerrptApprovedPayment.ReportSource = rpt1;
            ViwerrptApprovedPayment.Refresh();
            cnn.Close();

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
