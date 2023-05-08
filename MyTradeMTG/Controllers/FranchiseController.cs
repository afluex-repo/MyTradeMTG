using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyTradeMTG.Controllers
{
    public class FranchiseController : FranchiseBaseController
    {
        // GET: Franchise
        public ActionResult FranchiseDashBoard()
        {
            return View();
        }
    }
}