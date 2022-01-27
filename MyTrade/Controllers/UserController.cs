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
    public class UserController : UserBaseController
    {
        // GET: User
        public ActionResult UserDashBoard()
        {
            return View();
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
                        TempData["Activated"] = "User Activated Successfully";
                        FormName = "UserDashboard";
                        Controller = "User";
                    }
                    else
                    {
                        TempData["Activated"] = ds.Tables[0].Rows[0]["ErrorMessage"].ToString();
                        FormName = "CompleteRegistration";
                        Controller = "Home";
                    }

                }
                else {
                    FormName = "CompleteRegistration";
                    Controller = "Home";
                }

            }
            catch (Exception ex)
            {
                TempData["Activated"] = ex.Message;
                FormName = "CompleteRegistration";
                Controller = "Home";
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
    }
}