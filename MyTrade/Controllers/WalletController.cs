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
            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = Common.BindPaymentType();
            ViewBag.ddlpaymentType = ddlpaymentType;
            #endregion

            UserWallet obj = new UserWallet();
            obj.LoginId = Session["LoginId"].ToString();
            obj.BankBranch = Session["Branch"].ToString();
            obj.BankName = Session["Bank"].ToString();

            #region Check Balance
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = obj.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion

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


            obj.PaymentType = "Offline";
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

                if (model.PaymentMode == "1")
                {
                    model.BankName = null;
                    model.BankBranch = null;
                }
                DataSet ds = model.SaveEwalletRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Requested successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("AddWallet", "Wallet");
        }
        public ActionResult ROIWallet()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ROIWallet")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ROIWallet(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetROIWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.RoiWalletId = r["Pk_ROIWalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTps = lst;
            }
            return View(model);

        }

        public ActionResult ROIIncomeReports()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIIncomeReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROIId = r["Pk_ROIId"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TopUpAmount = r["TopUpAmount"].ToString();
                    obj.Date = r["TopUpDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROIIncome = lst;
                ViewBag.TopUpAmount = double.Parse(ds.Tables[0].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("ROIIncomeReports")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ROIIncomeReports(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetROIIncomeReportsDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROIId = r["Pk_ROIId"].ToString();
                    obj.Pk_InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.TopUpAmount = r["TopUpAmount"].ToString();
                    obj.Date = r["TopUpDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROIIncome = lst;
                ViewBag.TopUpAmount = double.Parse(ds.Tables[0].Compute("sum(TopUpAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        public ActionResult ViewROI(string InvId)
        {
            UserWallet model = new UserWallet();
            model.Pk_InvestmentId = InvId;
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetROIDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.ROI = r["Pk_ROIId"].ToString();
                    obj.ROI = r["ROI"].ToString();
                    obj.Date = r["ROIDate"].ToString();
                    lst.Add(obj);
                }
                model.lstROI = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.ReceivedAmount = ds.Tables[1].Rows[0]["ReceivedAmount"].ToString();
                ViewBag.TotalAmount = ds.Tables[1].Rows[0]["TotalAmount"].ToString();
                ViewBag.BalanceAmount = ds.Tables[1].Rows[0]["BalanceAmount"].ToString();
            }
            return View(model);
        }
        public ActionResult WalletList()
        {
            Admin model = new Admin();
            List<Admin> lst = new List<Admin>();
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetEwalletRequestDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Admin obj = new Admin();
                    obj.RequestID = r["PK_RequestID"].ToString();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.RequestCode = r["RequestCode"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.TransactionDate = r["RequestedDate"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.ChequeDDNo = r["ChequeDDNo"].ToString();
                    obj.ChequeDDDate = r["ChequeDDDate"].ToString();
                    obj.WalletId = r["WalletId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.DisplayName = r["Name"].ToString();
                    obj.Remark = r["Remark"].ToString();
                    lst.Add(obj);
                }
                model.lstWallet = lst;
            }
            return View(model);
        }
        public ActionResult WalletLedgerList()
        {
            UserWallet model = new UserWallet();
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = model.GetEWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.Pk_EwalletId = r["Pk_EwalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.Balance = r["Balance"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
                ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.Available = double.Parse(ds.Tables[0].Compute("sum(CrAmount)-sum(DrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("WalletLedgerList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult WalletLedgerList(UserWallet model)
        {
            List<UserWallet> lst = new List<UserWallet>();
            model.FK_UserId = Session["Pk_UserId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetEWalletDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    UserWallet obj = new UserWallet();
                    obj.Pk_EwalletId = r["Pk_EwalletId"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.Narration = r["Narration"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    //obj.Balance = r["Balance"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
                ViewBag.TotalCrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.TotalDrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.Available = double.Parse(ds.Tables[0].Compute("sum(CrAmount)-sum(DrAmount)", "").ToString()).ToString("n2");
            }
            return View(model);
        }

        public ActionResult DeleteWallet(string id)
        {
            try
            {
                UserWallet model = new UserWallet();
                model.AddedBy = Session["Pk_UserId"].ToString();
                model.PK_RequestID = id;
                DataSet ds = model.DeleteWallet();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Wallet requested deleted successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("WalletList", "Wallet");
        }
    }
}