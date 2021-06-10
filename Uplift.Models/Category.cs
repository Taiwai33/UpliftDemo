using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
    public class Category
    {
        public int Id { get; set; } // pk
        public string Name { get; set; }    //required, display = category name
        public int DisplayOrder { get; set; }   // required, display = display order

    }
}
