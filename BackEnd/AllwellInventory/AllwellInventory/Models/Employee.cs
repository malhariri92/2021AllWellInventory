using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AllwellInventory.Models
{
    [Table("employee")]
    public partial class Employee
    {
        public Employee()
        {
            AssignLogs = new HashSet<AssignLog>();
            Products = new HashSet<Product>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("fName")]
        [StringLength(20)]
        public string fName { get; set; }
        [Column("lName")]
        [StringLength(20)]
        public string lName { get; set; }
        [Column("username")]
        [StringLength(20)]
        public string Username { get; set; }
        [Column("password")]
        [StringLength(14)]
        public string Password { get; set; }
        [Column("isAdmin")]
        public bool IsAdmin { get; set; }
        [Column("inactive")]
        public bool inactive { get; set; }

        [InverseProperty(nameof(AssignLog.Employee))]
        public virtual ICollection<AssignLog> AssignLogs { get; set; }
        [InverseProperty(nameof(Product.Worker))]
        public virtual ICollection<Product> Products { get; set; }
    }
}
