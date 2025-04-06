using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DevExtremeLibreria.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            return View();
        }
        public ActionResult Autor()
        {
            return View();
        }

        public ActionResult Libro()
        {
            return View();
        }
        public ActionResult Cargo()
        {
            return View();
        }

        public ActionResult Sucursal()
        {
            return View();
        }
        public ActionResult Lector()
        {
            return View();
        }
    }
}