using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base.IServiceBase
{
    public interface IAuthorizeServices
    {
        bool authorize(string userId, string actionName);
    }
}
