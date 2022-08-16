using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IMenuServices : IAuthorizeServices
    {
        //List<MenuModel> getMenuList();
        List<MenuModel> getMenuList(string UserId);
    }
}
