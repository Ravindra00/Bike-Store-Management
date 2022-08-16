using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class ordersModel
    {
        public int OrderId { get; set; }
        public int? CustomerId { get; set; }
        public byte OrderStatus { get; set; }    
        public DateTime OrderDate { get; set; } 
        public DateTime RequiredDate { get; set; } 
        public DateTime? ShippedDate { get; set; } 
        public int StoreId { get; set; }
        public int StaffId { get; set; }
        public string orderdateinstring { get;set; }
        public string requireddateinstring { get; set; }
        public string shippeddateinstring { get; set; }
        public List<OrderItemsModel> orderItems { get; set; }

    }
    public class OrderViewModel
    {
        public int OrderId { get; set; }
        public String CustomerName { get; set; }
        public byte OrderStatus { get; set; }
        public String StoreName { get; set; }
        public String StaffName { get; set; }
        public DateTime? OrderDate { get; set; }
        public DateTime? RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public string Orderdateinstring { get; set; }
        public string Requireddateinstring { get; set; }
        public string Shippeddateinstring { get; set; }
        public string OrderStatusinstring { get; set; }

    }
    public enum ordstus
    {
        New = 1,
        [Description("In Progress")]
        InProgress = 2,
        Completed = 3,
        Cancled = 4

    }
    public class OrderItemsModel
    {
        public int OrderId { get; set; }    
        public int ItemId { get; set; }
        public int ProductId { get; set; }
        public int Quantity { get; set; }   
        public decimal ListPrice { get; set; }
        public decimal Discount { get; set; }   
    }
}