using DAL.Base.ServiceBase;
using DAL.IService;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using ViewModels;

namespace DAL.Service
{

    public class StoreServices : AuthorizeServices, IStoreServices
    {
        //BikeStoresEntities bikeStoresDbContext = new BikeStoresEntities();
        private readonly BikeStoresEntities bikeStoresDbContext;// = new BikeStoresEntities();

        public StoreServices(BikeStoresEntities _bikeStoresDbContext) : base(_bikeStoresDbContext)
        {
            bikeStoresDbContext = _bikeStoresDbContext;
        }

        public List<StoreModel> GetStoreList()
        {
            //using (bikeStoresDbContext = new BikeStoresEntities())
            //{
                List<StoreModel> storeList = (from b in bikeStoresDbContext.Stores
                                              select new StoreModel
                                              {
                                                  StoreId = b.store_id,
                                                  StoreName = b.store_name,
                                                  Phone = b.phone,
                                                  Email = b.email,
                                                  Street = b.street,
                                                  City = b.city,
                                                  State = b.state,
                                                  Zipcode = b.zip_code,
                                                  CreatedBy = b.CreatedBy,
                                                  CreatedDate = b.CreatedDate,
                                                  ModifiedBy = b.ModifiedBy,
                                                  ModifiedDate = b.ModifiedDate
                                              }).ToList();
                return storeList;
            //}
        }
        public void AddORUpdateStoreDetails(StoreModel model)
        {
            if (model.StoreId == 0)
            {
                Store _model = new Store();
                Stock _modelStocks = new Stock();
                _model.store_id = model.StoreId;
                _model.store_name = model.StoreName;
                _model.email = model.Email;
                _model.phone = model.Phone;
                _model.city = model.City;
                _model.street = model.Street;
                _model.state = model.State;
                _model.zip_code = model.Zipcode;
                _model.CreatedBy = 1;
                _model.CreatedDate = DateTime.Now;
                //_modelStocks.store_id=model.StoreId;
                //_modelStocks.quantity=model.Quantity;
                bikeStoresDbContext.Stores.Add(_model);
                bikeStoresDbContext.SaveChanges();
            }
            else
            {
                var _model = bikeStoresDbContext.Stores.Where(x => x.store_id == model.StoreId).FirstOrDefault();
                if (_model != null)
                {
                    //bikeStoresDb.Stores.Attach(_model);
                    _model.store_name = model.StoreName;
                    _model.email = model.Email;
                    _model.phone = model.Phone;
                    _model.city = model.City;
                    _model.street = model.Street;
                    _model.state = model.State;
                    _model.zip_code = model.Zipcode;
                    _model.ModifiedDate = DateTime.Now;
                    bikeStoresDbContext.SaveChanges();
                }
            }

        }
        public void DeleteStoreDetailes(int? id)
        {
            var data = bikeStoresDbContext.Stores.Where(x => x.store_id == id).FirstOrDefault();
            if (data != null)
            {
                bikeStoresDbContext.Stores.Remove(data);
                bikeStoresDbContext.SaveChanges();
            }
        }
    }
}