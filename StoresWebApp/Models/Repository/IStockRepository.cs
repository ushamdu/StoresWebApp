using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public interface IStockRepository
    {
        IEnumerable<StockInfo> GetAllStocks();
        StockInfo GetStockById(long stockId);
        string SaveProductDelivery(StockDetail oStock);
        IEnumerable<StockInfo> GetStocksByStoreId(long storeId);
    }
}
