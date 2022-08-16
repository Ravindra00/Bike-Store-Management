using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class CategoriesModel:BrandModel
    {
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
    }
}