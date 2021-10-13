using Microsoft.EntityFrameworkCore;
using Rentals.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals.Context
{
    public class RentalsDbContext : DbContext
    {
        public RentalsDbContext(DbContextOptions<RentalsDbContext> options) : base(options) { }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<CategoryItem> CategoryItems { get; set; }
        public DbSet<FavouriteItem> FavouriteItems { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Renting> Rentings { get; set; }
        public DbSet<RentingItem> RentingItems { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<RentingItem>().HasKey(sc => new { sc.ItemId, sc.RentingId });
            modelBuilder.Entity<RentingItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Rentings)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<RentingItem>()
                .HasOne(x => x.Renting)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.RentingId);

            modelBuilder.Entity<InventoryItem>().HasKey(sc => new { sc.ItemId, sc.UserId });
            modelBuilder.Entity<InventoryItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Inventories)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<InventoryItem>()
                .HasOne(x => x.User)
                .WithMany(x => x.Inventory)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<FavouriteItem>().HasKey(sc => new { sc.ItemId, sc.UserId });
            modelBuilder.Entity<FavouriteItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Favourites)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<FavouriteItem>()
                .HasOne(x => x.User)
                .WithMany(x => x.Favourite)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CartItem>().HasKey(sc => new { sc.ItemId, sc.UserId });
            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Carts)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<CartItem>()
                .HasOne(x => x.User)
                .WithMany(x => x.Cart)
                .HasForeignKey(x => x.UserId);

            modelBuilder.Entity<CategoryItem>().HasKey(sc => new { sc.ItemId, sc.CategoryId });
            modelBuilder.Entity<CategoryItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Categories)
                .HasForeignKey(x => x.ItemId);
            modelBuilder.Entity<CategoryItem>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Renting>()
                .HasOne(x => x.Owner)
                .WithMany(x => x.Rentings)
                .HasForeignKey(x => x.OwnerId)
                .OnDelete(DeleteBehavior.Restrict);


            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Fotoaparáty" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Kamery" });

            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, Name = "Jméno", Description = "Popis", Note = "Poznámka", IsDeleted = true, State = ItemState.Unavailable });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 2, Name = "Jméno2", Description = "Popis", Note = "Poznámka", IsDeleted = false, State = ItemState.Available });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 3, Name = "Jméno3", Description = "Popis", Note = "Poznámka", State = ItemState.Rented });

            modelBuilder.Entity<Renting>().HasData(new Renting { Id = 1, ApproverId = 1, OwnerId = 2, End = DateTime.Now.AddDays(5), Start = DateTime.Now, State = RentingState.InProgress});

            modelBuilder.Entity<RentingItem>().HasData(new RentingItem { ItemId = 2, RentingId = 1});

            modelBuilder.Entity<User>().HasData(new User { Id = 1, FirstName = "Admin" });
            modelBuilder.Entity<User>().HasData(new User { Id = 2, FirstName = "Jan", LastName = "Novák" });
        }
    }
}
