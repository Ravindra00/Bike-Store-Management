//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Model.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class UserRole
    {
        public int id { get; set; }
        public string User_id { get; set; }
        public int Role_id { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Role Role { get; set; }
    }
}