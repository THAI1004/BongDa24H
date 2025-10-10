using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace Backend.Models;

public partial class BongDa24HContext : DbContext
{
    public BongDa24HContext()
    {
    }

    public BongDa24HContext(DbContextOptions<BongDa24HContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<MatchRequest> MatchRequests { get; set; }

    public virtual DbSet<MatchResponse> MatchResponses { get; set; }

    public virtual DbSet<Message> Messages { get; set; }

    public virtual DbSet<Payment> Payments { get; set; }

    public virtual DbSet<Pitch> Pitches { get; set; }

    public virtual DbSet<PitchCluster> PitchClusters { get; set; }

    public virtual DbSet<PricingRule> PricingRules { get; set; }

    public virtual DbSet<Promotion> Promotions { get; set; }

    public virtual DbSet<Report> Reports { get; set; }

    public virtual DbSet<Review> Reviews { get; set; }

    public virtual DbSet<Team> Teams { get; set; }

    public virtual DbSet<User> Users { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            modelBuilder.Entity<User>().HasQueryFilter(u => !u.IsDeleted);
            entity.HasKey(e => e.Id).HasName("PK__Bookings__3214EC075AA1CD4F");

            entity.HasIndex(e => e.PitchId, "IX_Bookings_PitchId");

            entity.HasIndex(e => e.UserId, "IX_Bookings_UserId");

            entity.Property(e => e.DepositAmount).HasDefaultValue(0);
            entity.Property(e => e.TimeSlot).HasMaxLength(50);

            entity.HasOne(d => d.Pitch).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.PitchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Pitches");

            entity.HasOne(d => d.User).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.UserId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Bookings_Users");
        });

        modelBuilder.Entity<MatchRequest>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MatchReq__3214EC076C61D1BC");

            entity.HasIndex(e => e.CreatorId, "IX_MatchRequests_CreatorId");

            entity.HasIndex(e => e.PitchId, "IX_MatchRequests_PitchId");

            entity.Property(e => e.TimeSlot).HasMaxLength(50);

