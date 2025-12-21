using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public class OrdersRepository : IOrdersRepository
    {
        public BikeStoresEntities _storesDataContext;
        public OrdersRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }
        public IEnumerable<OrderDetails> GetAllOrders()
        {
            List<OrderDetails> lstOrderDetail = new List<OrderDetails>();

            try
            {
                lstOrderDetail = (from ord in _storesDataContext.Orders
                                  join cus in _storesDataContext.Customers on ord.Customer_Id equals cus.Customer_Id
                                  join sta in _storesDataContext.Order_Status on ord.Order_StatusId equals sta.Order_StatusId
                                  join sto in _storesDataContext.Stores on ord.Store_Id equals sto.Store_Id
                                  join stf in _storesDataContext.Staffs on ord.Staff_Id equals stf.Staff_Id
                                  select new OrderDetails
                                  {
                                      OrderId = ord.Order_Id,
                                      CustomerName = cus.First_Name + " " + cus.Last_Name,
                                      OrderDate = ord.Order_Date,
                                      StoreName = sto.Store_Name,
                                      StaffName = stf.First_Name + " " + stf.Last_Name,
                                      OrderStatus = sta.OrderStatus
                                  })
                                  .OrderBy(t => t.OrderDate)
                                  .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstOrderDetail;
        }

        public IEnumerable<OrderItemsDetails> GetItemsDetailsByOrderId(long orderId)
        {
            List<OrderItemsDetails> lstItemDet = new List<OrderItemsDetails>();
            try
            {
                lstItemDet = (from itm in _storesDataContext.Order_Items
                              join pro in _storesDataContext.Products on itm.Product_Id equals pro.Product_Id
                              where itm.Order_Id == orderId
                              select new OrderItemsDetails
                              {
                                  ItemId = itm.Item_Id,
                                  ProductName = pro.Product_Name,
                                  Quantity = itm.Quantity,
                                  Price = itm.List_Price,
                                  Amount = itm.Quantity * itm.List_Price,
                                  Discount = itm.Discount,
                                  DiscountAmount = itm.Quantity * itm.List_Price * itm.Discount / 100.00m,
                                  TotalAmount = (itm.Quantity * itm.List_Price) - (itm.Quantity * itm.List_Price * itm.Discount / 100.00m)
                              }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstItemDet;
        }

        public OrderInfo GetOrderById(long orderId)
        {
            OrderInfo oOrderInfo = new OrderInfo();
            List<OrderItemsInfo> lstOrderItemsInfo = new List<OrderItemsInfo>();

            try
            {
                //Order details
                oOrderInfo = (from ord in _storesDataContext.Orders
                              join sta in _storesDataContext.Order_Status on ord.Order_StatusId equals sta.Order_StatusId
                              join cus in _storesDataContext.Customers on ord.Customer_Id equals cus.Customer_Id
                              join sto in _storesDataContext.Stores on ord.Store_Id equals sto.Store_Id
                              join stf in _storesDataContext.Staffs on ord.Staff_Id equals stf.Staff_Id
                              where ord.Order_Id == orderId
                              select new OrderInfo
                              {
                                  OrderId = ord.Order_Id,
                                  CustomerId = cus.Customer_Id,
                                  CustomerName = string.Format("{0} {1}", cus.First_Name, cus.Last_Name),
                                  OrderStatusId = sta.Order_StatusId,
                                  OrderStatus = sta.OrderStatus,
                                  OrderDate = ord.Order_Date,
                                  RequiredDate = ord.Required_Date,
                                  ShippedDate = ord.Shipped_Date,
                                  StoreId = sto.Store_Id,
                                  StoreName = sto.Store_Name,
                                  StaffId = stf.Staff_Id,
                                  StaffName = string.Format("{0} {1}", stf.First_Name, stf.Last_Name)
                              }).FirstOrDefault();

                //Order Item Details

                lstOrderItemsInfo = (from item in _storesDataContext.Order_Items
                                     join pro in _storesDataContext.Products on item.Product_Id equals pro.Product_Id
                                     where item.Order_Id == orderId
                                     select new OrderItemsInfo
                                     {
                                         ItemId = item.Item_Id,
                                         ProductId = pro.Product_Id,
                                         ProductName = pro.Product_Name,
                                         Quantity = item.Quantity,
                                         Price = item.List_Price,
                                         Amount = item.Quantity * item.List_Price,
                                         Discount = item.Discount,
                                         DiscountAmount = item.Quantity * item.List_Price * item.Discount / 100.00m,
                                         TotalAmount = (item.Quantity * item.List_Price) - (item.Quantity * item.List_Price * item.Discount / 100.00m)
                                     }).ToList();

                oOrderInfo.lstOrderItemsInfo = lstOrderItemsInfo;

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oOrderInfo;
        }

        public IEnumerable<OrderDetails> GetSearchOrders(SearchParams oParams)
        {
            List<OrderDetails> lstOrderDetail = new List<OrderDetails>();

            try
            {
                lstOrderDetail = (from ord in _storesDataContext.Orders
                                  join cus in _storesDataContext.Customers on ord.Customer_Id equals cus.Customer_Id
                                  join sta in _storesDataContext.Order_Status on ord.Order_StatusId equals sta.Order_StatusId
                                  join sto in _storesDataContext.Stores on ord.Store_Id equals sto.Store_Id
                                  join stf in _storesDataContext.Staffs on ord.Staff_Id equals stf.Staff_Id
                                  where cus.Customer_Id == oParams.CustomerId || oParams.CustomerId == 0
                                  && sto.Store_Id == oParams.StoreId || oParams.StoreId == 0
                                  && stf.Staff_Id == oParams.StaffId || oParams.StaffId == 0
                                  && sta.Order_StatusId == oParams.StatusId || oParams.StatusId == 0
                                  && (oParams.StartDate == null ||
                                  DbFunctions.TruncateTime(ord.Order_Date) >= DbFunctions.TruncateTime(oParams.StartDate))
                                 && (oParams.EndDate == null ||
                                 DbFunctions.TruncateTime(ord.Order_Date) <= DbFunctions.TruncateTime(oParams.EndDate))
                                  select new OrderDetails
                                  {
                                      OrderId = ord.Order_Id,
                                      CustomerName = cus.First_Name + " " + cus.Last_Name,
                                      OrderDate = ord.Order_Date,
                                      StoreName = sto.Store_Name,
                                      StaffName = stf.First_Name + " " + stf.Last_Name,
                                      OrderStatus = sta.OrderStatus
                                  })
                                   .OrderBy(t => t.OrderDate)
                                  .ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstOrderDetail;
        }
        public IEnumerable<OrderStatusLookUp> GetOrderStatusLookUp()
        {
            List<OrderStatusLookUp> lstStatus = new List<OrderStatusLookUp>();
            try
            {
                lstStatus = _storesDataContext.Order_Status
                            .Select(x => new OrderStatusLookUp
                            {
                                OrderStatusId = x.Order_StatusId,
                                OrderStatus = x.OrderStatus
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStatus;
        }
        public AvailableStock IsStockAvailable(long productId, long storeId, int requiredQty)
        {
            AvailableStock oAvailableStock = new AvailableStock();
            try
            {
                oAvailableStock = (from s in _storesDataContext.Stocks
                                   join p in _storesDataContext.Products on s.Product_Id equals p.Product_Id
                                   join st in _storesDataContext.Stores on s.Store_Id equals st.Store_Id
                                   where s.Product_Id == productId && s.Store_Id == storeId
                                   select new AvailableStock
                                   {
                                       ProductId = p.Product_Id,
                                       Product_Name = p.Product_Name,
                                       StoreId = st.Store_Id,
                                       StoreName = st.Store_Name,
                                       Quantity = (int)s.Quantity,
                                       IsExist = (int)s.Quantity > requiredQty ? true : false
                                   }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oAvailableStock;
        }
        public void SaveOrderInfo(OrderInfo oOrderInfo)
        {
            try
            {
                //Order details
                if (oOrderInfo.OrderId == 0)
                {
                    Order oOrder = new Order
                    {
                        Order_Id = oOrderInfo.OrderId,
                        Customer_Id = oOrderInfo.CustomerId,
                        Order_StatusId = oOrderInfo.OrderStatusId,
                        Order_Date = oOrderInfo.OrderDate,
                        Required_Date = oOrderInfo.RequiredDate,
                        Shipped_Date = oOrderInfo.ShippedDate,
                        Store_Id = oOrderInfo.StoreId,
                        Staff_Id = oOrderInfo.StaffId,
                        CreatedTs = DateTime.Now
                    };

                    _storesDataContext.Orders.Add(oOrder);
                    _storesDataContext.SaveChanges();

                    //Order Item details
                    long orderId = oOrder.Order_Id;

                    foreach (OrderItemsInfo itemInfo in oOrderInfo.lstOrderItemsInfo)
                    {
                        Order_Items oItems = new Order_Items
                        {
                            Order_Id = orderId,
                            Item_Id = itemInfo.ItemId,
                            Product_Id = itemInfo.ProductId,
                            Quantity = itemInfo.Quantity,
                            List_Price = itemInfo.Price,
                            Discount = itemInfo.Discount
                        };

                        _storesDataContext.Order_Items.Add(oItems);

                        //Update stock
                        Stock oStock = _storesDataContext.Stocks.Where(x => x.Product_Id == itemInfo.ProductId && x.Store_Id == oOrder.Store_Id).FirstOrDefault();
                        if (oStock != null)
                        {
                            oStock.Quantity = oStock.Quantity - itemInfo.Quantity;
                        }
                    }

                    _storesDataContext.SaveChanges();
                }
                else
                {

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}