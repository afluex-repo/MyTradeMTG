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

            //if (model.Leg == "" || model.Leg == null)
            //{
            //    obj.Status = "1";
            //    obj.Message = "Please Select Leg";
            //    return Json(obj, JsonRequestBehavior.AllowGet);
            //}
            model.SponsorId = model.SponsorId;
            try
            {
                model.RegistrationBy = "Mobile";
                model.Password = Crypto.Encrypt(model.Password);
                DataSet ds = model.Registration();
                if (ds != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.PK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                        obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                        obj.FullName = ds.Tables[0].Rows[0]["Name"].ToString();
                        obj.Password = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.TransPassword = Crypto.Decrypt(ds.Tables[0].Rows[0]["Password"].ToString());
                        obj.MobileNo = ds.Tables[0].Rows[0]["MobileNo"].ToString();
                        //obj.Leg = model.Leg;
                        obj.RegistrationBy = model.RegistrationBy;
                        obj.SponsorId = model.SponsorId;
                        obj.LastName = model.LastName;
                        obj.PinCode = model.PinCode;
                        obj.Email = model.Email;
                        obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                        obj.Status = "0";
                        obj.Gender = model.Gender;
                        obj.Message = "Registered Successfully";
                        if (obj.Email != "" && obj.Email != null)
                        {
                            string Body = "Dear " + obj.FullName + ",\t\nThank you for your registration. Your Details are as Below: \t\nLogin ID: " + obj.LoginId + "\t\nPassword: " + obj.Password;
                            BLMail.SendMail(obj.Email, "Registration Successful", Body, false);
                        }
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        #endregion
        #region SponsporName
        public ActionResult GetSponsorName(SponsorNameAPI sponsorname)
        {
            SponsorNameA obj = new SponsorNameA();
            DataSet ds = sponsorname.GetMemberDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
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
            Reponse res = new Reponse();
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
                        if (model.Password == Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString()))
                        {
                            if ((dsResult.Tables[0].Rows[0]["UserType"].ToString() == "Associate"))
                            {
                                obj.LoginId = dsResult.Tables[0].Rows[0]["LoginId"].ToString();
                                obj.UserId = dsResult.Tables[0].Rows[0]["Pk_userId"].ToString();
                                obj.UserType = dsResult.Tables[0].Rows[0]["UserType"].ToString();
                                obj.FullName = dsResult.Tables[0].Rows[0]["FullName"].ToString();
                                obj.Password = Crypto.Decrypt(dsResult.Tables[0].Rows[0]["Password"].ToString());
                                obj.Profile = dsResult.Tables[0].Rows[0]["Profile"].ToString();
                                obj.Status = dsResult.Tables[0].Rows[0]["Status"].ToString();
                                obj.TeamPermanent = dsResult.Tables[0].Rows[0]["TeamPermanent"].ToString();
                                obj.Gender = dsResult.Tables[0].Rows[0]["Sex"].ToString();
                                obj.Status = "0";
                                obj.Message = "Successfully Logged in";
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
                                res.Status = "1";
                                res.Message = "Incorrect LoginId Or Password";
                                return Json(res, JsonRequestBehavior.AllowGet);
                            }
                        }
                        else
                        {

                            res.Status = "1";
                            res.Message = "Invalid LoginId or Password.";
                            return Json(res, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else
                    {

                        res.Status = "1";
                        res.Message = "Invalid LoginId or Password.";
                        return Json(res, JsonRequestBehavior.AllowGet);
                    }

                }


                return Json(res, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {
                res.Status = "1";
                res.Message = "Invalid LoginId or Password.";
                return Json(res, JsonRequestBehavior.AllowGet);
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
                            string Email = dsResult.Tables[0].Rows[0]["Email"].ToString();
                            obj.Status = "0";
                            obj.Message = "User Activated Successfully";
                            if (Email != null && Email != "")
                            {
                                string Body = "Dear User,</br> Your Account have been activated.";
                                BLMail.SendMail(Email, "Activation Successful", Body, false);
                            }
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
                obj.ActiveStatus = ds.Tables[2].Rows[0]["Status"].ToString();
                if (obj.ActiveStatus == "Active")
                {
                    obj.ReferralLink = "http://mytrade.co.in/Home/Registration?Pid=" + associate.Fk_UserId;
                }
                else
                {
                    obj.ReferralLink = "";
                }
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

        public ActionResult Topup(TopupByUser model)
        {
            TopupResponse obj = new TopupResponse();
            //model.TopUpDate = string.IsNullOrEmpty(model.TopUpDate) ? null : Common.ConvertToSystemDate(model.TopUpDate, "dd/mm/yyyy");
            //model.TransactionDate = string.IsNullOrEmpty(model.TransactionDate) ? null : Common.ConvertToSystemDate(model.TransactionDate, "dd/mm/yyyy");
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
                foreach (DataRow r in ds.Tables[0].Rows)
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
                    model.MinimumAmount = 1000;
                    model.MaximumAmount = 5000;
                    model.InMultipleOf = "1000";
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
        [HttpPost]
        public ActionResult DirectList(DirectRequest req)
        {
            DirectReponse model = new DirectReponse();
            List<DirectList> lst = new List<DirectList>();
            DataSet ds = req.GetDirectList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectList obj = new DirectList();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult DownlineList(DirectRequest req)
        {
            DirectReponse model = new DirectReponse();
            List<DirectList> lst = new List<DirectList>();
            DataSet ds = req.GetDownlineList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    DirectList obj = new DirectList();
                    obj.Mobile = r["Mobile"].ToString();
                    //obj.Email = r["Email"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult PinList(PinRequest req)
        {
            PinAPI model = new PinAPI();
            List<PinAPIResponse> lst = new List<PinAPIResponse>();
            DataSet ds = req.GetPinList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PinAPIResponse obj = new PinAPIResponse();
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
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult LevelTree(LevelTreeReq req)
        {
            LevelTreeAPI model = new LevelTreeAPI();
            List<LevelTreeResponse> lst = new List<LevelTreeResponse>();
            DataSet ds = req.GetLevelTreeData();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    LevelTreeResponse obj = new LevelTreeResponse();
                    obj.FK_ParentId = r["Parentid"].ToString();
                    obj.PK_UserId = r["PK_UserId"].ToString();
                    obj.FK_SponsorId = r["FK_SponsorID"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.MemberName = r["MemberName"].ToString();
                    obj.AssociateMemberName = r["AssociateMemberName"].ToString();
                    obj.ProfilePic = r["ProfilePic"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult AssociateTree(AssociateBookingRequest req)
        {
            AssociateBookingAPI model = new AssociateBookingAPI();
            List<AssciateBookingResponse> lst = new List<AssciateBookingResponse>();
            DataSet ds = req.GetDownlineTree();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AssciateBookingResponse obj = new AssciateBookingResponse();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.FirstName = r["FirstName"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ActiveStatus = r["ActiveStatus"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult WalletBalance(Wallet model)
        {
            WalletBalanceAPI obj = new WalletBalanceAPI();
            DataSet ds = model.GetWalletBalance();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.Balance = ds.Tables[0].Rows[0]["amount"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult TransferPin(TransferPin model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.ePinTransfer();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Transfer Successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        public ActionResult PinTransferReport(PinReport req)
        {
            PinResponse model = new PinResponse();
            List<PinDetails> lst = new List<PinDetails>();
            DataSet ds = req.GetTransferPinReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                model.Status = "0";
                model.Message = "Record Found";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    PinDetails obj = new PinDetails();
                    obj.ePinNo = r["EpinNo"].ToString();
                    obj.FromId = r["FromId"].ToString();
                    obj.FromName = r["FromName"].ToString();
                    obj.ToId = r["ToId"].ToString();
                    obj.ToName = r["ToName"].ToString();
                    obj.TransferDate = r["TransferDate"].ToString();
                    lst.Add(obj);
                }
                model.lst = lst;
            }
            else
            {
                model.Status = "1";
                model.Message = "No Record Found";
            }
            return Json(model, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UserProfile(Request model)
        {
            ProfileAPI obj = new ProfileAPI();
            DataSet ds = model.UserProfile();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                obj.Status = "0";
                obj.Message = "Record Found";
                obj.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                obj.FK_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                obj.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                obj.SponsorName = ds.Tables[0].Rows[0]["SponsorName"].ToString();
                obj.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                obj.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                obj.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                obj.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                obj.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                obj.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                obj.State = ds.Tables[0].Rows[0]["State"].ToString();
                obj.City = ds.Tables[0].Rows[0]["City"].ToString();
                obj.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                obj.AadharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                obj.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                obj.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
            }
            else
            {
                obj.Status = "1";
                obj.Message = "No Record Found";
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult UpdateProfile(ProfileAPI model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.UpdateProfile();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Profile Updated Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch(Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ChangePassword(Password model)
        {
            Reponse obj = new Reponse();
            try
            {
                model.OldPassword = Crypto.Encrypt(model.OldPassword);
                model.NewPassword = Crypto.Encrypt(model.NewPassword);
                DataSet ds = model.ChangePassword();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "Password Changed  Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public ActionResult ActivateUserByPin(ActivateUser model)
        {
            Reponse obj = new Reponse();
            try
            {
                DataSet ds = model.ActivateUserByPin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        obj.Status = "0";
                        obj.Message = "User Activated Successfully";
                    }
                    else
                    {
                        obj.Status = "1";
                        obj.Message = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                obj.Status = "1";
                obj.Message = ex.Message;
            }
            return Json(obj, JsonRequestBehavior.AllowGet);
        }
    }
}