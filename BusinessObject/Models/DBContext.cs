using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace BusinessObject.Models
{
    public partial class DBContext : DbContext
    {
        public DBContext()
        {
        }

        public DBContext(DbContextOptions<DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Application> Applications { get; set; } = null!;
        public virtual DbSet<InterviewQuestion> InterviewQuestions { get; set; } = null!;
        public virtual DbSet<Job> Jobs { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Profile> Profiles { get; set; } = null!;
        public virtual DbSet<SubscriptionPlan> SubscriptionPlans { get; set; } = null!;
        public virtual DbSet<User> Users { get; set; } = null!;
        public virtual DbSet<UserSubscription> UserSubscriptions { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer("Server=THIEN-NGUYEN;Database= JoobeDB2;Uid=sa;Pwd=12345;TrustServerCertificate=True;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Application>(entity =>
            {
                entity.Property(e => e.ApplicationId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.AppliedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.JobId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobSeekerId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Resume).HasColumnType("text");

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.HasOne(d => d.JobSeeker)
                    .WithMany(p => p.Applications)
                    .HasForeignKey(d => d.JobSeekerId)
                    .HasConstraintName("FK_Applications.JobSeekerId");
            });

            modelBuilder.Entity<InterviewQuestion>(entity =>
            {
                entity.HasKey(e => e.QuestionId)
                    .HasName("PK__Intervie__0DC06FACBA3ACAFA");

                entity.ToTable("InterviewQuestion");

                entity.Property(e => e.QuestionId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Answer).HasColumnType("text");

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Position)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<Job>(entity =>
            {
                entity.Property(e => e.JobId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.CreateAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.EmployerId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobType)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Location)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.SalaryRange)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Title)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Employer)
                    .WithMany(p => p.Jobs)
                    .HasForeignKey(d => d.EmployerId)
                    .HasConstraintName("FK_Jobs_EmployerId");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.PaymentId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");

                entity.Property(e => e.PaymentDate).HasColumnType("datetime");

                entity.Property(e => e.PaymentMethod)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Status)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Payments_UserId");
            });

            modelBuilder.Entity<Profile>(entity =>
            {
                entity.Property(e => e.ProfileId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Address)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Description).HasColumnType("text");

                entity.Property(e => e.FullName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.JobTitle)
                    .HasMaxLength(100)
                    .IsUnicode(false);

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(20)
                    .IsUnicode(false);

                entity.Property(e => e.ProfilePicture)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Profiles)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_Profiles_UserId");
            });

            modelBuilder.Entity<SubscriptionPlan>(entity =>
            {
                entity.HasKey(e => e.PlanId)
                    .HasName("PK__Subscrip__755C22B73C7E3AC2");

                entity.Property(e => e.PlanId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PlanName)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Price).HasColumnType("decimal(10, 2)");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email, "UQ__Users__A9D10534D02B0127")
                    .IsUnique();

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(true).
                    ValueGeneratedOnAdd();   

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("datetime")
                    .HasDefaultValueSql("(getdate())");

                entity.Property(e => e.Email)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.PasswordHash)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.Role)
                    .HasMaxLength(50)
                    .IsUnicode(false);

                entity.Property(e => e.Username)
                    .HasMaxLength(100)
                    .IsUnicode(false);
            });

            modelBuilder.Entity<UserSubscription>(entity =>
            {
                entity.Property(e => e.UserSubscriptionId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.EndDate).HasColumnType("datetime");

                entity.Property(e => e.PlanId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.Property(e => e.StartDate).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasMaxLength(255)
                    .IsUnicode(false);

                entity.HasOne(d => d.Plan)
                    .WithMany(p => p.UserSubscriptions)
                    .HasForeignKey(d => d.PlanId)
                    .HasConstraintName("FK_UserSubscriptions_PlanId");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.UserSubscriptions)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK_UserSubscriptions_UserId");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
