using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface ITestServices : IAuthorizeServices
    {
       void Add(TestModel model);
        TestModel getDetails();
        void Delete(DeleteModel delete);
    }
}
