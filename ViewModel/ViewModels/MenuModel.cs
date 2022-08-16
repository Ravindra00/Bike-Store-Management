using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class MenuModel
    {
        public int Id { get; set; }
        public string MenuItem { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }
        public int? MenuId { get; set; } 
        public List<ChildMenu> Child {get;set;}
    }
    public class ChildMenu
    {
        public int id { get; set; } 
        public int? menuId { get; set; }
        public string ChildMenuItem { get; set; }
        public string ActionName { get; set; }
        public string Icon { get; set; }    
    }
}