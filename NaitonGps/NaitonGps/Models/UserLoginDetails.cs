using System;
using System.Collections.Generic;
using System.Text;

namespace NaitonGps.Models
{
    public class UserLoginDetails
    {
        public string userEmail { get; set; }
        public string userPassword { get; set; }
        public string userToken { get; set; }

        public bool isEncrypted { get; set; }

        public int appId { get; set; }
        public string appVersion { get; set; }
        public string domain { get; set; }

        public string restServiceAddress { get; set; }

        public int PersonId { get; set; }

        public int RoleId { get; set; }

    }
}
