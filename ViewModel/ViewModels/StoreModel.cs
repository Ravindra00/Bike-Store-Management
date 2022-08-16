using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ViewModels
{
    public class StoreModel
    {
        [Key]
        public int StoreId { get; set; }
        [Required (ErrorMessage ="Store Name is required")]
        public string StoreName { get; set; }
        [MaxLength(10)]
        public string Phone { get; set; }
        [Required(ErrorMessage = "Email is required")]
        [EmailAddress]
        public string Email { get; set; }   
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Zipcode { get; set; }
        public int? CreatedBy { get; set; }
        public DateTime? CreatedDate { get; set; }
        public int? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }

        public List<StocksModel> StockList { get; set; }
        public List<ProductsModel> ProductList { get; set; }

    }
}




