using DAL.Base.IServiceBase;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace DAL.Base.ServiceBase
{
    public class AuthorizeServices : IAuthorizeServices
    {
        private readonly BikeStoresEntities _context;

        public AuthorizeServices(BikeStoresEntities context)
        {
            _context = context;
        }


        public bool authorize(string userId, string actionName)
        {
            IQueryable<Menu> list = (from a in _context.AspNetUsers
                                     join b in _context.UserRoles on a.Id equals b.User_id
                                     join rm in _context.RoleMenus on b.Role_id equals rm.Role_id
                                     join m in _context.Menus on rm.Menu_id equals m.id
                                     where a.Id == userId
                                     select m);
            var data = list.Where(a => a.ActionName == actionName).FirstOrDefault();
            return data != null ? true : false;

        }
    }
}