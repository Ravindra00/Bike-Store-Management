using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Linq.Dynamic;
using Microsoft.AspNet.Identity;
using DAL.IService;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class OrdersController : Controller
    {
        private readonly IOrdersServices OrderServices;
        public OrdersController(IOrdersServices _OrderServices)
        {
            OrderServices = _OrderServices;
        }

        public ActionResult ShowOrders()
        {
            var Actionlink = Action_Name.orderaction.GetEnumDescription();
            if (OrderServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");
        }

        //[HttpPost]
        //public ActionResult GetOrdersDetails()
        //{
        //    //Server Side Parameter
        //    int start = Convert.ToInt32(Request["start"]);
        //    //int length = 12;
        //    int length = Convert.ToInt32(Request["length"]);
        //    string searchValue = Request["search[value]"];
        //    string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
        //    string sortDirection = Request["order[0][dir]"];
        //    List<ordersModel> emplist = new List<ordersModel>();
        //    emplist = OrderServices.getOrdersDetails();
        //    int totalrows = emplist.Count;
        //    if (!string.IsNullOrEmpty(searchValue))//filter
        //    {
        //        emplist = emplist.
        //            Where(x => x.CustomerId.ToString().Contains(searchValue.ToLower())
        //            || x.OrderStatus.ToString().Contains(searchValue.ToLower())
        //            || x.OrderDate.ToString().Contains(searchValue.ToLower())
        //            || x.RequiredDate.ToString().Contains(searchValue.ToLower())
        //            || x.ShippedDate.ToString().Contains(searchValue.ToLower())
        //            || x.StoreId.ToString().Contains(searchValue.ToLower())
        //            || x.StaffId.ToString().Contains(searchValue.ToLower())).ToList<ordersModel>();
        //    }
        //    int totalrowsafterfiltering = emplist.Count;
        //    //sorting
        //    emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<ordersModel>();

        //    //paging
        //    emplist = emplist.Skip(start).Take(length).ToList<ordersModel>();
        //    return Json(new
        //    {
        //        data = emplist,
        //        draw = Request["draw"],
        //        recordsTotal = totalrows,
        //        recordsFiltered = totalrowsafterfiltering
        //    },
        //        JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public ActionResult GetOrdersDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            //int length = 12;
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<OrderViewModel> emplist = new List<OrderViewModel>();
            emplist = OrderServices.getOrdersDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.CustomerName.ToLower().Contains(searchValue.ToLower())
                    || x.OrderStatusinstring.ToString().Contains(searchValue.ToLower())
                    || x.Orderdateinstring.ToLower().Contains(searchValue.ToLower())
                    || x.Requireddateinstring.ToLower().Contains(searchValue.ToLower())
                    || x.Shippeddateinstring.ToLower().Contains(searchValue.ToLower())
                    || x.StaffName.ToLower().Contains(searchValue.ToLower())
                    || x.StoreName.ToLower().Contains(searchValue.ToLower())).ToList<OrderViewModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<OrderViewModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<OrderViewModel>();
            return Json(new
            {
                data = emplist,
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            },
                JsonRequestBehavior.AllowGet);
        }

        public ActionResult ShowOrdersItem()
        {
            var Actionlink = Action_Name.orderitemaction.GetEnumDescription();
            if (OrderServices.authorize(User.Identity.GetUserId(), Actionlink) == true)
            {
                return View();
            }
            else
                //return RedirectToAction("LogOff", "Account");
                return RedirectToAction("Login", "Account");
        }
        [HttpPost]
        public ActionResult GetOrdersItemDetails()
        {
            //Server Side Parameter
            int start = Convert.ToInt32(Request["start"]);
            //int length = 12;
            int length = Convert.ToInt32(Request["length"]);
            string searchValue = Request["search[value]"];
            string sortColumnName = Request["columns[" + Request["order[0][column]"] + "][name]"];
            string sortDirection = Request["order[0][dir]"];
            List<OrderItemsModel> emplist = new List<OrderItemsModel>();
            emplist = OrderServices.getOrdersItemDetails();
            int totalrows = emplist.Count;
            if (!string.IsNullOrEmpty(searchValue))//filter
            {
                emplist = emplist.
                    Where(x => x.OrderId.ToString().Contains(searchValue.ToLower())
                    || x.ItemId.ToString().Contains(searchValue.ToLower())
                    || x.ProductId.ToString().Contains(searchValue.ToLower())
                    || x.Quantity.ToString().Contains(searchValue.ToLower())
                    || x.ListPrice.ToString().Contains(searchValue.ToLower())
                    || x.Discount.ToString().Contains(searchValue.ToLower())).ToList<OrderItemsModel>();
            }
            int totalrowsafterfiltering = emplist.Count;
            //sorting
            emplist = emplist.OrderBy(sortColumnName + " " + sortDirection).ToList<OrderItemsModel>();

            //paging
            emplist = emplist.Skip(start).Take(length).ToList<OrderItemsModel>();
            return Json(new
            {
                data = emplist,
                draw = Request["draw"],
                recordsTotal = totalrows,
                recordsFiltered = totalrowsafterfiltering
            },
                JsonRequestBehavior.AllowGet);
        }
        // GET: Orders
        public ActionResult order()
        {
            var data = OrderServices.getProductsAndCategory();

            return View(data);
        }

        public ActionResult orderProduct(int? id)
        {
            var data = OrderServices.getProduct(id);
            return PartialView(data);
            //return Json(new { text = "success" });
        }
        //public ActionResult productDetails(int id)
        //{
        //    var data = OrderServices.productDetails(id);
        //    return Json(data);
        //}

        [HttpPost]
        public ActionResult saveorderitem(ordersModel model)
        {
            OrderServices.saveOrderAndItems(model);
            return Json(new { text = "success" });
            //return HttpNotFound();
        }
        [HttpPost]
        public ActionResult getDetails(int? id)
        {
            var data = OrderServices.getTable(id);
            return Json(data);

        }


        [HttpPost]
        public ActionResult DeleteOrders(int id)
        {
            OrderServices.deleteOrder(id);
            return Json(new { text = "Deleted Successfull!" });
        }

        [HttpPost]
        public ActionResult DeleteOrderItems(int id)
        {
            OrderServices.deleteOrderItems(id);
            return Json(new { text = "Deleted Successfull!" });  
        }



    }
}