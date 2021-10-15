using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace NaitonGps.Models
{
    public class PickList
    {
        [JsonProperty]
        public int PickListId { get; set; }

        [JsonProperty]
        public string PickerName { get; set; }

        [JsonProperty]
        public decimal Products { get; set; }
        
        [JsonProperty]
        public decimal Weight { get; set; }

        //[JsonProperty]
        //public int[] StatusIds { get; set; }

        public string ColorStatus
        {
            get
            {
                return "white";//listColors.ContainsKey(StatusIds?.FirstOrDefault() ?? -1) ? listColors[StatusIds?.FirstOrDefault() ?? -1] : listColors[-1];
            }
        }

        //readonly Dictionary<int, string> listColors = new Dictionary<int, string>
        //{
        //    {-1,"Gray" },
        //    { 0,"White"},
        //    { 2,"Orange"}
        //};
    }
}
