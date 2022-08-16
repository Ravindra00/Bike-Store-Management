using DAL.Base.IServiceBase;
using System.Collections.Generic;
using ViewModels;

namespace DAL.IService
{
    public interface IStoreServices : IAuthorizeServices
    {
        List<StoreModel> GetStoreList();
        void AddORUpdateStoreDetails(StoreModel model);
        void DeleteStoreDetailes(int? id);

    }
}
