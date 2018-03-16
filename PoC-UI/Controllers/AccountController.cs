using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL;

namespace PoC_UI.Controllers
{
    public class AccountController : Controller
    {
        IAccountMgr alertMgr;

        // GET: Alert
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GenereerAlerts()
        {
            alertMgr = new AccountMgr();
            alertMgr.genereerAlerts();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}