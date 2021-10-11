using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.Models
{
    public class UserDetails
    {
        [JsonProperty("employeeid")]
        public int EmployeeId { get; set; }
        
        [JsonProperty("rolerightid")]
        public int EmployeeRightId { get; set; }

        [JsonProperty("employeename")]
        public string EmployeeName { get; set; }
        
        [JsonProperty("currencyid")]
        public int CurrencyId { get; set; }

        [JsonProperty("defaultstockid")]
        public int DefaultStockId { get; set; }

        [JsonProperty("defaultcountryid")]
        public int DefaultCountryId { get; set; }

        [JsonProperty("databasename")]
        public string DatabaseName { get; set; }

        [JsonProperty("allowtasknotification")]
        public bool AllowTaskNotification { get; set; }

        [JsonProperty("issupervisor")]
        public bool IsSupervisor { get; set; }

        [JsonProperty("dashboardsettings")]
        public string DashboardSettings { get; set; }

        [JsonProperty("version")]
        public int NaitonVersion { get; set; }

        [JsonProperty("companyguid")]
        public string CompanyGuid { get; set; }

        [JsonProperty("isnotupdate")]
        public bool IsNotUpdate { get; set; }

        [JsonProperty("ispasswordexpired")]
        public bool IsPasswordExpired { get; set; }

        [JsonProperty("isloginallowed")]
        public bool IsLoginAllowed { get; set; }

        [JsonProperty("cansave")]
        public bool CanSave { get; set; }
    }
}
