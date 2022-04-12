using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using RestSharp;

namespace MyTrade.Controllers
{
    public class RechargeController : Controller
    {
        // GET: Recharge
        public ActionResult Recharge()
        {
            return View();
        }
        public ActionResult GetOperator()
        {
            var client = new RestClient("https://www.kwikapi.com/api/v2/operator_codes.php?api_key=be2609-04f9b3-b9e057-2aa5a1-f7bd1c");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            IRestResponse response = client.Execute(request);
            return Json(response.Content,JsonRequestBehavior.AllowGet);
        }
    }
}