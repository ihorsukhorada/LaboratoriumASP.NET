using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Zaliczenie.Models.Gravity;

public partial class GravityContext : DbContext
{
    public GravityContext()
    {
    }

    public GravityContext(DbContextOptions<GravityContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Address> Addresses { get; set; }

    public virtual DbSet<AddressStatus> AddressStatuses { get; set; }

    public virtual DbSet<Author> Authors { get; set; }

    public virtual DbSet<Book> Books { get; set; }

    public virtual DbSet<BookLanguage> BookLanguages { get; set; }

    public virtual DbSet<Country> Countries { get; set; }

    public virtual DbSet<CustOrder> CustOrders { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<CustomerAddress> CustomerAddresses { get; set; }

    public virtual DbSet<OrderHistory> OrderHistories { get; set; }

    public virtual DbSet<OrderLine> OrderLines { get; set; }

    public virtual DbSet<OrderStatus> OrderStatuses { get; set; }

    public virtual DbSet<Publisher> Publishers { get; set; }

    public virtual DbSet<ShippingMethod> ShippingMethods { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlite("Data source=C:\\data\\gravity.db");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Address>(entity =>
        {
            entity.ToTable("address");

            entity.Property(e => e.AddressId)
                .ValueGeneratedNever()
                .HasColumnName("address_id");
            entity.Property(e => e.City).HasColumnName("city");
            entity.Property(e => e.CountryId).HasColumnName("country_id");
            entity.Property(e => e.StreetName).HasColumnName("street_name");
            entity.Property(e => e.StreetNumber).HasColumnName("street_number");

            entity.HasOne(d => d.Country).WithMany(p => p.Addresses).HasForeignKey(d => d.CountryId);
        });

        modelBuilder.Entity<AddressStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("address_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.AddressStatus1).HasColumnName("address_status");
        });

        modelBuilder.Entity<Author>(entity =>
        {
            entity.ToTable("author");

            entity.Property(e => e.AuthorId)
                .ValueGeneratedNever()
                .HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasDefaultValueSql("NULL")
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<Book>(entity =>
        {
            entity.ToTable("book");

            entity.Property(e => e.BookId)
                .ValueGeneratedNever()
                .HasColumnName("book_id");
            entity.Property(e => e.Isbn13).HasColumnName("isbn13");
            entity.Property(e => e.LanguageId).HasColumnName("language_id");
            entity.Property(e => e.NumPages).HasColumnName("num_pages");
            entity.Property(e => e.PublicationDate)
                .HasColumnType("DATE")
                .HasColumnName("publication_date");
            entity.Property(e => e.PublisherId).HasColumnName("publisher_id");
            entity.Property(e => e.Title).HasColumnName("title");

            entity.HasOne(d => d.Language).WithMany(p => p.Books).HasForeignKey(d => d.LanguageId);

            entity.HasOne(d => d.Publisher).WithMany(p => p.Books).HasForeignKey(d => d.PublisherId);

            entity.HasMany(d => d.Authors).WithMany(p => p.Books)
                .UsingEntity<Dictionary<string, object>>(
                    "BookAuthor",
                    r => r.HasOne<Author>().WithMany()
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    l => l.HasOne<Book>().WithMany()
                        .HasForeignKey("BookId")
                        .OnDelete(DeleteBehavior.ClientSetNull),
                    j =>
                    {
                        j.HasKey("BookId", "AuthorId");
                        j.ToTable("book_author");
                        j.IndexerProperty<int>("BookId").HasColumnName("book_id");
                        j.IndexerProperty<int>("AuthorId").HasColumnName("author_id");
                    });
        });

        modelBuilder.Entity<BookLanguage>(entity =>
        {
            entity.HasKey(e => e.LanguageId);

            entity.ToTable("book_language");

            entity.Property(e => e.LanguageId)
                .ValueGeneratedNever()
                .HasColumnName("language_id");
            entity.Property(e => e.LanguageCode).HasColumnName("language_code");
            entity.Property(e => e.LanguageName).HasColumnName("language_name");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.ToTable("country");

            entity.Property(e => e.CountryId)
                .ValueGeneratedNever()
                .HasColumnName("country_id");
            entity.Property(e => e.CountryName).HasColumnName("country_name");
        });

        modelBuilder.Entity<CustOrder>(entity =>
        {
            entity.HasKey(e => e.OrderId);

            entity.ToTable("cust_order");

            entity.Property(e => e.OrderId)
                .ValueGeneratedNever()
                .HasColumnName("order_id");
            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.DestAddressId).HasColumnName("dest_address_id");
            entity.Property(e => e.OrderDate)
                .HasColumnType("DATETIME")
                .HasColumnName("order_date");
            entity.Property(e => e.ShippingMethodId).HasColumnName("shipping_method_id");

            entity.HasOne(d => d.Customer).WithMany(p => p.CustOrders).HasForeignKey(d => d.CustomerId);

            entity.HasOne(d => d.DestAddress).WithMany(p => p.CustOrders).HasForeignKey(d => d.DestAddressId);

            entity.HasOne(d => d.ShippingMethod).WithMany(p => p.CustOrders).HasForeignKey(d => d.ShippingMethodId);
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.ToTable("customer");

            entity.Property(e => e.CustomerId)
                .ValueGeneratedNever()
                .HasColumnName("customer_id");
            entity.Property(e => e.Email).HasColumnName("email");
            entity.Property(e => e.FirstName).HasColumnName("first_name");
            entity.Property(e => e.LastName).HasColumnName("last_name");
        });

        modelBuilder.Entity<CustomerAddress>(entity =>
        {
            entity.HasKey(e => new { e.CustomerId, e.AddressId });

            entity.ToTable("customer_address");

            entity.Property(e => e.CustomerId).HasColumnName("customer_id");
            entity.Property(e => e.AddressId).HasColumnName("address_id");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Address).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.AddressId)
                .OnDelete(DeleteBehavior.ClientSetNull);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerAddresses)
                .HasForeignKey(d => d.CustomerId)
                .OnDelete(DeleteBehavior.ClientSetNull);
        });

