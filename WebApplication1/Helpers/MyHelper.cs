using DAL.Base.IServiceBase;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebApplication1.Helpers
{
    public static class MyHelper
    {
        //private readonly BikeStoresEntities _context = new BikeStoresEntities();

        //private static BikeStoresEntities _context;

        //static MyHelper(BikeStoresEntities context)
        //{
        //    _context = context;
        //}





        public static string[] Get(string controller, string action)
        {
            using (var _context = new BikeStoresEntities())
            {
                string url = '/' + controller + '/' + action;

                //List<string> list = (from b in _context.Menus
                //                     join c in _context.RoleMenus on b.Id equals c.Menu_id
                //                     join r in _context.Roles on c.Role_id equals r.Role_id
                //                     where b.ActionName == url
                //                     select r.Role1
                //                     ).ToList();
                List<Role> list = (from b in _context.Menus
                                   join c in _context.RoleMenus on b.id equals c.Menu_id
                                   join r in _context.Roles on c.Role_id equals r.Role_id
                                   where b.ActionName == url
                                   select r
                                     ).ToList();
                string[] result = new string[list.Count];
                for (int i = 0; i < list.Count; i++)
                {
                    result[i] = list[i].Role_name;
                }


                //return new string[] { "Admin","monkey" };
                return result;
            }
            //return _logger.Get(controller, action)

        }




    }
}