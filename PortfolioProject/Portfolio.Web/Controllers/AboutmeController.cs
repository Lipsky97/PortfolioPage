using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Portfolio.Web.Controllers
{
    public class AboutmeController : Controller
    {
        public AboutmeController() { }
        public ActionResult Index()
        {
            return View();
        }
    }
}