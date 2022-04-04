using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using _123PayApp.Models.ExistingModels;
using Microsoft.Extensions.Configuration;

#nullable disable

namespace _123PayApp.Models
{
    public partial class db_123payContext : DbContext
    {
        private readonly IConfiguration _configuration;
        public db_123payContext()
        {
        }

        public db_123payContext(DbContextOptions<db_123payContext> options, IConfiguration configuration)
            : base(options)
        {
            this._configuration = configuration;
        }

        public virtual DbSet<TblMerchant> TblMerchants { get; set; }
        public virtual DbSet<TblPaymentTransaction> TblPaymentTransactions { get; set; }
        public virtual DbSet<TblStatus> TblStatuses { get; set; }
        public virtual DbSet<VwTransactionList> VwTransactionLists { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                var connStr = _configuration.GetConnectionString("DefaultConnection");
                optionsBuilder.UseSqlServer(connStr);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TblMerchant>(entity =>
            {
                entity.ToTable("tbl_merchant");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<TblPaymentTransaction>(entity =>
            {
                entity.HasKey(e => e.ReferenceNo);

                entity.ToTable("tbl_payment_transaction");

                entity.Property(e => e.ReferenceNo)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("reference_no")
                    .IsFixedLength(true);

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_name");

                entity.Property(e => e.AccountNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("account_no")
                    .IsFixedLength(true);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.Attachments)
                    .HasColumnType("image")
                    .HasColumnName("attachments");

                entity.Property(e => e.MerchantId).HasColumnName("merchant_id");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.StatusId).HasColumnName("status_id");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.HasOne(d => d.Merchant)
                    .WithMany(p => p.TblPaymentTransactions)
                    .HasForeignKey(d => d.MerchantId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_payment_transaction_tbl_merchant");

                entity.HasOne(d => d.Status)
                    .WithMany(p => p.TblPaymentTransactions)
                    .HasForeignKey(d => d.StatusId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_tbl_payment_transaction_tbl_status");
            });

            modelBuilder.Entity<TblStatus>(entity =>
            {
                entity.ToTable("tbl_status");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("name");
            });

            modelBuilder.Entity<VwTransactionList>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("vw_transaction_list");

                entity.Property(e => e.AccountName)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("account_name");

                entity.Property(e => e.AccountNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("account_no")
                    .IsFixedLength(true);

                entity.Property(e => e.Amount)
                    .HasColumnType("decimal(18, 2)")
                    .HasColumnName("amount");

                entity.Property(e => e.MobileNumber)
                    .IsRequired()
                    .HasMaxLength(13)
                    .IsUnicode(false)
                    .HasColumnName("mobile_number");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50)
                    .IsUnicode(false)
                    .HasColumnName("name");

                entity.Property(e => e.ReferenceNo)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("reference_no")
                    .IsFixedLength(true);

                entity.Property(e => e.Remarks)
                    .HasMaxLength(100)
                    .IsUnicode(false)
                    .HasColumnName("remarks");

                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("status");

                entity.Property(e => e.TransactionDate)
                    .HasColumnType("datetime")
                    .HasColumnName("transaction_date");

                entity.Property(e => e.Attachments)
                    .HasColumnType("image")
                    .HasColumnName("attachments");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
