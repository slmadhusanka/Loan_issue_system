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
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Drawing.Printing;

namespace Loan_issue.Report
{
    public partial class Daily_Collection : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;

        string ReSelectQ = "";
        public Daily_Collection()
        {
           
            InitializeComponent();
            LoadCustomers();
            LoadUser();

        }
        public void LoadCustomers()
        {
            #region Load Customer--------------------------------------------------------------------------
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String LoadCus = "select Cus_ID,FristName,LastName from Customer_Details";
            SqlCommand cmm = new SqlCommand(LoadCus,cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            cmbCustomerWise.Items.Clear();
            while (dr.Read())
            {
                cmbCustomerWise.Items.Add(dr[1].ToString() );

            }
            #endregion
        }
        
        public void LoadUser()
        {
            #region Load User-------------------------------------------------------------------------
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String LoadCus = "select UserCode,FirstName,LastName,DisplayOn from UserProfile";
            SqlCommand cmm = new SqlCommand(LoadCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            cmbuser.Items.Clear();
            while (dr.Read())
            {
                cmbuser.Items.Add(dr[1].ToString());

            }
            #endregion
        }


        public void Concat_SQL_Quary()
        {
            #region Concat SQL...........................................................
            ReSelectQ = "SELECT Payment_Add.Payment_ID, Payment_Add.Loan_ID, Payment_Add.Cus_ID,Payment_Add.Status, Payment_Add.dayPaid,  Payment_Add.timeStamp, Payment_Add.[user] FROM Payment_Add where 1=1";

            //-----------------------------------------------------------------------------------

            if (rbtByStatus.Checked ==true)
            {
                ReSelectQ += " AND Status='" + comboBox1.Text + "' ";
            }

            if(rbtByCustomer.Checked==true)
            {
                ReSelectQ += " AND Cus_ID='" + lblCusID.Text + "' ";
            }

            if(rbtbyUser.Checked==true)
            {
                ReSelectQ += " AND [user]='" + lbluserID.Text + "' ";
            }

            ReSelectQ.Replace("1=1 AND ", "");
            ReSelectQ.Replace(" WHERE 1=1 ", "");

            if(rbtDate.Checked==true)
            {
               

                ReSelectQ += " AND timeStamp BETWEEN '" + dateTimePicker1.Text + "' AND '" + dateTimePicker2.Text + "' ";
            }
           // MessageBox.Show(ReSelectQ);

            #endregion
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.Enabled = true;
        }

        private void Daily_Collection_Load(object sender, EventArgs e)
        {
            LgDisplayName.Text = User_ID.userName;

            rbtAllStatus.Checked = true;
            rbtnoDate.Checked = true;
            rbtAllCustomer.Checked = true;
            rbtAllUser.Checked = true;

            

           
        }

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


            DailyCollection rpt = new DailyCollection();

            TextObject StatusWise, CustomerWise, UserWise, EmOneDate, EmtwoDate, FristDate, secondDate, All, EmptyAll, CMBcusanduser, CMBcusanduserStatus,cmbCusStatus,cmbUserStatus;


            #region if condition-------------------------------------------------------------------------------
            if (rbtByStatus.Checked==true)
            {
                #region text change Status wise---------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                 {
                     StatusWise = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                     StatusWise.Text = "Status Wise Daily Collection";

                 }
                #endregion
            }

            if (rbtByCustomer.Checked == true)
            {
                #region text change Customer wise---------------------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    CustomerWise = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    CustomerWise.Text = "Customer Wise Daily Collection";

                }
                #endregion
            }


