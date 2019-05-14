namespace SkiRent.Entities
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class Model : DbContext
	{
		public Model()
			: base("name=db_aws")
		{
		}

		public virtual DbSet<Category> Categories { get; set; }
		public virtual DbSet<Customer> Customers { get; set; }
		public virtual DbSet<Employee> Employees { get; set; }
		public virtual DbSet<Item> Items { get; set; }
		public virtual DbSet<Order> Orders { get; set; }
		public virtual DbSet<Payment> Payments { get; set; }
		public virtual DbSet<RentedItem> RentedItems { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Category>()
				.Property(e => e.Name)
				.IsUnicode(false);

			modelBuilder.Entity<Category>()
				.HasMany(e => e.Items)
				.WithRequired(e => e.Category)
				.HasForeignKey(e => e.CategoryID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Category>()
				.HasOptional(d => d.ParentCategory)
				.WithMany(p => p.SubCategories)
				.HasForeignKey(p => p.ParentCategoryID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.FirstName)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.LastName)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.Property(e => e.IdentyficationNumber)
				.IsUnicode(false);

			modelBuilder.Entity<Customer>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Customer)
				.HasForeignKey(e => e.CustomerID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.FirstName)
				.IsUnicode(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.LastName)
				.IsUnicode(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.Address)
				.IsUnicode(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.Login)
				.IsUnicode(false);

			modelBuilder.Entity<Employee>()
				.Property(e => e.Password)
				.IsUnicode(false);

			modelBuilder.Entity<Employee>()
				.HasMany(e => e.Orders)
				.WithRequired(e => e.Employee)
				.HasForeignKey(e => e.EmployeeID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Item>()
				.Property(e => e.Manufacturer)
				.IsUnicode(false);

			modelBuilder.Entity<Item>()
				.Property(e => e.ModelName)
				.IsUnicode(false);

			modelBuilder.Entity<Item>()
				.Property(e => e.Size)
				.IsUnicode(false);

			modelBuilder.Entity<Item>()
				.Property(e => e.Avaiable)
				.IsUnicode(false);

			modelBuilder.Entity<Item>()
				.HasMany(e => e.Rented_Items)
				.WithRequired(e => e.Item)
				.HasForeignKey(e => e.ItemID)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Order>()
				.Property(e => e.Comment)
				.IsUnicode(false);

			modelBuilder.Entity<Order>()
				.HasMany(e => e.Rented_Items)
				.WithRequired(e => e.Order)
				.HasForeignKey(e => e.OrderID)
				.WillCascadeOnDelete(true);

			modelBuilder.Entity<Payment>()
				.Property(e => e.Currency)
				.IsUnicode(false);

            modelBuilder.Entity<Order>()
                .HasMany(e => e.Payments)
                .WithRequired(e => e.Order)
                .HasForeignKey(e => e.OrderID)
                .WillCascadeOnDelete(true);
        }
	}
}
