using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
    public class OrderHeader
    {
        public int Id { get; set; }
        public string Name { get; set; }    //required
        public string Phone { get; set; }   //required
        public string Email { get; set; } //required
        public string Address { get; set; }  //required   
        public string City { get; set; }     
        public string State { get; set; }
        public string PostCode { get; set; }
        public DateTime OrderDate { get; set; }
        public string Status { get; set; }
        public int ServiceCount { get; set; }



    }
}
