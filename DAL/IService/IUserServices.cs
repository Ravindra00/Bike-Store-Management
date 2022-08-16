using DAL.Base.IServiceBase;
using Model.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IUserServices : IAuthorizeServices
    {

        List<Uroles> getRoles();
        List<UserViewModel> getUser();
        void deleteUser(String id);
        List<MenuModel> getMenuList();

        void saveRoleMenu(RoleWithMenuModel model);
        List<RoleWithMenuViewModel> checkRoleMenu(int id);
    }
}
