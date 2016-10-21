using System;
using System.IO;
using Windows.Storage;
using HomeBudgetViewer.Database.Engine.Entities;
using Microsoft.EntityFrameworkCore;

namespace HomeBudgetViewer.Database.Engine.Engine
{
    public class BudgetContext : DbContext
    {
        public DbSet<User> User { get; set; }
        public DbSet<BudgetItem> BudgetItem { get; set; }

/*        static BudgetContext()
        {
            using (var database = new BudgetContext())
            {
                database.Database.Migrate();
                database.Database.EnsureCreated();
            }
        }*/

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string databaseFilePath = "Budget.db";
            try
            {
                databaseFilePath = Path.Combine(ApplicationData.Current.LocalFolder.Path, databaseFilePath);
            }
            catch (InvalidOperationException) { }

            optionsBuilder.UseSqlite($"Data source={databaseFilePath}");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<BudgetItem>()
                .HasOne(bi => bi.User)
                .WithMany(u => u.BudgetItems)
                .HasForeignKey(bi => bi.UserId);
            

        }

    }
}
