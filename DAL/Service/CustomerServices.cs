using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;

namespace DAL.Service
{
    public class CustomerServices :  AuthorizeServices,ICustomerServices
    {
        private readonly BikeStoresEntities _context;

        public CustomerServices(BikeStoresEntities context) : base(context)
        {
            _context = context;
        }



        public List<CustomersModel> getCustomerDetails()
        {
            List<CustomersModel> customers = (from b in _context.Customers
                                       select new CustomersModel
                                       {
                                           CustomerId = b.customer_id,
                                           FirstName = b.first_name +" "+ b.last_name,
                                           //LastName = b.last_name,
                                           Phone = b.phone,
                                           EmailAddress = b.email,
                                           Street = b.street,
                                           City = b.city,
                                           State = b.state,
                                           ZipCode = b.zip_code
                                       }).ToList();
            return customers;
        }
        public void deleteCustomers(int id)
        {
            var data =_context.Customers.Where(x=>x.customer_id == id).FirstOrDefault();
            _context.Customers.Remove(data);
            _context.SaveChanges();
        }

        public void createCustomers(CustomersModel model)
        {
            Customer _customer = new Customer
            {
                first_name = model.FirstName,
                last_name = model.LastName,
                phone = model.Phone,
                email = model.EmailAddress,
                street = model.Street,
                city = model.City,
                state = model.State,
                zip_code = model.ZipCode,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
            };
            _context.Customers.Add(_customer);
            _context.SaveChanges();
           
        }

        //public bool authorize(string userId, string actionName)
        //{
        //   IQueryable<Menu> list = (from a in _context.AspNetUsers
        //                                       join b in _context.UserRoles on a.Id equals b.user_id
        //                                       join rm in _context.RoleMenus on b.role_id equals rm.Role_id
        //                                       join m in _context.Menus on rm.Menu_id equals m.Id
        //                                       where a.Id == userId
        //                                       select m);
        //    var data = list.Where(a => a.ActionName == actionName).FirstOrDefault();
        //    var resutl = data != null ? true : false;   
        //    return resutl;



        //}

    }
}