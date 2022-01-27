using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class User:Common
    {
        #region property
        public string EPin { get; set; }
        public string PinStatus { get;  set; }
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
    }
}