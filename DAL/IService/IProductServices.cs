using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IProductServices : IAuthorizeServices
    {
        List<ProductModel> getProductDetails();
        List<ProductModel> ServerSideDatatable(int start, int Length,string searchvalue, int sortcoloumnIndex, string sortDirection);
        CategoryAndBrand getCategoryAndBrand();
        void saveProduct(ProductsModel model);
    }
}
