using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace AllwellInventory.Models
{
    public partial class CoreDbContext : DbContext
    {
        public CoreDbContext()
        {
        }

        public CoreDbContext(DbContextOptions<CoreDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<AssignLog> AssignLogs { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<Product> Products { get; set; }
        public virtual DbSet<Type> Types { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Data Source=DESKTOP-UQQHOLF;Initial Catalog=AllwellInventory;Integrated Security=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<AssignLog>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.AssignLogs)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assign_log_Employee");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.AssignLogs)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_assign_log_Products");
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.fName).IsFixedLength(true);

                entity.Property(e => e.lName).IsFixedLength(true);

                entity.Property(e => e.Password).IsFixedLength(true);

                entity.Property(e => e.Username).IsFixedLength(true);

            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.City).IsFixedLength(true);

                entity.Property(e => e.County).IsFixedLength(true);

                entity.Property(e => e.Name).IsFixedLength(true);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Condition).IsFixedLength(true);

                entity.Property(e => e.Name).IsFixedLength(true);

                entity.Property(e => e.SerialNo).IsFixedLength(true);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Location");

                entity.HasOne(d => d.Type)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.TypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Products_Type");

                entity.HasOne(d => d.Worker)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.WorkerId)
                    .HasConstraintName("FK_Products_Employee");
            });

            modelBuilder.Entity<Type>(entity =>
            {
                entity.Property(e => e.Id).ValueGeneratedNever();

                entity.Property(e => e.Name).IsFixedLength(true);
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
