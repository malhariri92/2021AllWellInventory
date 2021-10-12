using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllwellInventory.Models
{
    public class ProductDetail
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public decimal? Cost { get; set; }
        public string Location { get; set; }
        public string Condition { get; set; }
        public bool Damaged { get; set; }
        public string SerialNo { get; set; }
    }
}