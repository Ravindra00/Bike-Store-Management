using DAL.Base.ServiceBase;
using DAL.IService;
//using Microsoft.AspNet.Identity;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ViewModels;

namespace DAL.Service
{

    public class UserServices : AuthorizeServices,IUserServices
    {
        //private readonly UserManager<ApplicationUser> userManager;

        //public UserServices(UserManager<ApplicationUser> _userManager )
        //{
        //   userManager = _userManager;

        //}
        BikeStoresEntities context;
        public UserServices(BikeStoresEntities _context) :base(_context)
        { context = _context; }



        public List<Uroles> getRoles()
        {
            List<Uroles> roles = (from b in context.Roles
                                  select new Uroles
                                  {
                                      RoleId = b.Role_id,
                                      RoleName = b.Role_name
                                  }).ToList();
            return roles;
        }

        public List<UserViewModel> getUser()
        {
            List<UserViewModel> userViewModels = (from b in context.AspNetUsers
                                                  join c in context.UserRoles on b.Id equals c.User_id into e
                                                  from userrole in e.DefaultIfEmpty()
                                                  join d in context.Roles on userrole.Role_id equals d.Role_id into f
                                                  from roleuser in f.DefaultIfEmpty()

                                                  select new UserViewModel
                                                  {
                                                      UserId = b.Id,
                                                      UserName = b.UserName,
                                                      EmailAddress = b.Email,
                                                      //Password = b.PasswordHash,
                                                      PhoneNumber = b.PhoneNumber,
                                                      RoleName = roleuser.Role_name
                                                  }).ToList();
            return userViewModels;
        }


        public void deleteUser(String id)
        {
            var data = context.AspNetUsers.Where(x => x.Id == id).FirstOrDefault();
            context.AspNetUsers.Remove(data);
            context.SaveChanges();

        }

        public List<MenuModel> getMenuList()
        {
            List<MenuModel> menuModels = (from b in context.Menus
                                          where b.MenuId == null
                                          select new MenuModel
                                          {
                                              Id = b.id,
                                              MenuItem = b.MenuItem,
                                              Child = (from c in context.Menus
                                                       where c.MenuId == b.id
                                                       select new ChildMenu
                                                       {
                                                           id = c.id,
                                                           ChildMenuItem = c.MenuItem
                                                       }).ToList(),
                                          }).ToList();
            return menuModels;
        }

        public void saveRoleMenu(RoleWithMenuModel model)
        {
            var data = context.RoleMenus.Where(x => x.Role_id == model.RoleId).ToList();
            if (data.Count != 0)
            {
                foreach (var d in data)
                {
                    context.RoleMenus.Remove(d);
                }
                context.SaveChanges();
            }
            foreach (var m in model.MenuId)
            {
                RoleMenu roleMenu = new RoleMenu()
                {
                    Role_id = model.RoleId,
                    Menu_id = m
                };
                context.RoleMenus.Add(roleMenu);
            }
            context.SaveChanges();

        }

        public List<RoleWithMenuViewModel> checkRoleMenu(int id)
        {
            List<RoleWithMenuViewModel> roleWithMenuViewModels = (from b in context.RoleMenus
                                                     where b.Role_id == id
                                                     select new RoleWithMenuViewModel
                                                     {
                                                         //RoleId = id,
                                                         MenuId = b.Menu_id
                                                     }).ToList();
            return roleWithMenuViewModels;


        }
    }
}