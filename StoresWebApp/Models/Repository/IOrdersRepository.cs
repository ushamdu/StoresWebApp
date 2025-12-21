using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public interface IOrdersRepository
    {
        IEnumerable<OrderDetails> GetAllOrders();
        IEnumerable<OrderItemsDetails> GetItemsDetailsByOrderId(long orderId);
        OrderInfo GetOrderById(long orderId);
        IEnumerable<OrderStatusLookUp> GetOrderStatusLookUp();

        IEnumerable<OrderDetails> GetSearchOrders(SearchParams oParams);
        AvailableStock IsStockAvailable(long productId, long storeId,int requiredQty);
        void SaveOrderInfo(OrderInfo oOrderInfo);
       // void DeleteOrderInfo(long orderId);
    }
}
