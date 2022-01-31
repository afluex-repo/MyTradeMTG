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
        #region ActivateUser
        
        public ActionResult ActivateUser(EpinDetails model)
        {
            EpinDetails obj = new EpinDetails();

            if (model.EPin == "" || model.EPin == null)
            {
                obj.Status = "1";
                obj.Message = "Please Enter EPin No";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
           
            try
            {
                DataSet dsResult = model.ActivateUser();
                {
                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.Message = "User Activated Successfully";
                            return Json(obj, JsonRequestBehavior.AllowGet);
                           
                        }
                        else
                        {

                            obj.Status = "1";
                            obj.Message = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }
                     

                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Dashboard
        public ActionResult GetDashboard(AssociateDashBoard associate)
        {
            DashboardResponse obj = new DashboardResponse();
            DataSet ds = associate.GetAssociateDashboard();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {

                obj.TotalDownline = ds.Tables[0].Rows[0]["TotalDownline"].ToString();
                obj.TotalDirect = ds.Tables[0].Rows[0]["TotalDirect"].ToString();
                obj.TotalActive = ds.Tables[0].Rows[0]["TotalActive"].ToString();
                obj.TotalInActive = ds.Tables[0].Rows[0]["TotalInActive"].ToString();
                obj.Status = "0";
                obj.Message = "Data Fetched";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            else
            {
                obj.Status = "1";
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region Tree
        public ActionResult Tree(TreeAPI model)
        {

            UpdateProfile sta = new UpdateProfile();
            TreeAPI obj = new TreeAPI();
            if (model.LoginId == "" || model.LoginId == null)
            {
                model.Status = "1";
                model.Message = "Please enter LoginId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            if (model.Fk_headId == "" || model.Fk_headId == null)
            {
                model.Status = "1";
                model.Message = "Please enter headId";
                return Json(model, JsonRequestBehavior.AllowGet);
            }
            try
            {
                DataSet ds = model.GetTree();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {

                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "0")
                    {

                        List<Tree1> GetGenelogy = new List<Tree1>();
                        foreach (DataRow r in ds.Tables[0].Rows)
                        {
                            Tree1 obj1 = new Tree1();
                            obj1.Fk_UserId = r["Fk_UserId"].ToString();
                            obj1.Fk_ParentId = r["Fk_ParentId"].ToString();
                            obj1.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                            obj1.SponsorId = r["SponsorId"].ToString();
                            obj1.LoginId = r["LoginId"].ToString();
                            obj1.TeamPermanent = r["TeamPermanent"].ToString();
                            obj1.MemberName = r["MemberName"].ToString();
                            obj1.MemberLevel = r["MemberLevel"].ToString();
                            obj1.Leg = r["Leg"].ToString();
                            obj1.Id = r["Id"].ToString();

                            obj1.ActivationDate = r["ActivationDate"].ToString();
                            obj1.ActiveLeft = r["ActiveLeft"].ToString();
                            obj1.ActiveRight = r["ActiveRight"].ToString();
                            obj1.InactiveLeft = r["InactiveLeft"].ToString();
                            obj1.InactiveRight = r["InactiveRight"].ToString();
                            obj1.BusinessLeft = r["BusinessLeft"].ToString();
                            obj1.BusinessRight = r["BusinessRight"].ToString();
                            obj1.ImageURL = r["ImageURL"].ToString();
                            GetGenelogy.Add(obj1);
                        }
                        obj.GetGenelogy = GetGenelogy;
                        obj.Message = "Tree";
                        obj.Status = "0";
                        obj.LoginId = model.LoginId;
                        obj.Fk_headId = model.Fk_headId;

                    }
                    else
                    {
                        sta.Status = "1";
                        sta.Message = "No Data Found";
                        return Json(sta, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {
                    sta.Status = "1";
                    sta.Message = "No Data Found";
                    return Json(sta, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {

                sta.Status = "1";
                sta.Message = ex.Message;
                return Json(sta, JsonRequestBehavior.AllowGet);
            }


            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region ActivateUser

        public ActionResult Topup (TopupByUser model)
        {
            TopupResponse obj = new TopupResponse();
            model.TopUpDate = string.IsNullOrEmpty(model.TopUpDate) ? null : Common.ConvertToSystemDate(model.TopUpDate, "dd/mm/yyyy");
            model.TransactionDate= string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
            try
            {
                DataSet dsResult = model.TopUp();
                {
                    if (dsResult != null && dsResult.Tables.Count > 0 && dsResult.Tables[0].Rows.Count > 0)
                    {
                        if (dsResult.Tables[0].Rows[0]["Msg"].ToString() == "1")
                        {
                            obj.Status = "0";
                            obj.Message = "Top-Up Done successfully";
                            return Json(obj, JsonRequestBehavior.AllowGet);

                        }
                        else
                        {

                            obj.Status = "1";
                            obj.Message = dsResult.Tables[0].Rows[0]["ErrorMessage"].ToString();
                            return Json(obj, JsonRequestBehavior.AllowGet);
                        }
                    }


                }


                return Json(obj, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
                return Json(obj, JsonRequestBehavior.AllowGet);
            }
        }
        #endregion
        #region getPaymentMode
        public ActionResult GetPaymentMode()
        {
            List<PaymentMode> lst = new List<PaymentMode>();
            PaymentModeResponse obj = new PaymentModeResponse();
            DataSet ds = obj.PaymentList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach(DataRow r in ds.Tables[0].Rows)
                {
                    PaymentMode model = new PaymentMode();
                    model.PK_PaymentModeId = r["PK_paymentID"].ToString();
                    model.PaymentModeName = r["PaymentMode"].ToString();
                    lst.Add(model);
                }
                obj.lst = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        public ActionResult GetPackage()
        {
            List<Package> lst = new List<Package>();
            PackageResponse obj = new PackageResponse();
            DataSet ds = obj.PackageList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Package model = new Package();
                    model.PK_PackageId = r["Pk_ProductId"].ToString();
                    model.PackageName = r["ProductName"].ToString();
                    lst.Add(model);
                }
                obj.lst = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult DirectList()
        {
            List<Direct> lst = new List<Direct>();
            DirectResponse obj = new DirectResponse();
            DataSet ds = obj.Direct();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    //Direct model = new Direct();
                    //model.PK_PackageId = r["Pk_ProductId"].ToString();
                    //model.PackageName = r["ProductName"].ToString();
                    //lst.Add(model);
                }
                //obj.lst = lst;
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}