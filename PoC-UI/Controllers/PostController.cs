using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BL;

namespace PoC_UI.Controllers
{
    public class PostController : Controller
    {

        // GET: Post
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public void SyncData()
        {
            IPostMgr postMgr = new PostMgr();
            postMgr.ConvertJsonPosts();
            //return new HttpStatusCodeResult(HttpStatusCode.OK);
        }
    }
}