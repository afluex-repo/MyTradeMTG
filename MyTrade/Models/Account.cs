using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyTrade.Models
{
    public class Account
    {
        public string LoginId { get; set; }
        public string PackageId { get; set; }
        public string TopUpDate { get; set; }
        public string Amount { get; set; }
        public string Remarks { get; set; }
        public string FK_UserId { get; set; }
        public string PaymentMode { get; set; }
        public string BankName { get; set; }
        public string BankBranch { get; set; }
        public string TransactionNo { get; set; }
        public string TransactionDate { get; set; }
        public string AddedBy { get; set; }
        public string TotalAmount { get; set; }
        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", AddedBy),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@TopupDate", TopUpDate),
                                        new SqlParameter("@Amount", TotalAmount),
                                        new SqlParameter("@Description", Remarks),
                                          new SqlParameter("@PaymentMode", PaymentMode),
                                            new SqlParameter("@TransactionNo", TransactionNo),
                                              new SqlParameter("@TransactionDate", TransactionDate),
                                                new SqlParameter("@BankName", BankName),
                                                  new SqlParameter("@BankBranch", BankBranch)


                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
    }
}