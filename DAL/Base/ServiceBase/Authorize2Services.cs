using DAL.Base.IServiceBase;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.Base.ServiceBase
{
    public class Authorize2Services : IAuthorize2Services
    {
        private readonly BikeStoresEntities _context;

        public Authorize2Services(BikeStoresEntities context)
        {
            _context = context;
        }

        public string[] Get(string controller, string action)
        {
            string url = '/'+controller +'/'+action;

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
    }
}
