﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

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

    public virtual DbSet<CarePlan> CarePlans { get; set; }

    public virtual DbSet<CareRequest> CareRequests { get; set; }

    public virtual DbSet<CareRequestDetail> CareRequestDetails { get; set; }

    public virtual DbSet<Certification> Certifications { get; set; }

    public virtual DbSet<ConsignmentRequest> ConsignmentRequests { get; set; }

    public virtual DbSet<ConsignmentTransaction> ConsignmentTransactions { get; set; }

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
    {
        optionsBuilder.UseSqlServer(GetConnectionString());
    }

    private string GetConnectionString()
    {
        IConfiguration configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", true, true).Build();
        return configuration["ConnectionStrings:DefaultConnection"];
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BlogPost>(entity =>
        {
            entity.HasKey(e => e.PostId).HasName("blog_post_post_id_primary");

            entity.Property(e => e.PostId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
            entity.Property(e => e.IsActive).HasDefaultValueSql("('DEFAULT TRUE')");
        });

        modelBuilder.Entity<CarePlan>(entity =>
        {
            entity.HasKey(e => e.CarePlanId).HasName("PK__Care_Pla__1AE639AEE48BAF16");

            entity.Property(e => e.CarePlanId).ValueGeneratedNever();
        });

        modelBuilder.Entity<CareRequest>(entity =>
        {
            entity.HasKey(e => e.CareRequestId).HasName("PK__Care_Req__60ADF019247783DC");

            entity.Property(e => e.CareRequestId).ValueGeneratedNever();

            entity.HasOne(d => d.CarePlan).WithMany(p => p.CareRequests).HasConstraintName("FK__Care_Requ__care___2D7CBDC4");
        });

        modelBuilder.Entity<CareRequestDetail>(entity =>
        {
            entity.HasKey(e => e.CareRequestDetailId).HasName("PK__Care_Req__4E3C0C8E650C40E7");

            entity.Property(e => e.CareRequestDetailId).ValueGeneratedNever();

            entity.HasOne(d => d.CareRequest).WithMany(p => p.CareRequestDetails).HasConstraintName("FK__Care_Requ__care___314D4EA8");
        });

        modelBuilder.Entity<Certification>(entity =>
        {
            entity.Property(e => e.CertificateId).ValueGeneratedNever();

            entity.HasOne(d => d.OrderItem).WithMany(p => p.Certifications)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Certification_Order_Item");
        });

        modelBuilder.Entity<ConsignmentRequest>(entity =>
        {
            entity.HasKey(e => e.ConsignmentId).HasName("PK__Consignm__3114B3D0EC8A66E7");

            entity.ToTable("Consignment_Request", tb => tb.HasTrigger("trg_UpdateIsActive"));
        });

        modelBuilder.Entity<ConsignmentTransaction>(entity =>
        {
            entity.HasKey(e => e.TransactionId).HasName("PK__Consignm__85C600AFD7044212");

            entity.Property(e => e.CommissionAmount).HasComputedColumnSql("([sale_price]*(0.05))", false);
            entity.Property(e => e.Earnings).HasComputedColumnSql("([sale_price]-[commission_fee])", false);

            entity.HasOne(d => d.Consignment).WithOne(p => p.ConsignmentTransaction).HasConstraintName("FK_Consignment_Transaction_ConsignmentID");
        });

        modelBuilder.Entity<Customer>(entity =>
        {
            entity.HasKey(e => e.CustomerId).HasName("customer_customer_id_primary");

            entity.Property(e => e.CustomerId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("('GETDATE()')");
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
            entity.HasKey(e => e.KoiId).HasName("animal_animal_id_primary");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

            entity.HasOne(d => d.KoiType).WithMany(p => p.Kois).HasConstraintName("koi_koi_type_id_foreign");
        });

        modelBuilder.Entity<KoiType>(entity =>
        {
            entity.HasKey(e => e.KoiTypeId).HasName("animal_type_animal_type_id_primary");

            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
        });

        modelBuilder.Entity<Order>(entity =>
        {
            entity.HasKey(e => e.OrderId).HasName("order_order_id_primary");

            entity.Property(e => e.OrderId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.OrderDate).HasDefaultValueSql("(getdate())");

            entity.HasOne(d => d.Customer).WithMany(p => p.Orders).HasConstraintName("order_customer_id_foreign");
        });

        modelBuilder.Entity<OrderItem>(entity =>
        {
            entity.HasKey(e => e.OrderItemId).HasName("order_item_order_item_id_primary");

            entity.Property(e => e.OrderItemId).ValueGeneratedNever();
            entity.Property(e => e.CreatedAt).HasDefaultValueSql("(getdate())");
            entity.Property(e => e.IsActive).HasDefaultValue(true);

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