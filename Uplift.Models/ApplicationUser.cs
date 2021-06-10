using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
   public class ApplicationUser: IdentityUser
    {
        public string Name { get; set; }    //required
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostCode { get; set; }

    }
}
