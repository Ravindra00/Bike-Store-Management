

using DAL.Base.IServiceBase;
using ViewModels;

namespace DAL.IService
{
    public interface IStocksServices : IAuthorizeServices
    {
        StoreModel GetStocks(int id);
        void DeleteStocks(StocksModel model);
        void AddStocks(StocksModel stock);
        void UpdateQuantity(StocksModel stock);
        StocksModel GetProductName(StocksModel stock);
    }
}
