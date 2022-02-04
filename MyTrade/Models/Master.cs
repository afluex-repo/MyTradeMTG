using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MyTrade.Models
{
    public class Master:Common
    {
        public List<Master> lstpackage { get; set; }

        public string BinaryPercent { get;  set; }
        public string BV { get;  set; }
        public string CGST { get;  set; }
        public string DirectPercent { get;  set; }
        public string IGST { get;  set; }
        public string Packageid { get;  set; }
        public string ProductName { get;  set; }
        public string ProductPrice { get;  set; }
        public string ROIPercent { get;  set; }
        public string SGST { get;  set; }
        public string PackageTypeId { get; set; }
        public string ToAmount { get; set; }
        public string FromAmount { get; set; }
        public string PackageTypeName { get; set; }
        #region ProductMaster

        public DataSet SaveProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV",BV),
                                  new SqlParameter("@AddedBy", AddedBy),
                                  new SqlParameter("@PackageTypeId", PackageTypeId),
                                   new SqlParameter("@FromAmount", FromAmount),
                                    new SqlParameter("@ToAmount", ToAmount)
            };

        DataSet ds = DBHelper.ExecuteQuery("AddProduct", para);
            return ds;
        }
        public DataSet ProductList()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid) };
            DataSet ds = DBHelper.ExecuteQuery("ProductList", para);
            return ds;
        }
        public DataSet DeleteProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid),
                                  new SqlParameter("@DeletedBy", AddedBy),};

            DataSet ds = DBHelper.ExecuteQuery("DeleteProduct", para);
            return ds;
        }

        public DataSet UpdateProduct()
        {
            SqlParameter[] para = { new SqlParameter("@ProductID", Packageid),
                                  new SqlParameter("@ProductName", ProductName),
                                  new SqlParameter("@ProductPrice", ProductPrice),
                                  new SqlParameter("@IGST", IGST),
                                  new SqlParameter("@CGST", CGST),
                                  new SqlParameter("@SGST", SGST),
                                  new SqlParameter("@BinaryPercent", BinaryPercent),
                                  new SqlParameter("@DirectPercent", DirectPercent),
                                  new SqlParameter("@ROIPercent", ROIPercent),
                                  new SqlParameter("@BV", BV),
                                  new SqlParameter("@UpdatedBy", UpdatedBy),
                                     new SqlParameter("@PackageTypeId", PackageTypeId),
                                   new SqlParameter("@FromAmount", FromAmount),
                                    new SqlParameter("@ToAmount", ToAmount)
            };

            DataSet ds = DBHelper.ExecuteQuery("UpdateProduct", para);
            return ds;
        }

        #endregion
    }
}