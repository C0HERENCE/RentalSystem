using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using RentalSystem.Models;

#nullable disable

namespace RentalSystem.Dao
{
    public partial class RentalSystemDbContext : DbContext
    {
        public RentalSystemDbContext()
        {
        }

        public RentalSystemDbContext(DbContextOptions<RentalSystemDbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; }
        public virtual DbSet<Brand> Brands { get; set; }
        public virtual DbSet<Cart> Carts { get; set; }
        public virtual DbSet<Category> Categories { get; set; }
        public virtual DbSet<Good> Goods { get; set; }
        public virtual DbSet<Order> Orders { get; set; }
        public virtual DbSet<OrderInfo> OrderInfos { get; set; }
        public virtual DbSet<OrderOperation> OrderOperations { get; set; }
        public virtual DbSet<OrderReturn> OrderReturns { get; set; }
        public virtual DbSet<Review> Reviews { get; set; }
        public virtual DbSet<User> Users { get; set; }
        public virtual DbSet<Website> Websites { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Name=RentalSystem:ConnectionString");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "Chinese_PRC_CI_AS");

            modelBuilder.Entity<Address>(entity =>
            {
                entity.ToTable("address");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Detail)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("detail");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.Tel)
                    .HasMaxLength(20)
                    .IsUnicode(false)
                    .HasColumnName("tel");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Addresses)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_ADDRESS_REFERENCE_USER");
            });

            modelBuilder.Entity<Brand>(entity =>
            {
                entity.ToTable("brand");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<Cart>(entity =>
            {
                entity.ToTable("carts");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.GoodsId).HasColumnName("goods_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.GoodsId)
                    .HasConstraintName("FK_CARTS_REFERENCE_GOODS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Carts)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_CARTS_REFERENCE_USER");
            });

            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("category");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.ParentId).HasColumnName("parent_id");
            });

            modelBuilder.Entity<Good>(entity =>
            {
                entity.ToTable("goods");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.BrandId).HasColumnName("brand_id");

                entity.Property(e => e.CategoryId).HasColumnName("category_id");

                entity.Property(e => e.Description)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.DetailHtml)
                    .HasMaxLength(3000)
                    .IsUnicode(false)
                    .HasColumnName("detail_html");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.Name)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.OnSale).HasColumnName("on_sale");

                entity.Property(e => e.OriginalPrice)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("original_price");

                entity.Property(e => e.Pic)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("pic");

                entity.Property(e => e.Price)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("price");

                entity.Property(e => e.Stock).HasColumnName("stock");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Brand)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.BrandId)
                    .HasConstraintName("FK_GOODS_REFERENCE_BRAND");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.CategoryId)
                    .HasConstraintName("FK_GOODS_REFERENCE_CATEGORY");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Goods)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_GOODS_REFERENCE_USER");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.ToTable("order");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.AddressId).HasColumnName("address_id");

                entity.Property(e => e.CommitTime)
                    .HasColumnType("datetime")
                    .HasColumnName("commit_time");

                entity.Property(e => e.ConfirmTime)
                    .HasColumnType("datetime")
                    .HasColumnName("confirm_time");

                entity.Property(e => e.CustomerId).HasColumnName("customer_id");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.Property(e => e.PayTime)
                    .HasColumnType("datetime")
                    .HasColumnName("pay_time");

                entity.Property(e => e.PaymentStatus).HasColumnName("payment_status");

                entity.Property(e => e.SellerId).HasColumnName("seller_id");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Customer)
                    .WithMany(p => p.OrderCustomers)
                    .HasForeignKey(d => d.CustomerId)
                    .HasConstraintName("FK_ORDER_REFERENCE_CUSTOMER");

                entity.HasOne(d => d.Seller)
                    .WithMany(p => p.OrderSellers)
                    .HasForeignKey(d => d.SellerId)
                    .HasConstraintName("FK_ORDER_REFERENCE_SELLER");
            });

            modelBuilder.Entity<OrderInfo>(entity =>
            {
                entity.ToTable("order_info");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.GoodsId).HasColumnName("goods_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Quantity).HasColumnName("quantity");

                entity.Property(e => e.TotalPrice)
                    .HasColumnType("decimal(18, 0)")
                    .HasColumnName("total_price");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.GoodsId)
                    .HasConstraintName("FK_ORDER_IN_REFERENCE_GOODS");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderInfos)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ORDER_IN_REFERENCE_ORDER");
            });

            modelBuilder.Entity<OrderOperation>(entity =>
            {
                entity.ToTable("order_operation");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.Note)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("note");

                entity.Property(e => e.OperatorUserId).HasColumnName("operator_user_id");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.OrderStatus).HasColumnName("order_status");

                entity.HasOne(d => d.OperatorUser)
                    .WithMany(p => p.OrderOperations)
                    .HasForeignKey(d => d.OperatorUserId)
                    .HasConstraintName("FK_ORDER_OP_REFERENCE_USER");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderOperations)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ORDER_OP_REFERENCE_ORDER");
            });

            modelBuilder.Entity<OrderReturn>(entity =>
            {
                entity.ToTable("order_return");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.OrderId).HasColumnName("order_id");

                entity.Property(e => e.Status).HasColumnName("status");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderReturns)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_ORDER_RE_REFERENCE_ORDER");
            });

            modelBuilder.Entity<Review>(entity =>
            {
                entity.ToTable("review");

                entity.Property(e => e.Id)
                    .ValueGeneratedNever()
                    .HasColumnName("id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1000)
                    .IsUnicode(false)
                    .HasColumnName("content");

                entity.Property(e => e.CreateTime)
                    .HasColumnType("datetime")
                    .HasColumnName("create_time");

                entity.Property(e => e.DisplayStatus).HasColumnName("display_status");

                entity.Property(e => e.GoodsId).HasColumnName("goods_id");

                entity.Property(e => e.UserId).HasColumnName("user_id");

                entity.HasOne(d => d.Goods)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.GoodsId)
                    .HasConstraintName("FK_REVIEW_REFERENCE_GOODS");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Reviews)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_REVIEW_REFERENCE_USER");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.ToTable("user");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("description");

                entity.Property(e => e.Enabled).HasColumnName("enabled");

                entity.Property(e => e.IsAdmin).HasColumnName("is_admin");

                entity.Property(e => e.Password)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("password");

                entity.Property(e => e.Username)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("username");
            });

            modelBuilder.Entity<Website>(entity =>
            {
                entity.HasNoKey();

                entity.ToTable("website");

                entity.Property(e => e.WebsiteSubtitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("website_subtitle");

                entity.Property(e => e.WebsiteTitle)
                    .HasMaxLength(255)
                    .IsUnicode(false)
                    .HasColumnName("website_title");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
