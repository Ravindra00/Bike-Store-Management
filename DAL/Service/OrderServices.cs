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
    public class OrderServices : AuthorizeServices, IOrdersServices
    {
        BikeStoresEntities bikeOrderDbContext;

        public OrderServices(BikeStoresEntities _bikeOrderDbContext):base(_bikeOrderDbContext)
        {
            bikeOrderDbContext = _bikeOrderDbContext;
        }
        //public List<ordersModel> getOrdersDetails()
        //{
        //    List<ordersModel> ordersDetails = (from b in bikeOrderDbContext.Orders
        //                                       select new ordersModel
        //                                       {
        //                                           OrderId = b.order_id,
        //                                           CustomerId = b.customer_id,
        //                                           OrderStatus = b.order_status,
        //                                           OrderDate = b.order_date,
        //                                           RequiredDate = b.required_date,
        //                                           ShippedDate = b.shipped_date,
        //                                           StoreId = b.store_id,
        //                                           StaffId = b.staff_id
        //                                       }).ToList();

        //    foreach(var i in ordersDetails)
        //    {
        //        //i.orderdateinstring = i.OrderDate.ToString();
        //        i.orderdateinstring = DateTime.Parse(i.OrderDate.ToString()).ToString("yyyy-MM-dd");
        //        i.requireddateinstring = DateTime.Parse(i.RequiredDate.ToString()).ToString("yyyy-MM-dd");
        //        i.shippeddateinstring = DateTime.Parse(i.ShippedDate.ToString()).ToString("yyyy-MM-dd");


        //    }
        //    return ordersDetails;

        //}



        public List<OrderViewModel> getOrdersDetails()
        {
            List<OrderViewModel> ordersDetails = (from b in bikeOrderDbContext.Orders
                                               join c in bikeOrderDbContext.Customers on b.customer_id equals c.customer_id  into d
                                               from ordercustomer in d.DefaultIfEmpty()

                                               join e in bikeOrderDbContext.Staffs on b.staff_id equals e.staff_id into f
                                               from orderstaff in f.DefaultIfEmpty()

                                               join g in bikeOrderDbContext.Stores on b.store_id equals g.store_id into h
                                               from orderstore in h.DefaultIfEmpty()

                                               select new OrderViewModel
                                               {
                                                   OrderId = b.order_id,
                                                   CustomerName = ordercustomer.first_name,
                                                   //OrderStatus = b.order_status,
                                                   OrderStatusinstring = ((ordstus)b.order_status).ToString(),
                                                   OrderDate = b.order_date,
                                                   RequiredDate = b.required_date,
                                                   ShippedDate = b.shipped_date,
                                                   StaffName = orderstaff.first_name,
                                                   StoreName = orderstore.store_name
                                               }).ToList();

            foreach (var i in ordersDetails)
            {
                //i.orderdateinstring = i.OrderDate.ToString();
                i.Orderdateinstring = DateTime.Parse(i.OrderDate.ToString()).ToString("yyyy-MM-dd");
                i.Requireddateinstring = DateTime.Parse(i.RequiredDate.ToString()).ToString("yyyy-MM-dd");
                i.Shippeddateinstring = DateTime.Parse(i.ShippedDate.ToString()).ToString("yyyy-MM-dd");
                //i.OrderStatusinstring = (ordstus)i.OrderStatus;


            }

            return ordersDetails;

        }
        public List<OrderItemsModel> getOrdersItemDetails()
        {
            List<OrderItemsModel> orderItemsDetails = (from b in bikeOrderDbContext.OrderItems
                                               select new OrderItemsModel
                                               {
                                                    OrderId= b.order_id,
                                                    ItemId =b.item_id,
                                                    ProductId =b.product_id,
                                                    Quantity =b.quantity,
                                                    ListPrice = b.list_price,
                                                    Discount=b.discount    
                                               }).ToList();
            return orderItemsDetails;
        }

        public OrderModel getProductsAndCategory()
        {
            OrderModel orderModels = new OrderModel
            {

                Customers = (from c in bikeOrderDbContext.Customers
                             select new CustomersModel
                             {
                                 CustomerId = c.customer_id,
                                 FirstName = c.first_name,
                                 LastName = c.last_name
                             }).ToList(),

                Store = (from s in bikeOrderDbContext.Stores
                         select new StoreModel
                         {
                             StoreId = s.store_id,
                             StoreName = s.store_name
                         }).ToList(),

                Staff = (from s in bikeOrderDbContext.Staffs
                         select new StaffModel
                         {
                             StaffID = s.staff_id,
                             FirstName = s.first_name,
                             LastName = s.last_name

                         }).ToList(),
            };

            return orderModels;
        }
        public List<ProductModel> getProduct(int? id)
        {
            List<ProductModel> product = new List<ProductModel>();
            if (id != null)
            {
                product = (from b in bikeOrderDbContext.Stocks
                           join c in bikeOrderDbContext.Products on b.product_id equals c.product_id
                           where b.store_id == id
                           select new ProductModel
                           {
                               ProductId = c.product_id,
                               ProductName = c.product_name,
                               ModelYear = c.model_year,
                               ListPrice = c.list_price,
                               CategoryId = c.category_id,
                               BrandId = c.brand_id
                           }).ToList();
            }
            else
            {
                product = (from b in bikeOrderDbContext.Products
                           select new ProductModel
                           {
                               ProductId = b.product_id,
                               ProductName = b.product_name,
                               ModelYear = b.model_year,
                               ListPrice = b.list_price,
                               CategoryId = b.category_id,
                               BrandId = b.brand_id
                           }).ToList();

            }
            return product;

        }


        public void saveOrderAndItems(ordersModel model)
        {
            //var data = bikeOrderDbContext.Orders.Where(x => x.customer_id == model.CustomerId && x.order_date == model.OrderDate).FirstOrDefault();
            //if (data == null)
            //{
                Order _order = new Order();
                _order.order_id = model.OrderId;
                _order.customer_id = model.CustomerId;
                _order.order_status = model.OrderStatus;
                _order.order_date = model.OrderDate;
                _order.required_date = model.RequiredDate;
                _order.shipped_date = model.ShippedDate;
                _order.store_id = model.StoreId;
                _order.staff_id = model.StaffId;
                _order.CreatedBy = 1;
                _order.CreatedDate = System.DateTime.Today;
                bikeOrderDbContext.Orders.Add(_order);
                bikeOrderDbContext.SaveChanges();
                model.OrderId = _order.order_id;
            //}

            OrderItem _orderItem = new OrderItem();

            foreach (var d in model.orderItems)
            {
                _orderItem.order_id = model.OrderId;
                //_orderItem.item_id = d.ItemId;
                _orderItem.product_id = d.ProductId;
                _orderItem.quantity = d.Quantity;
                _orderItem.list_price = d.ListPrice;
                _orderItem.discount = d.Discount;
                _orderItem.CreatedBy = 1;
                _orderItem.CreatedDate = System.DateTime.Today;
                bikeOrderDbContext.OrderItems.Add(_orderItem);
                bikeOrderDbContext.SaveChanges();

            }
        }


        public List<ordersModel> getTable(int? id)
        {
            List<ordersModel> orders = (from b in bikeOrderDbContext.Orders
                                        where b.customer_id == id
                                        select new ordersModel
                                        {
                                            CustomerId = b.customer_id,
                                            OrderId = b.order_id,
                                            OrderStatus = b.order_status,
                                            OrderDate = b.order_date,
                                            RequiredDate = b.required_date,
                                            ShippedDate = b.shipped_date,
                                            StoreId = b.store_id,
                                            StaffId = b.staff_id,
                                            //orderItems = (from c in bikeOrderDbContext.OrderItems
                                            //              where c.order_id == b.order_id
                                            //              select new OrderItemsModel
                                            //              {
                                            //                  OrderId = c.order_id,
                                            //                  ItemId = c.item_id,
                                            //                  ProductId = c.product_id,
                                            //                  Quantity = c.quantity,
                                            //                  ListPrice = c.list_price,
                                            //                  Discount = c.discount
                                            //              }).ToList(),

                                        }).ToList();
            return orders;
        }

        public void deleteOrderItems(int id)
        {
            var data = bikeOrderDbContext.OrderItems.Where(x => x.item_id == id).FirstOrDefault();
            if (data != null)
            {
                bikeOrderDbContext.OrderItems.Remove(data);
                bikeOrderDbContext.SaveChanges();
            }
        }

        public void deleteOrder(int id)
        {
            var data = bikeOrderDbContext.Orders.Where(x => x.order_id == id).FirstOrDefault();
            if (data != null)
            {
                bikeOrderDbContext.Orders.Remove(data);
                bikeOrderDbContext.SaveChanges();
            }
        }


    }
}