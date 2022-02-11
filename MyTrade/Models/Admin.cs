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

        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string RequestID { get; set; }
        public string UserId { get; set; }
        public string RequestCode { get; set; }
        public string PaymentMode { get; set; }
        public string BankBranch { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }

        public string PaymentType { get; set; }
        public List<Admin> lstWallet { get; set; }

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



        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AdminChangePassword", para);
            return ds;

        }

        public DataSet GetEwalletRequestDetails()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequestDetails", para);

            return ds;
        }
        public DataSet ApproveDeclineEwalletRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",RequestID),
                 new SqlParameter("@Status",Status),
                new SqlParameter("@AddedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveDeclineEwalletRequest", para);
            return ds;
        }

        public DataSet SavePaymentType()
        {
            SqlParameter[] para = {
                new SqlParameter("@PaymentType",PaymentType),
                 new SqlParameter("@Status",Status),
                
                new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SavePaymentType", para);
            return ds;
        }

        public DataSet GetPaymentType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentType");
            return ds;
        }
        


    }
}