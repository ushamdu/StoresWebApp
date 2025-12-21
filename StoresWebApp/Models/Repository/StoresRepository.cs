using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public class StoresRepository : IStoresRepository
    {
        public BikeStoresEntities _storesDataContext;

        public StoresRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<StoreDetail> GetAllStores()
        {
            List<StoreDetail> lstStores = new List<StoreDetail>();
            try
            {
                lstStores = _storesDataContext.Stores
                            .Select(x => new StoreDetail
                            {
                                StoreId = x.Store_Id,
                                StoreName = x.Store_Name,
                                Phone = x.Phone,
                                Email = x.Email,
                                Street = x.Street,
                                City = x.City,
                                StateName = x.State_Name,
                                ZipCode = x.Zip_Code
                            }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStores;
        }

        public IEnumerable<StoreInfo> GetStoreDetails()
        {
            List<StoreInfo> lstBrand = new List<StoreInfo>();
            lstBrand = _storesDataContext.Stores.Select(x => new StoreInfo
            {
                StoreId = x.Store_Id,
                StoreName = x.Store_Name
            }).OrderBy(t => t.StoreName).ToList();

            return lstBrand;
        }
        public StoreDetail GetStoreById(long storeId)
        {
            StoreDetail oStoreDet = new StoreDetail();
            try
            {
                oStoreDet = _storesDataContext.Stores.Where(x => x.Store_Id == storeId)
                            .Select(x => new StoreDetail
                            {
                                StoreId = x.Store_Id,
                                StoreName = x.Store_Name,
                                Phone = x.Phone,
                                Email = x.Email,
                                Street = x.Street,
                                City = x.City,
                                StateName = x.State_Name,
                                ZipCode = x.Zip_Code
                            }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oStoreDet;
        }

        public string SaveStore(StoreDetail oStore)
        {
            string msg = string.Empty;

            try
            {
                if (oStore.StoreId == 0)
                {
                    Store store = new Store
                    {
                        Store_Id = oStore.StoreId,
                        Store_Name = oStore.StateName,
                        Phone = oStore.Phone,
                        Email = oStore.Email,
                        Street = oStore.Street,
                        City = oStore.City,
                        State_Name = oStore.StateName,
                        Zip_Code = oStore.ZipCode,
                        CreatedTs = DateTime.Now

                    };                   

                    _storesDataContext.Stores.Add(store);
                    _storesDataContext.SaveChanges();
                }
                else
                {
                    Store objStore = _storesDataContext.Stores.Find(oStore.StoreId);
                    objStore.Store_Name = oStore.StoreName;
                    objStore.Phone = oStore.Phone;
                    objStore.Email = oStore.Email;
                    objStore.Street = oStore.Street;
                    objStore.City = oStore.City;
                    objStore.State_Name = oStore.StateName;
                    objStore.Zip_Code = oStore.ZipCode;

                    objStore.LastModifiedTs = DateTime.Now;

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

        public void DeleteStore(long storeId)
        {
            try
            {
                Store objStore = _storesDataContext.Stores.Find(storeId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}