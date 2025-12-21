using StoresWebApp.Models.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public class StaffRepository : IStaffRepository
    {
        public BikeStoresEntities _storesDataContext;
        public StaffRepository(BikeStoresEntities storesDataContext)
        {
            _storesDataContext = storesDataContext;
        }

        public IEnumerable<StaffInfo> GetAllStaffs()
        {
            List<StaffInfo> lstStaffs = new List<StaffInfo>();
            try
            {
                lstStaffs = (from st in _storesDataContext.Staffs
                             join mg in _storesDataContext.Staffs on st.Manager_Id equals mg.Staff_Id into tempMgr
                             from tMgr in tempMgr.DefaultIfEmpty()
                             join tt in _storesDataContext.Stores on st.Store_Id equals tt.Store_Id
                             select new StaffInfo
                             {
                                 StaffId = st.Staff_Id,
                                 FirstName = st.First_Name,
                                 LastName = st.Last_Name,
                                 Email = st.Email,
                                 Phone = st.Phone,
                                 StoreName = tt.Store_Name,
                                 MgrFirstName = tMgr.First_Name ?? string.Empty,
                                 MgrLastName = tMgr.Last_Name ?? string.Empty
                             }).OrderBy(t => t.FirstName).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStaffs;
        }

        public StaffDetail GetStaffById(long staffId)
        {
            StaffDetail oStaffInfo = new StaffDetail();
            try
            {
                oStaffInfo = (from st in _storesDataContext.Staffs
                             join mg in _storesDataContext.Staffs on st.Manager_Id equals mg.Staff_Id
                             join tt in _storesDataContext.Stores on st.Store_Id equals tt.Store_Id
                             select new StaffDetail
                             {
                                 StaffId = st.Staff_Id,
                                 FirstName = st.First_Name,
                                 LastName = st.Last_Name,
                                 Email = st.Email,
                                 Phone = st.Phone, 
                                 StoreId = st.Store_Id,
                                 ManagerId = (long)st.Manager_Id
                             }).FirstOrDefault();

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return oStaffInfo;
        }

        public IEnumerable<StaffLookUp> GetStaffLookUp()
        {
            List<StaffLookUp> lstStaff = new List<StaffLookUp>();
            try
            {
                /*  lstStaff = _storesDataContext.Staffs.AsEnumerable()
                              .Where(x => x.Active == 1)
                              .Select(x => new StaffLookUp
                              {
                                  StaffId = x.Staff_Id,
                                  StaffName = string.Format("{0} {1}",x.First_Name,x.Last_Name)
                              }).ToList();*/

                lstStaff = _storesDataContext.Staffs.AsEnumerable()
                           .Where(x => x.Active == 1)
                           .Select(x => new StaffLookUp
                           {
                               StaffId = x.Staff_Id,
                               StaffName = $"{x.First_Name} {x.Last_Name}"
                           }).ToList();
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return lstStaff;
        }

        public string SaveStaff(StaffDetail oStaff)
        {
            string msg = string.Empty;
            try
            {
                if(oStaff.StaffId == 0)
                {
                    Staff staff = new Staff
                    {
                        Staff_Id = oStaff.StaffId,
                        First_Name = oStaff.FirstName,
                        Last_Name = oStaff.LastName,
                        Email = oStaff.Email,
                        Phone = oStaff.Phone,
                        Active = 1,
                        Store_Id = oStaff.StoreId,
                        Manager_Id = oStaff.ManagerId,
                        CreatedTs = DateTime.Now
                    };                   

                    _storesDataContext.Staffs.Add(staff);
                    _storesDataContext.SaveChanges();
                }
                else
                {
                    Staff objStaff = _storesDataContext.Staffs.Find(oStaff.StaffId);
                    if (objStaff != null)
                    {
                        objStaff.First_Name = oStaff.FirstName;
                        objStaff.Last_Name = oStaff.LastName;
                        objStaff.Email = oStaff.Email;
                        objStaff.Phone = oStaff.Phone;                       
                        objStaff.Store_Id = oStaff.StoreId; 
                        objStaff.Manager_Id = oStaff.ManagerId;
                        objStaff.LastModifiedTs = DateTime.Now;

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

        public void DeleteStaff(long staffId)
        {          
            try
            {
                Staff oStaff = _storesDataContext.Staffs.Find(staffId);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}