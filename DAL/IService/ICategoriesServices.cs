using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface ICategoriesServices : IAuthorizeServices
    {
        List<CategoriesModel> getCategoryDetails();
        void deleteCategory(int id);
        void createCategory(CategoriesModel model);
    }

}
