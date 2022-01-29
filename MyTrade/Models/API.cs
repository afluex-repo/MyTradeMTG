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
        public string Leg { get; set; }
        public string Password { get; set; }
        public string RegistrationBy { get; set; }
        public string SponsorId { get; set; }
        public string MobileNo { get; set; }
        public string FirstName { get; set; }
        public string FullName { get; set; }
        public string LoginId { get; set; }
        public string TransPassword { get; set; }
        public string LastName { get; set; }
        public string PinCode { get; set; }
         public DataSet Registration()
        {
            SqlParameter[] para = {

                                   new SqlParameter("@SponsorId",SponsorId),
                                   new SqlParameter("@MobileNo",MobileNo),
                                   new SqlParameter("@FirstName",FirstName),
                                   new SqlParameter("@LastName",LastName),
                                    new SqlParameter("@RegistrationBy",RegistrationBy),
                                     new SqlParameter("@PinCode",PinCode),
                                     new SqlParameter("@Leg",Leg),
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
        public string State { get;  set; }
        public string City { get;  set; }
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
        public string Status { get; set; }
        public string Message { get; set; }
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
        public string Fk_UserId { get;  set; }
        public string LoginId { get;  set; }
        public string Leg { get;  set; }
    }
    public class TopupByUser
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
    public class TopupResponse
    {
        public string Status { get; set; }
        public string Message { get; set; }
    }
}