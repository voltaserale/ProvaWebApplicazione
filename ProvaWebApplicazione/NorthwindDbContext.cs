using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.EntityFrameworkCore;
using ProvaWebApplicazione.Models;

namespace ProvaWebApplicazione;

public partial class NorthwindDbContext : DbContext
{
    public NorthwindDbContext()
    {
    }

    public NorthwindDbContext(DbContextOptions<NorthwindDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Category> Categories { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerDemographic> CustomerDemographics { get; set; }

    public virtual DbSet<Employee> Employees { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderDetail> OrderDetails { get; set; }

    public virtual DbSet<Product> Products { get; set; }

    public virtual DbSet<Region> Regions { get; set; }

    public virtual DbSet<Shipper> Shippers { get; set; }

    public virtual DbSet<Supplier> Suppliers { get; set; }

    public virtual DbSet<Territory> Territories { get; set; }

    public virtual DbSet<UsState> UsStates { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseMySQL("server=localhost;database=northwind_db;uid=root;pwd=5SIA_2023").LogTo(s => Debug.WriteLine(s));

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Category>(entity =>
        {
            entity.HasKey(e => e.CategoryId).HasName("PRIMARY");

            entity.ToTable("categories");

            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.CategoryName)
                .HasMaxLength(15)
                .HasColumnName("category_name");
            entity.Property(e => e.Description)
                .HasColumnType("text")
                .HasColumnName("description");
            entity.Property(e => e.Pictures)
                .HasColumnType("blob")
                .HasColumnName("pictures");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("PRIMARY");

            entity.ToTable("customers");

            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasColumnName("city");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(40)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactName)
                .HasMaxLength(30)
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactTitle)
                .HasMaxLength(30)
                .HasColumnName("contact_title");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasColumnName("country");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasColumnName("fax");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasColumnName("region");

            entity.HasMany(d => d.CustomerTypes).WithMany(p => p.Customers)
                .UsingEntity<Dictionary<string, object>>(
                    "CustomerCustomerDemo",
                    r => r.HasOne<CustomerDemographic>().WithMany()
                        .HasForeignKey("CustomerTypeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("customer_customer_demo_ibfk_1"),
                    l => l.HasOne<Customer>().WithMany()
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("customer_customer_demo_ibfk_2"),
                    j =>
                    {
                        j.HasKey("CustomerId", "CustomerTypeId").HasName("PRIMARY");
                        j.ToTable("customer_customer_demo");
                        j.HasIndex(new[] { "CustomerTypeId" }, "customer_type_id");
                        j.IndexerProperty<string>("CustomerId")
                            .HasMaxLength(10)
                            .HasColumnName("customer_id");
                        j.IndexerProperty<string>("CustomerTypeId")
                            .HasMaxLength(10)
                            .HasColumnName("customer_type_id");
                    });
        });

        modelBuilder.Entity<CustomerDemographic>(entity =>
        {
            entity.HasKey(e => e.CustomerTypeId).HasName("PRIMARY");

            entity.ToTable("customer_demographics");

            entity.Property(e => e.CustomerTypeId)
                .HasMaxLength(10)
                .HasColumnName("customer_type_id");
            entity.Property(e => e.CustomerDesc)
                .HasColumnType("text")
                .HasColumnName("customer_desc");
        });

        modelBuilder.Entity<Employee>(entity =>
        {
            entity.HasKey(e => e.EmployeeId).HasName("PRIMARY");

            entity.ToTable("employees");

            entity.HasIndex(e => e.ReportsTo, "reports_to");

            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.BirthDate)
                .HasColumnType("date")
                .HasColumnName("birth_date");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasColumnName("city");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasColumnName("country");
            entity.Property(e => e.Extension)
                .HasMaxLength(4)
                .HasColumnName("extension");
            entity.Property(e => e.FirstName)
                .HasMaxLength(10)
                .HasColumnName("first_name");
            entity.Property(e => e.HireDate)
                .HasColumnType("date")
                .HasColumnName("hire_date");
            entity.Property(e => e.HomePhone)
                .HasMaxLength(24)
                .HasColumnName("home_phone");
            entity.Property(e => e.LastName)
                .HasMaxLength(20)
                .HasColumnName("last_name");
            entity.Property(e => e.Notes)
                .HasColumnType("text")
                .HasColumnName("notes");
            entity.Property(e => e.Photo)
                .HasColumnType("blob")
                .HasColumnName("photo");
            entity.Property(e => e.PhotoPath)
                .HasMaxLength(255)
                .HasColumnName("photo_path");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasColumnName("region");
            entity.Property(e => e.ReportsTo).HasColumnName("reports_to");
            entity.Property(e => e.Title)
                .HasMaxLength(30)
                .HasColumnName("title");
            entity.Property(e => e.TitleOfCourtesy)
                .HasMaxLength(25)
                .HasColumnName("title_of_courtesy");

            entity.HasOne(d => d.ReportsToNavigation).WithMany(p => p.InverseReportsToNavigation)
                .HasForeignKey(d => d.ReportsTo)
                .HasConstraintName("employees_ibfk_1");

            entity.HasMany(d => d.Territories).WithMany(p => p.Employees)
                .UsingEntity<Dictionary<string, object>>(
                    "EmployeeTerritory",
                    r => r.HasOne<Territory>().WithMany()
                        .HasForeignKey("TerritoryId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("employee_territories_ibfk_1"),
                    l => l.HasOne<Employee>().WithMany()
                        .HasForeignKey("EmployeeId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("employee_territories_ibfk_2"),
                    j =>
                    {
                        j.HasKey("EmployeeId", "TerritoryId").HasName("PRIMARY");
                        j.ToTable("employee_territories");
                        j.HasIndex(new[] { "TerritoryId" }, "territory_id");
                        j.IndexerProperty<short>("EmployeeId").HasColumnName("employee_id");
                        j.IndexerProperty<string>("TerritoryId")
                            .HasMaxLength(20)
                            .HasColumnName("territory_id");
                    });
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("PRIMARY");

            entity.ToTable("orders");

            entity.HasIndex(e => e.CustomerId, "customer_id");

            entity.HasIndex(e => e.EmployeeId, "employee_id");

            entity.HasIndex(e => e.ShipVia, "ship_via");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.CustomerId)
                .HasMaxLength(10)
                .HasColumnName("customer_id");
            entity.Property(e => e.EmployeeId).HasColumnName("employee_id");
            entity.Property(e => e.Freight).HasColumnName("freight");
            entity.Property(e => e.OrderDate)
                .HasColumnType("date")
                .HasColumnName("order_date");
            entity.Property(e => e.RequiredDate)
                .HasColumnType("date")
                .HasColumnName("required_date");
            entity.Property(e => e.ShipAddress)
                .HasMaxLength(60)
                .HasColumnName("ship_address");
            entity.Property(e => e.ShipCity)
                .HasMaxLength(15)
                .HasColumnName("ship_city");
            entity.Property(e => e.ShipCountry)
                .HasMaxLength(15)
                .HasColumnName("ship_country");
            entity.Property(e => e.ShipName)
                .HasMaxLength(40)
                .HasColumnName("ship_name");
            entity.Property(e => e.ShipPostalCode)
                .HasMaxLength(10)
                .HasColumnName("ship_postal_code");
            entity.Property(e => e.ShipRegion)
                .HasMaxLength(15)
                .HasColumnName("ship_region");
            entity.Property(e => e.ShipVia).HasColumnName("ship_via");
            entity.Property(e => e.ShippedDate)
                .HasColumnType("date")
                .HasColumnName("shipped_date");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("orders_ibfk_1");

            entity.HasOne(d => d.Employee).WithMany(p => p.Orders)
                .HasForeignKey(d => d.EmployeeId)
                .HasConstraintName("orders_ibfk_2");

            entity.HasOne(d => d.ShipViaNavigation).WithMany(p => p.Orders)
                .HasForeignKey(d => d.ShipVia)
                .HasConstraintName("orders_ibfk_3");
        });

        modelBuilder.Entity<OrderDetail>(entity =>
        {
            entity.HasKey(e => new { e.OrderId, e.ProductId }).HasName("PRIMARY");

            entity.ToTable("order_details");

            entity.HasIndex(e => e.ProductId, "product_id");

            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.Discount).HasColumnName("discount");
            entity.Property(e => e.Quantity).HasColumnName("quantity");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.OrderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_details_ibfk_2");

            entity.HasOne(d => d.Product).WithMany(p => p.OrderDetails)
                .HasForeignKey(d => d.ProductId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("order_details_ibfk_1");
        });

        modelBuilder.Entity<Product>(entity =>
        {
            entity.HasKey(e => e.ProductId).HasName("PRIMARY");

            entity.ToTable("products");

            entity.HasIndex(e => e.CategoryId, "category_id");

            entity.HasIndex(e => e.SupplierId, "supplier_id");

            entity.Property(e => e.ProductId).HasColumnName("product_id");
            entity.Property(e => e.CategoryId).HasColumnName("category_id");
            entity.Property(e => e.Discontinued).HasColumnName("discontinued");
            entity.Property(e => e.ProductName)
                .HasMaxLength(40)
                .HasColumnName("product_name");
            entity.Property(e => e.QuantityPerUnit)
                .HasMaxLength(20)
                .HasColumnName("quantity_per_unit");
            entity.Property(e => e.ReorderLevel).HasColumnName("reorder_level");
            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.UnitPrice).HasColumnName("unit_price");
            entity.Property(e => e.UnitsInStock).HasColumnName("units_in_stock");
            entity.Property(e => e.UnitsOnOrder).HasColumnName("units_on_order");

            entity.HasOne(d => d.Category).WithMany(p => p.Products)
                .HasForeignKey(d => d.CategoryId)
                .HasConstraintName("products_ibfk_1");

            entity.HasOne(d => d.Supplier).WithMany(p => p.Products)
                .HasForeignKey(d => d.SupplierId)
                .HasConstraintName("products_ibfk_2");
        });

        modelBuilder.Entity<Region>(entity =>
        {
            entity.HasKey(e => e.RegionId).HasName("PRIMARY");

            entity.ToTable("region");

            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.RegionDescription)
                .HasMaxLength(30)
                .HasColumnName("region_description");
        });

