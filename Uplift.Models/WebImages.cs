using System;
using System.Collections.Generic;
using System.Text;

namespace Uplift.Models
{
    public class WebImages
    {
        public int Id { get; set; }
        public string Name { get; set; }// required
        public byte[] Picture { get; set; }//required
    }
}
