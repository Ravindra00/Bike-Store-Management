using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;


namespace ViewModels
{
    public class StaffModel
    {
        [Key]
        public int StaffID { get; set; }
        [Required]
        public String FirstName { get; set; }
        [Required]
        public String LastName { get; set; }
        [Required]
        [EmailAddress]
        public String Email { get; set; }
        [Required]
        [MaxLength(10)]
        public String PhoneNo { get; set; }
        [Required]
        public byte Active { get; set; }
        [Required]
        public int StoreID { get; set; }
        [Required]
        public int? ManagerID { get; set; }

        public List<StoreModel> store { get; set; }
        public List<StaffModel> staffs { get; set; }
    }
}
//StaffID
//    FirstName
//    LastName
//    Email
//    PhoneNo
//    Active
//    StoreID
//    ManagerID