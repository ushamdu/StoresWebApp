using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public class CustomerRepository : ICustomerRepository
    {
        public BikeStoresEntities _storesDataContext;
        public CustomerRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<Customer> GetAllCustomers()
        {
            try
            {
                return _storesDataContext.Customers.ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public IEnumerable<CustomerInfo> GetCustomerDetails()
        {
            List<CustomerInfo> lstCustomers = new List<CustomerInfo>();
            try
            {
                lstCustomers = _storesDataContext.Customers
                                .Select(x => new CustomerInfo
                                {
                                    CustomerId = x.Customer_Id,
                                    FirstName = x.First_Name,
                                    LastName = x.Last_Name,
                                    Phone = string.IsNullOrEmpty(x.Phone)?string.Empty : x.Phone,
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

            return lstCustomers;
        }
        public CustomerInfo GetCustomerById(long customerId)
        {
            CustomerInfo oCustomer = new CustomerInfo();
            try
            {
                oCustomer = _storesDataContext.Customers.Where(x => x.Customer_Id == customerId)
                            .Select(x => new CustomerInfo
                            {
                                CustomerId = x.Customer_Id,
                                FirstName = x.First_Name,
                                LastName = x.Last_Name,
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
            return oCustomer;
        }

        public IEnumerable<CustomerLookUp> GetCustomerLookUp()
        {
            List<CustomerLookUp> lstCustomer = new List<CustomerLookUp>();
            try
            {
                lstCustomer = _storesDataContext.Customers
                               .Select(x => new CustomerLookUp
                               {
                                   CustomerId = x.Customer_Id,
                                   CustomerName = x.First_Name + " " + x.Last_Name
                               }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstCustomer;
        }
        public string SaveCustomer(CustomerInfo oCustomer)
        {
            string msg = string.Empty;
            try
            { 
                if (oCustomer.CustomerId == 0)
                {
                    Customer customer = new Customer
                    {
                        Customer_Id = oCustomer.CustomerId,
                        First_Name = oCustomer.FirstName,
                        Last_Name = oCustomer.LastName,
                        Phone = oCustomer.Phone,
                        Email = oCustomer.Email,
                        Street = oCustomer.Street,
                        City = oCustomer.City,
                        State_Name = oCustomer.StateName,
                        Zip_Code = oCustomer.ZipCode
                    };

                    customer.CreatedTs = DateTime.Now;

                    _storesDataContext.Customers.Add(customer);
                    _storesDataContext.SaveChanges();
                }
                else
                {
                    Customer objCustomer = _storesDataContext.Customers.Find(oCustomer.CustomerId);
                    if(objCustomer != null)
                    {
                        objCustomer.First_Name = oCustomer.FirstName;
                        objCustomer.Last_Name = oCustomer.LastName;
                        objCustomer.Phone = oCustomer.Phone;
                        objCustomer.Email = oCustomer.Email;
                        objCustomer.Street = oCustomer.Street;
                        objCustomer.City = oCustomer.City; 
                        objCustomer.State_Name = oCustomer.StateName;
                        objCustomer.Zip_Code = oCustomer.ZipCode;
                        objCustomer.LastModifiedTs = DateTime.Now;

                        _storesDataContext.SaveChanges();
                    }
                }

                msg = "Success";
            }
            catch (Exception ex)
            {
                msg = ex.Message;
            }

            return msg;
        }

        public void DeleteCustomer(long customerId)
        {
            try
            {
                Customer objCustomer = _storesDataContext.Customers.Find(customerId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}