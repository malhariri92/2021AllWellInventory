using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AllwellInventory.Models
{
    [Table("type")]
    public partial class Type
    {
        public Type()
        {
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public int TypeId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(20)]
        public string Name { get; set; }

        [InverseProperty(nameof(Product.Type))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
