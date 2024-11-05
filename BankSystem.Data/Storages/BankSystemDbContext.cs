using BankSystem.Data.EntityConfigurations;
using BankSystem.Domain.Models;
using BankSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankSystem.Data.Storages
{
    public class BankSystemDbContext : DbContext
    {
        public DbSet<Client> Clients => Set<Client>();
        public DbSet<Employee> Employees => Set<Employee>();
        public DbSet<Account> Accounts => Set<Account>();

        static BankSystemDbContext()
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql($"Host={BankSystem.Data.Properties.Resources.ConnectionStringHost};" +
                                     $"Port = {BankSystem.Data.Properties.Resources.ConnectionStringPort};" +
                                     $"Database = {BankSystem.Data.Properties.Resources.ConnectionStringDatabase};" +
                                     $"Username = {BankSystem.Data.Properties.Resources.ConnectionStringUsername};" +
                                     $"Password = {BankSystem.Data.Properties.Resources.ConnectionStringPassword}");
        }

        public void OnModelCreating(ModelBuilder modelBuilder)
        {
            if (modelBuilder == null) throw new
                ArgumentNullException(nameof(modelBuilder));

            modelBuilder.ApplyConfiguration(new AccountConfiguration());
            modelBuilder.ApplyConfiguration(new ClientConfiguration());
            modelBuilder.ApplyConfiguration(new EmployeeConfiguration());

            base.OnModelCreating(modelBuilder);
        }
    }

}