        modelBuilder.Entity<Shipper>(entity =>
        {
            entity.HasKey(e => e.ShipperId).HasName("PRIMARY");

            entity.ToTable("shippers");

            entity.Property(e => e.ShipperId).HasColumnName("shipper_id");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(40)
                .HasColumnName("company_name");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
        });

        modelBuilder.Entity<Supplier>(entity =>
        {
            entity.HasKey(e => e.SupplierId).HasName("PRIMARY");

            entity.ToTable("suppliers");

            entity.Property(e => e.SupplierId).HasColumnName("supplier_id");
            entity.Property(e => e.Address)
                .HasMaxLength(60)
                .HasColumnName("address");
            entity.Property(e => e.City)
                .HasMaxLength(15)
                .HasColumnName("city");
            entity.Property(e => e.CompanyName)
                .HasMaxLength(40)
                .HasColumnName("company_name");
            entity.Property(e => e.ContactName)
                .HasMaxLength(30)
                .HasColumnName("contact_name");
            entity.Property(e => e.ContactTitle)
                .HasMaxLength(30)
                .HasColumnName("contact_title");
            entity.Property(e => e.Country)
                .HasMaxLength(15)
                .HasColumnName("country");
            entity.Property(e => e.Fax)
                .HasMaxLength(24)
                .HasColumnName("fax");
            entity.Property(e => e.Homepage)
                .HasColumnType("text")
                .HasColumnName("homepage");
            entity.Property(e => e.Phone)
                .HasMaxLength(24)
                .HasColumnName("phone");
            entity.Property(e => e.PostalCode)
                .HasMaxLength(10)
                .HasColumnName("postal_code");
            entity.Property(e => e.Region)
                .HasMaxLength(15)
                .HasColumnName("region");
        });

