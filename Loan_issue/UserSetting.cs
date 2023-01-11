using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Loan_issue
{
    class UserSetting
    {
        string IMS = ConfigurationManager.ConnectionStrings["IMS_DataString"].ConnectionString;





        public SqlDataReader User_Setting()
        {
            string LoginID = User_ID.UserID;

            SqlConnection Conn = new SqlConnection(IMS);
            Conn.Open();

            string Select_Details = @"SELECT   AllCustomerRPT, CompletedLoanRPT, DailyCollectionRPT, ExpireRPT, PaymethodRPT, ApprovePayment, BackUpSave, 
                      LoanIssue, NewCustomer, NewLoan, UserControl, UserProfile FROM User_Setting where UserId='" + LoginID + "'";

            SqlCommand com = new SqlCommand(Select_Details, Conn);
            SqlDataReader dr = com.ExecuteReader();
            return dr;
        }
    }
}
