﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTradeMTG.Models
{
    public class AdminReports : Common
    {
        public string Count { get; set; }
        public string TopupIDRandom { get; set; }
        public List<AdminReports> lstBonazaReward { get; set; }
        public List<AdminReports> lstTDSReport { get; set; }
        public List<AdminReports> lsttopupreport { get; set; }
        public string isBlocked { get; set; }
        public string Email { get; set; }
        public string FromDate { get; set; }
        public bool IsDownline { get; set; }
        public string JoiningDate { get; set; }
        public string LoginId { get; set; }
        public List<AdminReports> lstassociate { get; set; }
        public List<AdminReports> lstPinTransfer { get; set; }
        public List<AdminReports> lstDirect { get; set; }
        public string BasisOn { get; set; }
        public string Mobile { get; set; }
        public string Name { get; set; }
        public string Password { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string Status { get; set; }
        public string ToDate { get; set; }
        public string ToLoginID { get; set; }
        public string UpgradtionDate { get; set; }
        public string Amount { get; set; }
        public string TopupBy { get; set; }
        public string PrintingDate { get; set; }
        public string Description { get; set; }
        public string PaymentMode { get; set; }
        public string BusinessType { get; set; }
        public string ReceiptNo { get; set; }
        public string FromLoginID { get; set; }
        public string ePinNo { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string ToId { get; set; }
        public string ToName { get; set; }
        public string TransferDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string PermanentDate { get; set; }
        public string Gender { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNo { get; set; }
        public string AdharNo { get; set; }
        public string PanNo { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }
        public string Image { get; set; }
        public string MemberStatus { get; set; }
        public string UPIID { get; set; }
        public string PackageDays { get; set; }
        public string PinAmount { get; set; }
        public string BV { get; set; }
        public string TransactionBy { get; set; }
        public string ROIPercentage { get; set; }
        public string IsCalculated { get; set; }
        public string TopUpDate { get; set; }
        public string Pk_investmentId { get; set; }
        public string Pk_EwalletId { get; set; }
        public string Narration { get; set; }
        public string DrAmount { get; set; }
        public string CrAmount { get; set; }
        public string LastChanged { get; set; }
        public List<AdminReports> lstWalletLedger { get; set; }
        public List<AdminReports> lstActivateByPayment { get; set; }
        public List<AdminReports> lstWallet { get; set; }
        public List<AdminReports> lstcontact { get; set; }
        public string Remark { get; set; }
        public string BankBranch { get; set; }
        public string ChequeDDNo { get; set; }
        public string ChequeDDDate { get; set; }
        public string RequestID { get; set; }
        public string UserId { get; set; }
        public string RequestCode { get; set; }
        public string WalletId { get; set; }
        public string UsedFor { get; set; }
        public string AvailableBalance { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string Date { get; set; }
        public string DirectStatus { get; set; }
        public string Ids { get; set; }
        public string Level { get; set; }
        public string ActivationMode { get; set; }
        public string TopupVia { get; set; }
        public string OperatorId { get; set; }
        public string CircleId { get; set; }
        public string Provider { get; set; }
        public string TransactionFor { get; set; }
        public string ServerOrderId { get; set; }
        public string IsHoldTPS { get; set; }
        public string WithdrawalStatus { get; set; }
        //public string SponserName { get; set; }
        public string OrderNo { get; set; }
        public string Reward { get; set; }
        public string Fk_BonazaId { get; set; }
        public string BusinessTarget { get; set; }
        public string RewardAmount { get; set; }
        public string RewardImage { get; set; }
        public string FK_BonazaDetailsId { get; set; }
        public List<AdminReports> lstsalesreports { get; set; }
        public List<AdminReports> lstsaleViewReports { get; set; }
        public string UserCA { get; set; }
        public string FranchiseeCA { get; set; }
        
        public string UserContactAddress { get; set; }
        public string UserName { get; set; }
        public string FranchiseeContactAddress { get; set; }
        public string FirmName { get; set; }
        public string mtgtoken { get; set; }
        public string TransferCharge { get; set; }
        public string SaleDate { get; set; }

        public string TransactionId { get; set; }
        public string DocumentUrl { get; set; }
        public string FranchiseApprovalRejectionDate { get; set; }
        public string SaleRequestDate { get; set; }
        public string Pk_FranchisetransferId { get; set; }
        public string ActivationMTGToken { get; set; }

        public string Topupid { get; set; }
        public Boolean IsBlock { get; set; }




        public DataSet GetRechargeList()
        {
            SqlParameter[] para =
            {
               new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@FromDate",FromDate),
                 new SqlParameter("@ToDate",ToDate)

            };
            DataSet ds = DBHelper.ExecuteQuery("GetRechargeList", para);
            return ds;
        }
        #region associatelist
        public DataSet GetAssociateList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@SponsorID", SponsorId),
                                    new SqlParameter("@SponsorName", SponsorName),
                                    new SqlParameter("@Status", Status),
                                    new SqlParameter("@IsDownline", IsDownline),
                                    new SqlParameter("@Leg", Leg),
                                     new SqlParameter("@MemberStatus",MemberStatus),
                                           new SqlParameter("@Mobile",MobileNo),
                                             new SqlParameter("@ActivateBy",ActivationMode),
                                            
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateList", para);
            return ds;
        }
        #endregion
        #region topupreport
        public DataSet GetTopupReport()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginID", LoginId),
                                      new SqlParameter("@Name", Name),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate),
                                      new SqlParameter("@Package", Package),
                                      new SqlParameter("@ClaculationStatus", Status),
                                      new SqlParameter("@Fk_BusinessId", BusinessType)
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTopupreport", para);
            return ds;
        }
        #endregion

        public DataSet GetTransferPinReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromLoginId",LoginId),
                new SqlParameter("@ToLoginId",ToLoginID),
                new SqlParameter("@EpinNo",ePinNo)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetTransferPinReportForAdmin", para);
            return ds;
        }

        public DataSet GetDirectList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectListForAdmin", para);
            return ds;
        }
        public DataSet getJoiningPackagelist()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("getJoiningPackageReport", para);
            return ds;
        }
        public DataSet GetAdminProfileDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetAdminProfileDetails", para);
            return ds;
        }
        
        public DataSet UpdateAdminProfile()
        {
            SqlParameter[] para = {
                   new SqlParameter("@PK_UserId",Fk_UserId),
                      new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@SponserName",SponsorName),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                     new SqlParameter("@Sex",Gender),
                                   new SqlParameter("@Mobile",MobileNo),
                                    new SqlParameter("@Email",Email),
                                     new SqlParameter("@PinCode",PinCode),
                                    new SqlParameter("@State",State),
                                     new SqlParameter("@City",City),
                                   new SqlParameter("@PanNo",PanNo),
                                    new SqlParameter("@PanImage",Image),
                                    new SqlParameter("@Address",Address),
                                   new SqlParameter("@AadharNo",AdharNo),
                                   new SqlParameter("@BankName",BankName),
                                     new SqlParameter("@Branch",BranchName),
                                   new SqlParameter("@AccountNo",AccountNo),
                                    new SqlParameter("@IFSCCode",IFSCCode),
                                     new SqlParameter("@NomineeName",NomineeName),
                                    new SqlParameter("@NomineeRelation",NomineeRelation),
                                     new SqlParameter("@NomineeAge",NomineeAge),
                                      new SqlParameter("@UPIID",UPIID),
                                      new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateAdminProfile", para);
            return ds;
        }
        
        public DataSet DeleteUerDetails()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId),
                                    new SqlParameter("@DeletedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteUerDetails", para);
            return ds;
        }

        public DataSet ViewProfileVeriFy()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ViewProfileVeriFy", para);
            return ds;
        }

        public DataSet WalletLedger()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name",Name)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("WalletLedgerForAdmin", para);
            return ds;
        }
        public DataSet GetActivateByPaymentDetails()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name",Name),
                                      new SqlParameter("@UsedFor",UsedFor)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetActivateByPaymentDetails", para);
            return ds;
        }

        public DataSet DeclinedKyc()
        {
            SqlParameter[] para = { new SqlParameter("@PK_UserId", Fk_UserId),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeclinedKyc", para);
            return ds;
        }
        public DataSet DeleteTopUp()
        {
            SqlParameter[] para = { new SqlParameter("@PK_InvestmentId", Pk_investmentId),
                                    new SqlParameter("@UpdatedBy",UpdatedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("DeleteTopUp", para);
            return ds;
        }

        public DataSet ContactList()
        {
            SqlParameter[] para = {
                new SqlParameter("@Name", Name),
                 new SqlParameter("@FromDate", FromDate),
                  new SqlParameter("@ToDate", ToDate)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetContactList", para);
            return ds;
        }

        #region claim report
        public DataSet ClaimRewardReport()
        {
            SqlParameter[] para = { new SqlParameter("@PK_RewardAchieverId", RewardAchieverID),
                                      };
            DataSet ds = DBHelper.ExecuteQuery("ClaimRewardReport", para);
            return ds;
        }
        public string RewardAchieverID { get; set; }
        public string RewardName { get; set; }
        public string AssociateName { get; set; }
        public string PanImage { get; set; }
        public List<AdminReports> lstRew { get; set; }
        public string Target { get; set; }

        #endregion
        #region TPSStatus
        public DataSet UpdateTPSStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_Userid",Fk_UserId),
                new SqlParameter("@IsHoldTPS",IsHoldTPS),
                new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateTPSStatus", para);
            return ds;
        }
        public DataSet UpdateWithdrawalStatus()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Fk_Userid",Fk_UserId),
                new SqlParameter("@WithdrawalStatus",WithdrawalStatus),
                new SqlParameter("@UpdatedBy",UpdatedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateWithdrawalStatus", para);
            return ds;
        }
        #endregion

        public DataSet GetTdsReport()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@LoginId",LoginId),
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GETTDSREPORTS", para);
            return ds;
        }

        public DataSet GetBonazaRewardList()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FromDate",FromDate),
                new SqlParameter("@ToDate",ToDate),
                new SqlParameter("@Fk_BonazaId",FK_BonazaDetailsId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetBonazaRewardList", para);
            return ds;
        }

        public DataSet GetReward()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetReward");
            return ds;
        }

        public DataSet SaveBonaza()
        {
            SqlParameter[] para = {
                   new SqlParameter("@Fk_BonazaDetailsId",Fk_BonazaId),
                   new SqlParameter("@Reward",Reward),
                   new SqlParameter("@BusinessTarget",BusinessTarget),
                   new SqlParameter("@RewardAmount",RewardAmount),
                   new SqlParameter("@RewardImage",RewardImage),
                   new SqlParameter("@AddedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("SaveBonaza", para);
            return ds;
        }
        public DataSet updateBonaza()
        {
            SqlParameter[] para = {
                   new SqlParameter("@Fk_BonazaRewardId",Fk_BonazaId),
                   new SqlParameter("@Reward",Reward),
                   new SqlParameter("@BusinessTarget",BusinessTarget),
                   new SqlParameter("@RewardAmount",RewardAmount),
                   new SqlParameter("@RewardImage",RewardImage),
                   new SqlParameter("@AddedBy",AddedBy),
                   new SqlParameter("@Fk_BonazaDetailsId",FK_BonazaDetailsId)
            };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBonaza", para);
            return ds;
        }
        public DataSet deleteBonaza()
        {
            SqlParameter[] para =
            {
             new SqlParameter("@Fk_BonazaId",FK_BonazaDetailsId),
             new SqlParameter("@DeletedBy",AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("DeleteBonazaRewardDetails", para);
            return ds;
        }



        public DataSet GetSalesReportforAdmin()
        {
            SqlParameter[] para =
            {
             new SqlParameter("@UserCA",UserCA),
                 new SqlParameter("@FranchiseeCA",FranchiseeCA),
                     new SqlParameter("@FromDate",FromDate),
                         new SqlParameter("@ToDate",ToDate),
                             new SqlParameter("@Status",Status)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetSalesReportforAdmin", para);
            return ds;
        }


        public DataSet GetViewSalesReportforAdmin()
        {

            SqlParameter[] para =
           {
                 new SqlParameter("@Pk_FranchisetransferId",Pk_FranchisetransferId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetViewSalesReportforAdmin",para);
            return ds;
        }




        public DataSet ApproveRequest()
        {
            SqlParameter[] para = {
                       new SqlParameter("@Pk_FranchisetransferId",Pk_FranchisetransferId),
                           new SqlParameter("@Status","Approved"),
                   new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveRequest", para);
            return ds;
        }



        public DataSet RejectRequest()
        {
            SqlParameter[] para = {
                   new SqlParameter("@Pk_FranchisetransferId",Pk_FranchisetransferId),
                    new SqlParameter("@Status","Rejected"),
                   new SqlParameter("@AddedBy", AddedBy)
            };
            DataSet ds = DBHelper.ExecuteQuery("ApproveRequest", para);
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


    }
}