        modelBuilder.Entity<Territory>(entity =>
        {
            entity.HasKey(e => e.TerritoryId).HasName("PRIMARY");

            entity.ToTable("territories");

            entity.HasIndex(e => e.RegionId, "region_id");

            entity.Property(e => e.TerritoryId)
                .HasMaxLength(20)
                .HasColumnName("territory_id");
            entity.Property(e => e.RegionId).HasColumnName("region_id");
            entity.Property(e => e.TerritoryDescription)
                .HasMaxLength(30)
                .HasColumnName("territory_description");

            entity.HasOne(d => d.Region).WithMany(p => p.Territories)
                .HasForeignKey(d => d.RegionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("territories_ibfk_1");
        });

        modelBuilder.Entity<UsState>(entity =>
        {
            entity.HasKey(e => e.StateId).HasName("PRIMARY");

            entity.ToTable("us_states");

            entity.Property(e => e.StateId).HasColumnName("state_id");
            entity.Property(e => e.StateAbbr)
                .HasMaxLength(2)
                .HasColumnName("state_abbr");
            entity.Property(e => e.StateName)
                .HasMaxLength(100)
                .HasColumnName("state_name");
            entity.Property(e => e.StateRegion)
                .HasMaxLength(50)
                .HasColumnName("state_region");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