            if (rbtbyUser.Checked == true)
            {
                #region text change User wise---------------------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    UserWise = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    UserWise.Text = "User Wise Daily Collection";

                }
                #endregion
            }

            if (rbtnoDate.Checked == true)
            {
                #region Selected all Date ---------------------------------------------------------------------

                //Empty frist date----------------------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text11"] != null)
                {
                    EmOneDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text11"];
                    EmOneDate.Text = "";

                }

                //Empty Second date----------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text20"] != null)
                {
                    EmtwoDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text20"];
                    EmtwoDate.Text = "";

                }

                //Empty dash- text----------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text21"] != null)
                {
                    EmptyAll = (TextObject)rpt.ReportDefinition.ReportObjects["Text21"];
                    EmptyAll.Text = "";

                }
                #endregion
            }


            if(rbtDate.Checked==true)
            {
                #region Selected all Date ---------------------------------------------------------------------

                // frist date----------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text11"] != null)
                {
                    FristDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text11"];
                    FristDate.Text =dateTimePicker1.Text;

                }

                //Second date----------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text20"] != null)
                {
                    secondDate = (TextObject)rpt.ReportDefinition.ReportObjects["Text20"];
                    secondDate.Text = dateTimePicker2.Text;

                }

                //Empty All text----------------------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text10"] != null)
                {
                    EmptyAll = (TextObject)rpt.ReportDefinition.ReportObjects["Text10"];
                    EmptyAll.Text = "";

                }
                #endregion
            }

            if(rbtbyUser.Checked==true && rbtByCustomer.Checked==true )
            {
                #region select by user & Customer--------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    CMBcusanduser = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    CMBcusanduser.Text = "Customer & User Wise Daily Collection";

                }
                #endregion
            }

            if (rbtbyUser.Checked == true && rbtByCustomer.Checked == true && rbtByStatus.Checked == true )
            {
                #region select by user & Customer & Status--------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    CMBcusanduserStatus = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    CMBcusanduserStatus.Text = "Customer,User & Status Wise Daily Collection";

                }
                #endregion
            }
            if (rbtByStatus.Checked == true && rbtByCustomer.Checked == true && rbtbyUser.Checked==false)
            {
                #region select by Status & Customer--------------------------------------------------------
                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    cmbCusStatus = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    cmbCusStatus.Text = "Customer & Status Wise Daily Collection";

                }
                #endregion
            }

            if (rbtbyUser.Checked == true && rbtByStatus.Checked == true && rbtByCustomer.Checked ==false)
            {
                #region select by user & Status--------------------------------------------------------

                if (rpt.ReportDefinition.ReportObjects["Text12"] != null)
                {
                    cmbUserStatus = (TextObject)rpt.ReportDefinition.ReportObjects["Text12"];
                    cmbUserStatus.Text = "Status & User Wise Daily Collection";

                }
                #endregion
            }
            #endregion



            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();

            Concat_SQL_Quary();


            SqlDataAdapter dscmd = new SqlDataAdapter(ReSelectQ, cnn);
            DataSet1 ds = new DataSet1();
            dscmd.Fill(ds);

             //view the christtal report
            
            rpt.SetDataSource(ds.Tables[Convert.ToInt32(totalTables)]);
            crptDailyCollection.ReportSource = rpt;
            crptDailyCollection.Refresh();
            cnn.Close();
            }

        private void cmbCustomerWise_SelectedIndexChanged(object sender, EventArgs e)
        {

            #region select cusID------------------------------------------------------------------------
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String LoadCus = "select Cus_ID,FristName,LastName from Customer_Details where FristName='" + cmbCustomerWise.SelectedItem + "'";
            SqlCommand cmm = new SqlCommand(LoadCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            while (dr.Read())
            {
                lblCusID.Text = dr[0].ToString();

            }
            #endregion
        }

        private void rbtDate_CheckedChanged(object sender, EventArgs e)
        {
            dateTimePicker1.Enabled = true;
            dateTimePicker2.Enabled = true;
        }

        private void rbtAllStatus_CheckedChanged(object sender, EventArgs e)
        {
            comboBox1.SelectedIndex=-1;
            comboBox1.Enabled = false;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            lblCusID.Text = "";
            cmbCustomerWise.Enabled = false;
            cmbCustomerWise.SelectedIndex = -1;
            lblCusID.Visible = false;
            label5.Visible = false;
        }

        private void radioButton2_CheckedChanged_1(object sender, EventArgs e)
        {
            cmbCustomerWise.Enabled = true;
            lblCusID.Visible = true;
            label5.Visible = true;
        }

        private void cmbuser_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region select user ID---------------------------------------------------------------------------
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String LoadCus = "select UserCode,FirstName,LastName,DisplayOn from UserProfile where FirstName='" + cmbuser.SelectedItem + "'";
            SqlCommand cmm = new SqlCommand(LoadCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader(CommandBehavior.CloseConnection);
            
            while (dr.Read())
            {
                lbluserID.Text=dr[0].ToString();

            }
            #endregion
        }

        private void rbtAllUser_CheckedChanged(object sender, EventArgs e)
        {
            lbluserID.Visible = false;
            label7.Visible = false;
            cmbuser.SelectedIndex = -1;
            lbluserID.Text = "__";
            cmbuser.Enabled = false;


        }

        private void rbtbyUser_CheckedChanged(object sender, EventArgs e)
        {
            lbluserID.Visible = true;
            label7.Visible = true;
            cmbuser.Enabled = true;
        }

           
        }


        }
    

