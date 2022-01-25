using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class Admin :Common
    {
        #region property
        public string LoginId { get; set; }
        public string Amount { get; set; }
        public string NoofPins { get; set; }
        public string FinalAmount { get; set; }
        public List<Admin> lstunusedpins { get; set; }

        public string ePinNo { get; set; }

        public string RegisteredTo { get; set; }
        public string RegisteredToName { get; set; }
        public string OwnerID { get; set; }
        public string OwnerName { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string Status { get; set; }

        #endregion
        #region PinGenerated
        public DataSet CreatePin()
        {
            SqlParameter[] para = {

                                       
                                        new SqlParameter("@NoofPins", NoofPins),
                                         new SqlParameter("@LoginId", LoginId),
                                         new SqlParameter("@CreatedBy", AddedBy),
                                         new SqlParameter("@Amount", Amount),
                                         new SqlParameter("@FK_PaymentId",Fk_Paymentid),
                                         new SqlParameter("@TransactionDate",TransactionDate),
                                         new SqlParameter("@TransactionNo",TransactionNo),
                                         new SqlParameter("@bankname",BankName),
                                         new SqlParameter("@branchname",BranchName),
                                         new SqlParameter("@Fk_PackageId",Package)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinByAdmin", para);
            return ds;
        }
        public DataSet BindPriceByProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductId", Package) };
            DataSet ds = DBHelper.ExecuteQuery("GetProductList", para);
            return ds;
        }
        public DataSet GetUsedUnUsedPins()
        {
            SqlParameter[] para = {

                                        new SqlParameter("@Status", Status),
                                        new SqlParameter("@EPinNo", ePinNo),
                                        new SqlParameter("@Package", Package),
                                        new SqlParameter("@OwnerID", OwnerID ),
                                        new SqlParameter("@RegToId", RegisteredTo )

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetUnusedUsedPins", para);
            return ds;
        }
        #endregion
    }
}