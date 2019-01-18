namespace InventoryBusinessLogic.Entity
{
    using System;
    using System.Data.Entity;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Linq;

    public partial class Inventory : DbContext
    {
        public Inventory()
            : base("name=Inventory")
        {
        }

        public virtual DbSet<Adjustment> Adjustment { get; set; }
        public virtual DbSet<AdjustmentItem> AdjustmentItem { get; set; }
        public virtual DbSet<AspNetRoles> AspNetRoles { get; set; }
        public virtual DbSet<AspNetUserClaims> AspNetUserClaims { get; set; }
        public virtual DbSet<AspNetUserLogins> AspNetUserLogins { get; set; }
        public virtual DbSet<AspNetUserRoles> AspNetUserRoles { get; set; }
        public virtual DbSet<AspNetUsers> AspNetUsers { get; set; }
        public virtual DbSet<Catalogue> Catalogue { get; set; }
        public virtual DbSet<Department> Department { get; set; }
        public virtual DbSet<Order> Order { get; set; }
        public virtual DbSet<PurchaseItem> PurchaseItem { get; set; }
        public virtual DbSet<PurchaseOrder> PurchaseOrder { get; set; }
        public virtual DbSet<Request> Request { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Adjustment>()
                .Property(e => e.AdjustmentStatus)
                .IsFixedLength();

            modelBuilder.Entity<Adjustment>()
                .HasMany(e => e.AdjustmentItem)
                .WithRequired(e => e.Adjustment)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<AdjustmentItem>()
                .Property(e => e.Quantity)
                .IsUnicode(false);

            modelBuilder.Entity<AdjustmentItem>()
                .Property(e => e.Reason)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetRoles>()
                .HasMany(e => e.AspNetUserRoles)
                .WithRequired(e => e.AspNetRoles)
                .HasForeignKey(e => e.RoleId);

            modelBuilder.Entity<AspNetUsers>()
                .Property(e => e.UserType)
                .IsUnicode(false);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Adjustment)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.Supervisor);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Department)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.DepartmentRep);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Department1)
                .WithOptional(e => e.AspNetUsers1)
                .HasForeignKey(e => e.DepartmentHead);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.PurchaseOrder)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.OrderBy);

            modelBuilder.Entity<AspNetUsers>()
                .HasMany(e => e.Request)
                .WithOptional(e => e.AspNetUsers)
                .HasForeignKey(e => e.UserID);

            modelBuilder.Entity<Catalogue>()
                .Property(e => e.Category)
                .IsUnicode(false);

            modelBuilder.Entity<Catalogue>()
                .Property(e => e.MeasureUnit)
                .IsUnicode(false);

            modelBuilder.Entity<Catalogue>()
                .Property(e => e.BinNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Catalogue>()
                .HasMany(e => e.AdjustmentItem)
                .WithRequired(e => e.Catalogue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Catalogue>()
                .HasMany(e => e.PurchaseItem)
                .WithRequired(e => e.Catalogue)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.DepartmentName)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .Property(e => e.CollectionPoint)
                .IsUnicode(false);

            modelBuilder.Entity<Department>()
                .HasMany(e => e.AspNetUsers2)
                .WithOptional(e => e.Department2)
                .HasForeignKey(e => e.DepartmentID);

            modelBuilder.Entity<Order>()
                .Property(e => e.OrderStatus)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.DeliverAddress)
                .IsUnicode(false);

            modelBuilder.Entity<PurchaseOrder>()
                .Property(e => e.PurchaseOrderStatus)
                .IsFixedLength();

            modelBuilder.Entity<PurchaseOrder>()
                .HasMany(e => e.PurchaseItem)
                .WithRequired(e => e.PurchaseOrder)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Request>()
                .Property(e => e.Remarks)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.SupplierName)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.ContactName)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.PhoneNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.FaxNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.Address)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .Property(e => e.GSTNumber)
                .IsUnicode(false);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Catalogue)
                .WithOptional(e => e.Supplier)
                .HasForeignKey(e => e.Supplier1);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Catalogue1)
                .WithOptional(e => e.Supplier4)
                .HasForeignKey(e => e.Supplier2);

            modelBuilder.Entity<Supplier>()
                .HasMany(e => e.Catalogue2)
                .WithOptional(e => e.Supplier5)
                .HasForeignKey(e => e.Supplier3);
        }
    }
}
