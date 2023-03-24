using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTradeMTG.Models
{
    public class User : Common
    {
        #region property
        public string EPin { get; set; }
        public string PinStatus { get; set; }
        public string Password { get; set; }
        public string NewPassword { get; set; }
        public string ConfirmNewPassword { get; set; }

        public string AdharNo { get; set; }
        public string PanNumber { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string FileUpload { get; internal set; }
        public string FileName { get; internal set; }
        public string NomineeName { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }

        public string TDSAmount { get; set; }
        public decimal Amount { get; set; }
        public string NoofPins { get; set; }
        public string FinalAmount { get; set; }
        public string BankBranch { get; set; }
        public string PK_RequestID { get; set; }
        public string Name { get; set; }
        //public string LoginId { get; set; }
        public string ProductName { get; set; }
        public string IsVerified { get; set; }
        public string Image { get; set; }
        public List<User> lstEpinRequest { get; set; }

        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string IsDownline { get; set; }
        public string PK_ProductID { get; set; }
        public string Level { get; set; }
        public string BV { get; set; }
        public string Date { get; set; }
        public string PackageType { get; set; }
        public string PackageId { get; set; }
        public string PayoutBalance { get; set; }
        public string PaymentMode { get; set; }
        //public string FK_UserId { get; set; }
        public string Pk_userId { get; set; }
        public string Status { get; set; }
        public string ROIPercentage { get; set; }


        public List<User> lstBReports { get; set; }
        public List<SelectListItem> ddlProductName { get; set; }
        public List<User> lstPayoutRequest { get; set; }
        public List<User> lstFranchise { get; set; }
        public List<User> lstSalesReport { get; set; }
        public List<User> lstSaleRequest { get; set; }
        //public List<User> lstFranchiseSaleRequest { get; set; }
        public string Pk_FranchiseId { get; set; }
        public string UserName { get; set; }
        public string UserContactAddressId { get; set; }
        //public string Fk_FranchiseUserId { get; set; }
        public string FranchiseContactAddressId { get; set; }
        public string SalesDate { get; set; }
        public string FK_FranchiseUserId { get; set; }
        public string Documenturl { get; set; }

        public string MTGToken { get; set; }
        public string TransferCharge { get; set; }

        public string Title { get; set; }
        public List<User> lstReward { get; set; }
        public string PK_RewardId { get; set; }
        public string UPIID { get; set; }

        public string GrossAmount { get; set; }
        public string Franchise { get; set; }
        public string ProcessingFee { get; set; }
        public string PackageTypeId { get; set; }
        public string MemberTransferCharge { get; set; }

        public string BrokerTransferCharge { get; set; }
        public string DocumentType { get; set; }
        public string DocumentTypeNumber { get; set; }
        
        public List<User> QuickSendMTGList { get; set; }

        public string TransfertoName { get; set; }
        public string MTG { get; set; }
        public string TransferDate { get; set; }
        public string IndRupees { get; set; }
        public string percentAmt { get; set; }
        public string IndianValue { get; set; }
        public string TransferChargeInRupees { get; set; }
        public string GrossAmountRs { get; set; }







        #endregion


        #region Wallet Transfer for user
        public DataSet SaveWalletTransferBalance()
        {
            SqlParameter[] para = {
                 new SqlParameter("@Fk_UserId",Fk_UserId),
                new SqlParameter("@Amount",Amount),
                 new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveWalletTransfer", para);
            return ds;
        }

        public DataSet GetNameDetailsforUserWalletTransfer()
        {
            SqlParameter[] para = {
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetNameDetailsforTransferwallet", para);
            return ds;
        }
        #endregion


        public DataSet ValidateEpin()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@EPin", EPin),
                                      new SqlParameter("@Fk_UserId",Fk_UserId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("ValidatePin", para);

            return ds;
        }
        public DataSet ActivateUser()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@EPinNo", EPin),
                                      new SqlParameter("@Fk_UserId",Fk_UserId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);

            return ds;
        }

        public DataSet ChangePassword()
        {
            SqlParameter[] para = {new SqlParameter("@OldPassword",Password),
                                   new SqlParameter("@NewPassword",NewPassword),
                                   new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UserChangePassword", para);
            return ds;

        }

        public DataSet BankDetailsUpdate()
        {
            SqlParameter[] para = {
                                   new SqlParameter("@FK_UserId",Fk_UserId),
                                   new SqlParameter("@PanNo",PanNumber),
                                   new SqlParameter("@AadharNo",AdharNo),
                                   new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@Branch",BranchName),
                                   new SqlParameter("@AccountNo",AccountNo),
                                    new SqlParameter("@IFSCCode",IFSCCode),
                                     new SqlParameter("@NomineeName",NomineeName),
                                    new SqlParameter("@NomineeRelation",NomineeRelation),
                                     new SqlParameter("@NomineeAge",NomineeAge),
                                         new SqlParameter("@PanImage",Image),
                                           new SqlParameter("@UPIID",UPIID),

                                      new SqlParameter("@UpdatedBy",Fk_UserId),
                                       new SqlParameter("@DocumentType",DocumentType),
                                       new SqlParameter("@DocumentTypeNumber",DocumentTypeNumber)

            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBankDetails", para);
            return ds;
        }

        public DataSet UserProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", Fk_UserId),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }

        public DataSet SaveEpinRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@FK_PackageId",Package),
                                   new SqlParameter("@NoofPins",NoofPins),
                                   new SqlParameter("@FK_UserId",AddedBy),
                                    new SqlParameter("@Amount",FinalAmount),
            };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinByUser", para);
            return ds;
        }


        public DataSet GetEPinRequestDetails()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEPinRequestDetailsForUser", para);
            return ds;
        }


        public DataSet DeleteEPinRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@PK_RequestID",PK_RequestID),
                                   new SqlParameter("@DeletedBy",PK_RequestID)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteEPinRequest", para);
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
        public DataSet PayoutRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@Amount",Amount),
                                   new SqlParameter("@TransferChargeInRupees",IndRupees),
                                    new SqlParameter("@AmountinRs",IndianValue),
                                   new SqlParameter("@TransferChargeMTG",percentAmt),
                                    new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("PayoutRequest", para);
            return ds;
        }
        public DataSet GetPayoutBalance()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutBalance", para);
            return ds;
        }
        public DataSet GetPayoutRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@LoginId",LoginId),
                                   new SqlParameter("@FromDate",FromDate),
                                    new SqlParameter("@ToDate",ToDate),
                                     new SqlParameter("@Status",State)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetPayoutRequest", para);
            return ds;
        }
        public DataSet GetRewarDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Title",Title)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetRewarDetails", para);
            return ds;
        }
        public DataSet GetFileDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Title",Title)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetFilesDetails", para);
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
        public DataSet CreateOrder()
        {
            SqlParameter[] para = {new SqlParameter("@Fk_UserId",Fk_UserId),
                                   new SqlParameter("@Amount",Amount),
                                    new SqlParameter("@Status",Status),
                                    new SqlParameter("@Status",Status),
                                    new SqlParameter("@Status",Status),
                                    new SqlParameter("@Status",Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);
            return ds;
        }
        public DataSet FranchiseRequest()
        {
            SqlParameter[] para = {new SqlParameter("@FirmName",FirmName),
                                    new SqlParameter("@Email",Email),
                                    new SqlParameter("@Mobile",Mobile),
                                       new SqlParameter("@BankName",BankName),
                                    new SqlParameter("@BranchName",BranchName),
                                       new SqlParameter("@AccountNo",AccountNo),
                                    new SqlParameter("@IFSCCode",IFSCCode),
                                       new SqlParameter("@Address",Address),
                                           new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseRequest", para);
            return ds;
        }
        public string FirmName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public DataSet GetUserDetailsForMTGPurchaseSell()
        {
            SqlParameter[] para = {new SqlParameter("@Pk_userId",AddedBy)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetUserDetailsForMTGPurchaseSell", para);
            return ds;
        }
        public DataSet FranchiseList()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@Fk_UserId",AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("FranchiseRequestList", para);
            return ds;
        }
        public string Pk_FranchisetransferId { get; set; }
        public DataSet GetSalesReport()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@Fk_UserId", Fk_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)


                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetSalesReportforUser", para);
            return ds;
        }
        public DataSet GetSaleRequest()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@Fk_FranchiseUserId", FK_FranchiseUserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)


                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetSalesReportforFranchise", para);
            return ds;
        }
        public DataSet SaveMTGTransferCharge()
        {
            SqlParameter[] para = {
                                    new SqlParameter("@CustomerAddressId",CustomerId),
                                     new SqlParameter("@FirmName",FirmName),
                                      new SqlParameter("@MTGToken",MTGToken),
                                       new SqlParameter("@TransferCharge",TransferCharge),
                                        new SqlParameter("@AddedBy",AddedBy),
                                        //new SqlParameter("@AddedBy",Fk_UserId),
                                        new SqlParameter("@FK_FranchiseUserId",FK_FranchiseUserId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("SaveMTGTransferCharge", para);
            return ds;
        }
        public DataSet GetPaymentMode()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeListforSaleRequest");

            return ds;
        }
        public DataSet ApproveSaleRequest()
        {
            SqlParameter[] para = {
               
                new SqlParameter("@AddedBy",AddedBy),
                new SqlParameter("@TransactionDate",TransactionDate),
                new SqlParameter("@TransactionId",TransactionNo),
                new SqlParameter("@PaymentMode",PaymentMode),
                 new SqlParameter("@Pk_FranchisetransferId",Pk_FranchisetransferId),
                new SqlParameter("@BankName",BankName),
                new SqlParameter("@BranchName",BranchName),
                new SqlParameter("@Status",Status),
                new SqlParameter("@DocumentUrl",Documenturl),
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveSaleRequest", para);
            return ds;
        }

        public DataSet GetWalletTransfer()
        {
            SqlParameter[] para = {

                new SqlParameter("@Fk_UserId",Fk_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletTransfer", para);
            return ds;
        }

    }



  














    public class Pin
    {
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public string ParentLoginId { get; set; }
        public string Name { get; set; }
        public string PK_PinId { get; set; }
        public string PK_EPinDetailsId { get; set; }
        public string ePinNo { get; set; }
        public string PinAmount { get; set; }
        public string Amount { get; set; }
        public string PinStatus { get; set; }
        public string IsRegistered { get; set; }
        public string RegisteredTo { get; set; }
        public List<Pin> lst { get; set; }
        public string ProductName { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string ToId { get; set; }
        public string ToName { get; set; }
        public string TransferDate { get; set; }
        public string ToLoginId { get; set; }
        public string Response { get; set; }
        public string Login_Id { get; set; }

        public string UserName { get; set; }
        public string Message { get; set; }
        public string PinGenerationDate { get; set; }
        public string FromLoginId { get; set; }
        public string GST { get; set; }
        public string BV { get; set; }
        public string ActivationDate { get; set; }


        public DataSet GetPinList()
        {
            SqlParameter[] para = {
                  new SqlParameter("@PinStatus", PinStatus),
                                      new SqlParameter("@FK_UserId", FK_UserId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetPin", para);

            return ds;
        }
        public DataSet ePinTransfer()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Epino",ePinNo),
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ePinTransfer", para);
            return ds;
        }
        public DataSet GetTransferPinReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromLoginId",LoginId),
                new SqlParameter("@ToLoginId",ToLoginId),
                new SqlParameter("@EpinNo",ePinNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTransferPinReport", para);
            return ds;
        }

        public DataSet ActivatePin()
        {
            SqlParameter[] para = {new SqlParameter("@Fk_UserId",FK_UserId),
                                   new SqlParameter("@EPinNo",ePinNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);
            return ds;
        }
        public DataSet GetPaymentType()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentType");
            return ds;
        }



    }
}