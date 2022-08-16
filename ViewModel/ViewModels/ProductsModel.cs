using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class ProductsModel
    {
        [Required]
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public Int16 ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public int StoreId { get; set; }
        public int? Quantity { get; set; }
    }
    public class CategoryAndBrand
    {
        public List<CategoriesModel> cat { get; set; }
        public List<BrandModel> brn { get; set; }
    }
}
  