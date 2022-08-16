using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class TestingController : Controller
    {
        // GET: Test2
        public ActionResult DataTable()
        {
            return View();
        }

        public ActionResult PdfDownloader()
        {
            return View();
        }

        public ActionResult jspPDF()
        {
            return View();
        }
        public ActionResult CardToPanel()
        {
            return View();
        }

    }
}