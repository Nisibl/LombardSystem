using LombardSystem.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LombardSystem.Database
{
    public class DatabaseContext: DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Contract> Contracts { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Server=localhost;Port=5432;Database=lombard_db;Username=postgres;Password=2121");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка отношений
            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Client)
                .WithMany(c => c.Contracts)
                .HasForeignKey(c => c.ClientId);

            modelBuilder.Entity<Contract>()
                .HasOne(c => c.Item)
                .WithOne(i => i.Contract)
                .HasForeignKey<Contract>(c => c.ItemId);

            modelBuilder.Entity<Product>()
                .HasOne(p => p.Item)
                .WithOne(i => i.Product)
                .HasForeignKey<Product>(p => p.ItemId);
        }
    }
}
