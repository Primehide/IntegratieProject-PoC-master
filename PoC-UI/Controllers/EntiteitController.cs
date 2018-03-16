using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BL;

namespace PoC_UI.Controllers
{
    public class EntiteitController : Controller
    {
        IEntiteitMgr mgr;

        // GET: Entiteit
        public ActionResult Index()
        {
            return View();
        }

        public void LinkPost()
        {
            mgr = new EntiteitMgr();
            mgr.LinkPosts();
        }
    }
}