        modelBuilder.Entity<OrderHistory>(entity =>
        {
            entity.HasKey(e => e.HistoryId);

            entity.ToTable("order_history");

            entity.Property(e => e.HistoryId)
                .ValueGeneratedNever()
                .HasColumnName("history_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.StatusDate)
                .HasColumnType("DATETIME")
                .HasColumnName("status_date");
            entity.Property(e => e.StatusId).HasColumnName("status_id");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderHistories).HasForeignKey(d => d.OrderId);

            entity.HasOne(d => d.Status).WithMany(p => p.OrderHistories).HasForeignKey(d => d.StatusId);
        });

        modelBuilder.Entity<OrderLine>(entity =>
        {
            entity.HasKey(e => e.LineId);

            entity.ToTable("order_line");

            entity.Property(e => e.LineId)
                .ValueGeneratedNever()
                .HasColumnName("line_id");
            entity.Property(e => e.BookId).HasColumnName("book_id");
            entity.Property(e => e.OrderId).HasColumnName("order_id");
            entity.Property(e => e.Price).HasColumnName("price");

            entity.HasOne(d => d.Book).WithMany(p => p.OrderLines).HasForeignKey(d => d.BookId);

            entity.HasOne(d => d.Order).WithMany(p => p.OrderLines).HasForeignKey(d => d.OrderId);
        });

        modelBuilder.Entity<OrderStatus>(entity =>
        {
            entity.HasKey(e => e.StatusId);

            entity.ToTable("order_status");

            entity.Property(e => e.StatusId)
                .ValueGeneratedNever()
                .HasColumnName("status_id");
            entity.Property(e => e.StatusValue).HasColumnName("status_value");
        });

        modelBuilder.Entity<Publisher>(entity =>
        {
            entity.ToTable("publisher");

            entity.Property(e => e.PublisherId)
                .ValueGeneratedNever()
                .HasColumnName("publisher_id");
            entity.Property(e => e.PublisherName).HasColumnName("publisher_name");
        });

        modelBuilder.Entity<ShippingMethod>(entity =>
        {
            entity.HasKey(e => e.MethodId);

            entity.ToTable("shipping_method");

            entity.Property(e => e.MethodId)
                .ValueGeneratedNever()
                .HasColumnName("method_id");
            entity.Property(e => e.Cost).HasColumnName("cost");
            entity.Property(e => e.MethodName).HasColumnName("method_name");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
