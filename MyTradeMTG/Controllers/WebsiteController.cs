using MyTradeMTG.Models;
using Razorpay.Api;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;


namespace MyTradeMTG.Controllers
{
    public class WebsiteController : Controller
    {
        // GET: Website
        public ActionResult Index(string Pid)
        {
            Home model = new Home();
            if (!string.IsNullOrEmpty(Pid))
            {
                model.Fk_UserId = Pid;
                DataSet ds = model.GetMemberNameWithUserId();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.SponsorId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                }
            }

            Session.Abandon();
            return View(model);
        }

        public ActionResult LoginAction(Home obj)
        {
            string FormName = "";
            string Controller = "";
            //Home Modal = new Home();
            try
            {
                DataSet ds = obj.Login();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((ds.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {
                            var pass = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                            if (obj.Password == Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString()))
                            {


                                Session["IsFill"] = ds.Tables[0].Rows[0]["IsFill"].ToString();

                                //Session["PendingStatusLogin"] = ds.Tables[0].Rows[0]["PendingStatus"].ToString();
                                Session["FirmName"] = ds.Tables[0].Rows[0]["FirmName"].ToString();
                                Session["IsFranchise"] = ds.Tables[0].Rows[0]["IsFranchise"].ToString();
                                Session["Country"] = ds.Tables[0].Rows[0]["Country"].ToString();
                                Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                                Session["Pk_UserId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                                Session["UserType"] = ds.Tables[0].Rows[0]["UserType"].ToString();
                                Session["FullName"] = ds.Tables[0].Rows[0]["FullName"].ToString();
                                Session["Password"] = ds.Tables[0].Rows[0]["Password"].ToString();
                                Session["TransPassword"] = ds.Tables[0].Rows[0]["TransPassword"].ToString();
                                Session["Profile"] = ds.Tables[0].Rows[0]["Profile"].ToString();
                                Session["Gender"] = ds.Tables[0].Rows[0]["Sex"].ToString();
                                Session["Branch"] = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                                Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
                                Session["Contact"] = ds.Tables[0].Rows[0]["Mobile"].ToString();
                                Session["Bank"] = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                                Session["Status"] = ds.Tables[0].Rows[0]["Status"].ToString();
                                Session["UserActivationTopUp"] = ds.Tables[0].Rows[0]["UserActivationTopUp"].ToString();
                                Session["CurrencyName"] = ds.Tables[0].Rows[0]["CurrencyName"].ToString();
                                Session["CurrencySymbol"] = ds.Tables[0].Rows[0]["CurrencySymbol"].ToString();
                                Session["ISOcode"] = ds.Tables[0].Rows[0]["ISOcode"].ToString();
                                Session["IsActive"] = ds.Tables[1].Rows[0]["IsActive"].ToString();
                                DataSet ds1 = obj.GetFranchisedetails(Session["Pk_UserId"].ToString());
                                if (ds1 != null && ds1.Tables[0].Rows.Count > 0)
                                {
                                    Session["Franchisestatus"] = ds1.Tables[0].Rows[0]["Status"].ToString();
                                }
                                else
                                {
                                    Session["Franchisestatus"] = "";
                                }


                                FormName = "UserDashBoard";
                                Controller = "User";


                                //if (ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "O" || ds.Tables[0].Rows[0]["TeamPermanent"].ToString() == "P")
                                //{
                                //    Session["IdActivated"] = true;
                                //    FormName = "UserDashBoard";
                                //    Controller = "User";
                                //}
                                //else
                                //{
                                //    Session["IdActivated"] = false;
                                //    FormName = "CompleteRegistration";
                                //    Controller = "Home";
                                //}
                            }
                            else
                            {
                                TempData["Login"] = "Incorrect Password";
                                //return Redirect("/MyTradeMTGWebsite/error_page.html");
                                FormName = "Index";
                                Controller = "Website";
                            }
                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            Session["Pk_AdminId"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            Session["UsertypeName"] = ds.Tables[0].Rows[0]["UsertypeName"].ToString();
                            Session["Name"] = ds.Tables[0].Rows[0]["Name"].ToString();

                            //string Name = ds.Tables[0].Rows[0]["Name"].ToString();
                            //string LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                            //string Mobile = ds.Tables[0].Rows[0]["Contact"].ToString();
                            //string OtpVerify = ds.Tables[0].Rows[0]["OtpVerify"].ToString();
                            //string TempId = "1707166036874698573";

                            //string str = "Dear "+ Name + ", Your OTP "+ OtpVerify + " for "+ LoginId + ". MY TRADE";

                            if (ds.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                            {
                                Session["FranchiseAdminID"] = ds.Tables[0].Rows[0]["Pk_adminId"].ToString();
                                FormName = "Registration";
                                Controller = "FranchiseAdmin";
                            }
                            else
                            {
                                FormName = "AdminDashBoard";
                                //FormName = "OTPVerify";
                                Controller = "Admin";
                            }

                            //try
                            //{
                            //    BLSMS.SendSMS(Mobile,str,TempId);
                            //}
                            //catch { }

                            //TempData["OtpVerify"] = "Otp is sent successfully on registerd mobile no.";

                        }
                        else if (ds.Tables[0].Rows[0]["UserType"].ToString() == "Back Office")
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
                            //return Redirect("/MyTradeMTGWebsite/error_page.html");
                            FormName = "Index";
                            Controller = "Website";
                        }
                    }
                    else
                    {
                        TempData["Login"] = "Incorrect LoginId Or Password";
                        //return Redirect("/MyTradeMTGWebsite/error_page.html");
                        FormName = "Index";
                        Controller = "Website";
                    }
                }
                else
                {
                    TempData["Login"] = "Incorrect LoginId Or Password";
                    //return Redirect("/MyTradeMTGWebsite/error_page.html");
                    FormName = "Index";
                    Controller = "Website";
                    //Redirect("/MyTradeMTGWebsite/index.html");
                }
            }
            catch (Exception ex)
            {
                TempData["Login"] = ex.Message;
                //return Redirect("/MyTradeMTGWebsite/error_page.html");
                FormName = "Index";
                Controller = "Website";
            }
            return RedirectToAction(FormName, Controller);
        }


        public ActionResult Registration(string PId)
        {
            Home obj = new Home();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;

            #region ddlcountry
            List<SelectListItem> ddlcountry = Common.BindCountry();
            ViewBag.ddlcountry = ddlcountry;
            #endregion

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
        public ActionResult RegistrationAction(string SponsorId, string FirstName, string LastName, string MobileNo, string PinCode, string Leg, string Password, string Email, string Gender, string State, string City, string CountryCode, string Country)
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
                obj.Password2 = Crypto.Encrypt(Password);
                obj.Email = Email;
                obj.Gender = Gender;
                obj.CountryCode = CountryCode;
                obj.Country = Country;
                obj.State = State;
                obj.City = City;
                DataSet ds = obj.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {

                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Pk_UserId"] = ds.Tables[0].Rows[0]["Pk_userId"].ToString();
                        Session["LoginId"] = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        Session["DisplayName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["PassWord"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["Transpassword"] = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        Session["MobileNo"] = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        Session["FullName"] = ds.Tables[0].Rows[0]["Name"].ToString();
                        Session["Email"] = ds.Tables[0].Rows[0]["Email"].ToString();
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
            if (Session["LoginId"] == null)
            {
                return RedirectToAction("Login", "Home");
            }
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProductForJoiningForUser();
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


            Home model = new Home();
            DataSet ds = model.GetPaymentTypeList();
            List<Home> lst = new List<Home>();
            if (ds != null && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in ds.Tables[0].Rows)
                {
                    Home objList = new Home();
                    objList.Fk_Paymentid = dr["PK_PaymentTypeId"].ToString();
                    objList.PaymentType = dr["PaymentType"].ToString();
                    objList.IsActive = dr["IsActive"].ToString();
                    lst.Add(objList);
                }
                model.lstBannerImage = lst;
            }
            #endregion

            return View(model);
        }
        [HttpPost]
        public ActionResult CompleteRegistration(Home model)
        {
            OrderModel orderModel = new OrderModel();
            string random = Common.GenerateRandom();
            CreateOrderResponse obj1 = new CreateOrderResponse();
            if (Session["Pk_UserId"].ToString() == "259")
            {
                model.Amount = "1";
            }
            else
            {
                model.Amount = (Convert.ToInt32(model.Amount) * 100).ToString();
            }
            try
            {
                Dictionary<string, object> options = new Dictionary<string, object>();
                options.Add("amount", Convert.ToInt32(model.Amount)); // amount in the smallest currency unit
                options.Add("receipt", random);
                options.Add("currency", "INR");
                options.Add("payment_capture", "1");

                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;
                RazorpayClient client = new RazorpayClient(PaymentGateWayDetails.KeyName, PaymentGateWayDetails.SecretKey);
                Razorpay.Api.Order order = client.Order.Create(options);
                obj1.OrderId = order["id"].ToString();
                obj1.Status = "0";
                model.OrderId = order["id"].ToString();
                model.Fk_UserId = Session["Pk_UserId"].ToString();
                model.AddedBy = Session["Pk_UserId"].ToString();
                model.PaymentMode = "12";
                orderModel.orderId = order.Attributes["id"];
                orderModel.razorpayKey = "rzp_live_k8z9ufVw0R0MLV";
                orderModel.amount = Convert.ToInt32(model.Amount);
                orderModel.currency = "INR";
                orderModel.description = "Activate Account";
                orderModel.name = Session["FullName"].ToString();
                orderModel.contactNumber = Session["Contact"].ToString();
                orderModel.email = Session["Email"].ToString();
                orderModel.FK_ProductId = model.Package;
                //orderModel.image = "http://MyTradeMTG.co.in/MyTradeMTGWebsite/assets/img/logo.png";
                DataSet ds = model.SaveOrderDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        return View("ActivationPayment", orderModel);
                    }
                    else
                    {
                        return RedirectToAction("CompleteRegistration", "Home");
                    }
                }
                else
                {
                    return RedirectToAction("CompleteRegistration", "Home");
                }
                // Return on PaymentPage with Order data
            }
            catch (Exception ex)
            {
                obj1.Status = "1";
                TempData["error"] = ex.Message;
                return RedirectToAction("CompleteRegistration", "Home");
            }
        }




