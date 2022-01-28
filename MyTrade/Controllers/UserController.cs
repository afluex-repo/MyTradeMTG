using Mytrade.Controllers;
using MyTrade.Filter;
using MyTrade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Controllers
{
    public class UserController : UserBaseController
    {
        // GET: User
        public ActionResult UserDashBoard()
        {
            Dashboard obj = new Dashboard();
            List<Dashboard> lstinvestment = new List<Dashboard>();
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetAssociateDashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                ViewBag.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
            }
            return View(obj);
        }
        public ActionResult ActivateByPin(User model)
        {
            return View(model);
        }
        [HttpPost]
        [ActionName("ActivateByPin")]
        [OnAction(ButtonName = "btn_Pin")]
        public ActionResult ActivateUser(User obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.Fk_UserId = Session["Pk_UserId"].ToString();
                DataSet ds = obj.ActivateUser();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Activated"] = "User Activated Successfully";
                        FormName = "UserDashboard";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["Activated"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CompleteRegistration";
                        Controller = "Home";
                    }

                }
                else {
                    FormName = "CompleteRegistration";
                    Controller = "Home";
                }

            }
            catch (Exception ex)
            {
                TempData["Activated"] = ex.Message;
                FormName = "CompleteRegistration";
                Controller = "Home";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ValidatePin(string EPin)
        {
            User obj = new User();
            obj.EPin = EPin;
            obj.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.ValidateEpin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.PinStatus = ds.Tables[0].Rows[0]["PinStatus"].ToString();
                obj.Result = "Yes";
            }
            else { obj.Result = "Invalid Epin or Already Used"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TopUp()
        {
            Account model = new Account();
            model.LoginId = Session["LoginId"].ToString();
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForTopUp();
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
            return View(model);
        }
        [HttpPost]
        public ActionResult TopUp(Account obj)
        {
            try
            {
                obj.LoginId = Session["LoginId"].ToString();
                obj.AddedBy = Session["Pk_userId"].ToString();
                obj.TopUpDate = Common.ConvertToSystemDate(obj.TopUpDate, "mm/dd/yyyy");
                obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "mm/dd/yyyy");
                obj.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = obj.TopUp();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Topup"] = "TopUp Done successfully";
                    }
                    else
                    {
                        TempData["Topup"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["Topup"] = ex.Message;
            }
            return RedirectToAction("Topup", "User");
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
        public ActionResult BinaryTree()
        {
            ViewBag.Fk_UserId = Session["Pk_UserId"].ToString();
            return View();
        }
        public ActionResult GetUserList()
        {
            Profile obj = new Profile();
            List<Profile> lst = new List<Profile>();
            obj.LoginId = Session["LoginId"].ToString();
            DataSet ds = obj.GettingUserProfile();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Profile objList = new Profile();
                    objList.UserName = dr["Fullname"].ToString();
                    objList.LoginIDD = dr["LoginId"].ToString();
                    lst.Add(objList);
                }
            }
            return Json(lst, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinList()
        {
            Pin model = new Pin();
            List<Pin> lst = new List<Pin>();
            model.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        public ActionResult Tree()
        {
            return View();
        }
    }
}