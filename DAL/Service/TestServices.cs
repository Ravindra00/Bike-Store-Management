using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System;
using System.Linq;
using ViewModels;

namespace DAL.Service
{
    public class TestServices : AuthorizeServices, ITestServices
    {
        private readonly BikeStoresEntities bikeTestDbContext;

        public TestServices(BikeStoresEntities _bikeTestDbContext):base(_bikeTestDbContext)
        {
            bikeTestDbContext = _bikeTestDbContext;
        }

        //public void Add(TestModel model)
        //{ 
        //    Category _category = new Category();
        //    _category.category_id = model.CategoryId;
        //    _category.category_name = model.CategoryName;
        //    bikeTestDbContext.Categories.Add(_category);
        //    bikeTestDbContext.SaveChanges();

        //    Brand _brand = new Brand();
        //    _brand.brand_id=model.BrandId;
        //    _brand.brand_name = model.BrandName;
        //    bikeTestDbContext.Brands.Add(_brand);
        //    bikeTestDbContext.SaveChanges();

        //    var data = bikeTestDbContext.Categories.Where(x => x.category_name == model.CategoryName).FirstOrDefault();
        //    model.CategoryId = data.category_id;

        //    var data2 = bikeTestDbContext.Brands.Where(x => x.brand_name == model.BrandName).FirstOrDefault();
        //    model.BrandId = data2.brand_id;

        //    Product _product = new Product();
        //    _product.product_id = model.ProductId;
        //    _product.product_name = model.ProductName;  
        //    _product.list_price = model.ListPrice;
        //    _product.brand_id = model.BrandId;  
        //    _product.category_id=model.CategoryId;
        //    _product.model_year = model.ModelYear;
        //    bikeTestDbContext.Products.Add(_product);
        //    bikeTestDbContext.SaveChanges();
        //}    
        public void Add(TestModel model)
        {
            using (var bikeTestDbContext = new BikeStoresEntities())
            {
                using (var transaction = bikeTestDbContext.Database.BeginTransaction())
                {
                    try
                    {
                        Category _category = new Category();
                        _category.category_id = model.CategoryId;
                        _category.category_name = model.CategoryName;
                        bikeTestDbContext.Categories.Add(_category);
                        bikeTestDbContext.SaveChanges();

                        model.CategoryId = _category.category_id;

                        Brand _brand = new Brand();
                        _brand.brand_id = model.BrandId;
                        _brand.brand_name = model.BrandName;
                        bikeTestDbContext.Brands.Add(_brand);
                        bikeTestDbContext.SaveChanges();

                        model.BrandId = _brand.brand_id;

                        Product _product = new Product();
                        _product.product_id = model.ProductId;
                        _product.product_name = model.ProductName;
                        _product.list_price = model.ListPrice;
                        _product.brand_id = model.BrandId;
                        _product.category_id = model.CategoryId;
                        _product.model_year = model.ModelYear;
                        bikeTestDbContext.Products.Add(_product);

                        bikeTestDbContext.SaveChanges();
                        // Commit transaction
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback(); 
                    }
                }
            }
        }

        public void Delete(DeleteModel delete)
        {
            using (var bikeTestDbContext = new BikeStoresEntities())
            {
                using (var transaction = bikeTestDbContext.Database.BeginTransaction())
                {
                    try
                    {

                        if (delete.ProductId != 0)
                        {
                            var data = bikeTestDbContext.Products.Where(x => x.product_id == delete.ProductId).FirstOrDefault();
                            bikeTestDbContext.Products.Remove(data);
                            bikeTestDbContext.SaveChanges();
                        }
                        else if (delete.BrandId != 0)
                        {
                            var productdata = bikeTestDbContext.Products.Where(x => x.brand_id == delete.BrandId).FirstOrDefault();
                            if (productdata != null)
                            {
                                bikeTestDbContext.Products.Remove(productdata);
                                bikeTestDbContext.SaveChanges();
                            }
                            var branddata = bikeTestDbContext.Brands.Where(x => x.brand_id == delete.BrandId).FirstOrDefault();
                            bikeTestDbContext.Brands.Remove(branddata);
                            bikeTestDbContext.SaveChanges();
                        }
                        else if (delete.CatogeriesId != 0)
                        {
                            var productdata = bikeTestDbContext.Products.Where(x => x.category_id == delete.CatogeriesId).FirstOrDefault();
                            if (productdata != null)
                            {
                                bikeTestDbContext.Products.Remove(productdata);
                                bikeTestDbContext.SaveChanges();
                            }
                            var catdata = bikeTestDbContext.Categories.Where(x => x.category_id == delete.CatogeriesId).FirstOrDefault();
                            bikeTestDbContext.Categories.Remove(catdata);
                            bikeTestDbContext.SaveChanges();
                        }
                        transaction.Commit();
                    }
                    catch (Exception)
                    {
                        transaction.Rollback();
                    }
                }
            }
        }

        public TestModel getDetails()
        {
            TestModel test = new TestModel()
            {
                Products = (from b in bikeTestDbContext.Products
                            select new ProductModel
                            {
                                ProductId = b.product_id,
                                ProductName = b.product_name,
                                BrandId = b.brand_id,
                                CategoryId = b.category_id,
                                ModelYear = b.model_year,
                                ListPrice = b.list_price
                            }).ToList(),
                Brands = (from b in bikeTestDbContext.Brands
                          select new BrandModel
                          {
                              BrandId = b.brand_id, 
                              BrandName = b.brand_name
                          }).ToList(),
                catogeries =(from b in bikeTestDbContext.Categories
                             select new CategoriesModel
                             {
                                 CategoryId = b.category_id,
                                 CategoryName = b.category_name
                             }).ToList()
            };
            return test;
        }
    }
}