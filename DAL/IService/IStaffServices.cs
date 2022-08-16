using DAL.Base.IServiceBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ViewModels;

namespace DAL.IService
{
    public interface IStaffServices : IAuthorizeServices
    {
        List<StaffModel> GetStaffsDetails();
        void DeleteStaffDetails(int id);

        StaffModel getStoreAndManager(int id);

        void AddOrUpdateStaffDetail(StaffModel model);
    }
}
