using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.DAL;

namespace StoresWebApp.Models.Repository
{
    public interface IProductRepository
    {
        IEnumerable<ProductInfo> GetAllProducts();
        IEnumerable<ProductDet> GetProductDetails();
        ProductInfo GetProductById(long productId);
        string SaveProduct(ProductDetails oProduct);
        void DeleteProduct(long productId);
    }
}
