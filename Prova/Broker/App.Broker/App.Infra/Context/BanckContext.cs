using App.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
//using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Serilog;
using System;

namespace App.Infra.Data.Context
{
    public partial class BanckContext : DbContext
    {

        public BanckContext(DbContextOptions<BanckContext> options) : base(options)
        {
        }

        #region BanckAttendance
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountHistory> BankAccountHistories { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Attendance

            builder.Entity<BankAccountHistory>()
                .HasOne<BankAccount>(u => u.BankAccount)
                .WithMany(c => c.ListBankAccountHistories)
                .HasForeignKey(f => f.BankAccountCode);

            #endregion
        }
    }
}
