using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
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

        public string NomineeName { get; set; }
        public string NomineeAge { get; set; }
        public string NomineeRelation { get; set; }


        public decimal Amount { get; set; }
        public string NoofPins { get; set; }
        public string FinalAmount { get; set; }
        public string BankBranch { get; set; }
        public string PK_RequestID { get; set; }
        public string Name { get; set; }
        public string LoginId { get; set; }
        public string ProductName { get; set; }
        public string IsVerified { get; set; }
        public List<User> lstEpinRequest { get; set; }




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
                                      new SqlParameter("@UpdatedBy",Fk_UserId)
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
                                   new SqlParameter("@Amount",Amount),
                                   new SqlParameter("@PaymentMode",Fk_Paymentid),
                                   new SqlParameter("@BankName",BankName),
                                   new SqlParameter("@BankBranch",BranchName),
                                   new SqlParameter("@ChequeDDNo",TransactionNo),
                                   new SqlParameter("@ChequeDDDate",TransactionDate),
                                      new SqlParameter("@AddedBy",AddedBy)

            };
            DataSet ds = DBHelper.ExecuteQuery("SaveEpinRequest", para);
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
        
        public DataSet GetPinList()
        {
            SqlParameter[] para = {
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