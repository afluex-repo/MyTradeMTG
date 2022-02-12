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
    public class WalletController : UserBaseController
    {
        // GET: Wallet
        public ActionResult Index()
        {
            return View();
        }

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
            else { obj.Result = "No"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }

        public ActionResult AddWallet()
        {
            #region ddlpaymentmode
            //List<SelectListItem> ddlpaymentmode = Common.BindPaymentMode();
            //ViewBag.ddlpaymentmode = ddlpaymentmode;
            #endregion
            UserWallet obj = new UserWallet();
            obj.LoginId = Session["LoginId"].ToString();
            
            #region ddlpaymentmode

            int count = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds1 = obj.GetPaymentMode();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion

            return View(obj);
        }

        [HttpPost]
        [ActionName("AddWallet")]
        [OnAction(ButtonName = "Save")]
        public ActionResult AddWallet(UserWallet model)
        {
            try
            {
                model.DDChequeDate = string.IsNullOrEmpty(model.DDChequeDate) ? null : Common.ConvertToSystemDate(model.DDChequeDate, "dd/mm/yyyy");
                model.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = model.SaveEwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Wallet"] = "Requested successfully";
                    }
                    else
                    {
                        TempData["Wallet"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["Wallet"] = ex.Message;
            }
            return RedirectToAction("AddWallet", "Wallet");
        }

    }
}