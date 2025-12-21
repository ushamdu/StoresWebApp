using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public class ProductDetails
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long BrandId { get; set; }     
        public long CategoryId { get; set; }   
        public short ModelYear { get; set; }
        public decimal ListPrice { get; set; }
    }
}