using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;
using MyTrade.Models;
using Mytrade.Controllers;

namespace MyTrade.Controllers
{
    public class RechargeController : UserBaseController
    {
        // GET: Recharge
        public ActionResult Recharge()
        {
            return View();
        }
        public ActionResult GetOperator()
        {
            var client = new RestClient("https://www.kwikapi.com/api/v2/operator_codes.php?api_key=" + RechargeModel.APIKey);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
        public ActionResult AutoFillOperator(string MobileNo)
        {
            var client = new RestClient("https://www.kwikapi.com/api/v2/operator_fetch.php");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("api_key", RechargeModel.APIKey);
            request.AddParameter("number", MobileNo);
            IRestResponse response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetCircle()
        {
            var client = new RestClient("https://www.kwikapi.com/api/v2/circle_codes.php?api_key="+ RechargeModel.APIKey);
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(response.Content, JsonRequestBehavior.AllowGet);
        }
        public ActionResult GetPlan(string state_code,string opid)
        {
            var client = new RestClient("https://www.kwikapi.com/api/v2/recharge_plans.php");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddParameter("api_key", RechargeModel.APIKey);
            request.AddParameter("state_code", state_code);
            request.AddParameter("opid", opid);
            IRestResponse response = client.Execute(request);
            return Json(response.Content,JsonRequestBehavior.AllowGet);
        }
    }
}