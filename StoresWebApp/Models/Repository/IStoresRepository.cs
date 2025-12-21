using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public interface IStoresRepository
    {
        IEnumerable<StoreDetail> GetAllStores();
        IEnumerable<StoreInfo> GetStoreDetails();
        StoreDetail GetStoreById(long storeId);
        string SaveStore(StoreDetail oStore);
        void DeleteStore(long storeId);     
    }
}
