using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public interface ICategoryRepository
    {
        IEnumerable<Category> GetAllCategories();
        IEnumerable<CategoryInfo> GetCategoryDetails();
        CategoryInfo GetCategoryById(long categoryId);
        string SaveCategory(CategoryInfo oCategory);
        void DeleteCategory(long categoryId);
    }
}
