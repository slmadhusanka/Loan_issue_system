using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Loan_issue
{
    public partial class New_Loan_Type : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        
        public New_Loan_Type()
        {
            


            InitializeComponent();
            getCreateLoanCode();
            //pnlPaymentMethod.Visible = false;
            pnlLoanPeriodeCode.Visible = false;
            pnlInterestRateID.Visible = false;
            loadPaymentMrthod();
            loadPeriod();
            LoadInterestRate();
            LoanDetails_disable();
            AutoGeneratePaymentID();
            
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Load Customer.................................

            PnlCustomerSerch.Visible = true;
            

            dataGridView1.Rows.Clear();
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            string SELCus = "SELECT [Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Image],[Remark] FROM [Customer_Details]";
            SqlCommand cmm = new SqlCommand(SELCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read() == true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11]);
            }
            #endregion
        }

        public void getCreateLoanCode()
        {
            #region CREATE Loan ID............

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Loan_ID from New_Loan_Details";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblLoanID.Text = "LON1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(CusID) from Customer_Details";
                string sql1 = "select TOP 1 Loan_ID from New_Loan_Details ORDER BY Loan_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    lblLoanID.Text = "LON" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }

        public void AutoGeneratePaymentID()
        {
            #region AutoGenerate Payment ID........................................................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Payment_ID from Payment_Add";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblpaymenrid.Text = "PYT1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(CusID) from Customer_Details";
                string sql1 = "select TOP 1 Payment_ID from Payment_Add ORDER BY Payment_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    lblpaymenrid.Text = "PYT" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }

       


        public void getCreateInterestRateCode()
        {
            #region CREATE Interest Rate ID............

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Interest_ID from Interest_Rate";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblInterestRateID.Text = "INR1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(CusID) from Customer_Details";
                string sql1 = "select TOP 1 Interest_ID from Interest_Rate ORDER BY Interest_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    lblInterestRateID.Text = "INR" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }


        public void getCreateLoanPeriodeCode()
        {
            #region CREATE Loan Periode Code............

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select LoanPeriode_ID from Loan_Periode";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                lblLoanPeriodId.Text = "LOP1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(CusID) from Customer_Details";
                string sql1 = "select TOP 1 LoanPeriode_ID from Loan_Periode ORDER BY LoanPeriode_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    lblLoanPeriodId.Text = "LOP" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }

        Double DayTotal;
        public void PerDayTot()
        {
            #region calculate per total,rate.amunt.....................................................

            if (txtLoanAmount.Text == "")
            {
                return;
            }
            if (comboBox4.Text == "" || comboBox2.Text == "")
            {
                return;
            }

            Double DayAmount = Double.Parse(txtLoanAmount.Text) / (Double.Parse(comboBox2.Text));
            decimal y = Convert.ToDecimal(DayAmount);
            label26.Text = Convert.ToString(Math.Round(y,2));

            Double MounthRate = (Double.Parse(txtLoanAmount.Text)) / 100 * (Double.Parse(comboBox4.Text)) / 30;
            decimal z = Convert.ToDecimal(MounthRate);
            label46.Text = Convert.ToString(Math.Round(z,2));

            DayTotal = (Double.Parse(label26.Text)) + (Double.Parse(label46.Text));
            Decimal x = Convert.ToDecimal(DayTotal);
            label47.Text = Convert.ToString(Math.Round(x,2));

           ToatalLoanAmount.Text = (DayTotal * (Double.Parse(comboBox2.Text))).ToString();


            
        }
            #endregion
        private void ClearTextBoxes(Control.ControlCollection cc)
        {
            #region Clear All textbox...........................
            foreach (Control ctrl in cc)
            {
                TextBox tb = ctrl as TextBox;
                if (tb != null)
                    tb.Text = "";
                else
                    ClearTextBoxes(ctrl.Controls);

                

                label26.Text = "0.00";
                label46.Text = "0.00"; 
                label47.Text = "0.00";

                dateTimePicker1.ResetText();
                dateTimePicker2.ResetText();






            }
            #endregion
        }
        public void LoanDetails_disable()
        {
            txtbookNum.Enabled = false;
            comboBox1.Enabled = false;
            comboBox2.Enabled = false;
            dateTimePicker1.Enabled = false;
            txtLoanAmount.Enabled = false;
            comboBox4.Enabled = false;
            txtalredyPaid.Enabled = false;
        }
        public void LoanDetails_Ensble()
        {
            txtbookNum.Enabled = true;
            comboBox1.Enabled = true;
            comboBox2.Enabled = true;
            dateTimePicker1.Enabled = true;
            txtLoanAmount.Enabled = true;
           // comboBox4.Enabled = true;
            //txtalredyPaid.Enabled = true;
        }

        public void loadPaymentMrthod()
        {
            #region load Payment Method.............................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Name from Payment_Method";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();
            comboBox1.Items.Clear();
            //=====================================================================================================================
            
            while (dr.Read())
            {
                comboBox1.Items.Add(dr[0].ToString());
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }
            #endregion
        }

        public void loadPeriod()
        {
            #region Load period............................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Name from Loan_Periode order by Name asc";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            comboBox2.Items.Clear();
            while (dr.Read())
            {
                comboBox2.Items.Add(dr[0].ToString());
            }
            #endregion
        }

        public void LoadInterestRate()
        {
            #region Load Interest Rate................................
            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Value from Interest_Rate";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            comboBox4.Items.Clear();
            while (dr.Read())
            {
                
                comboBox4.Items.Add(dr[0].ToString());
                
            }
            #endregion
        }
        int x;
        public void CalculateratewithpaymentMethod()
        {
        #region Calculate rate with payment Method...................................................................


            if (comboBox2.Text == "" || comboBox4.Text == "")
            {
                return;
            }
           

            if (comboBox1.Text == "Day")
            {
                Double MounthRate = (Double.Parse(txtLoanAmount.Text)) / 100 * (Double.Parse(comboBox4.Text)) / (Double.Parse(comboBox2.Text)) * 1;
                label46.Text = MounthRate.ToString();
            }
            if (comboBox1.Text == "Week")
            {
                Double MounthRate = (Double.Parse(txtLoanAmount.Text)) / 100 * (Double.Parse(comboBox4.Text)) / (Double.Parse(comboBox2.Text)) * 7;
                label46.Text = MounthRate.ToString();
            }
            if (comboBox1.Text == "Months")
            {
                Double MounthRate = (Double.Parse(txtLoanAmount.Text)) / 100 * (Double.Parse(comboBox4.Text)) / (Double.Parse(comboBox2.Text)) * 30;
                label46.Text = MounthRate.ToString();
            }
            #endregion
    }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            #region select customer......................................................
            try
            {


                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();

                string SELCus = "SELECT [Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Image],[Remark] FROM [Customer_Details] where Cus_ID like'%" + textBox1.Text + "%' or FristName like'%" + textBox1.Text + "%' or LastName like'%" + textBox1.Text + "%' or MobileNo like'%" + textBox1.Text + "%' or  NIC like'%" + textBox1.Text + "%' ";
                SqlCommand cmm = new SqlCommand(SELCus, cnn);
                SqlDataReader dr = cmm.ExecuteReader();

                dataGridView1.Rows.Clear();
                while (dr.Read() == true)
                {
                    dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11]);
                }
                if (cnn.State == ConnectionState.Open)
                {
                    cnn.Close();
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            #endregion
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            listView1.Items.Clear();
            listView3.Items.Clear();

            #region data pass to textbox................................................

            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            CusID.Text = dr.Cells[0].Value.ToString();
            textBox4.Text = dr.Cells[1].Value.ToString();
            textBox5.Text = dr.Cells[2].Value.ToString();
            CusTelNUmber.Text = dr.Cells[3].Value.ToString();
            CusFaxNumber.Text = dr.Cells[4].Value.ToString();
            CusPersonalAddress.Text = dr.Cells[6].Value.ToString();
            textBox3.Text = dr.Cells[7].Value.ToString();
           

            #region Select the Image-----------------------------------------------------------------------

            //===================================================================================================
            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();


            string ItemImage = "SELECT Image FROM Customer_Details WHERE Cus_ID='" + CusID.Text + "'";
            SqlCommand cmd1 = new SqlCommand(ItemImage, con1);
            SqlDataReader dr1 = cmd1.ExecuteReader();

            if (dr1.Read())
            {


                //select the Image____________________________________

                // If a pic is available in the Database
                if (dr1[0] != DBNull.Value)
                {

                    byte[] img = (byte[])(dr1[0]);

                    MemoryStream ms = new MemoryStream(img);
                    ItmImage.Image = Image.FromStream(ms);

                }
                // If ther is no picture in the database......................
                else
                {
                    ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
                }
                //   ______________________________________________________


            }

            if (con1.State == ConnectionState.Open)
            {
                con1.Close();
            }
            #endregion


            PnlCustomerSerch.Visible = false;
            LoanDetails_Ensble();
            button2.Enabled = true;
            button1.Enabled = true;
            #endregion

            #region insert data to listview........................................

            #region Loan id Customer wise---------------------------------------------------------------------------------

            SqlConnection cnn1 = new SqlConnection(IMS);
            cnn1.Open();
            String PayID1 = @"SELECT  [Loan_ID],Cust_ID FROM New_Loan_Details where Cust_ID='"+CusID.Text+"'";
            SqlCommand cmm1 = new SqlCommand(PayID1, cnn1);
            SqlDataReader dr4 = cmm1.ExecuteReader(CommandBehavior.CloseConnection);
            #endregion
            listView3.Items.Clear();
            listView1.Items.Clear();

            while(dr4.Read())
            {
                String LoanID = dr4[0].ToString();
                label14.Text = dr4[0].ToString();
                

                #region select top balance Loan ID wise-------------------------------------------------------------------------------

                SqlConnection cnn2 = new SqlConnection(IMS);
                cnn2.Open();
                String PayID2 = @"select top 1 balance from Payment_Add where Loan_ID='"+LoanID+"' order by Auto_id desc";
                SqlCommand cmm2 = new SqlCommand(PayID2, cnn2);
                SqlDataReader dr5 = cmm2.ExecuteReader(CommandBehavior.CloseConnection);

                #endregion

                while (dr5.Read())
                {
                    String Balance = dr5[0].ToString();
                    
                    if ((Double.Parse(Balance)) == 0)
                    {
                        #region Select 0 balance Customer wise.............................................

                        SqlConnection cnn3 = new SqlConnection(IMS);
                        cnn3.Open();
                        String PayID3 = @"select New_Loan_Details.[Loan_ID],New_Loan_Details.[Book_No],New_Loan_Details.[Cust_ID],New_Loan_Details.[Pay_Method],New_Loan_Details.[LoanIssueDate],New_Loan_Details.[LoanPeriad],New_Loan_Details.[LoanEndDate],New_Loan_Details.[LoanAmount],New_Loan_Details.[InterestRate],New_Loan_Details.[AlreadyPaid],New_Loan_Details.[Balance],New_Loan_Details.[IssueBy],New_Loan_Details.[TimeStamp],New_Loan_Details.[LoanAmount_Day],New_Loan_Details.[loanRate_Day],New_Loan_Details.[LoanTotal_day],New_Loan_Details.[TotalLoanAmount] from  New_Loan_Details where  New_Loan_Details.Loan_ID='" + label14.Text + "' ";
                        SqlCommand cmm3 = new SqlCommand(PayID3, cnn3);
                        SqlDataReader dr6 = cmm3.ExecuteReader(CommandBehavior.CloseConnection);

                       
                        while (dr6.Read() == true)
                        {
                            #region insert data to listview........................................

                            ListViewItem li;
                            li = new ListViewItem(dr6[0].ToString());
                            li.SubItems.Add(dr6[1].ToString());
                            li.SubItems.Add(dr6[2].ToString());
                            li.SubItems.Add(dr6[3].ToString());
                            li.SubItems.Add(dr6[4].ToString());
                            li.SubItems.Add(dr6[5].ToString());
                            li.SubItems.Add(dr6[6].ToString());
                            li.SubItems.Add(dr6[7].ToString());
                            li.SubItems.Add(dr6[8].ToString());
                            li.SubItems.Add(dr6[9].ToString());
                            li.SubItems.Add(dr6[10].ToString());
                            li.SubItems.Add(dr6[11].ToString());
                            li.SubItems.Add(dr6[12].ToString());
                            li.SubItems.Add(dr6[13].ToString());
                            li.SubItems.Add(dr6[14].ToString());
                            li.SubItems.Add(dr6[15].ToString());

                            listView3.Items.Add(li);
                            #endregion

                        }
                        #endregion
  
                    }

                    if ((Double.Parse(Balance)) != 0)
                    {
                        #region insert !=0 data to listview........................................

                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        String PayID = @"select New_Loan_Details.[Loan_ID],New_Loan_Details.[Book_No],New_Loan_Details.[Cust_ID],New_Loan_Details.[Pay_Method],New_Loan_Details.[LoanIssueDate],New_Loan_Details.[LoanPeriad],New_Loan_Details.[LoanEndDate],New_Loan_Details.[LoanAmount],New_Loan_Details.[InterestRate],New_Loan_Details.[AlreadyPaid],New_Loan_Details.[Balance],New_Loan_Details.[IssueBy],New_Loan_Details.[TimeStamp],New_Loan_Details.[LoanAmount_Day],New_Loan_Details.[loanRate_Day],New_Loan_Details.[LoanTotal_day],New_Loan_Details.[TotalLoanAmount] from  New_Loan_Details where  New_Loan_Details.Loan_ID='" + label14.Text + "' ";
                        SqlCommand cmm = new SqlCommand(PayID, cnn);
                        SqlDataReader dr3 = cmm.ExecuteReader(CommandBehavior.CloseConnection);

                        
                        while (dr3.Read() == true)
                        {
                            ListViewItem li;
                            li = new ListViewItem(dr3[0].ToString());
                            li.SubItems.Add(dr3[1].ToString());
                            li.SubItems.Add(dr3[2].ToString());
                            li.SubItems.Add(dr3[3].ToString());
                            li.SubItems.Add(dr3[4].ToString());
                            li.SubItems.Add(dr3[5].ToString());
                            li.SubItems.Add(dr3[6].ToString());
                            li.SubItems.Add(dr3[7].ToString());
                            li.SubItems.Add(dr3[8].ToString());
                            li.SubItems.Add(dr3[9].ToString());
                            li.SubItems.Add(dr3[10].ToString());
                            li.SubItems.Add(dr3[11].ToString());
                            li.SubItems.Add(dr3[12].ToString());
                            li.SubItems.Add(dr3[13].ToString());
                            li.SubItems.Add(dr3[14].ToString());
                            li.SubItems.Add(dr3[15].ToString());

                            listView1.Items.Add(li);


                        }
                        #endregion
                    }
                }
            }

            #endregion



        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
        }

        private void label7_Click(object sender, EventArgs e)
        {
           
        }

        private void button3_Click(object sender, EventArgs e)
        {
          
            comboBox1.Items.Clear();
            loadPaymentMrthod();
           
        }

        private void button5_Click(object sender, EventArgs e)
        {
            //pnlPaymentMethod.Visible = false;
            //txtPaymentMethodID.Text = "";
            //getCreatePaymentMethodeCode();
        }

        private void label25_Click(object sender, EventArgs e)
        {
            getCreateLoanPeriodeCode();
            pnlLoanPeriodeCode.Visible = true;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            #region insert Loan_Periode .............................................................................
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String PayID = "insert into Loan_Periode(LoanPeriode_ID,Name,Status)values('" + lblLoanPeriodId.Text + "','" + txtLoanPeriod.Text + "','"+"Active"+"')";
            SqlCommand cmm = new SqlCommand(PayID, cnn);
            cmm.ExecuteNonQuery();

            MessageBox.Show("Insert Successfull...");

            //txtPaymentMethodID.Text = "";
            getCreateLoanPeriodeCode();
            pnlLoanPeriodeCode.Visible = false;
            comboBox2.Items.Clear();
            loadPeriod();
            
            #endregion
        }

        private void button6_Click(object sender, EventArgs e)
        {
            getCreateLoanPeriodeCode();
            pnlLoanPeriodeCode.Visible = false;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            #region insert Interest_Rate .............................................................................
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String PayID = "insert into Interest_Rate(Interest_ID,Value,Status)values('" + lblInterestRateID.Text + "','" + txtInterestRate.Text + "','" + "Active" + "')";
            SqlCommand cmm = new SqlCommand(PayID, cnn);
            cmm.ExecuteNonQuery();

            MessageBox.Show("Insert Successfull...");

            getCreateInterestRateCode();
            txtInterestRate.Text = "";
            pnlInterestRateID.Visible = false;
            comboBox4.Items.Clear();
            LoadInterestRate();
            #endregion
        }

        public int CalculateDays(int day, int month, int year)
        {

            DateTime dt = new DateTime(year, month, day);
            int days = DateTime.Now.Subtract(dt).Days;
            return days;

            //MessageBox.Show(days.ToString());

        }

        private void label18_Click(object sender, EventArgs e)
        {
            getCreateInterestRateCode();
            pnlInterestRateID.Visible = true;
        }

        private void button8_Click(object sender, EventArgs e)
        {
            txtInterestRate.Text = "";
            pnlInterestRateID.Visible = false;
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region load Payment Method.............................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select PaymentMethod_ID from Payment_Method where Name='"+comboBox1.Text+"'";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();
           
            //=====================================================================================================================

            while (dr.Read())
            {
                label34.Text=(dr[0].ToString());
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }
            #endregion

            #region Payment method check "WEEK".........................................

            if ( comboBox2.Text == "")
            {
                return;
            }

            if (comboBox1.SelectedIndex == 1)
            {
                Double sa = 0;
                sa = ((Double.Parse(comboBox2.Text)) % 7);
                //MessageBox.Show(sa.ToString());

                if (sa != 0)
                {
                    MessageBox.Show("Payment Period Invalide..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox2.SelectedIndex = -1;
                    comboBox2.Focus();
                }
            }
            #endregion

           #region Payment method check "MONTH"..........................................

           if (comboBox1.SelectedIndex == 2)
           {
               Double sa = 0;
               sa = ((Double.Parse(comboBox2.Text)) % 30);
              
               //MessageBox.Show(sa.ToString());

               if (sa != 0)
               {
                   MessageBox.Show("Payment Period Invalide..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information); 
                   comboBox2.SelectedIndex = -1;
                   comboBox2.Focus();
                   
               }

           }
           #endregion

        

            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
              lblInstallPayment.Text= (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion

            PerDayTot();

           
            
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text=="")
            {
                return;
            }

            double days = Convert.ToDouble(comboBox2.Text.Trim());
            dateTimePicker2.Text = Convert.ToDateTime(dateTimePicker1.Text.Trim()).AddDays(days).ToShortDateString();
            DateTime timeIn = Convert.ToDateTime(dateTimePicker1.Text.Trim());

            #region Payment method check "WEEK".........................................

            if (comboBox1.SelectedIndex == 1)
            {
                Double sa = 0;
                sa = ((Double.Parse(comboBox2.Text)) % 7);
                //MessageBox.Show(sa.ToString());

                if (sa != 0)
                {
                    MessageBox.Show("Payment Period Invalide..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox2.SelectedIndex = -1;
                    comboBox2.Focus();
                    
                }
            }
            #endregion

            #region Payment method check "MONTH"........................................

            if (comboBox1.SelectedIndex == 2)
            {
                Double sa = 0;
                sa = ((Double.Parse(comboBox2.Text)) % 30);
               // MessageBox.Show(sa.ToString());

                if (sa != 0)
                {
                    MessageBox.Show("Payment Period Invalide..", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    comboBox2.SelectedIndex = -1;
                    comboBox2.Focus();
                }
            
            }
            #endregion

            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion

            PerDayTot();

        }

        private void comboBox4_SelectedIndexChanged(object sender, EventArgs e)
        {
            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion

            PerDayTot();
        }

        private void textBox10_KeyPress(object sender, KeyPressEventArgs e)
        {
           
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtLoanAmount_TextChanged(object sender, EventArgs e)
        {       
            if(txtLoanAmount.Text=="")
            {
                return;
            }

            if (Convert.ToDouble(txtLoanAmount.Text) <= 0)
            {
                comboBox4.Enabled = false;
                txtalredyPaid.Enabled = false;
            }

            if (Convert.ToDouble(txtLoanAmount.Text) > 0)
            {
                comboBox4.Enabled = true;
                txtalredyPaid.Enabled = true;
            }

            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion

            PerDayTot();
       
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            #region calculate pay amount.................................................................

            if (txtalredyPaid.Text == "")
            {
                return;
            }
            if ((Double.Parse(txtalredyPaid.Text)) > 0)
            {
                Double ToatalWithRate = (DayTotal * (Double.Parse(comboBox2.Text)));

                label40.Text = ((ToatalWithRate) - (Double.Parse(txtalredyPaid.Text))).ToString();
                label43.Text = txtalredyPaid.Text;
            }
            else
            {
                label40.Text = (DayTotal * (Double.Parse(comboBox2.Text))).ToString();
            }
            #endregion
        }
        String BookNo;
        private void button2_Click(object sender, EventArgs e)
        {
            
            #region load book number........................................................................................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Book_No from New_Loan_Details ";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================

            while (dr.Read())
            {
                 BookNo = dr[0].ToString();
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }
            #endregion

            #region check validation.....................................................................................
            if (BookNo==txtbookNum.Text)
            {
                MessageBox.Show("Have");
                txtbookNum.Focus();
                return;
            }

            if(comboBox1.Text=="")
            {
                MessageBox.Show("Please Select Payment Method","Message",MessageBoxButtons.OK,MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }

            if (comboBox2.Text == "")
            {
                MessageBox.Show("Please Select Payment Period", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox1.Focus();
                return;
            }

            if (comboBox4.Text == "")
            {
                MessageBox.Show("Please Select Interest Rate", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                comboBox4.Focus();
                return;
            }

            if (txtLoanAmount.Text == "")
            {
                MessageBox.Show("Please Enter Loan Amount", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtLoanAmount.Focus();
                return;
            }
            #endregion

            #region insert New_Loan_Details .............................................................................

             DialogResult drlInsert = MessageBox.Show("Do You Complete This New Loan Details? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
             if (drlInsert == DialogResult.Yes)
             {
                 try
                 {


                     SqlConnection cnn = new SqlConnection(IMS);
                     cnn.Open();
                     String PayID = @"insert into New_Loan_Details([Loan_ID],[Book_No],[Cust_ID],[Pay_Method],[LoanIssueDate],[LoanPeriad],[LoanEndDate],[LoanAmount],[InterestRate],[AlreadyPaid],[Balance],[IssueBy],[TimeStamp],[LoanAmount_Day],[loanRate_Day],[LoanTotal_day],[TotalLoanAmount])values
                ('" + lblLoanID.Text + "','" + txtbookNum.Text + "','" + CusID.Text + "','" + comboBox1.Text + "','" + dateTimePicker1.Text.ToString() + "','" + comboBox2.Text + "','" + dateTimePicker2.Text.ToString() + "','" + txtLoanAmount.Text + "','" + comboBox4.Text + "','" + txtalredyPaid.Text + "','" + label40.Text + "','" + LgUser.Text + "','" + DateTime.Now.ToString() + "','" + label26.Text + "','" + label46.Text + "','" + label47.Text + "','" + ToatalLoanAmount.Text + "')";
                     SqlCommand cmm = new SqlCommand(PayID, cnn);
                     cmm.ExecuteNonQuery();

                     MessageBox.Show("Insert Successfull...");

                     cnn.Close();


            #endregion

                     #region insert data to Payment_Add...................................................................

                     SqlConnection cnn1 = new SqlConnection(IMS);
                     cnn1.Open();
                     String AddPaymentDb = @"insert into Payment_Add ([Payment_ID],[Loan_ID],[Cus_ID],[Amount],[Paid],[Balance],[Status],dayPaid,Difference,timeStamp,[user]) values('" + lblpaymenrid.Text + "','" + lblLoanID.Text + "','" + CusID.Text + "','" + ToatalLoanAmount.Text + "','" + txtalredyPaid.Text + "','" + label40.Text + "','" + "Openning" + "','" + "0.00" + "','" + "0.00" + "','" + DateTime.Now.ToString() + "','" + LgUser.Text + "')";
                     SqlCommand cmm1 = new SqlCommand(AddPaymentDb, cnn1);
                     cmm1.ExecuteNonQuery();

                     // MessageBox.Show("Insert Successfull...");
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
                 }
             }
             else
             {
                 return;
             }
            #endregion

             Report.rptNew_LoanDetails NewLoan = new Report.rptNew_LoanDetails();
             NewLoan.NewloanPrint = lblLoanID.Text;
             NewLoan.Visible = true;


            #region clear & reset.....................................................................
            LoanDetails_disable();

            comboBox4.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;

            LoanDetails_disable();
            ClearTextBoxes(this.Controls);

            getCreateLoanCode();
            AutoGeneratePaymentID();
           
            button1.Enabled = false;
            button2.Enabled = false;

            lblInstallPayment.Text = "0.00";
            label40.Text = "0.00";
            label43.Text = "0.00";

            listView1.Items.Clear();
            listView3.Items.Clear();

            //select defalt image------------------------------------------------------------

            ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
            #endregion


           
              
                
               
           

            
        }

        private void txtLoanPeriod_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region numeric only.....................................................

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please provide number only");
            }
            #endregion
        }

        private void comboBox1_MouseClick(object sender, MouseEventArgs e)
        {
            
           
        }

        private void comboBox2_MouseClick(object sender, MouseEventArgs e)
        {
           
            loadPeriod();
            
        }

        private void comboBox4_MouseClick(object sender, MouseEventArgs e)
        {
          
            LoadInterestRate();
        }
       
       
        private void button1_Click(object sender, EventArgs e)
        {
            #region cancel button...........................

            comboBox4.SelectedIndex = -1;
            comboBox2.SelectedIndex = -1;
            comboBox1.SelectedIndex = -1;

            LoanDetails_disable();
            ClearTextBoxes(this.Controls);
            getCreateLoanCode();
            

            button1.Enabled = false;
            button2.Enabled = false;

            lblInstallPayment.Text = "0.00";
            label40.Text = "0.00";
            label43.Text = "0.00";

            listView1.Items.Clear();
            listView3.Items.Clear();

            //select defalt image------------------------------------------------------------

            ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;


            #endregion


        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void txtLoanAmount_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtLoanAmount.Text == "")
            {
                return;

            }

            if (comboBox2.Text == "")
            {
                return;

            }

            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion
            PerDayTot();

            
        }

        private void label47_TextChanged(object sender, EventArgs e)
        {
            if (comboBox2.Text == "")
            {
                return;
            }
            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion
            
        }

        private void label26_TextChanged(object sender, EventArgs e)
        {
            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion
          
        }

        private void label32_TextChanged(object sender, EventArgs e)
        {
            #region Calculate alredypaid................................................................

            if(comboBox2.Text=="")
            {
                return;
            }



            if (txtalredyPaid.Text=="")
            {
                return;
            }

            if ((Double.Parse(txtalredyPaid.Text)) > 0)
            {
                Double ToatalWithRate = (DayTotal * (Double.Parse(comboBox2.Text)));

                label40.Text = ((ToatalWithRate) - (Double.Parse(txtalredyPaid.Text))).ToString();
                label43.Text = txtalredyPaid.Text;
            }
            else
            {
                label40.Text =  (DayTotal * (Double.Parse(comboBox2.Text))).ToString();
            }
            #endregion
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void txtbookNum_Leave(object sender, EventArgs e)
        {
            #region load book number........................................................................................................

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Book_No from New_Loan_Details where Book_No='"+txtbookNum.Text+"' ";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================

            if (dr.Read())
            {
                BookNo = dr[0].ToString();
            }
            if (Conn.State == ConnectionState.Open)
            {
                Conn.Close();
                dr.Close();
            }
            #endregion

            #region check validation.....................................................................................
            if (BookNo== txtbookNum.Text)
            {
                MessageBox.Show("This Book No Already in the DataBase");
                txtbookNum.Focus();
                return;
            }
            #endregion
        }

        private void label46_TextChanged(object sender, EventArgs e)
        {
            #region calculate amount with payment method .........................................................

            if (comboBox1.Text == "Day")
            {
                lblInstallPayment.Text = (DayTotal * 1).ToString();
            }
            if (comboBox1.Text == "Week")
            {
                lblInstallPayment.Text = (DayTotal * 7).ToString();
            }
            if (comboBox1.Text == "Months")
            {
                lblInstallPayment.Text = (DayTotal * 30).ToString();
            }
            #endregion
        }

        private void txtalredyPaid_KeyUp(object sender, KeyEventArgs e)
        {
            if (txtalredyPaid.Text == "")
            {
                txtalredyPaid.Text = "0.00";
                label43.Text = "0.00";
            }


        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void New_Loan_Type_Load(object sender, EventArgs e)
        {
            //user name--------------------------------------------------------

            LgDisplayName.Text = User_ID.userName;
        }

        private void txtalredyPaid_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtInterestRate_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar) && e.KeyChar != '.')
            {
                e.Handled = true;
            }

            // only allow one decimal point
            if (e.KeyChar == '.' && (sender as TextBox).Text.IndexOf('.') > -1)
            {
                e.Handled = true;
            }
        }

        private void txtInterestRate_TextChanged(object sender, EventArgs e)
        {
            if(txtInterestRate.Text=="")
            {
                return;
            }
        }

        private void txtInterestRate_Leave(object sender, EventArgs e)
        {
            if(txtInterestRate.Text=="")
            {
                return;
            }
        }

        private void txtalredyPaid_Leave(object sender, EventArgs e)
        {
            if(txtalredyPaid.Text=="")
            {
                return;
            }
        }

        private void txtLoanAmount_Leave(object sender, EventArgs e)
        {
            if (txtLoanAmount.Text == "")
            {
                txtLoanAmount.Text = "0.00";
                txtalredyPaid.Text = "0.00";
            }

            //if (Convert.ToDouble(txtLoanAmount.Text) > 0)
            //{
            //    comboBox4.Enabled = true;
            //    txtalredyPaid.Enabled = true;
            //}
        }
    }
}
