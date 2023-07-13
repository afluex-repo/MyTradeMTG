using MyTradeMTG.Filter;
using MyTradeMTG.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace MyTradeMTG.Controllers
{
    public class FranchiseController : FranchiseBaseController
    {
        // GET: Franchise
        public ActionResult FranchiseDashBoard()
        {
            Franchise obj = new Franchise();
            DataSet ds = obj.GetSalesRequestforFranchiseDashboard();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.TotalSaleRequest = ds.Tables[0].Rows[0]["TotalSaleRequest"].ToString();
                ViewBag.TotalApproved = ds.Tables[0].Rows[0]["TotalApproved"].ToString();
                ViewBag.TotalRejected = ds.Tables[0].Rows[0]["TotalRejected"].ToString();
                ViewBag.TotalPending = ds.Tables[0].Rows[0]["TotalPending"].ToString();
            }
            return View(obj);
        }
        public JsonResult GetChartFranchiseSaleRequest()
        {
            ProgressReport model = new ProgressReport();
            try
            {
                List<ProgressReport> lst = new List<ProgressReport>();
                DataSet ds = model.GetChartFranchiseSaleRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        ProgressReport obj = new ProgressReport();
                        obj.Year = r["Year"].ToString();
                        obj.TotalSaleRequest = r["TotalSaleRequest"].ToString();
                        obj.TotalApproved = r["TotalApproved"].ToString();
                        obj.TotalRejected = r["TotalRejected"].ToString();
                        obj.TotalPending = r["TotalPending"].ToString();
                        lst.Add(obj);
                    }
                    model.lstSaleRequestMTG = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return Json(model.lstSaleRequestMTG, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AddWallet()
        {
            UserWallet obj = new UserWallet();
            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = new List<SelectListItem>();
            DataSet dsp = obj.GetPaymentType();
            int count1 = 0;
            if (dsp != null && dsp.Tables.Count > 0 && dsp.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dsp.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentType.Add(new SelectListItem { Text = "Select", Value = "0" });
                    }
                    ddlpaymentType.Add(new SelectListItem { Text = r["PaymentType"].ToString(), Value = r["PK_PaymentTypeId"].ToString() });
                    count1 = count1 + 1;
                }
            }
            ViewBag.ddlpaymentType = ddlpaymentType;
            #endregion

            obj.Country = Session["Country"].ToString();
            obj.LoginId = Session["LoginId"].ToString();
            //if (Session["IdActivated"].ToString() == "true")
            //{
            obj.BankBranch = Session["Branch"].ToString();
            obj.BankName = Session["Bank"].ToString();
            //}

            #region Check Balance
            obj.FK_UserId = Session["Pk_UserId"].ToString();
            obj.CurrencyName = Session["CurrencyName"].ToString();
            obj.CurrencySymbol = Session["CurrencySymbol"].ToString();
            obj.ISOcode = Session["ISOcode"].ToString();
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




            #region QR Code File
            DataSet ds11 = obj.GetActiveQRCodeDetails();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                ViewBag.QRCodeURL = ds11.Tables[0].Rows[0]["QRCodeURL"].ToString();
            }
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
                if (model.PaymentType == "2")
                {
                    DataSet ds = model.SaveEwalletRequest();
                    if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                    {
                        if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            TempData["msg"] = "Requested Successfully.";
                        }
                        else
                        {
                            TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        }
                    }
                    else { }
                }
                else if (model.PaymentType == "1")
                {
                    OrderModel orderModel = new OrderModel();
                    string random = Common.GenerateRandom();
                    CreateOrderResponse obj1 = new CreateOrderResponse();
                    try
                    {
                        decimal amount = Convert.ToDecimal(model.Amount) * 100;
                        Dictionary<string, object> options = new Dictionary<string, object>();
                        options.Add("amount", Convert.ToInt32(amount)); // amount in the smallest currency unit
                        options.Add("receipt", random);
                        options.Add("currency", "INR");
                        options.Add("payment_capture", "1");

                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                        RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                        Razorpay.Api.Order order = client.Order.Create(options);
                        obj1.OrderId = order["id"].ToString();
                        obj1.Status = "0";
                        model.OrderId = order["id"].ToString();
                        model.LoginId = Session["LoginId"].ToString();
                        model.AddedBy = Session["Pk_UserId"].ToString();
                        model.PaymentType = "Online";
                        model.PaymentMode = "12";
                        model.Amount = amount.ToString();
                        orderModel.orderId = order.Attributes["id"];
                        orderModel.razorpayKey = "rzp_live_k8z9ufVw0R0MLV";
                        orderModel.amount = Convert.ToInt32(amount);
                        orderModel.currency = "INR";
                        orderModel.description = "Recharge Wallet";
                        orderModel.name = Session["FullName"].ToString();
                        orderModel.contactNumber = Session["Contact"].ToString();
                        orderModel.email = Session["Email"].ToString();
                        orderModel.image = "http://MyTradeMTG.co.in/MyTradeMTGWebsite/assets/img/logo.png";
                        DataSet ds = model.SaveEwalletRequestNew();
                        return View("PaymentPage", orderModel);
                        // Return on PaymentPage with Order data
                    }
                    catch (Exception ex)
                    {
                        obj1.Status = "1";
                        TempData["error"] = ex.Message;
                        return RedirectToAction("AddWallet", "Franchise");
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("AddWallet", "Franchise");
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
        public ActionResult SaleRequest(User model)
        {
            #region ddlpaymentmode

            int count1 = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds2 = model.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion

            Common objcomm = new Common();
            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds1 = objcomm.GetWalletBalance();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds1.Tables[0].Rows[0]["Amount"].ToString();
            }
            #endregion

            List<User> lst = new List<User>();
            model.FK_FranchiseUserId = Session["PK_UserId"].ToString();
            DataSet ds = model.GetSaleRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();

                    obj.Pk_FranchisetransferId = r["pk_franchisetransferid"].ToString();
                    obj.Fk_UserId = r["fk_userid"].ToString();
                    obj.UserContactAddressId = r["UserContactAddress"].ToString();
                    obj.UserName = r["Username"].ToString();
                    obj.MTGToken = r["mtgtoken"].ToString();
                    obj.TransferCharge = r["TransferCharge"].ToString();
                    obj.SalesDate = r["SaleRequestDate"].ToString();
                    obj.Status = r["Status"].ToString();

                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.AccountNo = r["MemberAccNo"].ToString();
                    obj.UPIID = r["UPIID"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    lst.Add(obj);
                }
                model.lstSaleRequest = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("SaleRequest")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult SearchSaleRequest(User model)
        {
            #region ddlpaymentmode

            int count1 = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds2 = model.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "0" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

            #endregion

            Common objcomm = new Common();
            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds1 = objcomm.GetWalletBalance();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds1.Tables[0].Rows[0]["Amount"].ToString();
            }
            #endregion

            List<User> lst = new List<User>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.FK_FranchiseUserId = Session["PK_UserId"].ToString();
            DataSet ds = model.GetSaleRequest();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();

                    obj.Pk_FranchisetransferId = r["pk_franchisetransferid"].ToString();
                    obj.Fk_UserId = r["fk_userid"].ToString();
                    obj.UserContactAddressId = r["UserContactAddress"].ToString();
                    obj.UserName = r["Username"].ToString();
                    obj.MTGToken = r["mtgtoken"].ToString();
                    obj.TransferCharge = r["TransferCharge"].ToString();
                    obj.SalesDate = r["SaleRequestDate"].ToString();
                    obj.Status = r["Status"].ToString();

                    obj.BankName = r["MemberBankName"].ToString();
                    obj.BranchName = r["MemberBranch"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.AccountNo = r["MemberAccNo"].ToString();
                    obj.UPIID = r["UPIID"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    lst.Add(obj);
                }
                model.lstSaleRequest = lst;
            }
            return View(model);
        }
        public ActionResult WalletTransfer()
        {
            Common objcomm = new Common();
            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = objcomm.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["Amount"].ToString();
            }
            #endregion


            #region GetDirectPayment
            DataSet ds1 = objcomm.GetWalletTransferCharge();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                Session["MemberTransferCharge"] = ds1.Tables[0].Rows[0]["MemberTransferCharge"].ToString();
            }
            #endregion



            User model = new User();
            List<User> lst = new List<User>();
            model.Fk_UserId = Session["PK_UserId"].ToString();
            DataSet ds11 = model.GetWalletTransfer();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    User obj1 = new User();
                    obj1.TransferDate = r["TransferDate"].ToString();
                    obj1.TransferFromName = r["TransferFromName"].ToString();
                    obj1.TransfertoName = r["TransfertoName"].ToString();
                    obj1.MTG = r["MTG"].ToString();
                    obj1.LoginId = r["loginid"].ToString();
                    obj1.CustomerId = r["CustomerId"].ToString();
                    obj1.TransferCharge = r["TransferCharge"].ToString();
                    obj1.TotalMTG = r["TotalMTG"].ToString();
                    lst.Add(obj1);
                }
                model.QuickSendMTGList = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("WalletTransfer")]
        [OnAction(ButtonName = "btnTransfer")]
        public ActionResult WalletTransfer(User model)
        {
            try
            {
                model.AddedBy = Session["PK_UserId"].ToString();
                DataSet ds = model.SaveWalletTransferBalance();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["wallettransfer"] = "Transferred  successfully";
                    }
                    else
                    {
                        TempData["wallettransfererror"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["wallettransfererror"] = ex.Message;
            }
            return RedirectToAction("WalletTransfer", "Franchise");
        }



    }
}