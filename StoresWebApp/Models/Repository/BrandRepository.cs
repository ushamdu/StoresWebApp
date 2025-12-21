 using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public class BrandRepository : IBrandRepository
    {
        public BikeStoresEntities _storesDataContext;
        public BrandRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<Brand> GetAllBrands()
        {
            try
            {
               return _storesDataContext.Brands.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<BrandInfo> GetBrandDetails()
        {
            List<BrandInfo> lstBrand = new List<BrandInfo>();
            lstBrand = _storesDataContext.Brands.Select(x => new BrandInfo
            {
                BrandId = x.Brand_Id,
                BrandName = x.Brand_Name
            }).OrderBy(t => t.BrandName).ToList();

            return lstBrand;
        }

        public BrandInfo GetBrandById(long brandId)
        {
            BrandInfo oBrand = new BrandInfo();
            try
            {
                oBrand = _storesDataContext.Brands.Where(x => x.Brand_Id == brandId).Select(x => new BrandInfo
                        {
                            BrandId = x.Brand_Id,
                            BrandName = x.Brand_Name
                        }).FirstOrDefault();              
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oBrand;
        }

        public string SaveBrand(BrandInfo oBrandInfo)
        {
            try
            { 
                if (oBrandInfo.BrandId == 0)
                {
                    Brand brand = new Brand();
                    brand.Brand_Id = oBrandInfo.BrandId;
                    brand.Brand_Name = oBrandInfo.BrandName;

                    brand.CreatedTs = DateTime.Now;

                    _storesDataContext.Brands.Add(brand);
                    _storesDataContext.SaveChanges();
                }
                else
                {
                    Brand objBrand = _storesDataContext.Brands.Find(oBrandInfo.BrandId);
                    objBrand.Brand_Name = oBrandInfo.BrandName;
                    objBrand.LastModifiedTs = DateTime.Now;

                    _storesDataContext.SaveChanges();
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void DeleteBrand(long brandId)
        {
            try
            {
                BrandInfo oBrand = GetBrandById(brandId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}