using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using DAL.IService;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class CategoriesController : Controller
    {
        private readonly ICategoriesServices _categoriesServices;
        public CategoriesController(ICategoriesServices categoriesServices)
        {
            _categoriesServices = categoriesServices;
        }
        // GET: Categories
        public ActionResult ShowCategories()
        {
            var Actionlink = Action_Name.customeraction.GetEnumDescription();
            if (_categoriesServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {

                return View();
            }
            else
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult GetCategoryDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = 9;
            //int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<CategoriesModel> emplist = new List<CategoriesModel>();
            emplist = _categoriesServices.getCategoryDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.CategoryId.ToString().Contains(searchValue.ToLower())
                    || x.CategoryName.ToLower().Contains(searchValue.ToLower())).ToList<CategoriesModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<CategoriesModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<CategoriesModel>();
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
        public ActionResult DeleteCategory(int id)
        {
            _categoriesServices.deleteCategory(id);
            return Json(new { text = "success" });
        }

        [HttpPost]
        public ActionResult CreateCategory(CategoriesModel model)
        {
            if (ModelState.IsValid)
            {
                _categoriesServices.createCategory(model);
                return Json(new { text = "success" });
            }
            else
                return HttpNotFound();
        }
    }
    }