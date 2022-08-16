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

    public class UserController : Controller
    {
        private readonly IUserServices userServices;
        public UserController(IUserServices _userServices)
        {
            userServices = _userServices;
        }
        // GET: User
        public ActionResult CreateRegistration()
        {
            var Actionlink = Action_Name.registrationaction.GetEnumDescription();
            if (userServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                var data = userServices.getUser();
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");


        }

        [HttpPost]
        public ActionResult GetRoles()
        {
            //var data = userServices.getRoles();
            var data = userServices.getRoles();
            return Json(data);

        }
        //GET: role managemnet
        public ActionResult RoleManagement()
        {
            var Actionlink = Action_Name.customeraction.GetEnumDescription();
            if (userServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");

        }
        [HttpPost]

        public ActionResult GetUser()
        {
            var data = userServices.getUser();
            return Json(data);
        }

        [HttpPost]
        public ActionResult GetUsersDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            //int length = 8;
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<UserViewModel> emplist = new List<UserViewModel>();
            emplist = userServices.getUser();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.UserId.ToString().Contains(searchValue.ToLower())
                    || x.UserName.ToLower().Contains(searchValue.ToLower())
                    || x.EmailAddress.ToLower().Contains(searchValue.ToLower())
                    //|| x.Password.ToLower().Contains(searchValue.ToLower())
                    || x.PhoneNumber.ToLower().Contains(searchValue.ToLower())
                    || x.RoleName.ToLower().Contains(searchValue.ToLower())).ToList<UserViewModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<UserViewModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<UserViewModel>();
            return Json(new
            {
                data = emplist,
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            },
                JsonRequestBehavior.AllowGet);
        }

    
        public ActionResult DeleteUser(string id)
        {
            userServices.deleteUser(id);
            return Json(new { text="Delete Successfull !" });
        }
        public ActionResult GetMenuList()
        {
            var data = userServices.getMenuList();
            return PartialView("_GetMenuList", data);
        }

        //save menu with role id
        [HttpPost]
        public ActionResult SaveRoleMenu(RoleWithMenuModel model)
        {
            userServices.saveRoleMenu(model);
            return Json(new { text = "success !" });
        }


        //for check role menu
        [HttpPost]
        public ActionResult CheckRoleMenu(int id)
        {
             var data =userServices.checkRoleMenu(id);
            return Json(data);
        }
    }
}