using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MyTrade.Models
{
    public class Dashboard :Common
    {
        public string cssclass { get; set; }
        public List<Dashboard> lstmessages { get; set; }
        public string FK_UserId { get; set; }
        public string MemberName { get;  set; }
        public string Message { get;  set; }
        public string MessageTitle { get;  set; }
        public string Pk_MessageId { get;  set; }

        public DataSet GetAssociateDashboard()
        {
            SqlParameter[] para = { new SqlParameter("@Fk_UserId", FK_UserId), };
            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetailsForAssociate", para);
            return ds;
        }
        public DataSet GetDashBoardDetails()
        {

            DataSet ds = DBHelper.ExecuteQuery("GetDashBoardDetails");
            return ds;
        }
    }
}