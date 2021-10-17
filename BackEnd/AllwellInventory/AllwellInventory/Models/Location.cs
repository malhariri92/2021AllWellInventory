using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AllwellInventory.Models
{
    [Table("location")]
    public partial class Location
    {
        public Location()
        {
            Employees = new HashSet<Employee>();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public int LocationId { get; set; }
        [Required]
        [Column("name")]
        [StringLength(30)]
        public string Name { get; set; }
        [Column("city")]
        [StringLength(30)]
        public string City { get; set; }
        [Column("county")]
        [StringLength(30)]
        public string County { get; set; }

        public virtual ICollection<Employee> Employees { get; set; }
        [InverseProperty(nameof(Product.Location))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
