using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class UserViewModel
    {
        public string UserId { get; set; }  
        public string UserName { get; set; }    
        public string EmailAddress { get; set; }
        public string Password { get; set; }
        public String PhoneNumber { get; set; }
        public String RoleName { get; set; }

        public String Role { get; set; }
    }

    public class RoleWithMenuModel
    {
        public int RoleId { get; set; }
        public List<int> MenuId { get; set; }
    }
    public class RoleWithMenuViewModel
    {
        public int RoleId { get; set; }
        public int? MenuId { get; set; }
    }
    public class rolename
    {
        public String RoleName { get; set; }


    }
}