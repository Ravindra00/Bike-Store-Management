using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ViewModels
{
    public class CustomersModel
    {
        public int CustomerId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public String Phone { get; set; }
        public string EmailAddress { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set; }   
        public String ZipCode { get; set; }

    }
}