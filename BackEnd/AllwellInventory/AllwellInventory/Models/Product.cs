using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AllwellInventory.Models
{
    [Table("products")]
    public partial class Product
    {
        public Product()
        {
            AssignLogs = new HashSet<AssignLog>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("name")]
        [StringLength(10)]
        public string Name { get; set; }
        [Column("typeId")]
        public int TypeId { get; set; }
        [Column("cost", TypeName = "decimal(6, 2)")]
        public decimal? Cost { get; set; }
        [Column("workerId")]
        public int? WorkerId { get; set; }
        [Column("locationId")]
        public int LocationId { get; set; }
        [Required]
        [Column("condition")]
        [StringLength(10)]
        public string Condition { get; set; }
        [Column("damaged")]
        public bool Damaged { get; set; }
        [Column("serialNo")]
        [StringLength(50)]
        public string SerialNo { get; set; }

        [ForeignKey(nameof(LocationId))]
        [InverseProperty("Products")]
        public virtual Location Location { get; set; }
        [ForeignKey(nameof(TypeId))]
        [InverseProperty("Products")]
        public virtual Type Type { get; set; }
        [ForeignKey(nameof(WorkerId))]
        [InverseProperty(nameof(Employee.Products))]
        public virtual Employee Worker { get; set; }
        [InverseProperty(nameof(AssignLog.Product))]
        public virtual ICollection<AssignLog> AssignLogs { get; set; }
    }
}
