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
    public class MenuServices : AuthorizeServices, IMenuServices
    {
        BikeStoresEntities _context;

        public MenuServices(BikeStoresEntities context):base(context)
        {
            _context = context;
        }

        public List<MenuModel> getMenuList(string UserId)
        {
            List < MenuModel > list = (from a in (from a in _context.AspNetUsers
                                                   join b in _context.UserRoles on a.Id equals b.User_id
                                                   join rm in _context.RoleMenus on b.Role_id equals rm.Role_id
                                                   join m in _context.Menus on rm.Menu_id equals m.id
                                                   where a.Id == UserId
                                                   select m)
                                       where a.MenuId == null
                                       select new MenuModel
                                       {
                                            Id = a.id,
                                            MenuItem = a.MenuItem,
                                            ActionName = a.ActionName,
                                            Icon = a.Icon,
                                            Child = (from z in (from f in _context.AspNetUsers
                                                     join b in _context.UserRoles on f.Id equals b.User_id
                                                     join rm in _context.RoleMenus on b.Role_id equals rm.Role_id
                                                     join n in _context.Menus on rm.Menu_id equals n.id
                                                     where f.Id == UserId
                                                     select n)
                                                     where z.MenuId == a.id
                                                     select new ChildMenu
                                                     {
                                                        id = z.id,
                                                        ChildMenuItem = z.MenuItem,
                                                        ActionName = z.ActionName,
                                                        Icon = z.Icon,
                                                        menuId = z.MenuId
                                                     }).ToList()
                                        }).ToList();
            return list;
        }

        //    List<MenuModel> firstlist = (from a in _context.AspNetUsers
        //                                 join b in _context.UserRoles on a.Id equals b.user_id
        //                                 join rm in _context.RoleMenus on b.role_id equals rm.Role_id
        //                                 join m in _context.Menus on rm.Menu_id equals m.Id
        //                                 where a.Id == UserId
        //                                 select new MenuModel
        //                                 {
        //                                     Id = m.Id,
        //                                     MenuItem = m.MenuItem,
        //                                     ActionName = m.ActionName,
        //                                     Icon = m.Icon,
        //                                     MenuId = m.MenuId
        //                                 }).ToList();

        //    List<MenuModel> list = (from a in _context.AspNetUsers
        //                            join b in _context.UserRoles on a.Id equals b.user_id
        //                            join rm in _context.RoleMenus on b.role_id equals rm.Role_id
        //                            join m in _context.Menus on rm.Menu_id equals m.Id
        //                            where a.Id == UserId & m.MenuId == null
        //                            select new MenuModel
        //                            {
        //                                Id = m.Id,
        //                                MenuItem = m.MenuItem,
        //                                ActionName = m.ActionName,
        //                                Icon = m.Icon,
        //                                Child = (from a in _context.AspNetUsers
        //                                         join b in _context.UserRoles on a.Id equals b.user_id
        //                                         join rm in _context.RoleMenus on b.role_id equals rm.Role_id
        //                                         join m2 in _context.Menus on rm.Menu_id equals m2.Id
        //                                         where a.Id == UserId & m2.MenuId == m.Id
        //                                         select new ChildMenu
        //                                         {
        //                                             id = m2.Id,
        //                                             ChildMenuItem = m2.MenuItem,
        //                                             ActionName = .ActionName,
        //                                             Icon = m.Icon,
        //                                             menuId = m.MenuId
        //                                         }).ToList()
        //                            }).ToList();
        //    return list;
        //}


        //public List<MenuModel> getMenuList()
        //{
        //    List<MenuModel> list = (from b in _context.Menus
        //                            where b.MenuId == null
        //                            select new MenuModel
        //                            {
        //                                Id = b.Id,
        //                                MenuItem = b.MenuItem,
        //                                ActionName = b.ActionName,
        //                                Icon = b.Icon,
        //                                Child = (from c in _context.Menus
        //                                         where c.MenuId == b.Id
        //                                         select new ChildMenu
        //                                         {
        //                                             id = c.Id,
        //                                             ChildMenuItem = c.MenuItem,
        //                                             ActionName = c.ActionName,
        //                                             Icon = c.Icon,
        //                                             menuId = c.MenuId
        //                                         }).ToList()
        //                            }).ToList();
        //    return list;

        //}

    }
}