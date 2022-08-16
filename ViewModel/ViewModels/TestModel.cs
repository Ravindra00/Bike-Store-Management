using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class TestModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public short ModelYear { get; set; }
        public int ListPrice { get; set; }
        public string BrandName { get; set; }
        public string CategoryName { get; set; }
        public List<ProductModel> Products { get; set; }
        public List<BrandModel> Brands { get; set; }
        public List<CategoriesModel> catogeries { get; set; }
    }
    public class ProductModel
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int BrandId { get; set; }
        public int CategoryId { get; set; }
        public Int16 ModelYear { get; set; }
        public decimal ListPrice { get; set; }
        public int TotalRecords { get; set; }
    }



    public class DeleteModel
    {
        public int ProductId { get; set; }
        public int CatogeriesId { get; set; }
        public int BrandId { get; set; }
    }
}