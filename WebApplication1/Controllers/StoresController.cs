using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using System;
using DAL.IService;
using ViewModels;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class StoresController : Controller  
    {
        private readonly IStoreServices StoreServices;
        public StoresController(IStoreServices _StoreServices)
        {
            StoreServices=_StoreServices;   
        }
       
        // GET: StoreDetails
        public ActionResult ShowStoresDetailes()
        {
            var storeList = StoreServices.GetStoreList();
            return View(storeList);
        }
        
        //Get: Insert or update Form
        public ActionResult AddORUpdateStoreDetails(StoreModel model)
            {
            if (model.StoreId == 0)
            {
                ModelState.Clear();
                return View(model);
            }
            else
            {
                return View(model);
            }
            //return View();
        }

        //Post: Insert and update New Store Details 
        [HttpPost]
        [ActionName("AddORUpdateStoreDetails")]
        public ActionResult AddORUpdateStoreDetail(StoreModel model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }
            //model.CreatedBy = Convert.ToInt32(User.Identity.GetUserId());
            //model.CreatedBy = Convert.ToInt32(User.Identity.GetUserId(),16);
            StoreServices.AddORUpdateStoreDetails(model);
            return RedirectToAction("ShowStoresDetailes");
        }
        
        //Get:Delete Store Details
        public ActionResult DeleteStoresDetails(int? id)
        {
            StoreServices.DeleteStoreDetailes(id);
            return RedirectToAction("ShowStoresDetailes");
        }
        

    }
}
