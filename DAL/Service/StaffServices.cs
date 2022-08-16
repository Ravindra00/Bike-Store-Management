using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace DAL.Service
{
    public class StaffServices : AuthorizeServices, IStaffServices
    {
        BikeStoresEntities bikeStaffDbContext;// = new BikeStoresEntities();

        public StaffServices(BikeStoresEntities _bikeStaffDbContext):base(_bikeStaffDbContext)
        {
            bikeStaffDbContext = _bikeStaffDbContext;
        }
 
        public void DeleteStaffDetails(int id)
        {
            var dt = bikeStaffDbContext.Staffs.Where(x=>x.manager_id == id).ToList();
            foreach(var d in dt)
            {
                d.manager_id = null;
            }
            var data = bikeStaffDbContext.Staffs.Where(x => x.staff_id == id).FirstOrDefault();
            if (data != null)
            {
                bikeStaffDbContext.Staffs.Remove(data);
                bikeStaffDbContext.SaveChanges();
            }
        }

        public List<StaffModel> GetStaffsDetails()
        {
            List<StaffModel> staffModel =
            (from p in bikeStaffDbContext.Staffs
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

        public StaffModel getStoreAndManager( int id)
        {
            StaffModel staff = new StaffModel
            {
               
                store = (from b in bikeStaffDbContext.Stores
                         select new StoreModel
                         {
                             StoreId = b.store_id,
                             StoreName = b.store_name
                         }).ToList(),
                staffs = (from b in bikeStaffDbContext.Staffs
                          select new StaffModel
                          {
                              StaffID = b.staff_id,
                              FirstName = b.first_name,
                              LastName = b.last_name
                          }).ToList(),

            };
            if (id != 0)
            {
                var _staff = bikeStaffDbContext.Staffs.Where(x => x.staff_id == id).FirstOrDefault();
                staff.StaffID = _staff.staff_id;
                staff.FirstName = _staff.first_name;
                staff.LastName = _staff.last_name;
                staff.PhoneNo = _staff.phone;
                staff.Active = _staff.active;
                staff.StoreID = _staff.store_id;
                staff.ManagerID = _staff.manager_id;
                staff.Email = _staff.email;
            }
            return staff;
        }

        public void AddOrUpdateStaffDetail(StaffModel model)
        {
            //if (model.StaffID == 0)
            //{
            //    Staff _staff = new Staff();
            //}
            //else {
            //    var _staff = bikeStaffDbContext.Staffs.Where(x => x.staff_id == model.StaffID).FirstOrDefault();
            //}

            if (model.StaffID == 0)
            {
                Staff _staff = new Staff();
                _staff.staff_id = model.StaffID;
                _staff.first_name = model.FirstName;
                _staff.last_name = model.LastName;
                _staff.email = model.Email;
                _staff.phone = model.PhoneNo;
                _staff.active = model.Active;
                _staff.manager_id = model.ManagerID;
                _staff.store_id = model.StoreID;
                _staff.CreatedBy = 0;
                _staff.CreatedDate = System.DateTime.Now;
                bikeStaffDbContext.Staffs.Add(_staff);
            }
            else
            {
                var _staff = bikeStaffDbContext.Staffs.Where(x => x.staff_id == model.StaffID).FirstOrDefault();
                _staff.staff_id = model.StaffID;
                _staff.first_name = model.FirstName;
                _staff.last_name = model.LastName;
                _staff.email = model.Email;
                _staff.phone = model.PhoneNo;
                _staff.active = model.Active;
                _staff.manager_id = model.ManagerID;
                _staff.store_id = model.StoreID;
            }
            bikeStaffDbContext.SaveChanges();
        }
    }
}




