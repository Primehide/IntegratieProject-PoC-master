using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace PoC_UI.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            IAccountMgr alertMgr = new AccountMgr();
            IEntiteitMgr entiteitMgr = new EntiteitMgr();
            Models.PoCViewModel model = new Models.PoCViewModel()
            {
                Alerts = alertMgr.getAlleAlerts(),
                Entiteiten = entiteitMgr.getAlleEntiteiten()
            };
            return View(model);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        /*
        public ActionResult ToonUsers()
        {
            IAccountMgr accountMgr = new AccountMgr();
            return View(accountMgr.getAlleAccounts());
        }
        */

        public ActionResult ShowAlerts(int id)
        {
            IAccountMgr alertMgr = new AccountMgr();
            return View(alertMgr.getAllertOnAccountId(id));
        }
    }
}