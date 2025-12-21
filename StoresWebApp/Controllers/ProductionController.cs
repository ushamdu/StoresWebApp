using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.Repository;

namespace StoresWebApp.Controllers
{
    public class ProductionController : Controller
    {
        private ICategoryRepository _categoryRepository;
        private IBrandRepository _brandRepository;
        private IProductRepository _productRepository;
        public ProductionController(CategoryRepository category, BrandRepository brandRepository,ProductRepository productRepository)
        {
            _categoryRepository = category;
            _brandRepository = brandRepository;
            _productRepository = productRepository;
        }

        #region Category      
        public ActionResult Categories()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetCategories()
        {
            List<CategoryInfo> lstCategory = (List<CategoryInfo>)_categoryRepository.GetCategoryDetails();
            return Json(new { data = lstCategory }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCategory(CategoryInfo oCategory)
        {
            string result = _categoryRepository.SaveCategory(oCategory);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditCategory(long categoryId)
        {
            CategoryInfo oCategory = _categoryRepository.GetCategoryById(categoryId);
            return Json(new { data = oCategory }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeleteCategory(long categoryId)
        {
            return View();
        }

        #endregion

        #region Brand      
        public ActionResult Brands()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetBrands()
        {
            List<BrandInfo> lstBrands = (List<BrandInfo>)_brandRepository.GetBrandDetails();
            return Json(new { data = lstBrands }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveBrand(BrandInfo oBrandInfo)
        {
            string result = _brandRepository.SaveBrand(oBrandInfo);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditBrand(long brandId)
        {
            BrandInfo oBrandInfo = _brandRepository.GetBrandById(brandId);
            return Json(new { data = oBrandInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeleteBrand(long brandId)
        {
            return View();
        }

        #endregion

        #region Product
        public ActionResult Products()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetProducts()
        {
            List<ProductInfo> lstProducts = (List<ProductInfo>)_productRepository.GetAllProducts();
            return Json(new { data = lstProducts }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveProduct(ProductDetails oProductInfo)
        {
            string result = _productRepository.SaveProduct(oProductInfo);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditProduct(long productId)
        {
            ProductInfo oProductInfo = _productRepository.GetProductById(productId);
            return Json(new { data = oProductInfo }, JsonRequestBehavior.AllowGet);
        }

        [HttpDelete]
        public ActionResult DeleteProduct(long productId)
        {
            return View();
        }

        #endregion

    }

}