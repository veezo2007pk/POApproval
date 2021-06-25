using POApproval.GlobalInfo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace POApproval.Controllers
{
    public class HomeController : Controller
    {
        [SessionAuthorize]
        public ActionResult Index()
        {
            return RedirectToAction("SearchPO", "PO");
            //return View();
        }
       
    }
}