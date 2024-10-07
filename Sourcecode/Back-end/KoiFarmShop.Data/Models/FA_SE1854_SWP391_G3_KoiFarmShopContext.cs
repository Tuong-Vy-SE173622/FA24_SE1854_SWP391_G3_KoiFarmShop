using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace KoiFarmShop.Data.Models;

public partial class FA_SE1854_SWP391_G3_KoiFarmShopContext : DbContext
{
    public FA_SE1854_SWP391_G3_KoiFarmShopContext()
    {
    }

    public FA_SE1854_SWP391_G3_KoiFarmShopContext(DbContextOptions<FA_SE1854_SWP391_G3_KoiFarmShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<BlogPost> BlogPosts { get; set; }

    public virtual DbSet<CareRequest> CareRequests { get; set; }

    public virtual DbSet<CareRequestDetail> CareRequestDetails { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<ConsignmentDetail> ConsignmentDetails { get; set; }

    public virtual DbSet<ConsignmentRequest> ConsignmentRequests { get; set; }

    public virtual DbSet<Customer> Customers { get; set; }

    public virtual DbSet<Feedback> Feedbacks { get; set; }

    public virtual DbSet<Koi> Kois { get; set; }

    public virtual DbSet<KoiType> KoiTypes { get; set; }

    public virtual DbSet<Order> Orders { get; set; }

    public virtual DbSet<OrderItem> OrderItems { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Rating> Ratings { get; set; }

    public virtual DbSet<Token> Tokens { get; set; }

    public virtual DbSet<User> Users { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=localhost,1433;Initial Catalog=FA_SE1854_SWP391_G3_KoiFarmShop;User ID=sa;Password=12345;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("blog_post_post_id_primary");

            entity.Property(e => e.PostId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
        });

        modelBuilder.Entity<CareRequest>(entity =>
        {
            entity.HasKey(e => e.RequestId).HasName("care_request_request_id_primary");

            entity.Property(e => e.RequestId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
            entity.Property(e => e.RequestedDate).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Customer).WithMany(p => p.CareRequests).HasConstraintName("care_request_customer_id_foreign");

            entity.HasOne(d => d.Koi).WithMany(p => p.CareRequests).HasConstraintName("care_request_koi_id_foreign");
        });

        modelBuilder.Entity<CareRequestDetail>(entity =>
        {
            entity.HasKey(e => e.RequestDetailId).HasName("care_request_detail_request_detail_id_primary");

            entity.Property(e => e.RequestDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Request).WithMany(p => p.CareRequestDetails)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("care_request_detail_request_id_foreign");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.Property(e => e.CertificateId).ValueGeneratedNever();

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Certifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Certification_Order_Item");
        });

        modelBuilder.Entity<ConsignmentDetail>(entity =>
        {
            entity.HasKey(e => e.ConsignmentDetailId).HasName("consignment_item_consignment_id_primary");

            entity.Property(e => e.ConsignmentDetailId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");

            entity.HasOne(d => d.Consignment).WithMany(p => p.ConsignmentDetails).HasConstraintName("FK_Consignment_Detail_Consignment_Request");

            entity.HasOne(d => d.Koi).WithMany(p => p.ConsignmentDetails).HasConstraintName("consignment_item_koi_id_foreign");
        });

        modelBuilder.Entity<ConsignmentRequest>(entity =>
        {
            entity.Property(e => e.ConsignmentId).ValueGeneratedNever();
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
            entity.Property(e => e.SubAmount).IsFixedLength();
            entity.Property(e => e.Vat).IsFixedLength();
            entity.Property(e => e.VatAmount).IsFixedLength();

            entity.HasOne(d => d.Customer).WithMany(p => p.ConsignmentRequests).HasConstraintName("FK_Consignment_Request_Customer");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_customer_id_primary");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.User).WithMany(p => p.Customers).HasConstraintName("customer_user_id_foreign");
        });

        modelBuilder.Entity<Feedback>(entity =>
        {
            entity.HasKey(e => e.FeedbackId).HasName("feedback_feedback_id_primary");

            entity.Property(e => e.FeedbackId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Feedbacks).HasConstraintName("feedback_customer_id_foreign");

            entity.HasOne(d => d.Order).WithMany(p => p.Feedbacks)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("feedback_order_id_foreign");
        });

        modelBuilder.Entity<Koi>(entity =>
        {
            entity.HasKey(e => e.KoiId).HasName("koi_koi_id_primary");

            entity.Property(e => e.KoiId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");

            entity.HasOne(d => d.KoiType).WithMany(p => p.Kois).HasConstraintName("koi_koi_type_id_foreign");
        });

        modelBuilder.Entity<KoiType>(entity =>
        {
            entity.HasKey(e => e.KoiTypeId).HasName("koi_type_koi_type_id_primary");

            entity.Property(e => e.KoiTypeId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("order_order_id_primary");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
            entity.Property(e => e.OrderDate).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("order_customer_id_foreign");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("order_item_order_item_id_primary");

            entity.Property(e => e.OrderItemId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");

            entity.HasOne(d => d.Koi).WithMany(p => p.OrderItems).HasConstraintName("FK_Order_Item_koi");

            entity.HasOne(d => d.Order).WithMany(p => p.OrderItems).HasConstraintName("order_item_order_id_foreign");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.PromotionId).HasName("promotion_promotion_id_primary");

            entity.Property(e => e.PromotionId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
        });

        modelBuilder.Entity<Rating>(entity =>
        {
            entity.HasKey(e => e.RatingId).HasName("rating_rating_id_primary");

            entity.Property(e => e.RatingId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");

            entity.HasOne(d => d.Customer).WithMany(p => p.Ratings).HasConstraintName("rating_customer_id_foreign");

            entity.HasOne(d => d.Koi).WithMany(p => p.Ratings).HasConstraintName("rating_animal_id_foreign");
        });

        modelBuilder.Entity<Token>(entity =>
        {
            entity.Property(e => e.UserId).ValueGeneratedNever();
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("user_user_id_primary");

            entity.Property(e => e.UserId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
