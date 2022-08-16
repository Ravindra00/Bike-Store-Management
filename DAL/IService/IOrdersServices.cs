using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IOrdersServices : IAuthorizeServices
    {
       //List<ordersModel> getOrdersDetails();
       List<OrderViewModel> getOrdersDetails();
        List<OrderItemsModel>getOrdersItemDetails();
       OrderModel getProductsAndCategory();
       List<ProductModel> getProduct(int? id);
       void saveOrderAndItems(ordersModel model);
       List<ordersModel> getTable (int? id);
       void deleteOrderItems(int id);
       void deleteOrder(int id);
    }
}
