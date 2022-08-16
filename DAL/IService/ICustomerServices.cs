using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface ICustomerServices:IAuthorizeServices
    {
       List<CustomersModel> getCustomerDetails();
       void deleteCustomers(int id);
       void createCustomers(CustomersModel model);
       //bool authorize(string userId, string actionName);
    }
}
