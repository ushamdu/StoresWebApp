using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public class ProductRepository : IProductRepository
    {
        public BikeStoresEntities _storesDataContext;
        public ProductRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }
        public IEnumerable<ProductInfo> GetAllProducts()
        {
            List<ProductInfo> lstProducts = new List<ProductInfo>();
            try
            {
                lstProducts = (from p in _storesDataContext.Products
                               join c in _storesDataContext.Categories on p.Category_Id equals c.Category_Id
                               join b in _storesDataContext.Brands on p.Brand_Id equals b.Brand_Id
                               select new ProductInfo
                               {
                                   ProductId = p.Product_Id,
                                   ProductName = p.Product_Name,
                                   CategoryId = c.Category_Id,
                                   CategoryName = c.Category_Name,
                                   BrandId = b.Brand_Id,
                                   BrandName = b.Brand_Name,
                                   ModelYear = p.Model_Year,
                                   ListPrice = p.List_Price
                               }).OrderBy(x => x.ProductName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProducts;
        }

        public IEnumerable<ProductDet> GetProductDetails()
        {
            List<ProductDet> lstProducts = new List<ProductDet>();
            try
            {
                lstProducts = _storesDataContext.Products                              
                                .Select(x => new ProductDet
                                   {
                                        ProductId = x.Product_Id,
                                        ProductName = x.Product_Name
                                    }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstProducts;
        }
        public ProductInfo GetProductById(long productId)
        {
            ProductInfo oProducts = new ProductInfo();
            try
            {
                oProducts = (from p in _storesDataContext.Products
                             join c in _storesDataContext.Categories on p.Category_Id equals c.Category_Id
                             join b in _storesDataContext.Brands on p.Brand_Id equals b.Brand_Id
                             where p.Product_Id == productId
                             select new ProductInfo
                             {
                                 ProductId = p.Product_Id,
                                 ProductName = p.Product_Name,
                                 CategoryId = c.Category_Id,
                                 CategoryName = c.Category_Name,
                                 BrandId = b.Brand_Id,
                                 BrandName = b.Brand_Name,
                                 ModelYear = p.Model_Year,
                                 ListPrice = p.List_Price
                             }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oProducts;
        }

        public string SaveProduct(ProductDetails oProductInfo)
        {
            try
            { 
                if (oProductInfo.ProductId == 0)
                {
                    Product oProduct = new Product();
                    oProduct.Product_Id = oProductInfo.ProductId;
                    oProduct.Product_Name = oProductInfo.ProductName;
                    oProduct.Brand_Id = oProductInfo.BrandId;
                    oProduct.Category_Id = oProductInfo.CategoryId;
                    oProduct.Model_Year = oProductInfo.ModelYear;
                    oProduct.List_Price = oProductInfo.ListPrice;
                    oProduct.CreatedTs = DateTime.Now;

                    _storesDataContext.Products.Add(oProduct);
                    _storesDataContext.SaveChanges();

                }
                else
                {
                    Product objProduct = _storesDataContext.Products.Find(oProductInfo.ProductId);
                    if (objProduct != null)
                    {
                        objProduct.Product_Name = oProductInfo.ProductName;
                        objProduct.Brand_Id = oProductInfo.BrandId;
                        objProduct.Category_Id = oProductInfo.CategoryId;
                        objProduct.Model_Year = oProductInfo.ModelYear;
                        objProduct.List_Price = oProductInfo.ListPrice;
                        objProduct.LastModifiedTs = DateTime.Now;

                        _storesDataContext.SaveChanges();
                    }
                }

                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
        public void DeleteProduct(long productId)
        {
            try
            {
                Product oProduct = _storesDataContext.Products.Find(productId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}