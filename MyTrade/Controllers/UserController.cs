using Mytrade.Controllers;
using MyTrade.Filter;
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
                ViewBag.TotalBusiness = ds.Tables[0].Rows[0]["TotalBusiness"].ToString();
                ViewBag.TeamBusiness = ds.Tables[0].Rows[0]["TeamBusiness"].ToString();
                ViewBag.SelfBusiness = ds.Tables[0].Rows[0]["SelfBusiness"].ToString();
                ViewBag.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                ViewBag.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                ViewBag.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                ViewBag.TPSId = ds.Tables[0].Rows[0]["TPSId"].ToString();
                ViewBag.TotalBlocked = ds.Tables[0].Rows[0]["TotalBlocked"].ToString();
                ViewBag.TotalROI = ds.Tables[0].Rows[0]["TotalROIWalletAmount"].ToString();
                ViewBag.TotalPayoutWallet = ds.Tables[0].Rows[0]["TotalPayoutWalletAmount"].ToString();
                ViewBag.TotalWalletAmount = ds.Tables[0].Rows[0]["TotalWalletAmount"].ToString();
                ViewBag.TotalTeam = ds.Tables[0].Rows[0]["TotalTeam"].ToString();
                ViewBag.TotalTeamActive = ds.Tables[0].Rows[0]["TotalTeamActive"].ToString();
                ViewBag.TotalTeamInActive = ds.Tables[0].Rows[0]["TotalTeamInActive"].ToString();
                ViewBag.TotalTeamTPSId = ds.Tables[0].Rows[0]["TotalTeamTPSId"].ToString();
                ViewBag.TotalIncome = Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"])+ Convert.ToDecimal(ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"]);
                ViewBag.LevelIncomeTr1 = ds.Tables[0].Rows[0]["TotalLevelIncomeTTP"].ToString();
                ViewBag.LevelIncomeTr2 = ds.Tables[0].Rows[0]["TotalLevelIncomeTPS"].ToString();
                ViewBag.LevelIncomeTR1ForPayout = ds.Tables[0].Rows[0]["LevelIncomeTR1ForPayout"].ToString();
                ViewBag.LevelIncomeTR2ForPayout = ds.Tables[0].Rows[0]["LevelIncomeTR2ForPayout"].ToString();
                ViewBag.TotalPayout = ds.Tables[0].Rows[0]["TotalPayout"].ToString();
                ViewBag.UsedPins = ds.Tables[0].Rows[0]["UsedPins"].ToString();
                ViewBag.AvailablePins = ds.Tables[0].Rows[0]["AvailablePins"].ToString();
                ViewBag.TotalPins = ds.Tables[0].Rows[0]["TotalPins"].ToString();
                ViewBag.Status = ds.Tables[2].Rows[0]["Status"].ToString();
                if (ViewBag.Status == "InActive")
                {
                    return RedirectToAction("ActivateByPin", "User");
                }
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.Tr1Business = ds.Tables[1].Rows[0]["Tr1Business"].ToString();
                if (ViewBag.Tr1Business == "")
                {
                    ViewBag.Tr1Business = 0;
                }
                ViewBag.Tr2Business = ds.Tables[1].Rows[0]["Tr2Business"].ToString();
            }

            
         
            List<Dashboard> lst = new List<Dashboard>();
            obj.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds1 = obj.GetRewarDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Dashboard obj1 = new Dashboard();
                    obj1.PK_RewardId = r["PK_RewardId"].ToString();
                    obj1.Title = r["Title"].ToString();
                    obj1.Image = "/UploadReward/" + r["postedFile"].ToString();
                    lst.Add(obj1);
                }
                obj.lstReward = lst;
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
                        string Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        if (Email != null && Email != "")
                        {
                            try
                            {
                                BLMail.SendActivationMail(Session["FullName"].ToString(), Session["LoginId"].ToString(), Crypto.Decrypt(Session["Password"].ToString()), "Activation Successful", Email);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
                        TempData["Activated"] = "User Activated Successfully";
                        FormName = "ConfirmActivation";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "ActivateByPin";
                        Controller = "User";
                    }

                }
                else
                {
                    FormName = "ActivateByPin";
                    Controller = "User";
                }

            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                FormName = "ActivateByPin";
                Controller = "User";
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
            model.BankName = Session["Bank"].ToString();
            model.BankBranch = Session["Branch"].ToString();
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForTopUp();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                int count = 0;
                ViewBag.FromAmount = ds1.Tables[0].Rows[0]["FromAmount"].ToString();
                ViewBag.ToAmount = ds1.Tables[0].Rows[0]["ToAmount"].ToString();
                ViewBag.InMultipleOf = ds1.Tables[0].Rows[0]["InMultipleOf"].ToString();
                ViewBag.ROIPercent = ds1.Tables[0].Rows[0]["ROIPercent"].ToString();
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
            #region Check Balance
            objcomm.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet ds = objcomm.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion
            #region ddlpaymentType
            List<SelectListItem> ddlpaymentType = Common.BindPaymentType();
            ViewBag.ddlpaymentType = ddlpaymentType;
            #endregion
            #region ddlpaymentmode
            UserWallet obj = new UserWallet();
            int count1 = 0;
            List<SelectListItem> ddlpaymentmode = new List<SelectListItem>();
            DataSet ds2 = obj.GetPaymentMode();
            if (ds2 != null && ds2.Tables.Count > 0 && ds2.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds2.Tables[0].Rows)
                {
                    if (count1 == 0)
                    {
                        ddlpaymentmode.Add(new SelectListItem { Text = "Select Payment Mode", Value = "" });
                    }
                    ddlpaymentmode.Add(new SelectListItem { Text = r["PaymentMode"].ToString(), Value = r["PK_paymentID"].ToString() });
                    count1 = count1 + 1;
                }
            }

            ViewBag.ddlpaymentmode = ddlpaymentmode;

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
                //  obj.TopUpDate = string.IsNullOrEmpty(obj.TopUpDate) ? null : Common.ConvertToSystemDate(obj.TopUpDate, "dd/mm/yyyy");
                //obj.TransactionDate = string.IsNullOrEmpty(obj.TransactionDate) ? null : Common.ConvertToSystemDate(obj.TransactionDate, "dd/mm/yyyy");
                obj.FK_UserId = Session["Pk_userId"].ToString();
                DataSet ds = obj.TopUp();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Top-Up Done successfully";
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
            model.PinStatus = (model.PinStatus = "T");
            model.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    obj.Amount = r["TotalAmount"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    obj.PinGenerationDate = r["PinGenerationDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.GST = r["IGST"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btnSearch")]
        [ActionName("PinList")]
        public ActionResult PinList(Pin model)
        {
            List<Pin> lst = new List<Pin>();
            model.FK_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["ePinNo"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PinStatus = r["PinStatus"].ToString();
                    obj.RegisteredTo = r["RegisteredTo"].ToString();
                    obj.Amount = r["TotalAmount"].ToString();
                    //obj.IsRegistered = r["IsRegistered"].ToString();
                    obj.PinGenerationDate = r["PinGenerationDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.GST = r["IGST"].ToString();

                    lst.Add(obj);
                }
                model.lst = lst;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "btnMutipleTranser")]
        [ActionName("PinList")]
        public ActionResult PinListTransfer(Pin model)
        {
            model.ParentLoginId = Session["LoginId"].ToString();

            if (model.LoginId != model.ParentLoginId)
            {
                try
                {
                    string hdrows = Request["hdRows"].Count().ToString();
                    string chkselect = "";
                    for (int i = 1; i < int.Parse(hdrows); i++)
                    {
                        try
                        {
                            chkselect = Request["chkSelect_ " + i];
                            if (chkselect == "on")
                            {
                                model.ePinNo = Request["ePinNo_ " + i].ToString();
                                DataSet ds = model.ePinTransfer();
                                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                                {
                                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                                    {
                                        TempData["msg"] = "Transferred Successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                    {
                                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                            }
                        }
                        catch { chkselect = "0"; }
                    }
                }
                catch (Exception ex)
                {
                    TempData["error"] = ex.Message;
                }
            }
            else
            {
                TempData["error"] = "You Can't transfer on the same Id";
            }
            return RedirectToAction("PinList");
        }
        public ActionResult Tree(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.PK_UserId = Session["Pk_UserId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                model.DisplayName = Session["FullName"].ToString();
            }
            List<TreeMembers> lst = new List<TreeMembers>();
            List<MemberDetails> lstMember = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembersCount();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TreeMembers obj = new TreeMembers();
                    obj.LevelName = r["LevelNo"].ToString();
                    obj.NumberOfMembers = r["TotalAssociate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.Level = ds.Tables[1].Rows[0]["Lvl"].ToString();
                ViewBag.Status = ds.Tables[1].Rows[0]["Status"].ToString();
                model.Color = ds.Tables[1].Rows[0]["Color"].ToString();
                model.DisplayName = ds.Tables[1].Rows[0]["Name"].ToString();
                model.PK_UserId = ds.Tables[1].Rows[0]["PK_UserId"].ToString();
                model.ProfilePic = ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                model.TotalDirect = ds.Tables[1].Rows[0]["TotalDirect"].ToString();
                model.TotalActive = ds.Tables[1].Rows[0]["TotalActive"].ToString();
                model.TotalInactive = ds.Tables[1].Rows[0]["TotalInActive"].ToString();
                model.TotalTeam = ds.Tables[1].Rows[0]["TotalTeam"].ToString();
                model.TotalActiveTeam = ds.Tables[1].Rows[0]["TotalActiveTeam"].ToString();
                model.TotalInActiveTeam = ds.Tables[1].Rows[0]["TotalInActiveTeam"].ToString();
                model.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
            }
            model.Level = "1";
            DataSet ds1 = model.GetLevelMembers();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    MemberDetails obj = new MemberDetails();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.SelfBV = r["SelfBV"].ToString();
                    obj.TeamBV = r["TeamBV"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lstMember.Add(obj);
                }
                model.lstMember = lstMember;
            }
            return View(model);
        }
        public ActionResult GetMemberName(string LoginId, string ePinNo)
        {
            Home model = new Home();
            try
            {
                model.ePinNo = ePinNo;
                model.LoginId = LoginId;
                DataSet ds = model.GetMemberName();

                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {

                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetInActiveUser(string LoginId)
        {
            Home model = new Home();
            try
            {
                model.LoginId = LoginId;
                DataSet ds = model.GetInActiveUser();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.DisplayName = ds.Tables[0].Rows[0]["Name"].ToString();
                }
            }
            catch (Exception ex)
            {

            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinTransferReport(Pin model)
        {
            List<Pin> list = new List<Pin>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.BV = r["BV"].ToString();
                    list.Add(obj);
                }
                model.lst = list;
            }
            return View(model);
        }
        [HttpPost]
        [OnAction(ButtonName = "GetDetails")]
        [ActionName("PinTransferReport")]
        public ActionResult PinTransferReportDetails(Pin model)
        {
            List<Pin> list = new List<Pin>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Pin obj = new Pin();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    list.Add(obj);
                }
                model.lst = list;
            }
            return View(model);
        }
        public ActionResult ConfirmActivation()
        {
            return View();
        }
        public ActionResult UserProfile()
        {
            Home model = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                }
            }
            return View(model);
        }
        public ActionResult ChangePasswordForUser()
        {
            return View();
        }
        [HttpPost]
        [ActionName("ChangePasswordForUser")]
        public ActionResult ChangePasswordForUser(User model)
        {
            try
            {
                model.UpdatedBy = Session["Pk_userId"].ToString();
                model.Password = Crypto.Encrypt(model.Password);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Password Changed  Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("ChangePasswordForUser", "User");
        }
        public ActionResult ActivatePin(string ePinNo, string Fk_UserId)
        {
            Pin model = new Pin();
            try
            {
                model.ePinNo = ePinNo;
                model.FK_UserId = Fk_UserId;
                DataSet ds = model.ActivatePin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Response = "1";
                    }
                    else
                    {
                        model.Response = "0";
                        model.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                model.Response = "0";
                model.Message = ex.Message;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BankDetailsUpdate()
        {
            User model = new User();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.IsVerified = ds.Tables[0].Rows[0]["IsVerified"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    model.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    model.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["PanImage"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("BankDetailsUpdate")]
        public ActionResult BankDetailsUpdate(User model, HttpPostedFileBase Image)
        {
            try
            {

                if (Image != null)
                {
                    model.Image = "/PanUpload/" + Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }

                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.BankDetailsUpdate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "KYC Details Updated Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("BankDetailsUpdate", "User");
        }
        public ActionResult ViewProfile()
        {
            Home model = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            model.Fk_UserId = Session["Pk_userId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "1")
                {
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                }
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult ViewProfile(Home model)
        {
            try
            {
                List<SelectListItem> Gender = Common.BindGender();
                ViewBag.Gender = Gender;
                if (model.postedFile != null)
                {
                    model.ProfilePic = "/ProfilePicture/" + Guid.NewGuid() + Path.GetExtension(model.postedFile.FileName);
                    model.postedFile.SaveAs(Path.Combine(Server.MapPath(model.ProfilePic)));
                }
                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Profile Updated Successfully";
                    }
                    else
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("ViewProfile");
        }
        public ActionResult GetMemberDetails(string LoginId)
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
        public ActionResult EPinRequest()
        {
            User model = new User();
            List<User> list = new List<User>();
            model.LoginId = Session["LoginId"].ToString();
            model.BankName = Session["Bank"].ToString();
            model.BranchName = Session["Branch"].ToString();
            DataSet dss = model.GetEPinRequestDetails();

            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.NoofPins = r["NoOfPins"].ToString();
                    obj.PK_RequestID = r["PK_RequestID"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.Fk_Paymentid = r["PaymentMode"].ToString();
                    obj.BankName = r["BankName"].ToString();
                    obj.BankBranch = r["BankBranch"].ToString();
                    obj.TransactionNo = r["ChequeDDNo"].ToString();
                    obj.TransactionDate = r["ChequeDDDate"].ToString();
                    list.Add(obj);
                }
                model.lstEpinRequest = list;
            }


            #region Check Balance
            model.Fk_UserId = Session["Pk_UserId"].ToString();
            DataSet dsss = model.GetWalletBalance();
            if (dsss != null && dsss.Tables.Count > 0 && dsss.Tables[0].Rows.Count > 0)
            {
                ViewBag.WalletBalance = dsss.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion


            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForJoining();
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
        [ActionName("EPinRequest")]
        [OnAction(ButtonName = "btnsave")]
        public ActionResult EPinRequest(User model)
        {
            try
            {
                model.AddedBy = Session["Pk_userId"].ToString();
                model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
                if (model.Fk_Paymentid == "12")
                {
                    model.BankName = null;
                    model.BranchName = null;
                }
                DataSet ds = model.SaveEpinRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "E_pin generated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("EPinRequest", "User");
        }
        public ActionResult DeleteEPinRequest(string Id)
        {
            try
            {
                User model = new User();
                model.PK_RequestID = Id;
                DataSet ds = model.DeleteEPinRequest();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "E_pin request deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["error"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }
            return RedirectToAction("EPinRequest", "User");
        }
        public JsonResult GetTreeMembers(string Level, string PK_UserId)
        {
            Tree model = new Tree();
            model.PK_UserId = PK_UserId;
            model.Level = Level;
            List<MemberDetails> lst = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembers();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    MemberDetails obj = new MemberDetails();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.SelfBV = r["SelfBV"].ToString();
                    obj.TeamBV = r["TeamBV"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lst.Add(obj);
                }
                model.lstMember = lst;
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        public ActionResult TreeTTP(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = Session["LoginId"].ToString();
                model.PK_UserId = Session["Pk_UserId"].ToString();
                model.LoginId = Session["LoginId"].ToString();
                model.DisplayName = Session["FullName"].ToString();
            }
            List<TreeMembers> lst = new List<TreeMembers>();
            List<MemberDetails> lstMember = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembersCountTR1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    TreeMembers obj = new TreeMembers();
                    obj.LevelName = r["LevelNo"].ToString();
                    obj.NumberOfMembers = r["TotalAssociate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[1].Rows.Count > 0)
            {
                ViewBag.Level = ds.Tables[1].Rows[0]["Lvl"].ToString();
                ViewBag.Status = ds.Tables[1].Rows[0]["Status"].ToString();
                model.Color = ds.Tables[1].Rows[0]["Color"].ToString();
                model.DisplayName = ds.Tables[1].Rows[0]["Name"].ToString();
                model.PK_UserId = ds.Tables[1].Rows[0]["PK_UserId"].ToString();
                model.ProfilePic = ds.Tables[1].Rows[0]["ProfilePic"].ToString();
                model.TotalDirect = ds.Tables[1].Rows[0]["TotalDirect"].ToString();
                model.TotalActive = ds.Tables[1].Rows[0]["TotalActive"].ToString();
                model.TotalInactive = ds.Tables[1].Rows[0]["TotalInActive"].ToString();
                model.TotalTeam = ds.Tables[1].Rows[0]["TotalTeam"].ToString();
                model.TotalActiveTeam = ds.Tables[1].Rows[0]["TotalActiveTeam"].ToString();
                model.TotalInActiveTeam = ds.Tables[1].Rows[0]["TotalInActiveTeam"].ToString();
                model.SponsorName = ds.Tables[1].Rows[0]["SponsorName"].ToString();
            }
            model.Level = "1";
            DataSet ds1 = model.GetLevelMembers();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    MemberDetails obj = new MemberDetails();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.SelfBV = r["SelfBV"].ToString();
                    obj.TeamBV = r["TeamBV"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lstMember.Add(obj);
                }
                model.lstMember = lstMember;
            }
            return View(model);
        }


        public ActionResult TopUpList()
        {
            Account model = new Account();
            List<Account> lst = new List<Account>();
            model.Pk_userId = Session["PK_UserId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds1 = model.GetTopUpDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Account obj = new Account();
                    obj.InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ROIPercentage = r["ROIPercentage"].ToString();
                    obj.TopUpDate = r["TopUpDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    obj.PackageDays = r["PackageDays"].ToString();
                    lst.Add(obj);
                }
                model.lstTopUp = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("TopUpList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopUpList(Account model)
        {
            List<Account> lst = new List<Account>();
            model.Pk_userId = Session["PK_UserId"].ToString();
            model.LoginId = Session["LoginId"].ToString();
            model.FK_UserId = model.FK_UserId == "0" ? null : model.FK_UserId;
            model.LoginId = model.LoginId == "0" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetTopUpDetails();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    Account obj = new Account();
                    obj.InvestmentId = r["Pk_InvestmentId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ROIPercentage = r["ROIPercentage"].ToString();
                    obj.TopUpDate = r["TopUpDate"].ToString();
                    obj.ProductName = r["ProductName"].ToString();
                    lst.Add(obj);
                }
                model.lstTopUp = lst;
            }
            return View(model);
        }


        public ActionResult BusinessReportsForUser()
        {
            User model = new User();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            List<User> lst = new List<User>();
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;
                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");
            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("BusinessReportsForUser")]
        [OnAction(ButtonName = "GetDetails")]
        public ActionResult BusinessReportsForUser(User model)
        {
            List<User> lst = new List<User>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            if (model.IsDownline == "on")
            {
                model.IsDownline = "1";
            }
            else
            {
                model.IsDownline = "0";
            }
            model.PK_ProductID = model.PK_ProductID == "0" ? null : model.PK_ProductID;
            model.Level = model.Level == "0" ? null : model.Level;
            model.IsDownline = model.IsDownline == "0" ? null : model.IsDownline;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            model.LoginId = Session["LoginId"].ToString();
            DataSet ds = model.GetBusinessReports();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["FirstName"].ToString();
                    obj.Amount = Convert.ToDecimal(r["Amount"].ToString());
                    obj.BV = r["BV"].ToString();
                    obj.Date = r["Date"].ToString();
                    obj.Level = r["Lvl"].ToString();
                    obj.PackageType = r["PackageType"].ToString();
                    lst.Add(obj);
                }
                model.lstBReports = lst;

                ViewBag.Amount = double.Parse(ds.Tables[0].Compute("sum(Amount)", "").ToString()).ToString("n2");
                ViewBag.BV = double.Parse(ds.Tables[0].Compute("sum(BV)", "").ToString()).ToString("n2");

            }

            #region ddlPlotSize
            int count = 0;
            List<SelectListItem> ddlProductName = new List<SelectListItem>();
            DataSet dss = model.GetProductName();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlProductName.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlProductName.Add(new SelectListItem { Text = r["ProductName"].ToString(), Value = r["PK_ProductID"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlProductName = ddlProductName;
            #endregion
            return View(model);
        }


        public ActionResult PayoutRequest()
        {
            string FormName = "";
            string Controller = "";
            User model = new User();
            model.LoginId = Session["LoginId"].ToString();
            model.Fk_UserId = Session["Pk_userId"].ToString();
            //DataSet ds = model.GetPayoutBalance();
            //model.PayoutBalance = ds.Tables[0].Rows[0]["Balance"].ToString();

            List<User> lst = new List<User>();
            model.State = model.State == "0" ? null : model.State;
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds1 = model.GetPayoutRequest();
            if (ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds1.Tables[0].Rows)
                {
                    User obj = new User();
                    obj.PK_RequestID = r["Pk_RequestId"].ToString();
                    obj.Amount =Convert.ToDecimal(r["AMount"].ToString());
                    obj.Date = r["RequestedDate"].ToString();
                    obj.IFSCCode = r["IFSCCode"].ToString();
                    obj.AccountNo = r["MemberAccNo"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.ROIPercentage = r["BackColor"].ToString();
                    obj.TransactionNo = r["TransactionNo"].ToString();
                    lst.Add(obj);
                }
                model.lstPayoutRequest = lst;
            }
            if(ds1 != null && ds1.Tables.Count > 0 && ds1.Tables[1].Rows.Count > 0)
            {
                if(ds1.Tables[1].Rows[0]["PanStatus"].ToString()!="Approved")
                {
                    return RedirectToAction("BankDetailsUpdate", "User");
                }
            }
            #region Check Balance
            DataSet ds11 = model.GetWalletBalance();
            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                model.PayoutBalance = ds11.Tables[0].Rows[0]["amount"].ToString();
            }
            #endregion
            return View(model);
        }

        [HttpPost]
        [ActionName("PayoutRequest")]
        [OnAction(ButtonName = "PayoutRequest")]
        public ActionResult PayoutRequest(User model)
        {
            try
            {
                model.AddedBy = Session["Pk_userId"].ToString();
                DataSet ds = model.PayoutRequest();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["msg"] = "Transfer To Account Initiated Successfully.";
                    }
                    else
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else { }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("PayoutRequest", "User");
        }


        public ActionResult Download()
        {
            User model = new User();
            List<User> lst = new List<User>();
            model.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds = model.GetRewarDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
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
        [ActionName("Download")]
        public ActionResult Download(User model)
        {
            List<User> lst = new List<User>();
             model.AddedBy = Session["Pk_userId"].ToString();
            DataSet ds = model.GetRewarDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    User obj = new User();
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