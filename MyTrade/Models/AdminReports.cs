using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class AdminReports :Common
    {
       public List<AdminReports> lsttopupreport { get; set; }

        public string isBlocked { get; set; }

        public string Email { get;  set; }
        public string FromDate { get;  set; }
        public bool IsDownline { get;  set; }
        public string JoiningDate { get;  set; }
        public string LoginId { get;  set; }
        public List<AdminReports> lstassociate { get; set; }
        public string Mobile { get;  set; }
        public string Name { get;  set; }
        public string Password { get;  set; }
        public string SponsorId { get;  set; }
        public string SponsorName { get;  set; }
        public string Status { get;  set; }
        public string ToDate { get;  set; }
        public string ToLoginID { get;  set; }
        public string UpgradtionDate { get;  set; }
        public string Amount { get;  set; }
        public string TopupBy { get;  set; }
        public string PrintingDate { get;  set; }
        public string Description { get;  set; }
        public string PaymentMode { get;  set; }
        public string BusinessType { get;  set; }
        public string ReceiptNo { get;  set; }
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
                                    new SqlParameter("@Leg", Leg)
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
    }
}