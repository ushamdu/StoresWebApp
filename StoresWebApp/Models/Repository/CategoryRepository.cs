using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        public BikeStoresEntities _storesDataContext;
        public CategoryRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _storesDataContext.Categories.ToList();
        }

        public IEnumerable<CategoryInfo> GetCategoryDetails()
        {
            List<CategoryInfo> lstCategory = new List<CategoryInfo>();
            lstCategory = _storesDataContext.Categories.Select(x => new CategoryInfo
                            {
                                CategoryId = x.Category_Id,
                                CategoryName = x.Category_Name
                            }).OrderBy(t => t.CategoryName).ToList();

            return lstCategory;
        }

        public CategoryInfo GetCategoryById(long categoryId)
        {
            CategoryInfo oCategory = new CategoryInfo();
            try
            {
                oCategory = _storesDataContext.Categories.Where(x => x.Category_Id == categoryId)
                                                  .Select(x => new CategoryInfo
                                                  {
                                                      CategoryId = x.Category_Id,
                                                      CategoryName = x.Category_Name
                                                  }).FirstOrDefault();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return oCategory;
        }     
        public string SaveCategory(CategoryInfo oCategory)
        {        
            try
            {  
                if (oCategory.CategoryId == 0)
                {
                    Category category = new Category();
                    category.Category_Id = oCategory.CategoryId;
                    category.Category_Name = oCategory.CategoryName;

                    category.CreatedTs = DateTime.Now;

                    _storesDataContext.Categories.Add(category);
                    _storesDataContext.SaveChanges();
                }
                else
                {
                    Category objCategory = _storesDataContext.Categories.Find(oCategory.CategoryId);
                    objCategory.Category_Name = oCategory.CategoryName;
                    objCategory.LastModifiedTs = DateTime.Now;

                    _storesDataContext.SaveChanges();
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        public void DeleteCategory(long categoryId)
        {
            try
            {
                CategoryInfo oCategory = GetCategoryById(categoryId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
      
    }
}