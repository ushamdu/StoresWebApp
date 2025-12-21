using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace StoresWebApp.Models.CustomModels
{
    public static class Utility
    {
        public static string GetDescription(this Enum enumValue)
        {
            var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());

            var descriptionAttributes = (DescriptionAttribute[])fieldInfo.GetCustomAttributes(typeof(DescriptionAttribute), false);

            return descriptionAttributes.Length > 0 ? descriptionAttributes[0].Description : enumValue.ToString();
        }
    }
    //-- Order status: 1 = Pending; 2 = Processing; 3 = Rejected; 4 = Completed
    public enum OrderStatus
    {
        [Description("Pending")]
        PENDING = 1,
        [Description("Processing")]
        PROCESSING = 2,
        [Description("Rejected")]
        REJECTED = 3,
        [Description("Completed")]
        COMPLETED = 4
    }
}