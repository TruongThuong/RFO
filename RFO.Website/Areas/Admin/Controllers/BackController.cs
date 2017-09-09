using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RFO.Website.Areas.Admin.Controllers
{
    public class BackController : Controller
    {
        // GET: Admin/Back
        public ActionResult Index()
        {
            return View();
        }
    }
}