            entity.HasOne(d => d.Creator).WithMany(p => p.MatchRequests)
                .HasForeignKey(d => d.CreatorId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatchRequests_Users");

            entity.HasOne(d => d.Pitch).WithMany(p => p.MatchRequests)
                .HasForeignKey(d => d.PitchId)
                .HasConstraintName("FK_MatchRequests_Pitches");
        });

        modelBuilder.Entity<MatchResponse>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__MatchRes__3214EC070DACD176");

            entity.HasIndex(e => e.RequestId, "IX_MatchResponses_RequestId");

            entity.HasIndex(e => e.ResponderId, "IX_MatchResponses_ResponderId");

            entity.Property(e => e.Content).HasMaxLength(255);

            entity.HasOne(d => d.Request).WithMany(p => p.MatchResponses)
                .HasForeignKey(d => d.RequestId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatchResponses_MatchRequests");

            entity.HasOne(d => d.Responder).WithMany(p => p.MatchResponses)
                .HasForeignKey(d => d.ResponderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_MatchResponses_Users");
        });

        modelBuilder.Entity<Message>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Messages__3214EC0711BA0588");

            entity.HasIndex(e => e.BookingId, "IX_Messages_BookingId");

            entity.HasIndex(e => e.ReceiverId, "IX_Messages_ReceiverId");

            entity.HasIndex(e => e.SenderId, "IX_Messages_SenderId");

            entity.Property(e => e.Content).HasMaxLength(500);
            entity.Property(e => e.SentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");

            entity.HasOne(d => d.Booking).WithMany(p => p.Messages)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_Bookings");

            entity.HasOne(d => d.Receiver).WithMany(p => p.MessageReceivers)
                .HasForeignKey(d => d.ReceiverId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_ReceiverUser");

            entity.HasOne(d => d.Sender).WithMany(p => p.MessageSenders)
                .HasForeignKey(d => d.SenderId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Messages_SenderUser");
        });

        modelBuilder.Entity<Payment>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Payments__3214EC07BF2F04E3");

            entity.HasIndex(e => e.BookingId, "IX_Payments_BookingId");

            entity.Property(e => e.Method).HasDefaultValue(0);
            entity.Property(e => e.PaymentTime)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Type).HasDefaultValue(0);

            entity.HasOne(d => d.Booking).WithMany(p => p.Payments)
                .HasForeignKey(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payments_Bookings");
        });

        modelBuilder.Entity<Pitch>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Pitches__3214EC070324212E");

            entity.HasIndex(e => e.ClusterId, "IX_Pitches_ClusterId");

            entity.Property(e => e.ImageUrl).HasMaxLength(255);
            entity.Property(e => e.PitchName).HasMaxLength(100);
            entity.Property(e => e.PitchType).HasDefaultValue(0);

            entity.HasOne(d => d.Cluster).WithMany(p => p.Pitches)
                .HasForeignKey(d => d.ClusterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Pitches_PitchClusters");
        });

        modelBuilder.Entity<PitchCluster>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PitchClu__3214EC07413191A0");

            entity.HasIndex(e => e.OwnerId, "IX_PitchClusters_OwnerId");

            entity.Property(e => e.Address).HasMaxLength(200);
            entity.Property(e => e.ClusterName).HasMaxLength(100);

            entity.HasOne(d => d.Owner).WithMany(p => p.PitchClusters)
                .HasForeignKey(d => d.OwnerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PitchClusters_Users");
        });

        modelBuilder.Entity<PricingRule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__PricingR__3214EC07673927EB");

            entity.HasIndex(e => e.PitchId, "IX_PricingRules_PitchId");

            entity.Property(e => e.TimeSlot).HasMaxLength(50);

            entity.HasOne(d => d.Pitch).WithMany(p => p.PricingRules)
                .HasForeignKey(d => d.PitchId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_PricingRules_Pitches");
        });

        modelBuilder.Entity<Promotion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Promotio__3214EC07B17FAE2F");

            entity.HasIndex(e => e.ClusterId, "IX_Promotions_ClusterId");

            entity.HasIndex(e => e.DiscountCode, "UQ__Promotio__A1120AF5A31A65A7").IsUnique();

            entity.Property(e => e.DiscountCode).HasMaxLength(50);

            entity.HasOne(d => d.Cluster).WithMany(p => p.Promotions)
                .HasForeignKey(d => d.ClusterId)
                .HasConstraintName("FK_Promotions_PitchClusters");
        });

        modelBuilder.Entity<Report>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reports__3214EC07E2BB0956");

            entity.HasIndex(e => e.ReporterId, "IX_Reports_ReporterId");

            entity.Property(e => e.Reason).HasMaxLength(255);
            entity.Property(e => e.ReportDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.Status).HasDefaultValue(0);
            entity.Property(e => e.TargetType).HasMaxLength(50);

            entity.HasOne(d => d.Reporter).WithMany(p => p.Reports)
                .HasForeignKey(d => d.ReporterId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reports_ReporterUser");
        });

        modelBuilder.Entity<Review>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Reviews__3214EC0727D03501");

            entity.HasIndex(e => e.BookingId, "UQ__Reviews__73951AECCD141392").IsUnique();

            entity.Property(e => e.Comment).HasMaxLength(255);
            entity.Property(e => e.OpponentAttitude).HasMaxLength(50);
            entity.Property(e => e.OpponentSkill).HasMaxLength(50);

            entity.HasOne(d => d.Booking).WithOne(p => p.Review)
                .HasForeignKey<Review>(d => d.BookingId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Reviews_Bookings");
        });

        modelBuilder.Entity<Team>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Teams__3214EC07FF40421D");

            entity.HasIndex(e => e.ManagerId, "IX_Teams_ManagerId");

            entity.Property(e => e.TeamName).HasMaxLength(100);
            entity.Property(e => e.TotalMatches).HasDefaultValue(0);
            entity.Property(e => e.Wins).HasDefaultValue(0);

            entity.HasOne(d => d.Manager).WithMany(p => p.Teams)
                .HasForeignKey(d => d.ManagerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Teams_Users");
        });

        modelBuilder.Entity<User>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Users__3214EC07D101141F");

            entity.HasIndex(e => e.Email, "UQ__Users__A9D1053416EF43B0").IsUnique();

            entity.Property(e => e.AccumulatedPoints).HasDefaultValue(0);
            entity.Property(e => e.Email).HasMaxLength(100);
            entity.Property(e => e.FullName).HasMaxLength(100);
            entity.Property(e => e.PasswordHash).HasMaxLength(255);
            entity.Property(e => e.PhoneNumber).HasMaxLength(20);
            entity.Property(e => e.Role).HasDefaultValue(0);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
