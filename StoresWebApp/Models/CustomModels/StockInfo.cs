using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public class StockInfo
    {
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public long BrandId { get; set; }
        public string BrandName { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }    
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; }
        public int ModelYear { get; set; }
    }

   public class AvailableStock
    {
        public long ProductId { get; set; }
        public string Product_Name { get; set; }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public int Quantity { get; set; }
        public bool IsExist { get; set; }
    }

    public class StockDetail
    {
        public long StoreId { get; set; }
        public long ProductId { get; set; }
        public int Quantity { get; set; }
    }
}