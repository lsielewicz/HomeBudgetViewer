using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using HomeBudgetViewer.Database.Engine.Engine;

namespace HomeBudgetViewer.Database.Engine.Migrations
{
    [DbContext(typeof(BudgetContext))]
    [Migration("20161013200248_Migration2")]
    partial class Migration2
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.1");

            modelBuilder.Entity("HomeBudgetViewer.Database.Engine.Entities.BudgetItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Category");

                    b.Property<DateTime>("Date");

                    b.Property<string>("Description");

                    b.Property<string>("ItemType");

                    b.Property<double>("MoneyValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("BudgetItem");
                });

            modelBuilder.Entity("HomeBudgetViewer.Database.Engine.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Currency");

                    b.Property<string>("Name")
                        .IsRequired();

                    b.HasKey("Id");

                    b.ToTable("User");
                });

            modelBuilder.Entity("HomeBudgetViewer.Database.Engine.Entities.BudgetItem", b =>
                {
                    b.HasOne("HomeBudgetViewer.Database.Engine.Entities.User", "User")
                        .WithMany("BudgetItems")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
