using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class API
    {

    }
    #region Registratio
    public class RegistrationAPI
    {

        public string Status { get; set; }
        public string Message { get; set; }
        //public string Leg { get; set; }
        public string PK_UserId { get; set; }
        public string Password { get; set; }
        public string RegistrationBy { get; set; }
        public string SponsorId { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string TransPassword { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfilePic { get; set; }
        public string PinCode { get; set; }
        public string Gender { get; set; }
        public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@Email",Email),
                                          new SqlParameter("@Gender",Gender),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",""),
                                     new SqlParameter("@Password",Password)

                                   };
            DataSet ds = DBHelper.ExecuteQuery("Registration", para);
            return ds;
        }

    }
    #endregion
    #region Sponsor
    public class SponsorNameAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string sponsorId { get; set; }

        public DataSet GetMemberDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@LoginId", sponsorId),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetMemberDetailsMobile", para);

            return ds;
        }
    }
    public class SponsorNameA
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string SponsorName { get; set; }

    }
    #endregion
    #region Sponsor
    public class Pincode
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PinCode { get; set; }
        public DataSet GetStateCity()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@PinCode", PinCode),

                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetStateCity", para);

            return ds;
        }
    }
    public class StateDetails
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string State { get; set; }
        public string City { get; set; }
    }
    #endregion
    #region Login
    public class LoginAPI
    {
        public string LoginId { get; set; }
        public string UserId { get; set; }
        public string Password { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string UserType { get; set; }
        public string FullName { get; set; }
        public string Pk_adminId { get; set; }
        public string TeamPermanent { get; set; }
        public string FranchiseAdminID { get; set; }
        public string Profile { get; set; }
        public string Gender { get; set; }
        public DataSet Login()
        {
            SqlParameter[] para ={new SqlParameter ("@LoginId",LoginId),
                                new SqlParameter("@Password",Password)};
            DataSet ds = DBHelper.ExecuteQuery("Login", para);
            return ds;
        }
    }
    #endregion
    #region EpinDetails
    public class EpinDetails
    {

        public string EPin { get; set; }
        public string Fk_UserId { get; set; }
        public string Message { get; set; }
        public string Status { get; set; }
        //public DataSet ValidateEpin()
        //{
        //    SqlParameter[] para = {
        //                              new SqlParameter("@EPin", EPin),

        //                          };
        //    DataSet ds = DBHelper.ExecuteQuery("ValidatePin", para);

        //    return ds;
        //}
        public DataSet ActivateUser()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@EPinNo", EPin),
                                      new SqlParameter("@Fk_UserId",Fk_UserId)

                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUser", para);

            return ds;
        }
    }
    public class EpinDetails1
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string PinStatus { get; internal set; }
    }
    #endregion
    public class AssociateDashBoard
    {
        public string Fk_UserId { get; set; }
        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", Fk_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }


    }
    public class DashboardResponse
    {
        public string TotalDownline { get; set; }
        public string TotalDirect { get; set; }
        public string TotalActive { get; set; }
        public string TotalInActive { get; set; }
        public string WalletBalance { get; set; }
        public string Status { get; set; }
        public string ActiveStatus { get; set; }
        public string Message { get; set; }
        public string ReferralLink { get; set; }
        public string TotalBusiness { get; set; }
        public string TeamBusiness { get; set; }
        public string SelfBusiness { get; set; }
        public string TotalTeam { get; set; }
        public string TotalTeamActive { get; set; }
        public string TotalTeamInActive { get; set; }
        public decimal TotalIncome { get; set; }
        public string LevelIncomeTr1 { get; set; }
        public string LevelIncomeTr2 { get; set; }
        public string UsedPins { get; set; }
        public string AvailablePins { get; set; }
        public string TotalPins { get; set; }
        public string TotalPayoutWallet { get; set; }
        public decimal TotalAmount { get; set; }
        public string Tr1Business { get; set; }
        public string Tr2Business { get; set; }
        public string TotalTPSAmountTobeReceived { get; set; }
        public string TotalTPSAmountReceived { get; set; }
        public dynamic TotalTPSBalanceAmount { get; set; }
        public List<Reward> lstReward { get; set; }
    }
    public class UpdateProfile
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class TreeAPI
    {
        public List<Tree1> GetGenelogy { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string Fk_headId { get; set; }
        public DataSet GetTree()
        {
            SqlParameter[] para = {   new SqlParameter("@LoginId", LoginId),
                 new SqlParameter("@Fk_headId", Fk_headId)
                                  };

            DataSet ds = DBHelper.ExecuteQuery("GetTree", para);
            return ds;
        }
    }
    public class Tree1
    {

        public string SponsorId { get; set; }
        public string Fk_ParentId { get; set; }
        public string TeamPermanent { get; set; }
        public string Fk_SponsorId { get; set; }
        public string MemberName { get; set; }
        public string MemberLevel { get; set; }
        public string Id { get; set; }
        public string ActivationDate { get; set; }
        public string ActiveLeft { get; set; }
        public string ActiveRight { get; set; }
        public string InactiveLeft { get; set; }
        public string InactiveRight { get; set; }
        public string BusinessLeft { get; set; }
        public string BusinessRight { get; set; }
        public string ImageURL { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Leg { get; set; }
    }
    public class TopupByUser
    {
        public string LoginId { get; set; }
        public string PackageId { get; set; }
        public string Amount { get; set; }
        public string FK_UserId { get; set; }
        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", FK_UserId),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@Amount", Amount),
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
    }
    public class TopupResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class PaymentMode
    {
        public string PK_PaymentModeId { get; set; }
        public string PaymentModeName { get; set; }
    }
    public class PaymentModeResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string UPIId { get; set; }
        public string UPIImage { get; set; }
        public List<PaymentMode> lst { get; set; }
        public DataSet PaymentList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetPaymentModeList");
            return ds;
        }
    }
    public class Package
    {
        public string PK_PackageId { get; set; }
        public string PackageName { get; set; }
        public string PackagePrice { get; set; }
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
        public decimal AmountWithGST { get; set; }
        public string InMultipleOf { get; set; }
    }
    public class PackageResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<Package> lst { get; set; }
        public DataSet PackageList()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForTopUp");
            return ds;
        }
        public DataSet BindProductForJoining()
        {
            DataSet ds = DBHelper.ExecuteQuery("GetProductListForJoining");
            return ds;
        }
    }
    public class DirectRequest
    {
        public string Ids { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public string Status { get; set; }
        public string DirectStatus { get; set; }
        public DataSet GetDirectList()
        {

            SqlParameter[] para = {
                                    new SqlParameter("@PK_UserIds", Ids),
                                    new SqlParameter("@FK_UserId", Fk_UserId),
                                    new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", Status),
                                       new SqlParameter("@DirectStatus", DirectStatus),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectList", para);
            return ds;
        }
        //public DataSet GetDownlineList()
        //{
        //    SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
        //                            new SqlParameter("@Name", Name),
        //                            new SqlParameter("@FromDate", FromDate),
        //                            new SqlParameter("@ToDate", ToDate),
        //                            new SqlParameter("@Leg", Leg),
        //                            new SqlParameter("@Status", ActivationStatus), };
        //    DataSet ds = DBHelper.ExecuteQuery("DownlineList", para);
        //    return ds;
        //}
    }
    public class DirectReponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<DirectList> lst { get; set; }
    }
    public class DirectList
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Leg { get; set; }
        public string Package { get; set; }
        public string JoiningDate { get; set; }
        public string PermanentDate { get; set; }
        public string Status { get; set; }
    }
    public class PinRequest
    {
        public string FK_UserId { get; set; }

        public DataSet GetPinList()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetPin", para);
            return ds;
        }
    }
    public class PinAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PinAPIResponse> lst { get; set; }
    }
    public class PinAPIResponse
    {
        public string ePinNo { get; set; }
        public string PinAmount { get; set; }
        public string PinStatus { get; set; }
        public string RegisteredTo { get; set; }
        public string ProductName { get; set; }
        public string Amount { get; set; }
        public string PinGenerationDate { get; set; }
        public string GST { get; set; }
    }
    public class LevelTreeReq
    {
        public string LoginId { get; set; }
        public string RootAgentCode { get; set; }

        public DataSet GetLevelTreeData()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@AgentCode", LoginId),
                                      new SqlParameter("@RootAgentCode", RootAgentCode),

            };

            DataSet ds = DBHelper.ExecuteQuery("LevelTree", para);
            return ds;
        }
    }
    public class LevelTreeAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<LevelTreeResponse> lst { get; set; }
    }
    public class LevelTreeResponse
    {
        public string FK_ParentId { get; set; }
        public string PK_UserId { get; set; }
        public string FK_SponsorId { get; set; }
        public string LoginId { get; set; }
        public string MemberName { get; set; }
        public string AssociateMemberName { get; set; }
        public string ProfilePic { get; set; }
    }
    public class AssociateBookingRequest
    {
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", FK_UserId),
                                          new SqlParameter("@LoginId", LoginId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
    }
    public class AssociateBookingAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<AssciateBookingResponse> lst { get; set; }

    }
    public class AssciateBookingResponse
    {
        public string ActiveStatus { get; set; }
        public string FirstName { get; set; }
        public string Fk_SponsorId { get; set; }
        public string Fk_UserId { get; set; }
        public string LoginId { get; set; }
        public string Status { get; set; }
    }
    public class Wallet
    {
        public string PK_UserId { get; set; }
        public DataSet GetWalletBalance()
        {
            SqlParameter[] para = { new SqlParameter("@PK_USerID", PK_UserId) };
            DataSet ds = DBHelper.ExecuteQuery("GetWalletBalance", para);

            return ds;

        }
    }
    public class WalletBalanceAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string Balance { get; set; }
    }
    public class TransferPin
    {
        public string EPinNo { get; set; }
        public string LoginId { get; set; }
        public DataSet ePinTransfer()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@Epino",EPinNo),
                new SqlParameter("@LoginId",LoginId)
            };
            DataSet ds = DBHelper.ExecuteQuery("ePinTransfer", para);
            return ds;
        }
    }
    public class Reponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
    public class PinReport
    {
        public string LoginId { get; set; }
        public string ToLoginId { get; set; }
        public string ePinNo { get; set; }
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
    }
    public class PinResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<PinDetails> lst { get; set; }
    }
    public class PinDetails
    {
        public string ePinNo { get; set; }
        public string FromId { get; set; }
        public string FromName { get; set; }
        public string PinAmount { get; set; }
        public string ProductName { get; set; }
        public string BV { get; set; }
        public string ToId { get; set; }
        public string ToName { get; set; }
        public string TransferDate { get; set; }
    }
    public class Request
    {
        public string FK_UserId { get; set; }
        public DataSet UserProfile()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }

    }
    public class ProfileAPI
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string FK_UserId { get; set; }
        public string LoginId { get; set; }
        public string SponsorId { get; set; }
        public string SponsorName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string MobileNo { get; set; }
        public string Email { get; set; }
        public string PinCode { get; set; }
        public string State { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ProfilePic { get; set; }
        public string AadharNo { get; set; }
        public string PanNo { get; set; }
        public DataSet UpdateProfile()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@AadharNo", AadharNo),
                                      new SqlParameter("@PanNo", PanNo),
                                      new SqlParameter("@Address", Address),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateProfile", para);

            return ds;
        }
    }



    public class BankDetailsUpdateRequest
    {
        public string FK_UserId { get; set; }
        public DataSet BankDetailsEdit()
        {
            SqlParameter[] para =
            {
                new SqlParameter("@FK_UserId",FK_UserId),
            };
            DataSet ds = DBHelper.ExecuteQuery("UserProfile", para);
            return ds;
        }

    }


    public class BankDetailsUpdateAPIResponse
    {

        public string Status { get; set; }
        public string Message { get; set; }
        public string Fk_UserId { get; set; }
        public string PanNumber { get; set; }
        public string AdharNo { get; set; }
        public string BankName { get; set; }
        public string BranchName { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string NomineeName { get; set; }
        public string NomineeRelation { get; set; }
        public string NomineeAge { get; set; }

        public DataSet BankUpdate()
        {
            SqlParameter[] para = {
                                 new SqlParameter("@FK_UserId", Fk_UserId),
                                   new SqlParameter("@PanNo", PanNumber),
                                   new SqlParameter("@AadharNo", AdharNo),
                                   new SqlParameter("@BankName", BankName),
                                     new SqlParameter("@Branch", BranchName),
                                   new SqlParameter("@AccountNo", AccountNo),
                                    new SqlParameter("@IFSCCode", IFSCCode),
                                     new SqlParameter("@NomineeName", NomineeName),
                                    new SqlParameter("@NomineeRelation", NomineeRelation),
                                     new SqlParameter("@NomineeAge", NomineeAge),
                                      new SqlParameter("@UpdatedBy", Fk_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UpdateBankDetails", para);
            return ds;
        }
    }


    public class AddWalletRequest
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string PaymentMode { get; set; }
        public string Amount { get; set; }
        public string DDChequeNo { get; set; }
        public string DDChequeDate { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string AddedBy { get; set; }
        public string Remark { get; set; }
        public DataSet AddWallet()
        {
            SqlParameter[] para = {
                                     new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@Amount", Amount),
                                      new SqlParameter("@PaymentMode", PaymentMode) ,
                                      new SqlParameter("@DDChequeNo", DDChequeNo) ,
                                      new SqlParameter("@DDChequeDate", DDChequeDate) ,
                                      new SqlParameter("@BankBranch", BankBranch) ,
                                          new SqlParameter("@BankName", BankName),
                                            new SqlParameter("@Remarks", Remark),
                                            new SqlParameter("@AddedBy", AddedBy)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("EwalletRequest", para);
            return ds;
        }
    }



    public class Password
    {
        public string FK_UserId { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public DataSet ChangePassword()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@OldPassword", OldPassword),
                                      new SqlParameter("@NewPassword", NewPassword),
                                       new SqlParameter("@UpdatedBy", FK_UserId)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("UserChangePassword", para);

            return ds;
        }
    }
    public class ActivateUser
    {
        public string ePinNo { get; set; }
        public string LoginId { get; set; }
        public DataSet ActivateUserByPin()
        {
            SqlParameter[] para = {

                                      new SqlParameter("@LoginId", LoginId),
                                      new SqlParameter("@EPinNo", ePinNo)
                                  };
            DataSet ds = DBHelper.ExecuteQuery("ActivateUserMobile", para);

            return ds;
        }
    }

    public class Reward
    {
        public string PK_RewardId { get; set; }
        public string Title { get; set; }
        public string Image { get; set; }
    }
    public class WalletRequestList
    {
        public string FK_UserId { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public DataSet GetEwalletRequestDetails()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",FK_UserId),
                                   new SqlParameter("@FromDate",FromDate),
                                   new SqlParameter("@ToDate",ToDate)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetEwalletRequestDetails", para);

            return ds;
        }
        public DataSet GetEWalletDetails()
        {
            SqlParameter[] para = {
                                      new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                     };

            DataSet ds = DBHelper.ExecuteQuery("GetEWalletDetails", para);
            return ds;
        }
        public DataSet GetTopUpDetails()
        {
            SqlParameter[] para = {
                                       new SqlParameter("@FK_UserId", FK_UserId),
                                      new SqlParameter("@FromDate", FromDate),
                                      new SqlParameter("@ToDate", ToDate)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("GetTopUpDetails", para);
            return ds;
        }
    }
    public class WalletResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<WalletDetails> lst { get; set; }
    }
    public class WalletDetails
    {
        public string RequestCode { get; set; }
        public string RequestID { get; set; }
        public string Amount { get; set; }
        public string BankBranch { get; set; }
        public string BankName { get; set; }
        public string ChequeDDDate { get; set; }
        public string ChequeDDNo { get; set; }
        public string DisplayName { get; set; }
        public string Fk_UserId { get; set; }
        public string FromDate { get; set; }
        public string LoginId { get; set; }
        public string PaymentMode { get; set; }
        public string Remark { get; set; }

        public string Status { get; set; }
        public string ToDate { get; set; }
        public string TransactionDate { get; set; }
        public string UserId { get; set; }
        public string WalletId { get; set; }
    }
    public class UserWalletAPI
    {
        public string Pk_EwalletId { get; set; }
        public string CrAmount { get; set; }
        public string DrAmount { get; set; }
        public string Balance { get; set; }
        public string Narration { get; set; }
        public string TransactionDate { get; set; }
    }
    public class UserWalletAPIResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string TotalCr { get; set; }
        public string TotalDr { get; set; }
        public string AvailableBalance { get; set; }
        public List<UserWalletAPI> lst { get; set; }
    }
    public class PinAPIRequest
    {
        public string FK_PackageId { get; set; }
        public string NoofPins { get; set; }
        public string FK_UserId { get; set; }
        public string FinalAmount { get; set; }
        public DataSet SaveEpinRequest()
        {
            SqlParameter[] para = {
                                  new SqlParameter("@FK_PackageId",FK_PackageId),
                                   new SqlParameter("@NoofPins",NoofPins),
                                   new SqlParameter("@FK_UserId",FK_UserId),
                                    new SqlParameter("@Amount",FinalAmount),
            };
            DataSet ds = DBHelper.ExecuteQuery("GenerateEPinByUser", para);
            return ds;
        }
    }
    public class TopUpModel
    {
        public string LoginId { get; set; }
        public string FK_UserId { get; set; }
        public string PackageId { get; set; }
        public string Amount { get; set; }
        public DataSet TopUp()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@LoginId", LoginId),
                                        new SqlParameter("@AddedBy", FK_UserId),
                                        new SqlParameter("@Fk_ProductId",PackageId),
                                        new SqlParameter("@Amount", Amount)
                                 };
            DataSet ds = DBHelper.ExecuteQuery("TopUp", para);
            return ds;
        }
    }
    public class TopUpListRes
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public List<TopUpListModel> lst { get; set; }
    }
    public class TopUpListModel
    {
        public string InvestmentId { get; set; }
        public string Name { get; set; }
        public string PinAmount { get; set; }
        public string UsedFor { get; set; }
        public string BV { get; set; }
        public string IsCalculated { get; set; }
        public string TransactionBy { get; set; }
        public string Status { get; set; }
        public string ROIPercentage { get; set; }
        public string TopUpDate { get; set; }
        public string ProductName { get; set; }
        public string PackageDays { get; set; }
    }
    public class DirectListAPI
    {
        public string Fk_UserId { get; set; }
        public string Fk_SponsorId { get; set; }
        public string LoginId { get; set; }
        public string FirstName { get; set; }
        public string ActiveStatus { get; set; }
        public string SponsorID { get; set; }
        public string Status { get; set; }
        public string SponsorName { get; set; }
        public string ActivationDate { get; set; }
        public string Lvl { get; set; }
    }
    public class DirectListAPIRes
    {
        public string Status { get; set; }
        public string Message { get; set; }
        public string LoginId { get; set; }
        public string FK_SponsorId { get; set; }
        public List<DirectListAPI> lst { get; set; }
    }
    public class RequestForDirect
    {
        public string Fk_UserId { get; set; }
        public string FK_RootId { get; set; }
        public DataSet GetDownlineTree()
        {
            SqlParameter[] para = {
                                        new SqlParameter("@Pk_UserId", Fk_UserId),
                                            new SqlParameter("@Fk_RootId", FK_RootId),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetAssociateDownlineTree", para);
            return ds;
        }
    }
}