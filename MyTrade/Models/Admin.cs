using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Models
{
    public class Admin : Common
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

        public string PaymentTypeId { get; set; }
        public string PaymentType { get; set; }
        public List<Admin> lstWallet { get; set; }
        public string WalletId { get; set; }
        public string Name { get; set; }
        public string PK_RequestID { get; set; }
        public List<Admin> lstEpinRequest { get; set; }
        public string ProductName { get; set; }
        public List<Admin> lstTps { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Narration { get; set; }
        public string RoiWalletId { get; set; }

        public string Date { get; set; }
        public string TopUpAmount { get; set; }
        public string ROIId { get; set; }
        public string ROI { get; set; }
        public List<Admin> lstROIIncome { get; set; }
        public List<Admin> lstROI { get; set; }
        public List<Admin> lst { get; set; }
        public string PK_PayoutWalletId { get; set; }
        public List<Admin> lstlevelIncome { get; set; }
        public List<Admin> lstlevel { get; set; }
        public List<Admin> lstPayout { get; set; }

        public string FromName { get; set; }
        public string FromLoginId { get; set; }
        public string BusinessAmount { get; set; }
        public string Percentage { get; set; }
        public string PayoutNo { get; set; }
        public string Level { get; set; }

        public string LevelIncomeTR1 { get; set; }
        public string LevelIncomeTR2 { get; set; }
        public string ClosingDate { get; set; }
        public string GrossAmount { get; set; }
        public string ProcessingFee { get; set; }
        public string TDSAmount { get; set; }
        public string NetAmount { get; set; }
        public string Remark { get; set; }
        public List<Admin> lstDistributePayment { get; set; }
        public List<Admin> lstDistributePaymentTPP { get; set; }
        public string DirectIncome { get; set; }
        public string LastClosingDate { get; set; }
        public string TPSLevelIncome { get; set; }
        public string TPPLevelIncome { get; set; }
        public string TPS { get; set; }
        public string Pk_UserId { get; set; }
        public List<Admin> lstBReports { get; set; }
        public string IsDownline { get; set; }
        public List<SelectListItem> ddlProductName { get; set; }
        public string PK_ProductID { get; set; }
        public string BV { get; set; }
        public string PackageType { get; set; }
        public string IFSCCode { get; set; }
        public string MemberAccNo { get; set; }
        public string Deduction { get; set; }
        public string Pk_AdvanceId { get; set; }
        public List<Admin> lstdeduction { get; set; }



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
                new SqlParameter("@Fk_UserId",Fk_UserId),
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

        public DataSet UpdatePaymentType()
        {
            SqlParameter[] para = {

                  new SqlParameter("@PaymentTypeId",PaymentTypeId),
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

        public DataSet GetEPinRequestDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Name",Name),
                 new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@Todate",ToDate)
            };

            DataSet ds = DBHelper.ExecuteQuery("GetEPinRequestDetails", para);
            return ds;
        }


        public DataSet AcceptRejectEPinRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",RequestID),
                 new SqlParameter("@Status",Status),
                new SqlParameter("@AddedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("AcceptRejectEPinRequest", para);
            return ds;
        }

        public DataSet GetROIWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };

            DataSet ds = DBHelper.ExecuteQuery("GetROIWalletDetails", para);
            return ds;
        }

        public DataSet GetROIIncomeReportsDetails()
        {
            SqlParameter[] para = {
                  new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };
            DataSet ds = DBHelper.ExecuteQuery("GetROIIncomeReportsDetails", para);
            return ds;
        }
        public DataSet GetROIDetails()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetROIDetails");
            return ds;
        }
        public DataSet PayoutWalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutWalletLedger", para);
            return ds;
        }

        public DataSet LevelIncomeTr1()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr1", para);
            return ds;
        }

        public DataSet LevelIncomeTr2()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetLevelIncomeTr2", para);
            return ds;
        }


        public DataSet PayoutDetail()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_Userid", Fk_UserId),
                new SqlParameter("@PayoutNo", PayoutNo),
                new SqlParameter("@FromDate", FromDate),
                    new SqlParameter("@ToDate", ToDate),
                new SqlParameter("@LoginId", LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutDetails", para);
            return ds;
        }


        public DataSet DistributePayment()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate",ClosingDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("MakePaymentList", para);
            return ds;
        }
        public DataSet SaveDistributePayment()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate", ClosingDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePayment", para);
            return ds;
        }
        public DataSet DistributePaymentTPS()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate",ClosingDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("MakePaymentListTPS", para);
            return ds;
        }
        public DataSet SaveDistributePaymentTPS()
        {
            SqlParameter[] para = {
                new SqlParameter("@ClosingDate", ClosingDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("AutoDistributePaymentTPS", para);
            return ds;
        }

        public DataSet GetBusinessReports()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@IsDownline", IsDownline),
                    new SqlParameter("@PackageType", PK_ProductID),
                       new SqlParameter("@Lvl", Level)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBusiness", para);
            return ds;
        }


        public DataSet GetProductName()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductName");
            return ds;
        }
        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId", LoginId),
                new SqlParameter("@FromDate", FromDate),
                new SqlParameter("@ToDate", ToDate),
                 new SqlParameter("@Status", Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
        public DataSet ApprovePayoutRequest()
        {
            SqlParameter[] para = {
                new SqlParameter("@Pk_RequestId",PK_RequestID),
                 new SqlParameter("@Status",Status),
                new SqlParameter("@ApprovedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApprovePayoutRequest", para);
            return ds;
        }

        public DataSet GetNameDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNameDetails", para);
            return ds;
        }

        public DataSet SaveTransferWallet()
        {
            SqlParameter[] para = {
                 new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@Amount",CrAmount),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveTransferWallet", para);
            return ds;
        }

        public DataSet SaveDeduction()
        {
            SqlParameter[] para = {
                   new SqlParameter("@Fk_UserId",Fk_UserId),
               new SqlParameter("@Amount",CrAmount),
              new SqlParameter("@Narration",Narration),
                new SqlParameter("@Remarks",Remark),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveDeduction", para);
            return ds;
        }


        public DataSet GetAdvanceDeductionReports()
        {
            SqlParameter[] para = {
               new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAdvanceDeductionReports", para);
            return ds;
        }
    }
}