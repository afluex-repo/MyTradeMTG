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
            DataSet ds = DBHelper.ExecuteQuery("GetMemberName", para);

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
        public string ActiveStatus { get; set; }
        public string Status { get; set; }
        public string Message { get; set; }
        public string ReferralLink { get; set; }
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
        public decimal MinimumAmount { get; set; }
        public decimal MaximumAmount { get; set; }
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
    }
    public class DirectRequest
    {
        public string LoginId { get; set; }
        public string Name { get; set; }
        public string FromDate { get; set; }
        public string ToDate { get; set; }
        public string FromActivationDate { get; set; }
        public string ToActivationDate { get; set; }
        public string Leg { get; set; }
        public string ActivationStatus { get; set; }
        public DataSet GetDirectList()
        {

            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@FromActivationDate", FromActivationDate),
                                    new SqlParameter("@ToActivationDate", ToActivationDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", ActivationStatus),
                                  };
            DataSet ds = DBHelper.ExecuteQuery("GetDirectList", para);
            return ds;
        }
        public DataSet GetDownlineList()
        {
            SqlParameter[] para = { new SqlParameter("@LoginId", LoginId),
                                    new SqlParameter("@Name", Name),
                                    new SqlParameter("@FromDate", FromDate),
                                    new SqlParameter("@ToDate", ToDate),
                                    new SqlParameter("@Leg", Leg),
                                    new SqlParameter("@Status", ActivationStatus), };
            DataSet ds = DBHelper.ExecuteQuery("DownlineList", para);
            return ds;
        }
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
        public string ePinNo { get;  set; }
        public string FromId { get;  set; }
        public string FromName { get;  set; }
        public string ToId { get;  set; }
        public string ToName { get;  set; }
        public string TransferDate { get;  set; }
    }
}