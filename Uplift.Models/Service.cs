using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Uplift.Models
{
    public class Service
    {
        public int Id { get; set; }
        public string Name { get; set; }    //required, display name = Service Name
        public double Price { get; set; }   //required
        public string LongDescription { get; set; }//display name = description
       
        [DataType(DataType.ImageUrl)]
        public string ImageUrl { get; set; }    //datatype = imageurl, display = Image
        public int CategoryId { get; set; } //required
        public Category Category { get; set; }//FK ref
        public int FrequencyId { get; set; }
        public Frequency Frequency { get; set; }





    }
}
