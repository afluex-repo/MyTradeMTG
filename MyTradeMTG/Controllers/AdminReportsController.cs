using MyTradeMTG.Filter;
using MyTradeMTG.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTradeMTG.Controllers
{
    public class AdminReportsController : AdminBaseController
    {
        // GET: AdminReports
        #region AssociateList
        #endregion
        public ActionResult AssociateList(AdminReports model, string Status)
        {
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            if (Status != "" && Status != null)
            {
                model.Status = Status;
            }
            List<AdminReports> lst = new List<AdminReports>();

            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["IsBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    obj.MemberStatus = r["MemberStatus"].ToString();
                    obj.ActivationMode = r["ActivationMode"].ToString();
                    obj.IsHoldTPS = r["IsTPSHold"].ToString();
                    obj.WithdrawalStatus = r["WithdrawalStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("AssociateList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult AssociateListBy(AdminReports model)
        {
            if (model.LoginId == null)
            {
                model.ToLoginID = null;
            }
            List<AdminReports> lst = new List<AdminReports>();
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            // model.LoginId = model.ToLoginID;
            model.MemberStatus = model.MemberStatus == "0" ? null : model.MemberStatus;
            DataSet ds = model.GetAssociateList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Fk_UserId = r["Pk_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Password = Crypto.Decrypt(r["Password"].ToString());
                    obj.Mobile = (r["Mobile"].ToString());
                    obj.Email = (r["Email"].ToString());
                    obj.SponsorId = (r["SponsorId"].ToString());
                    obj.SponsorName = (r["SponsorName"].ToString());
                    obj.isBlocked = (r["IsBlocked"].ToString());
                    obj.Status = r["MemberStatus"].ToString();
                    obj.MemberStatus = r["MemberStatus"].ToString();
                    obj.ActivationMode = r["ActivationMode"].ToString();
                    obj.IsHoldTPS = r["IsTPSHold"].ToString();
                    obj.WithdrawalStatus = r["WithdrawalStatus"].ToString();
                    lst.Add(obj);
                }
                model.lstassociate = lst;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.AssociateStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            return View(model);
        }
        public ActionResult BlockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.BlockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User Blocked";
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult UnblockAssociate(Profile obj, string LoginID)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                obj.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = obj.UnblockAssociate();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User UnBlocked";
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "AssociateList";
                        Controller = "AdminReports";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
                FormName = "AssociateList";
                Controller = "AdminReports";
            }
            return RedirectToAction(FormName, Controller);
        }
        public ActionResult ActivateUser(string FK_UserID)
        {
            Profile model = new Profile();
            try
            {
                model.Fk_UserId = FK_UserID;
                model.ProductID = "1";
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.ActivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User activated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult DeactivateUser(string lid)
        {
            Profile model = new Profile();
            try
            {
                model.LoginId = lid;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();

                DataSet ds = model.DeactivateUserByAdmin();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "User deactivated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        #region TPSStatusManage
        public ActionResult HoldTPS(string Fk_UserId)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Fk_UserId;
                model.UpdatedBy = Session["PK_AdminId"].ToString();
                model.IsHoldTPS = "1";
                DataSet ds = model.UpdateTPSStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "TPS Hold successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult UnHoldTPS(string Fk_UserId)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Fk_UserId;
                model.UpdatedBy = Session["PK_AdminId"].ToString();
                model.IsHoldTPS = "0";
                DataSet ds = model.UpdateTPSStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "TPS UNHold successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult StopWithdrawal(string Fk_UserId)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Fk_UserId;
                model.UpdatedBy = Session["PK_AdminId"].ToString();
                model.WithdrawalStatus = "1";
                DataSet ds = model.UpdateWithdrawalStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Withdrawal Stopped successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult StartWithdrawal(string Fk_UserId)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Fk_UserId;
                model.UpdatedBy = Session["PK_AdminId"].ToString();
                model.WithdrawalStatus = "0";
                DataSet ds = model.UpdateWithdrawalStatus();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["BlockUnblock"] = "Withdrawal Start successfully";
                    }
                    else if (ds.Tables[0].Rows[0]["Msg"].ToString() == "0")
                    {
                        TempData["BlockUnblock"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }

                }
            }
            catch (Exception ex)
            {

                TempData["BlockUnblock"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        #endregion
        #region topupreport
        public ActionResult TopupReport()
        {
            AdminReports newdata = new AdminReports();
            List<AdminReports> lst1 = new List<AdminReports>();
            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    AdminReports Obj = new AdminReports();
                    //Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.Fk_UserId = r["PK_UserId"].ToString();
                    Obj.Pk_investmentId = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.PackageDays = r["PackageDays"].ToString();
                    Obj.PinAmount = r["PinAmount"].ToString();
                    Obj.BV = r["BV"].ToString();
                    Obj.IsCalculated = r["IsCalculated"].ToString();
                    Obj.ROIPercentage = r["ROIPercentage"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.TransactionBy = r["TransactionBy"].ToString();
                    Obj.Status = r["Statuss"].ToString();
                    Obj.TopUpDate = r["TopUpDate"].ToString();
                    Obj.TopupVia = r["TopupVia"].ToString();
                    ViewBag.Total = ds11.Tables[1].Rows[0]["Total"].ToString();
                    Obj.BasisOn = r["BasisOn"].ToString();
                    Obj.ActivationMTGToken = r["ActivationMTGToken"].ToString();
                    Obj.Topupid = r["Topupid"].ToString();


                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
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

            return View(newdata);
        }
        [HttpPost]
        [ActionName("TopupReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult TopupReportBy(AdminReports newdata)
        {
            //if (newdata.LoginId == null)
            //{
            //    newdata.ToLoginID = null;
            //}
            List<AdminReports> lst1 = new List<AdminReports>();

            newdata.BusinessType = newdata.BusinessType == "" ? null : newdata.BusinessType;
            newdata.FromDate = string.IsNullOrEmpty(newdata.FromDate) ? null : Common.ConvertToSystemDate(newdata.FromDate, "dd/MM/yyyy");
            newdata.ToDate = string.IsNullOrEmpty(newdata.ToDate) ? null : Common.ConvertToSystemDate(newdata.ToDate, "dd/MM/yyyy");
            //newdata.LoginId = newdata.ToLoginID;

            newdata.LoginId = newdata.LoginId == "" ? null : newdata.LoginId;

            DataSet ds11 = newdata.GetTopupReport();

            if (ds11 != null && ds11.Tables.Count > 0 && ds11.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds11.Tables[0].Rows)
                {
                    AdminReports Obj = new AdminReports();
                    Obj.ToLoginID = r["Pk_InvestmentId"].ToString();
                    Obj.LoginId = r["LoginId"].ToString();
                    Obj.DisplayName = r["Name"].ToString();
                    Obj.UpgradtionDate = r["UpgradtionDate"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.Amount = r["Amount"].ToString();
                    Obj.TopupBy = r["TopupBy"].ToString();
                    Obj.Status = r["Status"].ToString();
                    Obj.PrintingDate = r["PrintingDate"].ToString();
                    Obj.Description = r["Description"].ToString();
                    Obj.PaymentMode = r["PaymentMode"].ToString();
                    Obj.PackageDays = r["PackageDays"].ToString();
                    Obj.PinAmount = r["PinAmount"].ToString();
                    Obj.BV = r["BV"].ToString();
                    Obj.IsCalculated = r["IsCalculated"].ToString();
                    Obj.ROIPercentage = r["ROIPercentage"].ToString();
                    Obj.Package = r["Package"].ToString();
                    Obj.TransactionBy = r["TransactionBy"].ToString();
                    Obj.Status = r["Statuss"].ToString();
                    Obj.TopUpDate = r["TopUpDate"].ToString();
                    Obj.TopupVia = r["TopupVia"].ToString();
                    ViewBag.Total = ds11.Tables[1].Rows[0]["Total"].ToString();
                    Obj.BasisOn = r["BasisOn"].ToString();
                    Obj.ActivationMTGToken = r["ActivationMTGToken"].ToString();
                    Obj.Topupid = r["Topupid"].ToString();

                    lst1.Add(Obj);
                }
                newdata.lsttopupreport = lst1;
            }
            #region ddlstatus
            List<SelectListItem> ddlstatus = Common.BindTopupStatus();
            ViewBag.ddlstatus = ddlstatus;
            #endregion
            #region Product Bind
            Common objcomm = new Common();
            List<SelectListItem> ddlProduct = new List<SelectListItem>();
            DataSet ds1 = objcomm.BindProduct();
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

            return View(newdata);
        }
        #endregion
        public ActionResult DownTeamTree(string Ids, string FK_UserId)
        {
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;

            Reports model = new Reports();

            List<Reports> lst = new List<Reports>();
            if (Ids == null || Ids == "")
            {
                model.Ids = "1";
            }
            else
            {
                model.Ids = Ids;
            }
            if (FK_UserId != null || FK_UserId != "")
            {
                model.Fk_UserId = FK_UserId;
            }
            model.DirectStatus = "Self";
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() != "0")
                {
                    Ids = "";
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        Reports obj = new Reports();
                        obj.Mobile = r["Mobile"].ToString();
                        obj.Email = r["Email"].ToString();
                        obj.SponsorId = r["SponsorId"].ToString();
                        obj.SponsorName = r["SponsorName"].ToString();
                        obj.JoiningDate = r["JoiningDate"].ToString();
                        obj.Leg = r["Leg"].ToString();
                        obj.PermanentDate = (r["PermanentDate"].ToString());
                        obj.Status = (r["Status"].ToString());
                        obj.LoginId = (r["LoginId"].ToString());
                        obj.Name = (r["Name"].ToString());
                        obj.Level = (r["Lvl"].ToString());
                        obj.Package = (r["ProductName"].ToString());
                        Ids = Ids + r["PK_UserId"].ToString() + ",";
                        lst.Add(obj);
                    }
                    model.lstassociate = lst;
                    model.Ids = Ids;
                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("DownTeamTree")]
        [OnAction(ButtonName = "Search")]
        public ActionResult DownTeamTree(Reports model)
        {

            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            List<Reports> lst = new List<Reports>();

            if (model.Ids == null || model.Ids == "")
            {
                model.Ids = model.Fk_UserId;
            }
            model.DirectStatus = "Self";
            DataSet ds = model.GetDirectList();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                string Ids = "";
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    Reports obj = new Reports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.SponsorId = r["SponsorId"].ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.JoiningDate = r["JoiningDate"].ToString();
                    obj.Leg = r["Leg"].ToString();
                    obj.PermanentDate = (r["PermanentDate"].ToString());
                    obj.Status = (r["Status"].ToString());
                    obj.LoginId = (r["LoginId"].ToString());
                    obj.Name = (r["Name"].ToString());
                    obj.Level = (r["Lvl"].ToString());
                    obj.Package = (r["ProductName"].ToString());
                    Ids = Ids + r["PK_UserId"].ToString() + ",";
                    lst.Add(obj);
                }
                model.lstassociate = lst;
                model.Ids = Ids;
            }
            List<SelectListItem> AssociateStatus = Common.AssociateStatus();
            ViewBag.ddlStatus = AssociateStatus;
            List<SelectListItem> Leg = Common.LegType();
            ViewBag.ddlleg = Leg;
            return View(model);
        }
        //[HttpPost]
        //[ActionName("DirectListForAdmin")]
        //[OnAction(ButtonName = "Search")]
        //public ActionResult DirectListForAdmin(AdminReports model)
        //{

        //    model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
        //    model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
        //    List<AdminReports> lst = new List<AdminReports>();
        //    //model.LoginId = Session["LoginId"].ToString();
        //    DataSet ds = model.GetDirectList();

        //    if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
        //    {
        //        foreach (DataRow r in ds.Tables[0].Rows)
        //        {
        //            AdminReports obj = new AdminReports();
        //            obj.Mobile = r["Mobile"].ToString();
        //            obj.Email = r["Email"].ToString();
        //            obj.Leg = r["Leg"].ToString();
        //            obj.JoiningDate = r["JoiningDate"].ToString();
        //            obj.PermanentDate = (r["PermanentDate"].ToString());
        //            obj.Status = (r["Status"].ToString());
        //            obj.SponsorId = (r["LoginId"].ToString());
        //            obj.SponsorName = (r["Name"].ToString());
        //            obj.Package = (r["ProductName"].ToString());
        //            lst.Add(obj);
        //        }
        //        model.lstDirect = lst;
        //    }
        //    List<SelectListItem> AssociateStatus = Common.AssociateStatus();
        //    ViewBag.ddlStatus = AssociateStatus;
        //    List<SelectListItem> Leg = Common.LegType();
        //    ViewBag.ddlleg = Leg;
        //    return View(model);
        //}
        public ActionResult ViewProfileForAdmin(string Id)
        {
            AdminReports model = new AdminReports();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            if (Id != null)
            {
                model.Fk_UserId = Id;
                DataSet ds = model.GetAdminProfileDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    model.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    model.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    model.SponsorName = ds.Tables[0].Rows[0]["SponserName"].ToString();
                    model.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    model.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    //model.AdharNo = ds.Tables[0].Rows[0]["AdharNumber"].ToString();
                    model.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    model.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    model.State = ds.Tables[0].Rows[0]["State"].ToString();
                    model.City = ds.Tables[0].Rows[0]["City"].ToString();
                    model.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    model.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    model.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    model.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    model.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    model.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    model.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    model.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    model.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    model.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    model.UPIID = ds.Tables[0].Rows[0]["UPIID"].ToString();
                    model.Image = ds.Tables[0].Rows[0]["PanImage"].ToString();

                }
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ViewProfileForAdmin")]
        [OnAction(ButtonName = "Update")]
        public ActionResult ViewProfileForAdmin(AdminReports model, HttpPostedFileBase Image)
        {
            try
            {
                List<SelectListItem> Gender = Common.BindGender();
                ViewBag.Gender = Gender;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                if (Image != null)
                {
                    model.Image = "/PanUpload/" + Guid.NewGuid() + Path.GetExtension(Image.FileName);
                    Image.SaveAs(Path.Combine(Server.MapPath(model.Image)));
                }
                DataSet ds = model.UpdateAdminProfile();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["AdminProfile"] = "User profile updated successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["AdminProfile"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["AdminProfile"] = ex.Message;
            }
            return RedirectToAction("ViewProfileForAdmin", "AdminReports", new { Id = model.Fk_UserId });
        }
        public ActionResult DeleteUerDetails(string Id)
        {
            try
            {
                AdminReports model = new AdminReports();
                model.Fk_UserId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteUerDetails();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "User deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("AssociateList", "AdminReports");
        }
        public ActionResult ViewProfile(string Id)
        {
            AdminReports model = new AdminReports();
            List<SelectListItem> Gender = Common.BindGender();
            ViewBag.Gender = Gender;
            if (Id != null)
            {
                model.Fk_UserId = Id;
                DataSet ds = model.GetAdminProfileDetails();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    ViewBag.Fk_UserId = ds.Tables[0].Rows[0]["PK_UserId"].ToString();
                    ViewBag.SponsorId = ds.Tables[0].Rows[0]["SponsorId"].ToString();
                    ViewBag.SponsorName = ds.Tables[0].Rows[0]["SponserName"].ToString();
                    ViewBag.FirstName = ds.Tables[0].Rows[0]["FirstName"].ToString();
                    ViewBag.LastName = ds.Tables[0].Rows[0]["LastName"].ToString();
                    model.Gender = ds.Tables[0].Rows[0]["Sex"].ToString();
                    ViewBag.PanNo = ds.Tables[0].Rows[0]["PanNumber"].ToString();
                    ViewBag.PinCode = ds.Tables[0].Rows[0]["PinCode"].ToString();
                    ViewBag.State = ds.Tables[0].Rows[0]["State"].ToString();
                    ViewBag.City = ds.Tables[0].Rows[0]["City"].ToString();
                    ViewBag.MobileNo = ds.Tables[0].Rows[0]["Mobile"].ToString();
                    ViewBag.Email = ds.Tables[0].Rows[0]["Email"].ToString();
                    ViewBag.NomineeName = ds.Tables[0].Rows[0]["NomineeName"].ToString();
                    ViewBag.NomineeAge = ds.Tables[0].Rows[0]["NomineeAge"].ToString();
                    ViewBag.NomineeRelation = ds.Tables[0].Rows[0]["NomineeRelation"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.BankName = ds.Tables[0].Rows[0]["MemberBankName"].ToString();
                    ViewBag.BranchName = ds.Tables[0].Rows[0]["MemberBranch"].ToString();
                    ViewBag.AccountNo = ds.Tables[0].Rows[0]["MemberAccNo"].ToString();
                    ViewBag.IFSCCode = ds.Tables[0].Rows[0]["IFSCCode"].ToString();
                    ViewBag.PanImage = ds.Tables[0].Rows[0]["PanImage"].ToString();
                    ViewBag.ProfilePic = ds.Tables[0].Rows[0]["ProfilePic"].ToString();
                    ViewBag.UPI = ds.Tables[0].Rows[0]["UPIID"].ToString();
                    ViewBag.Address = ds.Tables[0].Rows[0]["Address"].ToString();
                    ViewBag.DocumentType = ds.Tables[0].Rows[0]["DocumentType"].ToString();
                    ViewBag.DocumentTypeNumber = ds.Tables[0].Rows[0]["DocumentTypeNumber"].ToString();
                }
            }
            return View(model);
        }
        public ActionResult ViewProfileVeriFy(string Id)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ViewProfileVeriFy();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["verify"] = "Profile verified successfully";
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string TempId = "1707166036842875866";
                        string str = BLSMS.KycApprovel(Name, Status);
                        try
                        {
                            BLSMS.SendSMS(Mobile, str, TempId);
                        }
                        catch
                        {
                        }
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["verify"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["verify"] = ex.Message;
            }
            return RedirectToAction("KYCUpdateDeatilsOfUser", "Admin");
        }
        public ActionResult WalletLedger()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.WalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;

            }
            return View(model);
        }
        [HttpPost]
        [ActionName("WalletLedger")]
        [OnAction(ButtonName = "Search")]
        public ActionResult WalletLedger(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.WalletLedger();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.CrAmount = r["CrAmount"].ToString();
                    obj.DrAmount = r["DrAmount"].ToString();
                    obj.AvailableBalance = r["AvailableBalance"].ToString();
                    lst.Add(obj);
                }
                ViewBag.CrAmount = double.Parse(ds.Tables[0].Compute("sum(CrAmount)", "").ToString()).ToString("n2");
                ViewBag.DrAmount = double.Parse(ds.Tables[0].Compute("sum(DrAmount)", "").ToString()).ToString("n2");
                ViewBag.AvailableBalance = double.Parse(ds.Tables[0].Compute("sum(AvailableBalance)", "").ToString()).ToString("n2");
                model.lstWalletLedger = lst;
            }
            return View(model);
        }
        public ActionResult ActivateByPaymentList()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.Name = model.Name == "" ? null : model.Name;
            DataSet ds = model.GetActivateByPaymentDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Pk_EwalletId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.TransactionDate = r["ActivatationDate"].ToString();
                    lst.Add(obj);
                }
                model.lstActivateByPayment = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ActivateByPaymentList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ActivateByPaymentList(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.LoginId = model.LoginId == "" ? null : model.LoginId;
            model.Name = model.Name == "" ? null : model.Name;
            model.UsedFor = model.UsedFor == "" ? null : model.UsedFor;
            DataSet ds = model.GetActivateByPaymentDetails();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Pk_EwalletId = r["Pk_InvestmentId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.PinAmount = r["PinAmount"].ToString();
                    obj.UsedFor = r["UsedFor"].ToString();
                    obj.BV = r["BV"].ToString();
                    obj.IsCalculated = r["IsCalculated"].ToString();
                    obj.TransactionBy = r["TransactionBy"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.TransactionDate = r["ActivatationDate"].ToString();
                    lst.Add(obj);
                }
                model.lstActivateByPayment = lst;
            }
            return View(model);
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
        public ActionResult DeclineKyc(string Id)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Fk_UserId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeclinedKyc();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["verify"] = "Kyc declined successfully";
                        string Name = ds.Tables[0].Rows[0]["Name"].ToString();
                        string Status = ds.Tables[0].Rows[0]["Status"].ToString();
                        string Mobile = ds.Tables[0].Rows[0]["Mobile"].ToString();
                        string TempId = "1707166036842875866";
                        string str = BLSMS.KycApprovel(Name, Status);
                        try
                        {
                            BLSMS.SendSMS(Mobile, str, TempId);
                        }
                        catch
                        {
                        }
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["verify"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["verify"] = ex.Message;
            }
            return RedirectToAction("KYCUpdateDeatilsOfUser", "Admin");
        }
        public ActionResult DeleteTopUp(string Id)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Pk_investmentId = Id;
                model.UpdatedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.DeleteTopUp();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0][0].ToString() == "1")
                    {
                        TempData["msg"] = "Top-Up deleted successfully";
                    }
                    else if (ds.Tables[0].Rows[0][0].ToString() == "0")
                    {
                        TempData["msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msg"] = ex.Message;
            }
            return RedirectToAction("TopUpReport", "AdminReports");
        }
        public ActionResult ContactList()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            model.Name = model.Name == "" ? null : model.Name;
            DataSet ds = model.ContactList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Name = r["Name"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Subject = r["Subject"].ToString();
                    obj.Message = r["Message"].ToString();
                    obj.Date = r["Date"].ToString();
                    lst.Add(obj);
                }
                model.lstcontact = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("ContactList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult ContactList(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.Name = model.Name == "" ? null : model.Name;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.ContactList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Name = r["Name"].ToString();
                    obj.Email = r["Email"].ToString();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.Subject = r["Subject"].ToString();
                    obj.Message = r["Message"].ToString();
                    obj.Date = r["Date"].ToString();
                    lst.Add(obj);
                }
                model.lstcontact = lst;
            }
            return View(model);
        }
        public ActionResult DirectListForAdmin(string AssociateID, string FK_UserId)
        {
            AssociateBooking model = new AssociateBooking();
            if (AssociateID != null && AssociateID != "")
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {

            }
            model.FK_RootId = FK_UserId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDownlineTree();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {

                }
                else
                {
                    ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        obj.Fk_UserId = r["Pk_UserId"].ToString();
                        obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.FirstName = r["FirstName"].ToString();
                        obj.Status = r["Status"].ToString();
                        obj.ActiveStatus = r["ActiveStatus"].ToString();
                        obj.SponsorID = r["SponsorId"].ToString();
                        obj.SponsorName = r["SponsorName"].ToString();
                        obj.ActivationDate = r["PermanentDate"].ToString();
                        obj.Lvl = r["Level"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }

            }
            return View(model);
        }
        [HttpPost]
        public ActionResult DirectListForAdmin(AssociateBooking model, string AssociateID)
        {
            if (AssociateID != null && AssociateID != "")
            {
                model.Fk_UserId = AssociateID;
            }
            else
            {

            }
            model.FK_RootId = model.Fk_UserId;
            List<AssociateBooking> lst = new List<AssociateBooking>();
            DataSet ds = model.GetDownlineTree();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {

                }
                else
                {
                    ViewBag.Fk_SponsorId = ds.Tables[0].Rows[0]["Fk_SponsorId"].ToString();
                    ViewBag.LoginId = ds.Tables[0].Rows[0]["LoginId"].ToString();
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AssociateBooking obj = new AssociateBooking();
                        obj.Fk_UserId = r["Pk_UserId"].ToString();
                        obj.Fk_SponsorId = r["Fk_SponsorId"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.FirstName = r["FirstName"].ToString();
                        obj.Status = r["Status"].ToString();
                        obj.ActiveStatus = r["ActiveStatus"].ToString();
                        obj.SponsorID = r["SponsorId"].ToString();
                        obj.SponsorName = r["SponsorName"].ToString();
                        obj.ActivationDate = r["PermanentDate"].ToString();
                        obj.Lvl = r["Level"].ToString();
                        lst.Add(obj);
                    }
                    model.lstPlot = lst;
                }

            }
            return View(model);
        }
        #region TreeTTP ForAdmin
        public ActionResult TreeTTPForAdmin(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = LoginId;
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = "MyTrade";
                model.PK_UserId = "1";
                model.LoginId = "MyTrade";
                model.DisplayName = "MyTrade";

                //model.RootAgentCode = "MyTradeMTG";
                //model.PK_UserId = "1";
                //model.LoginId = "MyTradeMTG";
                //model.DisplayName = "MyTradeMTG";
            }
            List<TreeMembers> lst = new List<TreeMembers>();
            List<MemberDetails> lstMember = new List<MemberDetails>();
            DataSet ds = model.GetLevelMembersCountTR1();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                if (ds.Tables[0].Rows[0][0].ToString() == "0")
                {

                }
                else
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        TreeMembers obj = new TreeMembers();
                        obj.LevelName = r["LevelNo"].ToString();
                        obj.NumberOfMembers = r["TotalAssociate"].ToString();
                        lst.Add(obj);
                    }
                    model.lst = lst;
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
                }
            }
            return View(model);
        }
        #endregion
        public ActionResult TreeForAdmin(string LoginId, string Id)
        {
            Tree model = new Tree();
            if (LoginId != "" && LoginId != null)
            {
                model.RootAgentCode = LoginId;
                model.LoginId = LoginId;
                model.PK_UserId = Id;
            }
            else
            {
                model.RootAgentCode = "MyTrade";
                model.PK_UserId = "1";
                model.LoginId = "MyTrade";
                model.DisplayName = "MyTrade";
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
                model.SelfBV = ds.Tables[1].Rows[0]["SelfBV"].ToString();
                model.TeamBV = ds.Tables[1].Rows[0]["TeamBV"].ToString();
                model.SelfBVDollar = Math.Round((Convert.ToDouble(ds.Tables[1].Rows[0]["SelfBV"]) / 76.805), 2).ToString();
                model.TeamBVDollar = Math.Round((Convert.ToDouble(ds.Tables[1].Rows[0]["TeamBV"]) / 76.805), 2).ToString();
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
                    //obj.SelfBVDollar = (Convert.ToDouble(r["SelfBV"]) / 76.805).ToString();
                    //obj.TeamBVDollar = (Convert.ToDouble(r["TeamBV"]) / 76.805).ToString();
                    obj.SponsorName = r["SponsorName"].ToString();
                    obj.Color = r["Color"].ToString();
                    lstMember.Add(obj);
                }
                model.lstMember = lstMember;
            }
            return View(model);
        }
        public ActionResult RechargeListForAdmin(AdminReports model)
        {

            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetRechargeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.OrderNo = r["OrderNo"].ToString();
                    obj.TransactionFor = r["TransactionFor"].ToString();
                    obj.Remark = r["Msg"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ServerOrderId = r["ServerOrderId"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Provider = r["Provider"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("RechargeListForAdmin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult rechargelist(AdminReports model)
        {

            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetRechargeList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Amount = r["Amount"].ToString();
                    obj.OrderNo = r["OrderNo"].ToString();
                    obj.TransactionFor = r["TransactionFor"].ToString();
                    obj.Remark = r["Msg"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.ServerOrderId = r["ServerOrderId"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Provider = r["Provider"].ToString();
                    lst.Add(obj);
                }
                model.lstWalletLedger = lst;
            }
            return View(model);
        }
        #region ClaimRewardReport
        public ActionResult ClaimRewardReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.ClaimRewardReport();

            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.RewardAchieverID = r["PK_RewardAchieverId"].ToString();
                    obj.UserId = r["FK_UserId"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.AssociateName = r["Name"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.PanImage = r["RewardImage"].ToString();
                    obj.Target = r["Target"].ToString();
                    lst.Add(obj);
                }
                model.lstRew = lst;
            }
            return View(model);
        }


        #endregion
        public ActionResult JoiningPackageReport()

        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            try
            {
                DataSet ds = model.getJoiningPackagelist();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AdminReports obj = new AdminReports();
                        obj.SponsorId = r["SponsorId"].ToString();
                        obj.SponsorName = r["SponsorName"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.Package = r["PackageType"].ToString();
                        obj.Date = r["Date"].ToString();
                        lst.Add(obj);
                    }
                    model.lstWalletLedger = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }
        [HttpPost]
        [ActionName("JoiningpackageReport")]
        [OnAction(ButtonName = "btnSearch")]
        public ActionResult JoiningReport(AdminReports model)

        {
            List<AdminReports> lst = new List<AdminReports>();
            try
            {
                model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
                model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
                DataSet ds = model.getJoiningPackagelist();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow r in ds.Tables[0].Rows)
                    {
                        AdminReports obj = new AdminReports();
                        obj.SponsorId = r["SponsorId"].ToString();
                        obj.SponsorName = r["SponsorName"].ToString();
                        obj.LoginId = r["LoginId"].ToString();
                        obj.Name = r["Name"].ToString();
                        obj.Amount = r["Amount"].ToString();
                        obj.BV = r["BV"].ToString();
                        obj.Package = r["PackageType"].ToString();
                        obj.Date = r["Date"].ToString();
                        lst.Add(obj);
                    }
                    model.lstWalletLedger = lst;
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return View(model);
        }

        public ActionResult TDSReport()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetTdsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Amount = r["tdsAmount"].ToString();
                    obj.Date = r["CurrentDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTDSReport = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("TDSReport")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetTDSReport(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetTdsReport();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Mobile = r["Mobile"].ToString();
                    obj.PanNo = r["PanNumber"].ToString();
                    obj.LoginId = r["LoginId"].ToString();
                    obj.Name = r["Name"].ToString();
                    obj.Amount = r["tdsAmount"].ToString();
                    obj.Date = r["CurrentDate"].ToString();
                    lst.Add(obj);
                }
                model.lstTDSReport = lst;
            }
            return View(model);
        }

        public ActionResult BonazaRewardList()
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetBonazaRewardList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.FK_BonazaDetailsId = r["Pk_BonazaDetailsId"].ToString();
                    obj.BusinessTarget = r["BusinessTarget"].ToString();
                    obj.Reward = r["Reward"].ToString();
                    obj.Amount = r["RewardAmount"].ToString();
                    obj.Image = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    lst.Add(obj);
                }
                model.lstBonazaReward = lst;
            }
            return View(model);
        }

        [HttpPost]
        [ActionName("BonazaRewardList")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetBonazaRewardList(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetBonazaRewardList();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Target = r["BusinessTarget"].ToString();
                    obj.Reward = r["Reward"].ToString();
                    obj.Amount = r["RewardAmount"].ToString();
                    obj.Image = r["RewardImage"].ToString();
                    obj.RewardName = r["RewardName"].ToString();
                    obj.FromDate = r["FromDate"].ToString();
                    obj.ToDate = r["ToDate"].ToString();
                    lst.Add(obj);
                }
                model.lstBonazaReward = lst;
            }
            return View(model);
        }

        public ActionResult Bonaza(string BonazaId)
        {
            AdminReports model = new AdminReports();
            #region ddlReward
            int count = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            DataSet ds = model.GetReward();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "-Select-", Value = "" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["Pk_BonazaRewardId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlReward = ddlReward;
            #endregion
            if (BonazaId != null && BonazaId != "")
            {
                model.FK_BonazaDetailsId = BonazaId;
                DataSet bds = model.GetBonazaRewardList();
                if (bds != null && bds.Tables.Count > 0 && bds.Tables[0].Rows.Count > 0)
                {
                    model.Fk_BonazaId = bds.Tables[0].Rows[0]["Fk_BonazaRewardId"].ToString();
                    model.Reward = bds.Tables[0].Rows[0]["Reward"].ToString();
                    model.FK_BonazaDetailsId = bds.Tables[0].Rows[0]["Pk_BonazaDetailsId"].ToString();
                    model.BusinessTarget = bds.Tables[0].Rows[0]["BusinessTarget"].ToString();
                    model.RewardAmount = bds.Tables[0].Rows[0]["RewardAmount"].ToString();
                    model.RewardImage = bds.Tables[0].Rows[0]["RewardImage"].ToString();
                }

            }
            return View(model);
        }


        [HttpPost]
        [ActionName("Bonaza")]
        [OnAction(ButtonName = "save")]
        public ActionResult SaveBonaza(AdminReports model, HttpPostedFileBase RewardImage)
        {
            #region ddlReward
            int count = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            DataSet dss = model.GetReward();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Reward Name", Value = "" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["Pk_BonazaRewardId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlReward = ddlReward;
            #endregion
            try
            {
                if (RewardImage != null)
                {
                    model.RewardImage = "/BannerImage/" + Guid.NewGuid() + Path.GetExtension(RewardImage.FileName);
                    RewardImage.SaveAs(Path.Combine(Server.MapPath(model.RewardImage)));
                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.SaveBonaza();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Bonaza"] = "Bonaza Save Successfully !!";
                    }
                    else
                    {
                        TempData["Bonaza"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Bonaza"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Bonaza"] = ex.Message;
            }
            return RedirectToAction("Bonaza", "AdminReports");
        }
        [HttpPost]
        [ActionName("Bonaza")]
        [OnAction(ButtonName = "Update")]
        public ActionResult UpdateBonaza(AdminReports model, HttpPostedFileBase RewardImage)
        {
            #region ddlReward
            int count = 0;
            List<SelectListItem> ddlReward = new List<SelectListItem>();
            DataSet dss = model.GetReward();
            if (dss != null && dss.Tables.Count > 0 && dss.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in dss.Tables[0].Rows)
                {
                    if (count == 0)
                    {
                        ddlReward.Add(new SelectListItem { Text = "Reward Name", Value = "" });
                    }
                    ddlReward.Add(new SelectListItem { Text = r["RewardName"].ToString(), Value = r["Pk_BonazaRewardId"].ToString() });
                    count = count + 1;
                }
            }

            ViewBag.ddlReward = ddlReward;
            #endregion
            try
            {
                if (RewardImage != null)
                {
                    model.RewardImage = "/BannerImage/" + Guid.NewGuid() + Path.GetExtension(RewardImage.FileName);
                    RewardImage.SaveAs(Path.Combine(Server.MapPath(model.RewardImage)));
                }
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.updateBonaza();
                if (ds.Tables != null && ds.Tables[0].Rows.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["Msg"].ToString() == "1")
                    {
                        TempData["Bonaza"] = "Bonaza Details Update Successfully !!";
                    }
                    else
                    {
                        TempData["Bonaza"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
                else
                {
                    TempData["Bonaza"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                }
            }
            catch (Exception ex)
            {
                TempData["Bonaza"] = ex.Message;
            }
            return RedirectToAction("Bonaza", "AdminReports");
        }
        public ActionResult DeleteBonazaReward(string BonazaId)
        {
            string FormName = "";
            string Controller = "";
            try
            {
                AdminReports obj = new AdminReports();
                obj.FK_BonazaDetailsId = BonazaId;
                obj.AddedBy = Session["PK_AdminId"].ToString();
                DataSet ds = obj.deleteBonaza();
                if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
                {
                    if ((ds.Tables[0].Rows[0][0].ToString() == "1"))
                    {
                        TempData["Msg"] = "Reward Details deleted successfully";
                        FormName = "BonazaRewardList";
                        Controller = "AdminReports";
                    }
                    else
                    {
                        TempData["Msg"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "BonazaRewardList";
                        Controller = "AdminReports";
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["Msg"] = ex.Message;
                FormName = "BonazaRewardList";
                Controller = "AdminReports";
            }

            return RedirectToAction(FormName, Controller);
        }




        public ActionResult SalesReportsForAdmin(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            DataSet ds = model.GetSalesReportforAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Pk_FranchisetransferId = r["Pk_FranchisetransferId"].ToString();
                    obj.UserContactAddress = r["UserContactAddress"].ToString();
                    obj.UserName = r["UserName"].ToString();
                    obj.FranchiseeContactAddress = r["FranchiseeContactAddress"].ToString();
                    obj.FirmName = r["FirmName"].ToString();
                    obj.mtgtoken = r["mtgtoken"].ToString();
                    obj.TransferCharge = r["TransferCharge"].ToString();
                    obj.SaleDate = r["SaleDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    obj.SaleRequestDate = r["SaleRequestDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.BankName = r["Bankname"].ToString();
                    obj.BankBranch = r["BranchName"].ToString();
                    obj.TransactionId = r["TransactionId"].ToString();
                    obj.DocumentUrl = r["DocumentUrl"].ToString();
                    obj.FranchiseApprovalRejectionDate = r["FranchiseApprovalRejectionDate"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();


                    lst.Add(obj);
                }
                model.lstsalesreports = lst;
            }
            return View(model);
        }


        [HttpPost]
        [ActionName("SalesReportsForAdmin")]
        [OnAction(ButtonName = "Search")]
        public ActionResult GetSalesReportsForAdmin(AdminReports model)
        {
            List<AdminReports> lst = new List<AdminReports>();
            model.UserCA = model.UserCA == "" ? null : model.UserCA;
            model.FranchiseeCA = model.FranchiseeCA == "" ? null : model.FranchiseeCA;
            model.Status = model.Status == "0" ? null : model.Status;
            model.FromDate = string.IsNullOrEmpty(model.FromDate) ? null : Common.ConvertToSystemDate(model.FromDate, "dd/MM/yyyy");
            model.ToDate = string.IsNullOrEmpty(model.ToDate) ? null : Common.ConvertToSystemDate(model.ToDate, "dd/MM/yyyy");
            DataSet ds = model.GetSalesReportforAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Pk_FranchisetransferId = r["Pk_FranchisetransferId"].ToString();
                    obj.UserContactAddress = r["UserContactAddress"].ToString();
                    obj.UserName = r["UserName"].ToString();
                    obj.FranchiseeContactAddress = r["FranchiseeContactAddress"].ToString();
                    obj.FirmName = r["FirmName"].ToString();
                    obj.mtgtoken = r["mtgtoken"].ToString();
                    obj.TransferCharge = r["TransferCharge"].ToString();
                    obj.SaleDate = r["SaleDate"].ToString();
                    obj.Status = r["Status"].ToString();

                    obj.SaleRequestDate = r["SaleRequestDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.BankName = r["Bankname"].ToString();
                    obj.BankBranch = r["BranchName"].ToString();
                    obj.TransactionId = r["TransactionId"].ToString();
                    obj.DocumentUrl = r["DocumentUrl"].ToString();
                    obj.FranchiseApprovalRejectionDate = r["FranchiseApprovalRejectionDate"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();

                    lst.Add(obj);
                }
                model.lstsalesreports = lst;
            }
            return View(model);
        }



        public ActionResult ViewReports(string Id)
        {
            AdminReports model = new AdminReports();
            List<AdminReports> lst = new List<AdminReports>();
            model.Pk_FranchisetransferId = Id;
            DataSet ds = model.GetViewSalesReportforAdmin();
            if (ds != null && ds.Tables.Count > 0 && ds.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow r in ds.Tables[0].Rows)
                {
                    AdminReports obj = new AdminReports();
                    obj.Pk_FranchisetransferId = r["Pk_FranchisetransferId"].ToString();
                    obj.UserContactAddress = r["UserContactAddress"].ToString();
                    obj.UserName = r["UserName"].ToString();
                    obj.FranchiseeContactAddress = r["FranchiseeContactAddress"].ToString();
                    obj.FirmName = r["FirmName"].ToString();
                    obj.SaleRequestDate = r["SaleRequestDate"].ToString();
                    obj.PaymentMode = r["PaymentMode"].ToString();
                    obj.BankName = r["Bankname"].ToString();
                    obj.BankBranch = r["BranchName"].ToString();
                    obj.TransactionId = r["TransactionId"].ToString();
                    obj.DocumentUrl = r["DocumentUrl"].ToString();
                    obj.FranchiseApprovalRejectionDate = r["FranchiseApprovalRejectionDate"].ToString();
                    obj.TransactionDate = r["TransactionDate"].ToString();
                    obj.Status = r["Status"].ToString();
                    lst.Add(obj);
                }
                model.lstsaleViewReports = lst;
            }
            return View(model);
        }




        public ActionResult ApproveRequest(string Pk_FranchitraferId,string Status)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Pk_FranchisetransferId = Pk_FranchitraferId;
                model.Status = Status;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.ApproveRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        model.Result = "yes";
                        TempData["msgs"] = "Request approved successfully. ";
                    }
                    else
                    {
                        TempData["msgs"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgs"] = ex.Message;
            }
            //return RedirectToAction("ViewReports", "AdminReports", new { Id = Id });
            return Json(model,JsonRequestBehavior.AllowGet);
        }

        public ActionResult RejectRequest(string Pk_FranchitraferId, string Status)
        {
            AdminReports model = new AdminReports();
            try
            {
                model.Pk_FranchisetransferId = Pk_FranchitraferId;
                model.Status = Status;
                model.AddedBy = Session["Pk_AdminId"].ToString();
                DataSet ds = model.RejectRequest();
                if (ds != null && ds.Tables.Count > 0)
                {
                    if (ds.Tables[0].Rows[0]["msg"].ToString() == "1")
                    {
                        model.Result = "yes";
                        TempData["msgs"] = "Request rejected successfully. ";
                    }
                    else
                    {
                        TempData["msgs"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                    }
                }
            }
            catch (Exception ex)
            {
                TempData["msgs"] = ex.Message;
            }
            //return RedirectToAction("ViewReports", "AdminReports", new { Id = Id });
            return Json(model, JsonRequestBehavior.AllowGet);
        }



    }
}