using DAL.IService;
using System.Collections.Generic;
using System.Web.Mvc;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class StocksController : Controller
    {
        private readonly IStocksServices StoreServices;
        public StocksController(IStocksServices _StoreServices)
        {
            StoreServices = _StoreServices; 
        }       
        //GET: Stocks Details
        public ActionResult ShowStocksDetails(int id=0)
            {
            var storeList = StoreServices.GetStocks(id);
            if(storeList.StockList.Count== 0)
            {
                return RedirectToAction("AddStocksDetails");
            }
            else
                return View(storeList);
        }

        //Delete Stocks
        public ActionResult Delete(StocksModel model)
        {
            StoreServices.DeleteStocks(model);
            //return RedirectToAction("ShowStoreAndStocksDetails");
            //return RedirectToAction("Action", "controller", new { @id = id });
            return RedirectToAction("ShowStoreAndStocksDetails", new { @id = model.StoreId });
        }
        //GET: Add Stocks 
        public ActionResult AddStocksDetails(StocksModel stock)
        {
            ModelState.Clear();
            var data = StoreServices.GetProductName(stock);
            return View(data);
        }
        //POST: Add stocks
        [HttpPost]
        [ActionName("AddStocksDetails")]
        public ActionResult AddStocksDetail(StocksModel stock)
        {
            if (!ModelState.IsValid)
            {
                return View(stock);
            }
            StoreServices.AddStocks(stock);
            return RedirectToAction("ShowStocksDetails", new { @id = stock.StoreId });
        }
        //GET: Edit Stocks
        public ActionResult Edit(StocksModel obj)
        {
            ViewBag.pid = obj.ProductId;
            return View(obj);
        }

        //POST: Edit Stocks
        [HttpPost]
        [ActionName("Edit")]
        public ActionResult Edits(StocksModel stock)
        {
            StoreServices.UpdateQuantity(stock);
            return RedirectToAction("ShowStoreAndStocksDetails", new { @id = stock.StoreId });
        }
    }
}

            //Necessary comments
////GET: Add Stocks  sending as viewbag
//public ActionResult AddStocksDetails(StocksModel stock)
//{
//    ModelState.Clear();
//    var data = StoreServices.GetProductName(stock);
//    SelectList list = new SelectList(data.ProductItems, "ProductId", "ProductName");
//    ViewBag.list = list;
//    return View(stock);
//}




////POST: Add stocks
//[HttpPost]
//[ActionName("AddStocksDetails")]
//public ActionResult AddStocksDetail(StocksModel stock)
//{ 
//    if (!ModelState.IsValid)
//    {
//        return View(stock);
//    }
//    StoreServices.AddStocks(stock);
//    return RedirectToAction("ShowStoreAndStocksDetails", new {@id=stock.StoreId});
//}