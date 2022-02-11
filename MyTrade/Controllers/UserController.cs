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
                            string Body = "Dear User,\t\n Your Account has been activated.";
                            BLMail.SendMail(Email, "Activation Successful", Body, false);
                        }
                        TempData["Activated"] = "User Activated Successfully";
                        FormName = "ConfirmActivation";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["Activated"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
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
                TempData["Activated"] = ex.Message;
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
                        TempData["Topup"] = "Top-Up Done successfully";
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
                foreach (DataRow r in ds.Tables[0].Rows)
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
        [HttpPost]
        public ActionResult PinList(Pin model)
        {
            model.ParentLoginId = Session["LoginId"].ToString();

            if (model.LoginId != model.ParentLoginId)
            {
                try
                {
                    string hdrows = Request["hdRows"].ToString();
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
                                        TempData["Pin"] = "Transferred Successfully";
                                    }
                                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                                    {
                                        TempData["Pin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                                    }
                                }
                            }
                        }
                        catch { chkselect = "0"; }
                    }
                }
                catch (Exception ex)
                {
                    TempData["Pin"] = ex.Message;
                }
            }
            else
            {
                TempData["Pin"] = "You Can't transfer on the same Id";
            }
            return RedirectToAction("PinList");
        }
        public ActionResult Tree()
        {
            return View();
        }
        public ActionResult GetMemberName(string LoginId,string ePinNo)
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
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("ChangePasswordForUser", "User");
        }
        
        public ActionResult ActivatePin(string ePinNo, string Fk_UserId)
        {
            try
            {
                Pin model = new Pin();
                model.ePinNo = ePinNo;
                model.FK_UserId = Fk_UserId;
                DataSet ds = model.ActivatePin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["Pin"] = "Pin Activated  Successfully";
                    }
                    else
                    {
                        TempData["Pin"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Pin"] = ex.Message;
            }
            return RedirectToAction("PinList", "User");
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
                   
                    model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNumber = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    model.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    model.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                }
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("BankDetailsUpdate")]
        public ActionResult BankDetailsUpdate(User model)
        {
            try
            {
                model.Fk_UserId = Session["Pk_userId"].ToString();
                DataSet ds = model.BankDetailsUpdate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Bank Details Update Successfully";
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
                        TempData["UserProfile"] = "Profile Updated Successfully";
                    }
                    else
                    {
                        TempData["UserProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["UserProfile"] = ex.Message;
            }
            return View(model);
        }
    }
}