using Microsoft.EntityFrameworkCore;
using Rentals_API_NET6.Models.DatabaseModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Rentals_API_NET6.Context
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
        public DbSet<AccessoryItem> AccessoryItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentingHistoryLog> RentingHistoryLogs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("Rentals_Database");
            optionsBuilder.UseSqlServer(connectionString);
        }

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

            modelBuilder.Entity<AccessoryItem>().HasKey(sc => new { sc.ItemId, sc.AccessoryId });
            modelBuilder.Entity<AccessoryItem>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Accessories)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);

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

            modelBuilder.Entity<RentingHistoryLog>()
                .HasOne(x => x.Renting)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.RentingId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<RentingHistoryLog>()
                .HasOne(x => x.User)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.OauthId)
                .IsUnique();

            //Kategorie
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Těla fotoaparátů" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Objektivy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Fotoaparáty" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Stativy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Příslušenství" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 6, Name = "Ostatní" });

            //Předměty
            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, Name = "Kamera", Description = "Kamera", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 2, Name = "Kamera", Description = "Kamera", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 3, Name = "Mikrofon", Description = "Dobrý zvuk", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 4, Name = "Mikrofon", Description = "Dobrý zvuk", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 5, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 6, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 7, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 8, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 9, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 10, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 11, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 12, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 13, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Prstenec transfokátoru má vůli", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 14, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Určen primárně do ateliéru!", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 15, Name = "Fotoaparát Canon EOS 70D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 16, Name = "Fotoaparát Canon EOS 70D", Note = "Určen primárně do ateliéru!", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 17, Name = "Objektiv SIGMA 24-70 mm 1:2.8", Description = "FullFrame objektiv", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 18, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 19, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 20, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 21, Name = "Videokamera Sony HDR-CX320", Description = "F1.8/f1.9-57 (černá)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 22, Name = "Bateriový grip Phottix BG 70D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 23, Name = "Knoflíková baterie GP Alkaline A76 LR44 V13GA 1.5 V", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 24, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 25, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 26, Name = "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 27, Name = "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 28, Name = "Alkalická baterie Eneloop 1.2 V HR 3UTG AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 29, Name = "Alkalická baterie Eneloop 1.2 V HR 4UTG AAA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 30, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 31, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 32, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 33, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 34, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 35, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 36, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 37, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 38, Name = "MicroSD karta adapter SAMSUNG", Description = "Černý", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 39, Name = "SD karta Kingston SD10G3 32 GB", Description = "Černý", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 40, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 41, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 42, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 43, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 44, Name = "Objektiv Canon ULTRASONIC 70-200 mm F4", Description = "FullFrame", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 45, Name = "Fotoaparát Canon EOS 350D", Description = "Černý", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 46, Name = "Objektiv Canon 50 mm F1.8", Description = "FullFrame", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 47, Name = "Baterie SONY NP FV70A 1900mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 48, Name = "Baterie Canon LP E6N 1865mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 49, Name = "Baterie Canon LP E6 1800mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 50, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 51, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 52, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 53, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 54, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 55, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 56, Name = "Baterie Canon LP E6 1865mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 57, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 58, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 59, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 60, Name = "Baterie Panasonic VW VBN130 1250mAh", Description = "náhradní akumulátor", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 61, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 62, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 63, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 64, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 65, Name = "Nabíječka baterií FK technics BC 450", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 66, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 67, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 68, Name = "Testujeme s.r.o.", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 69, Name = "Objektiv SIGMA 18-200 mm 1:3.5-6.3", Description = "(Kz)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 70, Name = "Stativ Velbon C-600", Note = "xxx 2308", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 71, Name = "Stativ Velbon C-600", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 72, Name = "SD karta Lexar 64GB", Description = "95 MB/s", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 73, Name = "Ateliér B210", Description = "Rezervace místnosti se studiovými světly a odpalovačem.", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 74, Name = "Stativ Hama Star 62", Description = "Hlava má vůli; nevhodný na video", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 75, Name = "Flycam HD-3000", Description = "Steadicam Camtree Wonder-3", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 76, Name = "Rekordér Zoom H1", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 77, Name = "Rekordér Zoom H1", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 78, Name = "stativ MiniTripod plochý", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 79, Name = "Objektiv Sigma 30mm/F1.4", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 80, Name = "Videokamera Panasonic HC-X920", Description = "F1,5 f2,84-34,1mm (včetně akumulátoru)", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 81, Name = "Stativ Rig Spider FT-10", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 82, Name = "Stativ Rig Spider FT-10", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 83, Name = "Stativ Rig Spider FT-10", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 84, Name = "Stativ video MS-007H", Description = "(kulová hlava)", Note = "poškozen zip", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 85, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 86, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 87, Name = "Stativ Sony VCT-D680RM", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 88, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "první předmět", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 89, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "druhý předmět", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 90, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 91, Name = "Fiktivní předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 92, Name = "Fiktivní předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });

            //Kategorie itemů
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 1, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 2, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 3, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 4, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 5, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 6, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 7, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 8, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 9, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 10, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 11, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 12, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 13, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 14, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 15, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 16, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 17, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 18, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 19, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 20, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 21, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 22, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 23, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 24, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 25, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 26, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 27, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 28, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 29, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 30, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 31, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 32, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 33, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 34, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 35, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 36, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 37, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 38, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 39, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 40, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 41, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 42, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 43, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 44, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 45, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 46, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 47, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 48, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 49, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 50, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 51, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 52, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 53, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 54, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 55, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 56, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 57, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 58, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 59, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 60, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 61, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 62, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 63, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 64, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 65, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 66, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 67, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 68, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 69, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 70, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 71, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 72, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 73, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 74, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 75, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 76, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 77, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 78, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 79, CategoryId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 80, CategoryId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 81, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 82, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 83, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 84, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 85, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 86, CategoryId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 87, CategoryId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 88, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 89, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 90, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 91, CategoryId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { ItemId = 92, CategoryId = 6 });
        }
    }
}
