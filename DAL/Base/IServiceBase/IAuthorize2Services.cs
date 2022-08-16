using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Base.IServiceBase
{
    public interface IAuthorize2Services
    {
        string[] Get(string controller, string action);
    }
}
