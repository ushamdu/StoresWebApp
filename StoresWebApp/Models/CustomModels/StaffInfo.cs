using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public class StaffInfo
    {
        public long StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }    
        public string Email { get; set; }
        public string Phone { get; set; } 
        public string StoreName { get; set; }   
        public string MgrFirstName { get; set; }
        public string MgrLastName { get; set; }
    }

    public class StaffDetail
    {
        public long StaffId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }     
        public long StoreId { get; set; }   
        public long ManagerId { get; set; }    
    }

    public class StaffLookUp
    {
        public long StaffId { get; set; }
        public string StaffName { get; set; }      
    }
}