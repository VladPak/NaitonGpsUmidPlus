using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.Models
{
    public class PickList
    {
        [JsonProperty]
        public int PickListId { get; set; }
        [JsonProperty]
        public string PickerName { get; set; }
        [JsonProperty]
        public int Racks { get; set; }
        [JsonProperty]
        public decimal Weight { get; set; }
        [JsonProperty]
        public int? StatusId { get; set; }

        public string ColorStatus
        {
            get
            {
                return listColors.ContainsKey(StatusId ?? -1) ? listColors[StatusId ?? -1] : listColors[-1];
            }
        }

        readonly Dictionary<int, string> listColors = new Dictionary<int, string>
        {
            {-1,"Gray" },
            { 0,"White"},
            { 2,"Orange"}
        };
    }
}
