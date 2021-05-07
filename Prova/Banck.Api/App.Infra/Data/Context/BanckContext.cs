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
        private readonly ILoggerFactory _loggerFactory;

        public BanckContext(DbContextOptions<BanckContext> options, ILoggerFactory loggerFactory) : base(options)
        {
            _loggerFactory = loggerFactory;
        }

        #region BanckAttendance
        public DbSet<User> Users { get; set; }
        public DbSet<UserCredentials> UsersCredentialss { get; set; }
        public DbSet<Assets> Assetss { get; set; }
        public DbSet<BankAccount> BankAccounts { get; set; }
        public DbSet<BankAccountHistory> BankAccountHistories { get; set; }
        public DbSet<TransactionHistory> TransactionHistories { get; set; }
        public DbSet<Traded> Tradeds { get; set; }
        #endregion

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (optionsBuilder.IsConfigured) return;

            optionsBuilder.UseLoggerFactory(_loggerFactory);
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region Attendance

            builder.Entity<UserCredentials>()
                .HasOne<User>(u => u.User)
                .WithMany(c => c.UserCredentialsItens)
                .HasForeignKey(f => f.UserCode);

            builder.Entity<Assets>()
                .HasOne<User>(u => u.User)
                .WithMany(c => c.ListAssets)
                .HasForeignKey(f => f.UserCode);

            builder.Entity<User>()
                .HasOne(u => u.BankAccount)
                .WithOne(c => c.User)
                .HasForeignKey<BankAccount>(u => u.UserCode);

            builder.Entity<BankAccountHistory>()
                .HasOne<BankAccount>(u => u.BankAccount)
                .WithMany(c => c.ListBankAccountHistories)
                .HasForeignKey(f => f.BankAccountCode);

            #endregion
        }
    }
}
