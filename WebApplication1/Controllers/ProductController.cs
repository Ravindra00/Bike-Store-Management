
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Linq.Dynamic;
using System.Data.SqlClient;
using Microsoft.AspNet.Identity;
using DAL.IService;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class ProductController : Controller
    {
        private IProductServices productServices;
        public ProductController(IProductServices _productServices)
        {
            productServices = _productServices;
        }
        public ActionResult ShowProduct()
        {

            var Actionlink = Action_Name.productaction.GetEnumDescription();
            if (productServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");
        }

        [HttpPost]
        public ActionResult GetList()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = 7;
            //int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<ProductModel> emplist = new List<ProductModel>();
            emplist = productServices.getProductDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.ProductId.ToString().Contains(searchValue.ToLower())
                    || x.ProductName.ToLower().Contains(searchValue.ToLower())
                    || x.CategoryId.ToString().Contains(searchValue.ToLower())
                    || x.BrandId.ToString().Contains(searchValue.ToLower())
                    || x.ModelYear.ToString().Contains(searchValue.ToLower())
                    || x.ListPrice.ToString().Contains(searchValue.ToLower())).ToList<ProductModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<ProductModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<ProductModel>();
            return Json(new
            {
                data = emplist,
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            },
                JsonRequestBehavior.AllowGet);
        }


        // using SPf 
        //[HttpPost]
        //public ActionResult GetList()
        //{
        //    List<ProductModel> data = new List<ProductModel>();
        //    var start = (Convert.ToInt32(Request["start"]));

        //    var Length = (Convert.ToInt32(Request["length"])) == 0 ? 8 : (Convert.ToInt32(Request["length"]));
        //    //var Length = 5;

        //    var searchvalue = Request["search[value]"] ?? "";
        //    var sortcoloumnIndex = Convert.ToInt32(Request["order[0][column]"]);
        //    //var sortColumn = Request["columns[" + Request["order[0][column]"] + "][name]"];

        //    //var sortDirection = Request["order[0][dir]"] ?? "asc";
        //    string sortDirection = Request["order[0][dir]"];

        //    data = productServices.ServerSideDatatable(start, Length, searchvalue, sortcoloumnIndex, sortDirection);

        //    var recordsTotal = data.Count > 0 ? data[0].TotalRecords : 0;
        //    return Json(new
        //    {
        //        data = data,
        //        draw = Request["draw"],
        //        recordsTotal = recordsTotal,
        //        recordsFiltered = recordsTotal
        //    },
        //        JsonRequestBehavior.AllowGet);
        //}

        [HttpPost]
        public ActionResult GetcategoryAndBrand()
        {
            var data = productServices.getCategoryAndBrand();
            return Json(data);
        }

        [HttpPost]
        public ActionResult SaveProduct(ProductsModel model)
        {
            productServices.saveProduct(model);
            return Json(new { text = "success" });
        }
    }

}