using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public class OrderInfo
    {
        public long OrderId { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public long OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime RequiredDate { get; set; }
        public DateTime? ShippedDate { get; set; }
        public long StoreId { get; set; }
        public string StoreName { get; set; }
        public long StaffId { get; set; }
        public string StaffName { get; set; }
        public List<OrderItemsInfo> lstOrderItemsInfo { get; set; }
    }

    public class OrderItemsInfo
    {       
        public long ItemId { get; set; }
        public long ProductId { get; set; }
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderDetails
    {
        public long OrderId { get; set; }    
        public string CustomerName { get; set; }       
        public DateTime OrderDate { get; set; }   
        public string StoreName { get; set; }    
        public string StaffName { get; set; }
        public string OrderStatus { get; set; }
    }

    public class OrderItemsDetails
    {
        public long ItemId { get; set; }     
        public string ProductName { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public decimal Discount { get; set; }
        public decimal DiscountAmount { get; set; }
        public decimal Amount { get; set; }
        public decimal TotalAmount { get; set; }
    }

    public class OrderStatusLookUp
    {
        public long OrderStatusId { get; set; }
        public string OrderStatus { get; set; }
    }

    public class SearchParams
    {
        public long CustomerId { get; set; }
        public long StoreId { get; set; }
        public long StaffId { get; set; }
        public long StatusId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
    }
}