using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IBrandServices : IAuthorizeServices
    {
        List<BrandModel> getBrandDetails();
        void deleteBrand(int id);
        void createBrand(BrandModel model);
    }
}
