using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.Repository;

namespace StoresWebApp.Controllers
{
    public class DeliveryController : Controller
    {
        private IStoresRepository _storesRepository;
        private IProductRepository _productRepository;
        private IStockRepository _stockRepository;
        public DeliveryController(StoresRepository storesRepository, ProductRepository productRepository, IStockRepository stockRepository)
        {
            _storesRepository = storesRepository;
            _productRepository = productRepository;
            _stockRepository = stockRepository;
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult NewProduct()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStores()
        {
            List<StoreInfo> lstStores = (List<StoreInfo>)_storesRepository.GetStoreDetails();
            return Json(new { data = lstStores }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductDetails()
        {
            List<ProductDet> lstProducts = (List<ProductDet>)_productRepository.GetProductDetails();
            return Json(new { data = lstProducts }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetProductDetailsById(long productId)
        {
            ProductInfo oProduct = new ProductInfo();
            oProduct = _productRepository.GetProductById(productId);
            return Json(new { data = oProduct }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveNewProductInStore(StockDetail oStock)
        {
          string result =  _stockRepository.SaveProductDelivery(oStock);           
          return Json(new { data = result }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Stocks()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetStockDetailsByStoresId(long storeId)
        {
            List<StockInfo> lstStocks = (List<StockInfo>)_stockRepository.GetStocksByStoreId(storeId);
            return Json(new { data = lstStocks }, JsonRequestBehavior.AllowGet);
        }
    }
}