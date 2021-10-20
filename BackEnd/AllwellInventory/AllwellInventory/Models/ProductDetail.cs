using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AllwellInventory.Models
{
    public class ProductDetail
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public decimal? Cost { get; set; }
        public int LocationId { get; set; }
        public string Condition { get; set; }
        public bool Damaged { get; set; }
        public string SerialNo { get; set; }
    }
}