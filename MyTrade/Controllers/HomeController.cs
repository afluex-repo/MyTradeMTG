using MyTrade.Filter;
using MyTrade.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;

namespace MyTrade.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return Redirect("~/MyTradeWebsite/index.html");
        }
        public ActionResult Login()
        {
            Session.Abandon();
            return View();
        }
        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                Home Modal = new Home();
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {
                            if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                            {
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                                Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                                Session["TransPassword"] = ds.Tables[0].Rows[0]["TransPassword"].ToString();
                                Session["Profile"] = ds.Tables[0].Rows[0]["Profile"].ToString();
                                Session["Gender"] = ds.Tables[0].Rows[0]["Sex"].ToString();
                                Session["Branch"] = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                                Session["Bank"] = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                                Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                                if (ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "O" || ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "P")
                                {
                                    Session["IdActivated"] = true;
                                    FormName = "UserDashBoard";
                                    Controller = "User";
                                }
                                else
                                {
                                    Session["IdActivated"] = false;
                                    FormName = "CompleteRegistration";
                                    Controller = "Home";
                                }

                            }
                            else
                            {
                                TempData["Login"] = "Incorrect Password";
                                FormName = "Login";
                                Controller = "Home";

                            }
                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UsertypeName"] = ds.Tables[0].Rows[0]["UsertypeName"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                            if (ds.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                            {
                                Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                FormName = "Registration";
                                Controller = "FranchiseAdmin";
                            }
                            else
                            {
                                FormName = "AdminDashBoard";
                                Controller = "Admin";
                            }
                        }
                        else
                        {
                            TempData["Login"] = "Incorrect LoginId Or Password";
                            FormName = "Login";
                            Controller = "Home";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        FormName = "Login";
                        Controller = "Home";

                    }

                }

                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    FormName = "Login";
                    Controller = "Home";

                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                FormName = "Login";
                Controller = "Home";
            }

            return RedirectToAction(FormName, Controller);
        }
        public ActionResult Registration(string PId)
        {
            Home obj = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            if (!string.IsNullOrEmpty(PId))
            {
                obj.Fk_UserId = PId;
                // var d = Crypto.Decrypt(PId);
                DataSet ds = obj.GetMemberNameWithUserId();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.SponsorId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                }
                // ViewBag.SponsorId = d.Split('|')[0];
                //ViewBag.Leg = d.Split('|')[1];
            }
            return View();
        }

        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string MobileNo, string PinCode, string Leg, string Password, string Email, string Gender, string State, string City)
        {
            Home obj = new Home();
            try
            {
                obj.SponsorId = SponsorId;
                obj.FirstName = FirstName;
                obj.LastName = LastName;
                obj.MobileNo = MobileNo;
                obj.RegistrationBy = "Web";
                obj.PinCode = PinCode;
                obj.Leg = null;
                obj.Password = Crypto.Encrypt(Password);
                obj.Email = Email;
                obj.Gender = Gender;
                obj.State = State;
                obj.City = City;
                DataSet ds = obj.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        Session["Pk_userId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Transpassword"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        Session["Profile"] = "";
                        obj.Result = "1";
                        if (obj.Email != "" && obj.Email != null)
                        {
                            try
                            {
                                BLMail.SendRegistrationMail(Session["DisplayName"].ToString(), Session["LoginId"].ToString(), Session["PassWord"].ToString(), "Registration Successful", obj.Email);
                            }
                            catch (Exception ex)
                            {

                            }
                        }
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
        public ActionResult ConfirmRegistration()
        {
            return View();
        }
        public ActionResult CompleteRegistration()
        {
            return View();
        }
        public ActionResult emailtemplate()
        {
            return View();
        }
        public ActionResult GetSponserDetails(string ReferBy)
        {
            Common obj = new Common();
            obj.ReferBy = ReferBy;
            DataSet ds = obj.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "P" || ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "O")
                {
                    obj.DisplayName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    obj.Result = "Yes";
                }
                else
                {
                    obj.Result = "Sponsor Id is not Active";
                }

            }
            else { obj.Result = "Invalid SponsorId"; }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetStateCity(string PinCode)
        {
            Common obj = new Common();
            obj.PinCode = PinCode;
            DataSet ds = obj.GetStateCity();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Result = "1";
            }
            else
            {
                obj.Result = "Invalid PinCode";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #region Menu
        public virtual ActionResult Menu()
        {
            Home Menu = null;

            if (Session["_Menu"] != null)
            {
                Menu = (Home)Session["_Menu"];
            }
            else
            {
                if (Session["Pk_AdminId"] == null)
                {
                    return RedirectToAction("Login", "Home");
                }
                else
                {
                    Menu = Home.GetMenus(Session["Pk_AdminId"].ToString(), Session["UserTypeName"].ToString()); // pass employee id here
                    Session["_Menu"] = Menu;
                }

            }
            return PartialView("_Menu", Menu);
        }
        #endregion

        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ActionName("ForgetPassword")]
        [OnAction(ButtonName = "forgetpassword")]
        public ActionResult ForgetPassword(Home model)
        {

            SmtpClient smtpClient = new SmtpClient();
            MailMessage message = new MailMessage();

            try
            {
                DataSet ds = model.ForgetPassword();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                        model.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        model.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());

                        string signature = " &nbsp;&nbsp;&nbsp; Dear  " + model.Name + ",<br/>&nbsp;&nbsp;&nbsp; Your Password Is : " + model.Password;

                        using (MailMessage mail = new MailMessage())
                        {
                            mail.From = new MailAddress("email@gmail.com");
                            mail.To.Add(model.Email);
                            mail.Subject = "Forget Password";
                            mail.Body = signature;
                            mail.IsBodyHtml = true;
                            using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                            {
                                smtp.Credentials = new NetworkCredential("coustomer.mytrade@gmail.com", "Mytrade@2022");
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }
                        }
                        TempData["Login"] = "password sent your email-id successfully.";
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Login"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
            }
            return RedirectToAction("Login", "Home");
        }
        public ActionResult SignUp()
        {
            return View();
        }

        public ActionResult CalculateLevelIncomeTr2()
        {
            Home model = new Home();
            DataSet ds = model.CalculateLevelIncomeTr2();
            return View();
        }

        public ActionResult CalculateROI()
        {
            Home model = new Home();
           DataSet ds = model.CalculateROI();
            return View();
        }

        public ActionResult CalculateDistributePaymentTPS()
        {
            Home model = new Home();
            DataSet ds = model.CalculateDistributePaymentTPS();
            return View();
        }



        
    }
}