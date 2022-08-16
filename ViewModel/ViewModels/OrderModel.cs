using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class OrderModel
    {
        public List<ProductModel> Products { get; set; }
        public List<CategoriesModel> Categories { get; set; }   
        public List<CustomersModel> Customers { get; set; }
        public List<StoreModel> Store { get; set; }
        public List<StaffModel> Staff { get; set; }
    }
}