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
    public class CategoriesServices : AuthorizeServices, ICategoriesServices
    {
        BikeStoresEntities _context;

        public CategoriesServices(BikeStoresEntities context):base(context)
        {
            _context = context;
        }



        public List<CategoriesModel> getCategoryDetails()
        {
            List<CategoriesModel> categories = (from b in _context.Categories
                                                select new CategoriesModel
                                                {
                                                    CategoryId = b.category_id,
                                                    CategoryName = b.category_name
                                                }).ToList();
            return categories;
        }
        public void deleteCategory(int id)
        {

                var data = _context.Categories.Where(x => x.category_id == id).FirstOrDefault();
                _context.Categories.Remove(data);
                _context.SaveChanges();

        }

        public void createCategory(CategoriesModel model)
        {
            Category _category = new Category
            {
                category_name = model.CategoryName,
                CreatedBy = 1,
                CreatedDate = DateTime.Now,
            };
            _context.Categories.Add(_category);
            _context.SaveChanges();

        }


    }
}