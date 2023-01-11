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
using System.IO;

namespace Loan_issue
{
    public partial class Loan_Issue : Form
    {
        String IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        public Loan_Issue()
        {
            

            InitializeComponent();
            AutoGeneratePaymentID();
            btnAdd.Enabled = false;
            BtnCancel.Enabled = false;
            btnSave.Enabled = false;
            textBox4.Enabled = false;

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
                lblPaymentID.Text = "PYT1001";
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

                    lblPaymentID.Text = "PYT" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }

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


            }
            #endregion
        }

        public void GetTotalAmount()
        {
            #region GetTotalAmount.................................................................

            decimal gtotal = 0;
            foreach (ListViewItem lstItem in listView1.Items)
            {
                gtotal += Math.Round(decimal.Parse(lstItem.SubItems[4].Text), 2);
            }
            LblTotalForPaymentID.Text = Convert.ToString(gtotal);
            #endregion

        }

        private void Loan_Issue_Load(object sender, EventArgs e)
        {
            //user name--------------------------------------------------------

            LgDisplayName.Text = User_ID.userName;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Load loan to grideview.................................

            PnlLoanSerch.Visible = true;


            dataGridView1.Rows.Clear();
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            string SELCus = @"SELECT distinct      New_Loan_Details.Loan_ID,   New_Loan_Details.Book_No,New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
                         New_Loan_Details.Balance, New_Loan_Details.IssueBy, New_Loan_Details.LoanAmount_Day, New_Loan_Details.loanRate_Day, 
                         New_Loan_Details.LoanTotal_day, New_Loan_Details.TotalLoanAmount,Customer_Details.FristName, Customer_Details.LastName,Customer_Details.NIC, Customer_Details.Telephone
                        FROM New_Loan_Details inner JOIN Payment_Add  ON New_Loan_Details.Loan_ID = Payment_Add.Loan_ID inner join Customer_Details on Customer_Details.Cus_ID= Payment_Add.Cus_ID where Payment_Add.Balance!='0'    ";

            SqlCommand cmm = new SqlCommand(SELCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read() == true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19]);
            }
            #endregion
        }

        private void textBox3_KeyUp(object sender, KeyEventArgs e)
        {
            #region Search loan.................................

//            



            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            string SELCus = @"SELECT      New_Loan_Details.Loan_ID,   New_Loan_Details.Book_No,New_Loan_Details.Cust_ID, New_Loan_Details.Pay_Method, New_Loan_Details.LoanIssueDate, New_Loan_Details.LoanPeriad, 
                         New_Loan_Details.LoanEndDate, New_Loan_Details.LoanAmount, New_Loan_Details.InterestRate, New_Loan_Details.AlreadyPaid, 
                         New_Loan_Details.Balance, New_Loan_Details.IssueBy, New_Loan_Details.LoanAmount_Day, New_Loan_Details.loanRate_Day, 
                         New_Loan_Details.LoanTotal_day, New_Loan_Details.TotalLoanAmount,  
                         Customer_Details.FristName, Customer_Details.LastName,Customer_Details.NIC, Customer_Details.Telephone
                         FROM            New_Loan_Details INNER JOIN
                         Customer_Details ON New_Loan_Details.Cust_ID = Customer_Details.Cus_ID where New_Loan_Details.Loan_ID like '%" + textBox3.Text + "%' or  New_Loan_Details.Book_No like '%" + textBox3.Text + "%' or New_Loan_Details.Cust_ID like '%" + textBox3.Text + "%'or Customer_Details.FristName like '%" + textBox3.Text + "%' or Customer_Details.LastName like '%" + textBox3.Text + "%'or Customer_Details.NIC like '%" + textBox3.Text + "%' or Customer_Details.Telephone like '%" + textBox3.Text + "%' ";


            dataGridView1.Rows.Clear();
            SqlCommand cmm = new SqlCommand(SELCus, cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read() == true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11], dr[12], dr[13], dr[14], dr[15], dr[16], dr[17], dr[18], dr[19]);
            }
            #endregion
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {

        }

        private void label20_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            ClearTextBoxes(this.Controls);

            lblBalanceUpto.Text = "00.00";
            lblExcess.Text = "00.00";
            lblinstall.Text = "00.00";
            lblinterestRate.Text = "00.00";
           // lblLoanId.Text = "--";
            lblPaidAmount.Text = "00.00";
            lblbalanceAmount.Text = "00.00";

           // listView1.Items.Clear();

            btnAdd.Enabled = false;
            textBox4.Enabled = false;

            #region data pass to textbox...........................................................................

            DataGridViewRow dr;
            dr = dataGridView1.SelectedRows[0];
            lblLoanId.Text = dr.Cells[0].Value.ToString();
            txtbookNo.Text = dr.Cells[1].Value.ToString();
            txtpaymethod.Text = dr.Cells[3].Value.ToString();
            dateTimePicker1.Text = dr.Cells[4].Value.ToString();
            txtloanperiod.Text = dr.Cells[5].Value.ToString();
            dateTimePicker2.Text = dr.Cells[6].Value.ToString();
            ltxtloanamount.Text = dr.Cells[7].Value.ToString();
            lblinterestRate.Text = dr.Cells[8].Value.ToString();
            lblinstall.Text = dr.Cells[14].Value.ToString();
            lblbalanceAmount.Text = dr.Cells[10].Value.ToString();
            lblPaidAmount.Text = dr.Cells[9].Value.ToString();
            label1.Text = dr.Cells[14].Value.ToString();
           // textBox4.Text = dr.Cells[14].Value.ToString();
            label11.Text = dr.Cells[2].Value.ToString();

            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            String AmountPay = "SELECT top 1 Payment_ID, [Loan_ID],[Amount],[Paid],[Balance],[Status],[dayPaid],[Difference],[timeStamp] FROM [Payment_Add] where [Loan_ID]='" + lblLoanId.Text + "' order by [Auto_id] desc ";
            SqlCommand cmm4 = new SqlCommand(AmountPay, cnn);
            SqlDataReader dr4=cmm4.ExecuteReader();
            while(dr4.Read())
            {
                lblbalanceAmount.Text = dr4[4].ToString();
                lblPaidAmount.Text = dr4[3].ToString();

            }



            #endregion

            #region Select the Image--------------------------------------------------------------

            //====================================================================================
            SqlConnection con1 = new SqlConnection(IMS);
            con1.Open();


            string ItemImage = "SELECT Image FROM Customer_Details WHERE Cus_ID='" + dr.Cells[2].Value.ToString() +"'";
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

            #region Selct Sum of Difference..............................

            SqlConnection cnon = new SqlConnection(IMS);
            cnon.Open();
            String getDiffSum = "SELECT SUM(Difference) as Difference FROM Payment_Add where Loan_ID='" + dr.Cells[0].Value.ToString() +"'";
            SqlCommand cmm=new SqlCommand(getDiffSum,cnon);
            SqlDataReader dr5=cmm.ExecuteReader(CommandBehavior.CloseConnection);
            if(dr5.Read())
            {
                lblExcess.Text = dr5[0].ToString();
            }
            #endregion

            PnlLoanSerch.Visible = false;

            btnAdd.Enabled = true;
            BtnCancel.Enabled = true;
            btnSave.Enabled = true;
            textBox4.Enabled = true;
           
        }

        private void lblbalanceAmount_Click(object sender, EventArgs e)
        {

        }

        private void lblbalanceAmount_TextChanged(object sender, EventArgs e)
        {
            lblBalanceUpto.Text = lblbalanceAmount.Text;
        }

        private void textBox4_KeyUp(object sender, KeyEventArgs e)
        {
            #region calculate  Balance Up to......................................................
            if (textBox4.Text == "")
            {
                return;
            }
            Double minDay = (Double.Parse(lblbalanceAmount.Text)) - (Double.Parse(textBox4.Text));
            lblBalanceUpto.Text = minDay.ToString();
            #endregion
        }
        String diff;
        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            #region Difference Value calculate..........................................
            if (textBox4.Text=="")
            {
                return;
            }
            Double DifferenceDayPay = (Double.Parse(textBox4.Text)) - (Double.Parse(lblinstall.Text));
                diff=DifferenceDayPay.ToString();
            #endregion
        }
        Double ims;
        private void button3_Click(object sender, EventArgs e)
        {
            if ((Double.Parse(lblBalanceUpto.Text)) < (Double.Parse(textBox4.Text)))
            {
                MessageBox.Show(" payment is grater than available Balance", "Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
                textBox4.Text = "0.00";
            }
            GetTotalAmount();

            #region Check items in the list view=====================================
            

            for (int i = 0; i <= listView1.Items.Count - 1; i++)
            {
                //if equal items
                    if (listView1.Items[i].SubItems[1].Text == lblLoanId.Text)
                    {

                        MessageBox.Show("The Item alredy in the list.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }



            }


           
            #endregion
     

             #region Add to list view..........................................................




             if (textBox4.Text=="" ||(Double.Parse( textBox4.Text))==0)
            {
                MessageBox.Show("Please Enter Today payment.","Message");
                textBox4.Focus();
                return;

            }
            ListViewItem li;
            li = new ListViewItem(lblPaymentID.Text);
            li.SubItems.Add(lblLoanId.Text);
            li.SubItems.Add(label11.Text);
            li.SubItems.Add(lblbalanceAmount.Text);
            li.SubItems.Add(textBox4.Text);
            li.SubItems.Add(lblBalanceUpto.Text);
            li.SubItems.Add("Pending");
            li.SubItems.Add(lblinstall.Text);
            li.SubItems.Add(diff);

            //MessageBox.Show(diff);

            listView1.Items.Add(li);
            GetTotalAmount();

            #endregion

            BtnCancel.Enabled = true;

            ClearTextBoxes(this.Controls);

            lblBalanceUpto.Text = "00.00";
            lblExcess.Text = "00.00";
            lblinstall.Text = "00.00";
            lblinterestRate.Text = "00.00";
            lblLoanId.Text = "--";
            lblPaidAmount.Text = "00.00";
            lblbalanceAmount.Text = "00.00";

            btnAdd.Enabled = false;
            textBox4.Enabled = false;

        }

        private void lblBalanceUpto_TextChanged(object sender, EventArgs e)
        {
           
        }
        
        private void button1_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);

            lblBalanceUpto.Text = "00.00";
            lblExcess.Text = "00.00"; 
            lblinstall.Text = "00.00";
            lblinterestRate.Text = "00.00";
            lblLoanId.Text = "--";
            lblPaidAmount.Text = "00.00";
            lblbalanceAmount.Text = "00.00";

            btnAdd.Enabled = true;
            
            btnSave.Enabled = true;
            textBox4.Enabled = false;
           
        }

        String addPaid,emp;
        private void button2_Click(object sender, EventArgs e)
        {
            #region Insert data in db...................................................................
            if (listView1.Items.Count == 0)
            {
                MessageBox.Show("please add Value...");
                return;
            }

            if (listView1.Items.Count != 0)
            {
              DialogResult drlInsert = MessageBox.Show("Do You Complete This Loan issue Details? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (drlInsert == DialogResult.Yes)
               {

                for (int i = 0; i <= listView1.Items.Count - 1; i++)
                {
                    #region select top Paid balance.....................................................................................................



                    SqlConnection cnn4 = new SqlConnection(IMS);
                    cnn4.Open();
                    String newtop = "select top 1 Paid  from Payment_Add  where Loan_ID ='" + listView1.Items[i].SubItems[1].Text + "' order by Auto_id desc";
                    SqlCommand cmm4 = new SqlCommand(newtop, cnn4);

                    SqlDataReader dr4 = cmm4.ExecuteReader();
                    if (dr4.Read())
                    {
                        addPaid = (dr4[0].ToString());
                        // MessageBox.Show(addPaid);
                    }
                    dr4.Close();
                    #endregion

                    #region Calulation detween PaidAmount..............................................................


                    double Calc_Bal = Convert.ToDouble(addPaid);
                    Calc_Bal = Calc_Bal + Convert.ToDouble(listView1.Items[i].SubItems[4].Text);
                    // MessageBox.Show(Calc_Bal.ToString());


                    //======================================================================================================




                    #endregion

                    #region insert data to Payment_Add DB............................................

                   

                        SqlConnection Conn = new SqlConnection(IMS);
                        Conn.Open();
                        String addLoanPay = "insert into [Payment_Add] ([Payment_ID],[Loan_ID],[Cus_ID],[Amount],[Paid],[Balance] ,[Status],[dayPaid],[Difference],[timeStamp],[user])values (@Payment_ID,@Loan_ID,@Cus_ID,@Amount,@Paid,@Balance ,@Status,@dayPaid,@Difference,@timeStamp,@user)";
                        SqlCommand cmm = new SqlCommand(addLoanPay, Conn);

                        cmm.Parameters.AddWithValue("Payment_ID", lblPaymentID.Text);
                        cmm.Parameters.AddWithValue("Loan_ID", listView1.Items[i].SubItems[1].Text);
                        cmm.Parameters.AddWithValue("Cus_ID", listView1.Items[i].SubItems[2].Text);
                        cmm.Parameters.AddWithValue("Amount", listView1.Items[i].SubItems[3].Text);
                        cmm.Parameters.AddWithValue("Paid", Calc_Bal);
                        cmm.Parameters.AddWithValue("Balance", listView1.Items[i].SubItems[5].Text);
                        cmm.Parameters.AddWithValue("Status", listView1.Items[i].SubItems[6].Text);
                        cmm.Parameters.AddWithValue("dayPaid", listView1.Items[i].SubItems[4].Text);
                        cmm.Parameters.AddWithValue("Difference", listView1.Items[i].SubItems[8].Text);
                        cmm.Parameters.AddWithValue("timeStamp", DateTime.Now.ToString());
                        cmm.Parameters.AddWithValue("user", LgUser.Text);


                        cmm.ExecuteNonQuery();

                    
                    #endregion

                    }
                   }
                    else
                    {
                        return;
                    }
                    #region data insert payment doc DB..................................................................

                    SqlConnection Conn1 = new SqlConnection(IMS);
                    Conn1.Open();
                    String PaymentDoc = "insert into Payment_id_Doc (Payment_id,ToatalForPaymentID,Status)values('" + lblPaymentID.Text + "','" + LblTotalForPaymentID.Text + "','" + "Pending" + "')";
                    SqlCommand cmm1 = new SqlCommand(PaymentDoc, Conn1);
                    cmm1.ExecuteNonQuery();

                    //,'" + DateTime.Now.ToString() + "',,PaymentConformDate

                    #endregion

                
                MessageBox.Show("Successfull");
                //Cristal report---------------------------------------------------------------

                Report.rptPaymentAdd NewLoan = new Report.rptPaymentAdd();
                NewLoan.PaymentAdd = lblPaymentID.Text;
                NewLoan.Visible = true;

                ClearTextBoxes(this.Controls);

                lblBalanceUpto.Text = "00.00";
                lblExcess.Text = "00.00";
                lblinstall.Text = "00.00";
                lblinterestRate.Text = "00.00";
                lblLoanId.Text = "--";
                lblPaidAmount.Text = "00.00";
                //lblPaymentID.Text = "00.00";
                lblbalanceAmount.Text = "00.00";

                listView1.Items.Clear();

                btnAdd.Enabled = false;
                BtnCancel.Enabled = false;
                btnSave.Enabled = false;
                textBox4.Enabled = false;
                AutoGeneratePaymentID();

            }
            #endregion
        }

        private void label28_Click(object sender, EventArgs e)
        {
            ClearTextBoxes(this.Controls);
        }

        private void label35_Click(object sender, EventArgs e)
        {

        }

        private void label26_Click(object sender, EventArgs e)
        {

        }

        private void label36_Click(object sender, EventArgs e)
        {

        }

        private void lblExcess_Click(object sender, EventArgs e)
        {
            
        }

        private void lblinstall_Click(object sender, EventArgs e)
        {

        }

        private void lblinstall_TextChanged(object sender, EventArgs e)
        {
            if (lblinstall.Text == "" || lblExcess.Text == "")
            {
                return;
            }

            
            lbluptoBalance.Text = ((Double.Parse(lblinstall.Text)) +(-1) * (Double.Parse(lblExcess.Text))).ToString();
        }

        private void lblExcess_TextChanged(object sender, EventArgs e)
        {
            if (lblinstall.Text == "" || lblExcess.Text == "")
            {
                return;
            }

           Decimal x=Convert.ToDecimal ((Double.Parse(lblinstall.Text)) +(-1)* (Double.Parse(lblExcess.Text)));
           lbluptoBalance.Text = Convert.ToString(Math.Round(x, 2));
        }

        private void lblinstall_TextAlignChanged(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click_1(object sender, EventArgs e)
        {

        }

        private void listView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            listView1.SelectedItems[0].Remove();
            ClearTextBoxes(this.Controls);

            lblBalanceUpto.Text = "00.00";
            lblExcess.Text = "00.00";
            lblinstall.Text = "00.00";
            lblinterestRate.Text = "00.00";
            lblLoanId.Text = "--";
            lblPaidAmount.Text = "00.00";
            lblbalanceAmount.Text = "00.00";

            btnAdd.Enabled = false;
            textBox4.Enabled = false;

        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlLoanSerch.Visible = false;
        }

        private void btnAdd_KeyDown(object sender, KeyEventArgs e)
        {
            GetTotalAmount();
        }

        private void LblTotalForPaymentID_Click(object sender, EventArgs e)
        {

        }

        private void label35_Click_2(object sender, EventArgs e)
        {

        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void label15_Click(object sender, EventArgs e)
        {
           
        }

        private void textBox4_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                btnAdd.Focus();
            }
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void textBox4_KeyPress(object sender, KeyPressEventArgs e)
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
    }
}
