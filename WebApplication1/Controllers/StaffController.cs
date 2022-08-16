
using DAL.IService;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ViewModels;

namespace WebApplication1.Controllers
{ 
    [Authorize]
    public class StaffController : Controller
    {
        private readonly IStaffServices staffServices;
        public StaffController(IStaffServices _staffServices)
        {
            staffServices = _staffServices;
        }       

        // GET: Staff
        public ActionResult ShowStaffsDetails()
        {
            var Actionlink = Action_Name.staffaction.GetEnumDescription();
            if (staffServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                var data = staffServices.GetStaffsDetails();
                return View(data);
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult DeleteStaffDetails(int id)
        {
            staffServices.DeleteStaffDetails(id);
            return Json(new { success = true, responseText = "deleted" });

        }

        public ActionResult AddOrEditStaffDetails(int id=0)
        {
            var Actionlink = Action_Name.staffaction.GetEnumDescription();
            if (staffServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                var data = staffServices.getStoreAndManager(id);
                return View(data);
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");

        }
        [HttpPost]
        public ActionResult AddOrEditStaffDetails(StaffModel model)
        {
            staffServices.AddOrUpdateStaffDetail(model);
            return RedirectToAction("ShowStaffsDetails");
        }
        [HttpPost]
        public ActionResult Test(StaffModel model)
        {
            //return RedirectToAction("ShowStaffsDetails");
            return Json(new { success = true, responseText = "successfull" });

        }

    }
}