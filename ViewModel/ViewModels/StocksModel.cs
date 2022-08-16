using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class StocksModel
    {
        [Required]
        public int StoreId { get; set; }
        [Required]
        public int ProductId { get; set; }
        [Required]

        public int? Quantity { get; set; }
        public string ProductName { get; set; }
        public List<ProductsModel> ProductItems { get; set; }   
    }
}