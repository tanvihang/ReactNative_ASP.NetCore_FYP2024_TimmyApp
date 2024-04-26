using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace webapi.Models;

public partial class TimmyDbContext : DbContext
{
    public TimmyDbContext()
    {
    }

    public TimmyDbContext(DbContextOptions<TimmyDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Administrator> Administrators { get; set; }

    public virtual DbSet<DailySearch> DailySearches { get; set; }

    public virtual DbSet<Notification> Notifications { get; set; }

    public virtual DbSet<PriceHistory> PriceHistories { get; set; }

    public virtual DbSet<Scraper> Scrapers { get; set; }

    public virtual DbSet<SubscribedProduct> SubscribedProducts { get; set; }

    public virtual DbSet<TimmyProduct> TimmyProducts { get; set; }

    public virtual DbSet<TimmyProductBase> TimmyProductBases { get; set; }

    public virtual DbSet<TimmyProductModel> TimmyProductModels { get; set; }

    public virtual DbSet<UserFavourite> UserFavourites { get; set; }

    public virtual DbSet<UserSearchHistory> UserSearchHistories { get; set; }

    public virtual DbSet<UserSubscription> UserSubscriptions { get; set; }

    public virtual DbSet<UserSubscriptionProduct> UserSubscriptionProducts { get; set; }

    public virtual DbSet<UserT> UserTs { get; set; }

    public virtual DbSet<UserVerificationCode> UserVerificationCodes { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\localDB1;Initial Catalog=TimmyDBV2;Integrated Security=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Administrator>(entity =>
        {
            entity.HasKey(e => e.AdministratorId).HasName("PK__Administ__3871E7AC5C29EDE9");
        });

        modelBuilder.Entity<DailySearch>(entity =>
        {
            entity.HasKey(e => e.ProductName).HasName("PK__DailySea__2B5A6A5E7F396C36");
        });

        modelBuilder.Entity<Notification>(entity =>
        {
            entity.HasKey(e => e.NotificationId).HasName("PK__Notifica__E059842F4337283A");

            entity.HasOne(d => d.User).WithMany(p => p.Notifications)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_Notification_User_user_id");
        });

        modelBuilder.Entity<PriceHistory>(entity =>
        {
            entity.HasKey(e => e.PriceHistoryId).HasName("PK__PriceHis__875C191D61AEC111");

            entity.HasOne(d => d.TimmyProductFullNameNavigation).WithMany(p => p.PriceHistories)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_PriceHistory_TimmyProduct_timmy");
        });

        modelBuilder.Entity<Scraper>(entity =>
        {
            entity.HasKey(e => e.ScrapeId).HasName("PK__Scraper__EC5B6F2B822CDC20");
        });

        modelBuilder.Entity<SubscribedProduct>(entity =>
        {
            entity.HasKey(e => e.SubscribedProductFullName).HasName("PK__Subscrib__6F6F2EEDB5DB2356");
        });

        modelBuilder.Entity<TimmyProduct>(entity =>
        {
            entity.HasKey(e => e.TimmyProductFullName).HasName("PK__TimmyPro__46BE4CD6645E9D4C");
        });

        modelBuilder.Entity<TimmyProductBase>(entity =>
        {
            entity.HasKey(e => e.TimmyProductBaseId).HasName("PK__TimmyPro__EBCF09F2BB8A04A1");
        });

        modelBuilder.Entity<TimmyProductModel>(entity =>
        {
            entity.HasKey(e => e.TimmyProductModelId).HasName("PK__TimmyPro__96C0EB96283B2860");

            entity.HasOne(d => d.TimmyProductBase).WithMany(p => p.TimmyProductModels)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_TimmyProductModel_TimmyProductBase_base_id");
        });

        modelBuilder.Entity<UserFavourite>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.ProductUniqueId }).HasName("PK__UserFavo__8AEBDAE7FBFCC272");

            entity.HasOne(d => d.User).WithMany(p => p.UserFavourites).HasConstraintName("FK_UserFavourite_User_user_id");
        });

        modelBuilder.Entity<UserSearchHistory>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.UserSearchHistoryProductFullName }).HasName("PK__UserSear__0AA9CCD9F5DC3880");

            entity.HasOne(d => d.User).WithMany(p => p.UserSearchHistories).HasConstraintName("FK_UserSearchHistory_User_user_id");
        });

        modelBuilder.Entity<UserSubscription>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionId).HasName("PK__UserSubs__14DBAAD9A144F226");

            entity.HasOne(d => d.User).WithMany(p => p.UserSubscriptions)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserSubscription_User_user_id");
        });

        modelBuilder.Entity<UserSubscriptionProduct>(entity =>
        {
            entity.HasKey(e => e.UserSubscriptionProductId).HasName("PK__UserSubs__F979348170CE3B60");

            entity.HasOne(d => d.UserSubscription).WithMany(p => p.UserSubscriptionProducts)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserSubscriptionProduct_UserSubscription_id");
        });

        modelBuilder.Entity<UserT>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK__UserT__B9BE370FB0848483");
        });

        modelBuilder.Entity<UserVerificationCode>(entity =>
        {
            entity.HasKey(e => e.UserEmail).HasName("PK__UserVeri__B0FBA21380A9F186");

            entity.HasOne(d => d.User).WithMany(p => p.UserVerificationCodes)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK_UserVerificationCode_User_user_id");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
