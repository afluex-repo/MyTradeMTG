using Mytrade.Controllers;
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