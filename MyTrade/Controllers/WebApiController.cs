using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MyTrade.Models;
using System.Data;
using System.IO;
namespace MyTrade.Controllers
{
    public class WebApiController : Controller
    {
        // GET: API
        #region Registration
        public ActionResult Registration(RegistrationAPI model)
        {
            RegistrationAPI obj = new RegistrationAPI();
            if (model.SponsorId == "" || model.SponsorId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Sponsor Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.FirstName == "" || model.FirstName == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter First Name";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.MobileNo == "" || model.MobileNo == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Mobile No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Leg == "" || model.Leg == null)
            {
                obj.Status = "1";
                obj.Message = "Please Select Leg";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            model.SponsorId = model.SponsorId;
            try
            {
                string password = Common.GenerateRandom();
                model.Password = Crypto.Encrypt(password);
                model.RegistrationBy = "Mobile";
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.FullName = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.TransPassword = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        obj.Leg = model.Leg;
                        obj.RegistrationBy = model.RegistrationBy;
                        obj.SponsorId = model.SponsorId;
                        obj.LastName = model.LastName;
                        obj.PinCode = model.PinCode;
                        obj.Status = "0";
                        obj.Message = "Registered Successfully";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }
                }
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }


        }
        #endregion
        #region SponsporName
        public ActionResult GetSponsorName(SponsorNameAPI sponsorname)
        {
            SponsorNameA obj = new SponsorNameA();
            DataSet ds = sponsorname.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

               if(ds.Tables[0].Rows[0]["TeamPermanent"].ToString()=="P")
                {
                    obj.SponsorName = ds.Tables[0].Rows[0]["FullName"].ToString();
                    obj.Status = "0";
                    obj.Message = "Sponsor Name Fetched";
                    return Json(obj, JsonRequestBehavior.AllowGet);
                }
               else
                {
                    sponsorname.Status = "1";
                    sponsorname.Message = "Invalid SponsorId"; return Json(sponsorname, JsonRequestBehavior.AllowGet);
                }
               
            }
            else
            {
                sponsorname.Status = "1";
                sponsorname.Message = "Invalid SponsorId"; return Json(sponsorname, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region getState
        public ActionResult GetState(Pincode Pindetails)
        {
            StateDetails obj = new StateDetails();
            DataSet ds = Pindetails.GetStateCity();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.Status = "0";
                obj.Message = "Details Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                Pindetails.Status = "1";
                Pindetails.Message = "Invalid PinCode"; return Json(Pindetails, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Login
        public ActionResult Login(LoginAPI model)
        {
            LoginAPI obj = new LoginAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Login Id";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            if (model.Password == "" || model.Password == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter Password";
            }
            try
            {
                DataSet dsResult = model.Login();
                {
                    if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                        {
                            if (model.Password == Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString()))
                            {

                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                                obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                                obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                                obj.Password = dsResult.Tables[0].Rows[0]["Password"].ToString();
                                obj.Profile = dsResult.Tables[0].Rows[0]["Profile"].ToString();
                                obj.Status = dsResult.Tables[0].Rows[0]["Status"].ToString();
                                obj.TeamPermanent = dsResult.Tables[0].Rows[0]["TeamPermanent"].ToString();
                                obj.Status = "0";
                                obj.Message = "Successfully Logged in";

                                return Json(obj, JsonRequestBehavior.AllowGet);

                            }
                            obj.Status = "1";
                            obj.Message = "Incorrect LoginId Or Password";
                            return Json(obj, JsonRequestBehavior.AllowGet);

                        }
                        else if (dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Admin")
                        {
                            obj.Status = "0";
                            obj.Message = "Successfully Logged in";
                            obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                            obj.Pk_adminId = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            obj.UserType = dsResult.Tables[0].Rows[0]["UsertypeName"].ToString();
                            obj.FullName = dsResult.Tables[0].Rows[0]["Name"].ToString();

                            if (dsResult.Tables[0].Rows[0]["isFranchiseAdmin"].ToString() == "True")
                            {
                                obj.FranchiseAdminID = dsResult.Tables[0].Rows[0]["Pk_adminId"].ToString();
                            }

                        }
                        else
                        {
                            obj.Status = "1";
                            obj.Message = "Incorrect LoginId Or Password";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        obj.Status = "1";
                        obj.Message = "Invalid LoginId or Password.";
                        return Json(obj, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = "Invalid LoginId or Password.";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }

        #endregion
    }
}