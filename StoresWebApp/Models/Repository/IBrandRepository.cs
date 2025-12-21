using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public interface IBrandRepository
    {
        IEnumerable<Brand> GetAllBrands();
        IEnumerable<BrandInfo> GetBrandDetails();
        BrandInfo GetBrandById(long brandId);
        string SaveBrand(BrandInfo oBrand);
        void DeleteBrand(long brandId);
    }
}