        public ActionResult ForgetPassword()
        {
            return View();
        }
        //[HttpPost]
        //[ActionName("ForgetPassword")]
        //[OnAction(ButtonName = "forgetpassword")]
        public ActionResult ForgetPasswordAction(Home model)
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
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string TempId = "1707166036828812593";
                        string str = BLSMS.ForgetPassword(model.Name, model.Password);
                        try
                        {
                            BLSMS.SendSMS(Mobile, str, TempId);
                        }
                        catch
                        {
                        }
                        string signature = " &nbsp;&nbsp;&nbsp; Dear  " + model.Name + ",<br/>&nbsp;&nbsp;&nbsp; Your Password Is : " + model.Password;

                        //using (MailMessage mail = new MailMessage())
                        //{
                        //    mail.From = new MailAddress("email@gmail.com");
                        //    mail.To.Add(model.Email);
                        //    mail.Subject = "Forget Password";
                        //    mail.Body = signature;
                        //    mail.IsBodyHtml = true;
                        //    using (SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587))
                        //    {
                        //        smtp.Credentials = new NetworkCredential("coustomer.MyTradeMTG@gmail.com", "MyTradeMTG@2022");
                        //        smtp.EnableSsl = true;
                        //        smtp.Send(mail);
                        //    }
                        //}
                        TempData["PassMag"] = "Password sent successfully.";
                    }

                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["PassErrMag"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["PassErrMag"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["PassErrMag"] = ex.Message;
            }
            //return RedirectToAction("Login", "Home");
            return RedirectToAction("Index", "Website");
        }

        

    }
}