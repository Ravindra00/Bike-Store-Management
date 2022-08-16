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
    public class BrandServices : AuthorizeServices, IBrandServices
    {
        BikeStoresEntities _context;
        public BrandServices(BikeStoresEntities context):base(context)
        {
            _context = context;
        }

        public List<BrandModel> getBrandDetails()
        {
            List<BrandModel> brands = (from b in _context.Brands
                                       select new BrandModel
                                       {
                                           BrandId = b.brand_id,
                                           BrandName = b.brand_name
                                       }).ToList();
            return brands;
        }
        public void deleteBrand(int id)
        {
            var data = _context.Brands.Where(b => b.brand_id == id).FirstOrDefault();
            //var product =_context.Products.Where(b => b.product_id == id).ToList();
            //for(int i = 0; i < product.Count; i++)
            //{
            //    product[i].brand_id = 1 ; 
            //}
            _context.Brands.Remove(data);
            _context.SaveChanges();
        }

        public void createBrand(BrandModel model)
        {
            Brand _brand = new Brand
            {
                brand_id = model.BrandId,
                brand_name = model.BrandName,
                CreatedBy = 1,
                CreatedDate = DateTime.Now
            };
            _context.Brands.Add(_brand);
            _context.SaveChanges();
        }
    }
}