using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTradeMTG.Models
{
    public class Franchise
    {


        public string FranchiseID { get; set; }


        public DataSet GetSalesRequestforFranchiseDashboard()
        {
            
            DataSet ds = DBHelper.ExecuteQuery("GetSalesRequestforFranchiseDashboard");
            return ds;

        }
    }
}