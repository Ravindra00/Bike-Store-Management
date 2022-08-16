using DAL.IService;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ViewModels;

namespace WebApplication1.Controllers.api
{
    public class StaffController : ApiController
    {
        //private readonly BikeStoresEntities et;
        ////public StaffController() { }
        //public StaffController(BikeStoresEntities _et)
        //{
        //    et = _et;
        //}

        //[HttpGet]
        //// GET: Staff
        //public List<StaffModel> ShowStaffsDetails()
        //{
        //    return staffServices.GetStaffsDetails();
        //}


        BikeStoresEntities et = new BikeStoresEntities();
        [HttpGet]

        public List<StaffModel> ShowStaffsDetails()
        {
            List<StaffModel> staffModel = (from p in et.Staffs
                                           select new StaffModel
                                           {
                                               StaffID = p.staff_id,
                                               FirstName = p.first_name,
                                               LastName = p.last_name,
                                               Email = p.email,
                                               PhoneNo = p.phone,
                                               Active = p.active,
                                               StoreID = p.store_id,
                                               ManagerID = p.manager_id,
                                           }).ToList();
            return staffModel;
        }

    }
}
