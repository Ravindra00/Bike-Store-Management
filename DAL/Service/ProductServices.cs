using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using ViewModels;

namespace DAL.Service
{
    public class ProductServices : AuthorizeServices, IProductServices
    {
        BikeStoresEntities _context;

        public ProductServices(BikeStoresEntities _bikeProductDbContext):base(_bikeProductDbContext)
        {
            _context = _bikeProductDbContext;
        }


        public List<ProductModel> getProductDetails()
        {
            List<ProductModel> product =
            (from b in _context.Products
             select new ProductModel
             {
                 ProductId = b.product_id,
                 ProductName = b.product_name,
                 BrandId = b.brand_id,
                 CategoryId = b.category_id,
                 ModelYear = b.model_year,
                 ListPrice = b.list_price
             }).ToList();
            return product;
        }

        public List<ProductModel> ServerSideDatatable(int start, int Length, string searchvalue, int sortcoloumnIndex, string sortDirection)
        {
            List<ProductModel> data = _context.Database.SqlQuery<ProductModel>("ProductPaging @Pageno, @filter, @pagesize, @Sorting, @SortOrder",
                                                    new SqlParameter("@Pageno", start),
                                                    new SqlParameter("@filter", searchvalue),
                                                    new SqlParameter("@pagesize", Length),
                                                    new SqlParameter("@Sorting", sortcoloumnIndex),
                                                    new SqlParameter("@SortOrder", sortDirection)).ToList();
            return data;
        }

        public CategoryAndBrand getCategoryAndBrand()
        {
            CategoryAndBrand categoryAndBrand = new CategoryAndBrand()
            {
                cat = (from b in _context.Categories
                       select new CategoriesModel
                       {
                           CategoryId = b.category_id,
                           CategoryName = b.category_name
                       }).ToList(),
                brn = (from b in _context.Brands
                       select new BrandModel
                       {
                           BrandId  = b.brand_id,   
                           BrandName = b.brand_name
                       }).ToList()
            };
            return categoryAndBrand;
        }

        public void saveProduct(ProductsModel model)
        {
            Product product = new Product()
            {
                product_id = model.ProductId,
                product_name = model.ProductName,
                model_year = model.ModelYear,
                list_price = model.ListPrice,
                category_id = model.CategoryId,
                brand_id = model.BrandId,
                CreatedBy = 1,
                CreatedDate = DateTime.Now

            };
            _context.Products.Add(product);
            _context.SaveChanges();
        }
    }
}