using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoresWebApp.Models.CustomModels;
using StoresWebApp.Models.Repository;

namespace StoresWebApp.Controllers
{
    public class SalesController : Controller
    {
        #region Customers   
        private ICustomerRepository _customerRepository;
        private IStaffRepository _staffRepository;
        private IStoresRepository _storeRepository;
        private IOrdersRepository _orderRepository;
        public SalesController(CustomerRepository customer, StaffRepository staff, StoresRepository store, OrdersRepository order)
        {
            _customerRepository = customer;
            _staffRepository = staff;
            _storeRepository = store;
            _orderRepository = order;
        }
        public ActionResult Customers()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllCustomers()
        {
            List<CustomerInfo> lstCustomers = (List<CustomerInfo>)_customerRepository.GetCustomerDetails();
            return Json(new { data = lstCustomers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveCustomer(CustomerInfo oCustomer)
        {
            string result = _customerRepository.SaveCustomer(oCustomer);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditCustomer(long customerId)
        {
            CustomerInfo oCustomer = _customerRepository.GetCustomerById(customerId);
            return Json(new { data = oCustomer }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetCustomerLookUp()
        {
            List<CustomerLookUp> lstCustomer = (List<CustomerLookUp>)_customerRepository.GetCustomerLookUp();
            return Json(new { data = lstCustomer }, JsonRequestBehavior.AllowGet);
        }
        #endregion

        #region Staffs       
        public ActionResult Staffs()
        {
            return View();
        }
        [HttpGet]
        public ActionResult GetAllStaffs()
        {
            List<StaffInfo> lstCustomers = (List<StaffInfo>)_staffRepository.GetAllStaffs();
            return Json(new { data = lstCustomers }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveStaff(StaffDetail oStaff)
        {
            string result = _staffRepository.SaveStaff(oStaff);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditStaff(long staffId)
        {
            StaffDetail oStaff = _staffRepository.GetStaffById(staffId);
            return Json(new { data = oStaff }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStaffLookUp()
        {
            List<StaffLookUp> lstLookUps = (List<StaffLookUp>)_staffRepository.GetStaffLookUp();
            return Json(new { data = lstLookUps }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Stores
        public ActionResult Stores()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllStores()
        {
            List<StoreDetail> lstStores = (List<StoreDetail>)_storeRepository.GetAllStores();
            return Json(new { data = lstStores }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SaveStore(StoreDetail oStore)
        {
            string result = _storeRepository.SaveStore(oStore);
            return Json(new { message = result }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult EditStore(long storeId)
        {
            StoreDetail oStaff = _storeRepository.GetStoreById(storeId);
            return Json(new { data = oStaff }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Orders
        public ActionResult Orders()
        {
            return View();
        }

        [HttpGet]
        public ActionResult GetAllOrders()
        {
            List<OrderDetails> lstStores = (List<OrderDetails>)_orderRepository.GetAllOrders();
            return Json(new { data = lstStores }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetItemsDetailsByOrderId(long orderId)
        {
            List<OrderItemsDetails> lstItemDet = (List<OrderItemsDetails>)_orderRepository.GetItemsDetailsByOrderId(orderId);
            return Json(new { data = lstItemDet }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetStatusLookUp()
        {
            List<OrderStatusLookUp> lstStatus = (List<OrderStatusLookUp>)_orderRepository.GetOrderStatusLookUp();
            return Json(new { data = lstStatus }, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult GetOrderSearch(SearchParams oSearch)
        {
            List<OrderDetails> lstStores = (List<OrderDetails>)_orderRepository.GetSearchOrders(oSearch);
            return Json(new { data = lstStores }, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}