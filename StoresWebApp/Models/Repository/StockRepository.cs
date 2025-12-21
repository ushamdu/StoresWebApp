using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.Repository
{
    public class StockRepository : IStockRepository
    {
        public BikeStoresEntities _storesDataContext;
        public StockRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<StockInfo> GetAllStocks()
        {
            List<StockInfo> lstStocks = new List<StockInfo>();
            try
            {
                lstStocks = (from p in _storesDataContext.Products
                             join c in _storesDataContext.Categories on p.Category_Id equals c.Category_Id
                             join b in _storesDataContext.Brands on p.Brand_Id equals b.Brand_Id
                             join s in _storesDataContext.Stocks on p.Product_Id equals s.Product_Id
                             join t in _storesDataContext.Stores on s.Store_Id equals t.Store_Id
                             select new StockInfo
                             {
                                 ProductId = p.Product_Id,
                                 ProductName = p.Product_Name,
                                 CategoryId = c.Category_Id,
                                 CategoryName = c.Category_Name,
                                 BrandId = b.Brand_Id,
                                 BrandName = b.Brand_Name,
                                 StoreId = t.Store_Id,
                                 StoreName = t.Store_Name,
                                 Quantity = (int)s.Quantity,
                                 ModelYear = p.Model_Year
                             }).OrderBy(x => x.StoreName).ThenBy(t => t.CategoryName)
                             .ThenBy(t => t.BrandName).ThenBy(t => t.ProductName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStocks;
        }

        public StockInfo GetStockById(long stockId)
        {
            StockInfo oStocks = new StockInfo();
            try
            {
                oStocks = (from p in _storesDataContext.Products
                           join c in _storesDataContext.Categories on p.Category_Id equals c.Category_Id
                           join b in _storesDataContext.Brands on p.Brand_Id equals b.Brand_Id
                           join s in _storesDataContext.Stocks on p.Product_Id equals s.Product_Id
                           join t in _storesDataContext.Stores on s.Store_Id equals t.Store_Id
                           where s.Id == stockId
                           select new StockInfo
                           {
                               ProductId = p.Product_Id,
                               ProductName = p.Product_Name,
                               CategoryId = c.Category_Id,
                               CategoryName = c.Category_Name,
                               BrandId = b.Brand_Id,
                               BrandName = b.Brand_Name,
                               StoreId = t.Store_Id,
                               StoreName = t.Store_Name,
                               Quantity = (int)s.Quantity,
                               ModelYear = p.Model_Year
                           }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oStocks;
        }
        public string SaveProductDelivery(StockDetail oStock)
        {
            string msg = string.Empty;
            try
            {              
                // If product exist, then update the quantity
               Stock existStock = _storesDataContext.Stocks.Where(x => x.Product_Id == oStock.ProductId).FirstOrDefault();
                if(existStock != null)
                {
                    existStock.Quantity += oStock.Quantity;
                    existStock.LastModifiedTs = DateTime.Now;

                    _storesDataContext.SaveChanges();
                }
                else
                {
                    // If product doesnot exist, insert as new product in stock
                    Stock objStock = new Stock
                    {
                        Store_Id = oStock.StoreId,
                        Product_Id = oStock.ProductId,
                        Quantity = oStock.Quantity,
                        CreatedTs = DateTime.Now
                    };
                    _storesDataContext.Stocks.Add(objStock);
                    _storesDataContext.SaveChanges();
                }               

                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        public IEnumerable<StockInfo> GetStocksByStoreId(long storeId)
        {
            List<StockInfo> lstStocks = new List<StockInfo>();
            try
            {
                lstStocks = (from p in _storesDataContext.Products
                             join c in _storesDataContext.Categories on p.Category_Id equals c.Category_Id
                             join b in _storesDataContext.Brands on p.Brand_Id equals b.Brand_Id
                             join s in _storesDataContext.Stocks on p.Product_Id equals s.Product_Id
                             join t in _storesDataContext.Stores on s.Store_Id equals t.Store_Id
                             where (storeId == 0) || t.Store_Id == storeId
                             select new StockInfo
                             {
                                 ProductId = p.Product_Id,
                                 ProductName = p.Product_Name,
                                 CategoryId = c.Category_Id,
                                 CategoryName = c.Category_Name,
                                 BrandId = b.Brand_Id,
                                 BrandName = b.Brand_Name,
                                 StoreId = t.Store_Id,
                                 StoreName = t.Store_Name,
                                 Quantity = (int)s.Quantity,
                                 ModelYear = p.Model_Year
                             }).OrderBy(x => x.StoreName).ThenBy(t => t.CategoryName)
                             .ThenBy(t => t.BrandName).ThenBy(t => t.ProductName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStocks;
        }
    }
}