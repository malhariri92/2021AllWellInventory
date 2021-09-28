using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace AllwellInventory.Models
{
    [Table("assign_log")]
    public partial class AssignLog
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("employeeId")]
        public int EmployeeId { get; set; }
        [Column("productId")]
        public int ProductId { get; set; }
        [Column("assignedDate", TypeName = "datetime")]
        public DateTime AssignedDate { get; set; }
        [Column("returnedDate", TypeName = "datetime")]
        public DateTime? ReturnedDate { get; set; }

        [ForeignKey(nameof(EmployeeId))]
        [InverseProperty("AssignLogs")]
        public virtual Employee Employee { get; set; }
        [ForeignKey(nameof(ProductId))]
        [InverseProperty("AssignLogs")]
        public virtual Product Product { get; set; }
    }
}
