using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DAL.IService;
using Microsoft.AspNet.Identity;

namespace WebApplication1.Controllers
{
    [Authorize]
    public class MenuController : Controller
    {
        private IMenuServices menuServices;
        public MenuController(IMenuServices _menuServices)
        {
            menuServices = _menuServices;
        }
        // GET: Menu
        public ActionResult GetMenu()
        {
            var UserId = User.Identity.GetUserId();
            var data = menuServices.getMenuList(UserId);
            //var data = menuServices.getMenuList();
            return PartialView("_GetMenu",data);
        }
    }
}