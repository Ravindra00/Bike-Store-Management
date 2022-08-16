using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using EnumsNET;
using DAL.IService;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]

    public class CustomersController : Controller
    {
        private readonly ICustomerServices CustomerServices;
        public CustomersController(ICustomerServices _CustomerServices)
        {
            CustomerServices = _CustomerServices;
        }
        // GET: Customers
        public ActionResult ShowCustomers()
        {
            //string actionName = this.ControllerContext.RouteData.Values["action"].ToString();
            //string controllerName = this.ControllerContext.RouteData.Values["controller"].ToString();
            var Actionlink = Action_Name.customeraction.GetEnumDescription();
            if (CustomerServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult GetCustomersDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            int length = 7;
            //int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<CustomersModel> emplist = new List<CustomersModel>();
            emplist = CustomerServices.getCustomerDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.CustomerId.ToString().Contains(searchValue.ToLower())
                    || x.FirstName.ToLower().Contains(searchValue.ToLower())
                    //|| x.LastName.ToLower().Contains(searchValue.ToLower())
                    || x.Phone.ToLower().Contains(searchValue.ToLower())
                    || x.EmailAddress.ToLower().Contains(searchValue.ToLower())
                    || x.Street.ToLower().Contains(searchValue.ToLower())
                    || x.City.ToLower().Contains(searchValue.ToLower())
                    || x.State.ToLower().Contains(searchValue.ToLower())
                    || x.ZipCode.ToLower().Contains(searchValue.ToLower())).ToList<CustomersModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<CustomersModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<CustomersModel>();
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
        public ActionResult DeleteCustomers(int id)
        {
            CustomerServices.deleteCustomers(id);
            return Json(new { text = "success" });
        }
    
        [HttpPost]
        public ActionResult CreateCustomers(CustomersModel model)
        {
            if (ModelState.IsValid)
            {
                CustomerServices.createCustomers(model);
                return Json(new { text = "success" });
            }
            else
                return HttpNotFound();
        }
    }
}