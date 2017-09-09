using RFO.AspNet.Utilities.Attribute;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFO.Website.Controllers
{
    public class HomeController : Controller
    {
        /// <summary>
        /// Indexes this instance.
        /// </summary>
        /// <returns></returns>
        [CompressContent]
        public ActionResult Index()
        {
            return RedirectToAction("Index", "Back", new { Area = "Admin" });
        }
    }
}