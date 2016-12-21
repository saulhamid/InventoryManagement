using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Inven_Management.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        [Authorize]
        public ActionResult Index()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return RedirectToAction("Index", "Home", new { area = "InventoryManagement" });
        }
        public ActionResult Invent()
        {
            ViewBag.Message = "Modify this template to jump-start your ASP.NET MVC application.";

            return RedirectToAction("Index", "Home", new { area = "InventoryManagement" });
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your app description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}
