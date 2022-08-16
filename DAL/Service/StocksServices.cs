using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System.Linq;
using ViewModels;

namespace DAL.Service
{
    public class StocksServices: AuthorizeServices,IStocksServices
    {
        //public List<StoreAndQuantity> StoreAndStocksForAll()
        //{
        //    List<StoreAndQuantity> storeList = (from b in bikeStoresDbContext.Stores
        //                                        join c in bikeStoresDbContext.Stocks on b.store_id equals c.store_id
        //                                        select new StoreAndQuantity
        //                                        {
        //                                            StoreId = b.store_id,
        //                                            StoreName = b.store_name,
        //                                            Phone = b.phone,
        //                                            Email = b.email,
        //                                            Street = b.street,
        //                                            City = b.city,
        //                                            State = b.state,
        //                                            Zipcode = b.zip_code,
        //                                            Quantity = c.quantity,
        //                                            ProductId = c.product_id
        //                                        }).ToList();
        //    return storeList;
        //}
        //List<StoreAndQuantity> storeList = (from b in bikeStoresDbContext.Stores
        //                                    join c in bikeStoresDbContext.Stocks on b.store_id
        //                                         equals c.store_id into d
        //                                    from stock in d.DefaultIfEmpty()
        //                                    where b.store_id == id
        //                                    select new StoreAndQuantity
        //                                    {
        //                                        StoreId = id,
        //                                        ProductId = stock == null ? 0 : stock.product_id,
        //                                        Quantity = stock == null ? 0 : stock.quantity
        //                                    }).ToList();
        private readonly BikeStoresEntities bikeStoresDbContext;// = new BikeStoresEntities();

        public StocksServices(BikeStoresEntities _bikeStoresDbContext) : base(_bikeStoresDbContext)
        {
            bikeStoresDbContext = _bikeStoresDbContext;
        }

        //BikeStoresEntities bikeStoresDbContext = new BikeStoresEntities();
        public StoreModel GetStocks(int id)
        {
            StoreModel store = new StoreModel
            {
                StoreId = id,
                StoreName  =  (from c in bikeStoresDbContext.Stores
                               where c.store_id == id
                               select c.store_name).Single(),
                StockList = (from c in bikeStoresDbContext.Stocks
                             join d in bikeStoresDbContext.Products on c.product_id equals d.product_id into e
                             from stockproduct in e.DefaultIfEmpty()
                             where c.store_id == id
                             select new StocksModel
                               {
                                   StoreId = c.store_id,
                                   ProductName =stockproduct.product_name,
                                   ProductId = c.product_id,
                                   Quantity = c.quantity
                               }).ToList()
            };
            return store;
        }
        public void DeleteStocks(StocksModel model)
        {
            var data = bikeStoresDbContext.Stocks.Where(x => x.store_id == model.StoreId && x.product_id == model.ProductId && x.quantity == model.Quantity).FirstOrDefault();
            if (data != null)
            {
                bikeStoresDbContext.Stocks.Remove(data);
                bikeStoresDbContext.SaveChanges();
            }
        }
        public void UpdateQuantity(StocksModel stock)
        {
            var data = bikeStoresDbContext.Stocks.Where(x => x.product_id == 1).FirstOrDefault();
            data.quantity = stock.Quantity;
            bikeStoresDbContext.SaveChanges();
        }
        public void AddStocks(StocksModel stock)
        {
            Stock _modelStocks = new Stock();
            _modelStocks.store_id = stock.StoreId;
            _modelStocks.product_id = stock.ProductId;
            _modelStocks.quantity = stock.Quantity;
            _modelStocks.CreatedBy = 1;
            _modelStocks.CreatedDate =System.DateTime.Now;
            bikeStoresDbContext.Stocks.Add(_modelStocks);
            bikeStoresDbContext.SaveChanges();
        }
        public StocksModel GetProductName(StocksModel stock)
        {
            
            StocksModel store = new StocksModel 
            {
                StoreId = stock.StoreId,
                ProductItems = (from a in bikeStoresDbContext.Products
                                select new ProductsModel
                                {
                                    ProductId = a.product_id,
                                    ProductName = a.product_name
                                }).ToList()
            };
            return store;
        }

    }
}