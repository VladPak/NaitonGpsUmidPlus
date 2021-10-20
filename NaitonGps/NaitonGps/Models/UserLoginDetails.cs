using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.Models
{
  public class UserLoginDetails
  {
    [JsonProperty]
    public string userEmail { get; set; }

    [JsonProperty]
    public string userPassword { get; set; }

    [JsonProperty]
    public bool isEncrypted { get; set; }

    [JsonProperty]
    public int appId { get; set; }

    [JsonProperty]
    public string appVersion { get; set; }

    [JsonProperty]
    public string domain { get; set; }

    [JsonProperty]
    public int PersonId { get; set; }

    [JsonProperty]
    public int RoleId { get; set; }
  }
}
