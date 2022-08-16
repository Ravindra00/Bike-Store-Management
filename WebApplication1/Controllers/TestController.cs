using DAL.IService;
using LinqKit;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Web.Mvc;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class TestController : Controller
    {

        private readonly ITestServices testServices;
        public TestController(ITestServices _testServices)
        {
            testServices = _testServices;
        }
        public ActionResult Add()
        { 
            return View();
        }
        [HttpPost]
        public ActionResult Add(TestModel model)
        {
            testServices.Add(model);
            return RedirectToAction("Add");


        }
        public ActionResult ShowBrand()
        {
            var data = testServices.getDetails();

            return View(data);
        }       
        public ActionResult Delete(DeleteModel delete)
        {
            testServices.Delete(delete);
            return RedirectToAction("ShowBrand");  
        }
        public ActionResult ShowProudct()
        {
            var data = testServices.getDetails();

            return View(data);

        }

    }
}