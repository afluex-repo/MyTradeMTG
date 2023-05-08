using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyTradeMTG.Models
{
    public class Dashboard : Common
    {
        public string from1 { get; set; }
        public string to1 { get; set; }
        public string cssclass { get; set; }
        public List<Dashboard> lstmessages { get; set; }
        public string FK_UserId { get; set; }
        public string PK_UserId { get; set; }
        public string MemberName { get; set; }
        public string Message { get; set; }
        public string MessageTitle { get; set; }
        public string Pk_MessageId { get; set; }
        public string FirstName { get; set; }
        public string ProfilePicture{ get; set; }

        public string Image { get; set; }
        public string Title { get; set; }
        public List<Dashboard> lstReward { get; set; }
        public List<Dashboard> lstCustomer { get; set; }
        public string PK_RewardId { get; set; }


        public string FirmName { get; set; }
        public string Email { get; set; }
        public string Mobile { get; set; }
        public string AccountNo { get; set; }
        public string IFSCCode { get; set; }
        public string IsFranchise { get; set; }



        public string Addresss { get; set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", FK_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }

        public DataSet GetCustomerList()
        {
            SqlParameter[] para = {
                 new SqlParameter("@PK_UserId", AddedBy),
            };
            DataSet ds = DBHelper.ExecuteQuery("GetCustomerList", para);
            return ds;
        }


        public DataSet GetDashBoardDetails()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails");
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


        //public DataSet FranchiseRequest()
        //{
        //    SqlParameter[] para = {new SqlParameter("@FirmName",FirmName),
        //                            new SqlParameter("@Email",Email),
        //                            new SqlParameter("@Mobile",Mobile),
        //                               new SqlParameter("@BankName",BankName),
        //                            new SqlParameter("@BranchName",BranchName),
        //                               new SqlParameter("@AccountNo",AccountNo),
        //                            new SqlParameter("@IFSCCode",IFSCCode),
        //                               new SqlParameter("@Address",Address),
        //                                   new SqlParameter("@AddedBy",AddedBy)
        //    };
        //    DataSet ds = DBHelper.ExecuteQuery("FranchiseRequest", para);
        //    return ds;
        //}

        


    }


    public class ProgressReport
    {

        public string FK_UserId { get; set; }
        public string Year { get; set; }
        public string TotalBusiness { get; set; }
        public string Cramount { get; set; }
        public string Dramount { get; set; }
        public List<ProgressReport> lstCoin { get; set; }
        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",FK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetchartBarRunningData", para);
            return ds;
        }

        public DataSet GetlineChart()
        {
            SqlParameter[] para = {
                new SqlParameter("@Fk_UserId",FK_UserId)
            };
            DataSet ds = DBHelper.ExecuteQuery("GetlineChart", para);
            return ds;
        }





    }



}