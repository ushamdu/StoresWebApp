using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public class StoreInfo
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }
    }

    public class StoreDetail
    {
        public long StoreId { get; set; }
        public string StoreName { get; set; }     
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string StateName { get; set; }
        public string ZipCode { get; set; }
    }

}