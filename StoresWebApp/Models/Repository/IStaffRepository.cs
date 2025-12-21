using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StoresWebApp.Models.DAL;
using StoresWebApp.Models.CustomModels;

namespace StoresWebApp.Models.Repository
{
    public interface IStaffRepository
    {
        IEnumerable<StaffInfo> GetAllStaffs();
        StaffDetail GetStaffById(long staffId);
        IEnumerable<StaffLookUp> GetStaffLookUp();
        string SaveStaff(StaffDetail oStaff);
        void DeleteStaff(long staffId);
    }
}
