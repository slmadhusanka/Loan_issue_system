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
    public partial class New_Customer : Form
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;
        string ImgLoc = "";
        public New_Customer()
        {
            

            InitializeComponent();
            getCreateVendorCode();
            RbNew.Enabled = true;
            RbNew.Checked = true;
        }

        public void getCreateVendorCode()
        {
            #region CREATE CUSid............

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();


            //=====================================================================================================================
            string sql = "select Cus_ID from Customer_Details";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            SqlDataReader dr = cmd.ExecuteReader();

            //=====================================================================================================================
            if (!dr.Read())
            {
                CusID.Text = "CUS1001";
                cmd.Dispose();
                dr.Close();
            }
            else
            {
                cmd.Dispose();
                dr.Close();

                // string sql1 = "select MAX(CusID) from Customer_Details";
                string sql1 = "select TOP 1 Cus_ID from Customer_Details ORDER BY Cus_ID DESC";
                SqlCommand cmd1 = new SqlCommand(sql1, Conn);
                SqlDataReader dr7 = cmd1.ExecuteReader();

                while (dr7.Read())
                {
                    string no;
                    no = dr7[0].ToString();

                    string VenNumOnly = no.Substring(3);

                    no = (Convert.ToInt32(VenNumOnly) + 1).ToString();

                    CusID.Text = "CUS" + no;


                }
                cmd1.Dispose();
                dr7.Close();
            }
            Conn.Close();
            #endregion
        }

        private void New_Customer_Activated(object sender, EventArgs e)
        {
            CusFirstName.Focus();
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
                txtCusNIC.Clear();

            }
            #endregion
        }

        public void disable()
        {
            #region Disable all text,button...........................

            CusCountry.Enabled = false;
            CusEmailAddress.Enabled = false;
            CusFaxNumber.Enabled = false;
            CusFirstName.Enabled = false;
            CusID.Enabled = false;
            CusLastName.Enabled = false;
            CusMobileNumber.Enabled = false;
            CusPersonalAddress.Enabled = false;
            CusRemarks.Enabled = false;
            CusTelNUmber.Enabled = false;
            txtCusNIC.Enabled = false;
            BtnCancel.Enabled = false;
            BtnNew.Enabled = false;
            BtnSave.Enabled = false;
            button1.Enabled = false;
            #endregion

        }


        public void Enable()
        {
            #region Enable all text,button...........................

            CusCountry.Enabled = true;
            CusEmailAddress.Enabled = true;
            CusFaxNumber.Enabled = true;
            CusFirstName.Enabled = true;
            CusLastName.Enabled = true;
            CusMobileNumber.Enabled = true;
            CusPersonalAddress.Enabled = true;
            CusRemarks.Enabled = true;
            CusTelNUmber.Enabled = true;
            txtCusNIC.Enabled = true;
            BtnCancel.Enabled = true;
            BtnNew.Enabled = true;
            BtnSave.Enabled = true;
            button1.Enabled = true;

            #endregion
        }

        private void button4_Click(object sender, EventArgs e)
        {
            #region Load Customer.................................

            PnlCustomerSerch.Visible = true;
            RbNew.Checked = false;
            
            dataGridView1.Rows.Clear();
            SqlConnection cnn = new SqlConnection(IMS);
            cnn.Open();
            string SELCus = "SELECT [Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Image],[Remark] FROM [Customer_Details]";
            SqlCommand cmm = new SqlCommand(SELCus,cnn);
            SqlDataReader dr = cmm.ExecuteReader();
            while (dr.Read()==true)
            {
                dataGridView1.Rows.Add(dr[0], dr[1], dr[2], dr[3], dr[4], dr[5], dr[6], dr[7], dr[8], dr[9], dr[10], dr[11]);
            }
            #endregion
        }

        private void VenSerCancel_Click(object sender, EventArgs e)
        {
            PnlCustomerSerch.Visible = false;
        }

        private void BtnSave_Click(object sender, EventArgs e)
        {
             byte[] img = null;
            
            try
            {
                #region Insert New Customer Data......................

            if (RbNew.Checked == true)
            {
               

                    if (CusPersonalAddress.Text == "" && txtCusNIC.Text == "" && CusMobileNumber.Text == "" && CusFirstName.Text == "")
                    {
                        MessageBox.Show("Please Fill all  Details", "Message");
                        return;
                    }
                    if (RbNew.Checked == false)
                    {
                        MessageBox.Show("Please cheked New button .", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    DialogResult drlInsert = MessageBox.Show("Do You Complete This New Customer Details? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (drlInsert == DialogResult.Yes)
                    {

                    if (ImgLoc != "")
                    {
                        #region if available image in DB............................................

                        FileStream fs = new FileStream(ImgLoc, FileMode.Open, FileAccess.Read);
                        BinaryReader br = new BinaryReader(fs);
                        img = br.ReadBytes((int)fs.Length);

                        SqlConnection cnn = new SqlConnection(IMS);
                        cnn.Open();
                        String AddCus = @"insert into [Customer_Details] ([Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Image],[Remark])values('" + CusID.Text + "','" + CusFirstName.Text + "','" + CusLastName.Text + "','" + CusMobileNumber.Text + "','" + CusFaxNumber.Text + "','" + CusTelNUmber.Text + "','" + CusPersonalAddress.Text + "','" + txtCusNIC.Text + "','" + CusEmailAddress.Text + "','" + CusCountry.Text + "',@img, '" + CusRemarks.Text + "')";
                        SqlCommand cmd = new SqlCommand(AddCus, cnn);


                        cmd.Parameters.Add(new SqlParameter("@img", img));
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Successfully Saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTextBoxes(this.Controls);
                        getCreateVendorCode();
                        ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
                        RbUp.Enabled = false;
                        RbUp.Checked = false;

                        #endregion
                    }
                    else
                    {
                        #region if not available image in DB............................................
                        SqlConnection cnn1 = new SqlConnection(IMS);
                        cnn1.Open();
                        String AddCus1 = @"insert into [Customer_Details] ([Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Remark])values('" + CusID.Text + "','" + CusFirstName.Text + "','" + CusLastName.Text + "','" + CusMobileNumber.Text + "','" + CusFaxNumber.Text + "','" + CusTelNUmber.Text + "','" + CusPersonalAddress.Text + "','" + txtCusNIC.Text + "','" + CusEmailAddress.Text + "','" + CusCountry.Text + "','" + CusRemarks.Text + "')";
                        SqlCommand cmd1 = new SqlCommand(AddCus1, cnn1);
                        cmd1.ExecuteNonQuery();

                        MessageBox.Show("Successfully Saved.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        ClearTextBoxes(this.Controls);
                        getCreateVendorCode();
                        ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
                        RbUp.Enabled = false;
                        RbUp.Checked = false;
                        #endregion

                    }
                    getCreateVendorCode();
                }
                else
                {
                    return;
                }
                    button1.Focus();
                #endregion
            }

            #region Update Customer........................................................

            if (RbUp.Checked == true)
            {
                RbNew.Checked = false;
               DialogResult drlInsert = MessageBox.Show("Do You Complete This New Customer Details? ", "Message", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
               if (drlInsert == DialogResult.Yes)
               {
                   if (ImgLoc != "")
                   {
                       #region if available image in DB............................................

                       if (RbUp.Checked == false)
                       {
                           MessageBox.Show("Please cheked UpDate button .", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);
                       }

                       FileStream fs = new FileStream(ImgLoc, FileMode.Open, FileAccess.Read);
                       BinaryReader br = new BinaryReader(fs);
                       img = br.ReadBytes((int)fs.Length);

                       SqlConnection cnn = new SqlConnection(IMS);
                       cnn.Open();
                       String UpCust = @"update Customer_Details set [FristName]='" + CusFirstName.Text + "',[LastName]='" + CusLastName.Text + "',[MobileNo]='" + CusMobileNumber.Text + "',[Fax]='" + CusFaxNumber.Text + "' ,[Telephone]='" + CusTelNUmber.Text + "',[Address]='" + CusPersonalAddress.Text + "',[NIC]='" + txtCusNIC.Text + "',[Email]='" + CusEmailAddress.Text + "',[Country]= '" + CusCountry.Text + "',[Image]=@img,[Remark]='" + CusRemarks.Text + "' where [Cus_ID]='" + CusID.Text + "'";
                       SqlCommand cmd = new SqlCommand(UpCust, cnn);

                       cmd.Parameters.Add(new SqlParameter("@img", img));
                       cmd.ExecuteNonQuery();

                       MessageBox.Show("Successfully Updated.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       ClearTextBoxes(this.Controls);
                       getCreateVendorCode();
                       ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
                       RbUp.Enabled = false;
                       RbUp.Checked = false;
                       RbNew.Checked = true;
                       BtnSave.Text = "Save";


                       if (cnn.State == ConnectionState.Open)
                       {
                           cnn.Close();
                       }
                       #endregion
                   }
                   else
                   {
                       #region if not available image............................................

                       SqlConnection cnn = new SqlConnection(IMS);
                       cnn.Open();
                       String UpCust = @"update Customer_Details set [FristName]='" + CusFirstName.Text + "',[LastName]='" + CusLastName.Text + "',[MobileNo]='" + CusMobileNumber.Text + "',[Fax]='" + CusFaxNumber.Text + "' ,[Telephone]='" + CusTelNUmber.Text + "',[Address]='" + CusPersonalAddress.Text + "',[NIC]='" + txtCusNIC.Text + "',[Email]='" + CusEmailAddress.Text + "',[Country]= '" + CusCountry.Text + "',[Remark]='" + CusRemarks.Text + "' where [Cus_ID]='" + CusID.Text + "'";
                       SqlCommand cmd = new SqlCommand(UpCust, cnn);

                       MessageBox.Show("Successfully Updated.", "Record", MessageBoxButtons.OK, MessageBoxIcon.Information);

                       if (cnn.State == ConnectionState.Open)
                       {
                           cnn.Close();
                       }

                       ClearTextBoxes(this.Controls);
                       getCreateVendorCode();
                       ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
                       RbUp.Enabled = false;
                       RbUp.Checked = false;
                       RbNew.Checked = true;
                       BtnSave.Text = "Save";
                       #endregion
                   }

                   getCreateVendorCode();
               }
               else
               {
                   return;
               }
            }

            #endregion
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "System Error", MessageBoxButtons.RetryCancel, MessageBoxIcon.Error);
            }
            button4.Focus();
               
        }

        private void button1_Click(object sender, EventArgs e)
        {

            #region Browes path..............................

            try
            {


                OpenFileDialog dlg = new OpenFileDialog();
                dlg.Filter = ("Image Files |*.png; *.bmp; *.jpg;*.jpeg; *.gif;");
                dlg.FilterIndex = 4;

                dlg.Title = "select Product Image";
                if (dlg.ShowDialog() == DialogResult.OK)
                {
                    ItmImage.Image = null;
                    ImgLoc = dlg.FileName.ToString();
                    ItmImage.ImageLocation = ImgLoc;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            #endregion

           
        }

        private void label8_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_KeyUp(object sender, KeyEventArgs e)
        {
            PnlCustomerSerch.Visible = true;
            RbNew.Checked = false;
            RbNew.Enabled = false;
            #region select customer......................................................
            try
            {
               

                SqlConnection cnn = new SqlConnection(IMS);
                cnn.Open();
                
                string SELCus = "SELECT [Cus_ID],[FristName],[LastName],[MobileNo],[Fax] ,[Telephone],[Address],[NIC],[Email],[Country],[Image],[Remark] FROM [Customer_Details] where Cus_ID like'" + textBox1.Text + "%' or FristName like'" + textBox1.Text + "%' or LastName like'" + textBox1.Text + "%' or MobileNo like'" + textBox1.Text + "%' or  NIC like'" + textBox1.Text + "%' ";
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

        private void dataGridView1_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            
        }

        private void dataGridView1_RowHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            #region values pass textbox and picture box..............................................

            DataGridViewRow dr = dataGridView1.SelectedRows[0];
            CusID.Text = dr.Cells[0].Value.ToString();
            CusFirstName.Text = dr.Cells[1].Value.ToString();
            CusLastName.Text = dr.Cells[2].Value.ToString();
            CusMobileNumber.Text = dr.Cells[3].Value.ToString();
            CusFaxNumber.Text = dr.Cells[4].Value.ToString();
            CusTelNUmber.Text = dr.Cells[5].Value.ToString();
            CusPersonalAddress.Text = dr.Cells[6].Value.ToString();
            txtCusNIC.Text = dr.Cells[7].Value.ToString();
            CusEmailAddress.Text = dr.Cells[8].Value.ToString();
            CusCountry.Text = dr.Cells[9].Value.ToString();
            CusRemarks.Text = dr.Cells[11].Value.ToString();


           
            PnlCustomerSerch.Visible = false;
            RbUp.Enabled = true;
            
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

            disable();
            #endregion
        }

        private void BtnCancel_Click(object sender, EventArgs e)
        {
            // clear all text-------------------------------------------------------------

            ClearTextBoxes(this.Controls);

            //generate cus code------------------------------------------------------------

            getCreateVendorCode();

            //select defalt image------------------------------------------------------------

            ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;

            RbUp.Enabled = false;
            RbUp.Checked = false;
            RbNew.Checked = true;
            RbNew.Enabled = true;
        }

        private void RbUp_CheckedChanged(object sender, EventArgs e)
        {
            Enable();
            BtnSave.Text = "UpDate";
        }

        private void RbNew_CheckedChanged(object sender, EventArgs e)
        {
            // clear all text-------------------------------------------------------------
            ClearTextBoxes(this.Controls);

            //generate cus code------------------------------------------------------------
            getCreateVendorCode();

            Enable();
            RbUp.Enabled = false;
            BtnSave.Text = "Save";

            //select defalt image------------------------------------------------------------

            ItmImage.Image = Loan_issue.Properties.Resources.User_No_Frame_mirror;
        }

        private void txtCusNIC_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region numeric only.....................................................

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please provide number only");
            }
            #endregion
        }

        private void CusTelNUmber_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region numeric only.....................................................

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please provide number only");
            }
            #endregion
        }

        private void CusFaxNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region numeric only.....................................................

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please provide number only");
            }
            #endregion
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void CusFirstName_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyValue==13)
            {
                CusLastName.Focus();
            }
        }

        private void CusLastName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusMobileNumber.Focus();
            }
        }

        private void CusMobileNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusFaxNumber.Focus();
            }
        }

        private void CusFaxNumber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusTelNUmber.Focus();
            }
        }

        private void CusTelNUmber_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusPersonalAddress.Focus();
            }
        }

        private void CusPersonalAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                txtCusNIC.Focus();
            }
        }

        private void txtCusNIC_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusEmailAddress.Focus();
            }
        }

        private void CusEmailAddress_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusCountry.Focus();
            }
        }

        private void CusCountry_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                CusRemarks.Focus();
            }
        }

        private void CusRemarks_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                button1.Focus();
            }
        }

        private void BtnSave_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue == 13)
            {
                BtnSave.Focus();
            }
        }

        private void New_Customer_Load(object sender, EventArgs e)
        {
            //user name--------------------------------------------------------

            LgDisplayName.Text = User_ID.userName;
        }

        private void CusMobileNumber_KeyPress(object sender, KeyPressEventArgs e)
        {
            #region numeric only.....................................................

            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
                MessageBox.Show("Please provide number only");
            }
            #endregion
        }

    }
}
