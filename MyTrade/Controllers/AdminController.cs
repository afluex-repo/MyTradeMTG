using MyTrade.Filter;
using MyTrade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Mytrade.Models;
namespace MyTrade.Controllers
{
    public class AdminController : AdminBaseController
    {
        // GET: Admin
        public ActionResult AdminDashboard()
        {
            Dashboard newdata = new Dashboard();
            DataSet Ds = newdata.GetDashBoardDetails();

            ViewBag.TotalUsers = Ds.Tables[1].Rows[0]["TotalUsers"].ToString();
            ViewBag.BlockedUsers = Ds.Tables[1].Rows[0]["BlockedUsers"].ToString();
            ViewBag.InactiveUsers = Ds.Tables[1].Rows[0]["InactiveUsers"].ToString();
            ViewBag.ActiveUsers = Ds.Tables[1].Rows[0]["ActiveUsers"].ToString();
            if (Ds != null && Ds.Tables.Count > 0 && Ds.Tables[2].Rows.Count > 0)
            {
                ViewBag.Tr1Business = Ds.Tables[2].Rows[0]["Tr1Business"].ToString();
                ViewBag.Tr2Business = Ds.Tables[2].Rows[0]["Tr2Business"].ToString();
            }
            return View(newdata);
        }
        #region GeneratePin
        public ActionResult Generate_EPin()
        {
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }

            ViewBag.ddlProduct = ddlProduct;

            #endregion
            #region PaymentMode
            Common com = new Common();
            List<SelectListItem> ddlPayment = new List<SelectListItem>();
            DataSet ds = com.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                int paycount = 0;
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (paycount == 0)
                    {
                        ddlPayment.Add(new SelectListItem { Text = "Select Payment", Value = "0" });
                    }
                    ddlPayment.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    paycount++;
                }
            }

            ViewBag.ddlPayment = ddlPayment;

            #endregion
            return View();
        }
        [HttpPost]
        [ActionName("Generate_EPin")]
        [OnAction(ButtonName = "btn_Pin")]
        public ActionResult CreatePinAction(Admin obj)
        {
            try
            {
                obj.AddedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = obj.CreatePin();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["createpin"] = "Pin Created Successfully";
                    }
                    else
                    {
                        TempData["createpin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
                else { }

            }
            catch (Exception ex)
            {
                TempData["createpin"] = ex.Message;
            }
            return RedirectToAction("Generate_EPin", "Admin");
        }
        public ActionResult UnUsedPin(Admin obj)
        {

            List<Admin> lst = new List<Admin>();
            obj.Package = obj.Package == "0" ? null : obj.Package;
            DataSet ds = obj.GetUsedUnUsedPins();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {

                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Admin Objload = new Admin();
                    Objload.ePinNo = dr["ePinNo"].ToString();
                    Objload.Package = dr["Package"].ToString();
                    Objload.DisplayName = dr["PinUser"].ToString();
                    Objload.AddedOn = dr["CreatedDate"].ToString();
                    Objload.RegisteredTo = dr["RegisteredTo"].ToString();
                    Objload.Status = dr["PinStaus"].ToString();
                    lst.Add(Objload);
                }
                obj.lstunusedpins = lst;
            }
            #region Product Bind

            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProduct.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlProduct.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["Pk_ProductId"].ToString() });
                    count++;
                }
            }
            ViewBag.ddlProduct = ddlProduct;
            #endregion
            return View(obj);
        }
        public ActionResult FillAmount(string ProductId)
        {
            Admin obj = new Admin();
            obj.Package = ProductId;
            DataSet ds = obj.BindPriceByProduct();
            if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.Amount = ds.Tables[0].Rows[0]["ProductPrice"].ToString();
            }
            else { }
            return Json(obj, JsonRequestBehavior.AllowGet);

        }
        #endregion
        public ActionResult GetMemberName(string LoginId)
        {
            Common obj = new Common();
            obj.ReferBy = LoginId;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid LoginId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ChangePassword")]
        public ActionResult ChangePassword(Admin model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Password Changed  Successfully";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ChangePassword", "Admin");
        }


        public ActionResult PinTransferReportForAdmin()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPinTransfer = lst;
            }
            return View(model);
        }

        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("PinTransferReportForAdmin")]
        public ActionResult PinTransferReportForAdmin(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.ePinNo = model.ePinNo == "0" ? null : model.ePinNo;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.ToLoginID = model.ToLoginID == "0" ? null : model.ToLoginID;
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lstPinTransfer = lst;
            }
            return View(model);
        }


    }
}