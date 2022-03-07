using MyTrade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Controllers
{
    public class MasterController : AdminBaseController
    {

        #region PackageMaster
        public ActionResult PackageList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.ProductList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.Packageid = r["Pk_ProductId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.ProductPrice = r["ProductPrice"].ToString();
                    obj.IGST = r["IGST"].ToString();
                    obj.CGST = r["CGST"].ToString();
                    obj.SGST = (r["SGST"].ToString());
                    obj.BinaryPercent = (r["BinaryPercent"].ToString());
                    obj.DirectPercent = (r["DirectPercent"].ToString());
                    obj.ROIPercent = (r["ROIPercent"].ToString());
                    obj.BV = (r["BV"].ToString());
                    obj.PackageTypeId = (r["PackageTypeId"].ToString());
                    obj.PackageTypeName = (r["PackageTypeName"].ToString());
                    obj.FromAmount = (r["FromAmount"].ToString());
                    obj.ToAmount = (r["ToAmount"].ToString());
                    lst.Add(obj);
                }
                model.lstpackage = lst;
            }
            return View(model);
        }

        public ActionResult DeletePackage(string id)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Master obj = new Master();
                obj.Packageid = id;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.DeleteProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Package"] = "Product deleted successfully";
                        FormName = "PackageList";
                        Controller = "Master";
                    }
                    else
                    {
                        TempData["Package"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "PackageList";
                        Controller = "Master";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Package"] = ex.Message;
                FormName = "PackageList";
                Controller = "Master";
            }

            return RedirectToAction(FormName, Controller);
        }

        public ActionResult PackageMaster(string PackageID)
        {
            Master obj = new Master();
            #region pacakgeTpe Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlPackageType = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindPackageType();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlPackageType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlPackageType.Add(new SelectListItem { Text = r["PackageTypeName"].ToString(), Value = r["Pk_PackageTypeId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlPackageType = ddlPackageType;

            #endregion
            if (PackageID != null)
            {

                try
                {
                    obj.Packageid = PackageID;

                    DataSet ds = obj.ProductList();
                    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                    {

                        obj.PackageTypeId = ds.Tables[0].Rows[0]["PackageTypeId"].ToString();
                        obj.Packageid = ds.Tables[0].Rows[0]["Pk_ProductId"].ToString();
                        obj.ProductName = ds.Tables[0].Rows[0]["ProductName"].ToString();
                        obj.ProductPrice = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
                        obj.IGST = ds.Tables[0].Rows[0]["IGST"].ToString();
                        obj.CGST = ds.Tables[0].Rows[0]["CGST"].ToString();
                        obj.SGST = ds.Tables[0].Rows[0]["SGST"].ToString();
                        obj.BinaryPercent = ds.Tables[0].Rows[0]["BinaryPercent"].ToString();
                        obj.DirectPercent = ds.Tables[0].Rows[0]["DirectPercent"].ToString();
                        obj.ROIPercent = ds.Tables[0].Rows[0]["ROIPercent"].ToString();
                        obj.BV = ds.Tables[0].Rows[0]["BV"].ToString();
                        obj.PackageTypeId = (ds.Tables[0].Rows[0]["PackageTypeId"].ToString());
                        obj.PackageTypeName = (ds.Tables[0].Rows[0]["PackageTypeName"].ToString());
                        obj.FromAmount = (ds.Tables[0].Rows[0]["FromAmount"].ToString());
                        obj.ToAmount = (ds.Tables[0].Rows[0]["ToAmount"].ToString());
                    }

                }
                catch (Exception ex)
                {
                    TempData["Package"] = ex.Message;
                }
                return View(obj);
            }
            else
            {
                return View(obj);
            }

        }

        public ActionResult SaveProduct(string PackageType, string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent, string BV, string FromAmount, string ToAmount)
        {
            Master obj = new Master();
            try
            {
                obj.PackageTypeId = PackageType;
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.FromAmount = FromAmount == "" ? null : FromAmount;
                obj.ToAmount = ToAmount == "" ? null : ToAmount;
                obj.AddedBy = Session["PK_AdminId"].ToString();

                DataSet ds = obj.SaveProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product saved successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult UpdateProduct(string PackageType, string Packageid, string ProductName, string ProductPrice, string IGST, string CGST, string SGST, string BinaryPercent, string DirectPercent, string ROIPercent, string BV, string FromAmount, string ToAmount)
        {
            Master obj = new Master();
            try
            {
                obj.PackageTypeId = PackageType;
                obj.Packageid = Packageid;
                obj.ProductName = ProductName;
                obj.ProductPrice = ProductPrice;
                obj.IGST = IGST;
                obj.CGST = CGST;
                obj.SGST = SGST;
                obj.BinaryPercent = BinaryPercent;
                obj.DirectPercent = DirectPercent;
                obj.ROIPercent = ROIPercent;
                obj.BV = BV;
                obj.UpdatedBy = Session["PK_AdminId"].ToString();
                obj.FromAmount = FromAmount == "" ? null : FromAmount;
                obj.ToAmount = ToAmount == "" ? null : ToAmount;
                DataSet ds = obj.UpdateProduct();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        obj.Result = "Product updated successfully";
                    }
                    else
                    {
                        obj.Result = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }

            }
            catch (Exception ex)
            {
                obj.Result = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion


        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ActionName("Upload")]
        public ActionResult Upload(Master model, HttpPostedFileBase postedFile)
        {
            try
            {
                if (postedFile != null)
                {
                    model.Image = "../UploadReward/" + Guid.NewGuid() + Path.GetExtension(postedFile.FileName);
                    postedFile.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                model.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = model.Upload();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Upload"] = "File upload successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Upload"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Upload"] = ex.Message;
            }
            return RedirectToAction("Upload", "Master");

        }

    
        public ActionResult UploadList()
        {
            Master model = new Master();
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRewarDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("UploadList")]
        public ActionResult UploadList(Master model)
        {
            List<Master> lst = new List<Master>();
            DataSet ds = model.GetRewarDetails();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Master obj = new Master();
                    obj.PK_RewardId = r["PK_RewardId"].ToString();
                    obj.Title = r["Title"].ToString();
                    obj.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj);
                }
                model.lstReward = lst;
            }
            return View(model);
        }


    }
}