using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using DAL.IService;
using ViewModels;
using WebApplication1.Helpers;

namespace WebApplication1.Controllers
{
    //[Authorize(Roles = "admin"/*,Users ="bikram123@gmail.com"*//*,Order ='1'*/)]
    [Authorize]
    public class BrandController : Controller
    {
        private readonly IBrandServices _brandServices;
        public BrandController(IBrandServices brandServices)
        {
            _brandServices = brandServices;
        }


        [DynamicRoleAuthorize]
        // GET: Brand
        public ActionResult ShowBrand()
        {
            var Actionlink = Action_Name.brandaction.GetEnumDescription();
            if (_brandServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult GetBrandDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = 9;
            //int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<BrandModel> emplist = new List<BrandModel>();
            emplist = _brandServices.getBrandDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.BrandId.ToString().Contains(searchValue.ToLower())
                    || x.BrandName.ToLower().Contains(searchValue.ToLower())).ToList<BrandModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<BrandModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<BrandModel>();
            return Json(new
            {
                data = emplist,
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            },
                JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult DeleteBrand(int id)
        {
            _brandServices.deleteBrand(id);
            return Json(new { text = "success" });
        }

        [HttpPost]
        public ActionResult CreateBrand(BrandModel model)
        {
            if (ModelState.IsValid)
            {
                _brandServices.createBrand(model);
                return Json(new { text = "success" });
            }
            else
                return HttpNotFound();
        }
    }
}