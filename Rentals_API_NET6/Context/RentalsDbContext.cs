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
        public DbSet<FavouriteItem> FavouriteItems { get; set; }
        public DbSet<InventoryItem> InventoryItems { get; set; }
        public DbSet<Item> Items { get; set; }
        public DbSet<Renting> Rentings { get; set; }
        public DbSet<RentingItem> RentingItems { get; set; }
        public DbSet<AccessoryItem> AccessoryItems { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<RentingHistoryLog> RentingHistoryLogs { get; set; }
        public DbSet<UploadedFile> Files { get; set; }
        public DbSet<ItemHistoryLog> ItemHistoryLogs { get; set; }
        public DbSet<ItemChange> ItemChanges { get; set; }
        public DbSet<ItemPreChangeConnection> ItemPreChangeConnections { get; set; }
        public DbSet<ItemChangeConnection> ItemChangeConnections { get; set; }

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

            modelBuilder.Entity<ItemHistoryLog>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemHistoryLog>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemHistoryLog>()
                .HasOne(x => x.Item)
                .WithMany(x => x.Logs)
                .HasForeignKey(x => x.UserInventoryId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemHistoryLog>()
                .HasMany(x => x.ItemChanges)
                .WithOne(x => x.ItemHistoryLog);

            modelBuilder.Entity<ItemChange>()
                .HasOne(x => x.ItemHistoryLog)
                .WithMany(x => x.ItemChanges)
                .HasForeignKey(x => x.ItemHistoryLogId);

            modelBuilder.Entity<User>()
                .HasIndex(u => u.OauthId)
                .IsUnique();

            modelBuilder.Entity<Item>()
                .HasOne(x => x.Category)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.CategoryId);

            modelBuilder.Entity<Item>()
                .HasOne(x => x.ImgFile)
                .WithMany(x => x.Items)
                .HasForeignKey(x => x.Img);

            modelBuilder.Entity<ItemPreChangeConnection>().HasKey(sc => new { sc.ItemId, sc.ItemChangeId });
            modelBuilder.Entity<ItemPreChangeConnection>()
                .HasOne(x => x.Item)
                .WithMany(x => x.PreChangeAccessory)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemPreChangeConnection>()
                .HasOne(x => x.ItemChange)
                .WithMany(x => x.PreviousAccessories)
                .HasForeignKey(x => x.ItemChangeId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<ItemChangeConnection>().HasKey(sc => new { sc.ItemId, sc.ItemChangeId });
            modelBuilder.Entity<ItemChangeConnection>()
                .HasOne(x => x.Item)
                .WithMany(x => x.ChangeAccessory)
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<ItemChangeConnection>()
                .HasOne(x => x.ItemChange)
                .WithMany(x => x.ChangedAccessories)
                .HasForeignKey(x => x.ItemChangeId)
                .OnDelete(DeleteBehavior.Restrict);


            //Kategorie
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Přístroje" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Objektivy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Stativy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Příslušenství" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Audiotechnika" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 6, Name = "Ostatní" });

            //Předměty
            modelBuilder.Entity<Item>().HasData(new Item { Id = 1, Name = "Kamera", Description = "Kamera", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 2, Name = "Kamera", Description = "Kamera", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 3, Name = "Mikrofon", Description = "Dobrý zvuk", IsDeleted = true, CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 4, Name = "Mikrofon", Description = "Dobrý zvuk", IsDeleted = true, CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 5, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 6, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 7, Name = "Kamera Sony", Description = "24,1 Mpx", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 8, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 9, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 10, Name = "Zrcadlovka SONY Alpha A6300", Description = "Lehké tělo, kompaktní, s možností až 4K videa", IsDeleted = true, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 11, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false, Img = "EOS650D_5a40.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 12, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false, Img = "EOS650D_5a40.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 13, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Prstenec transfokátoru má vůli", IsDeleted = false, Img = "Sigma18-50_f918.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 14, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Určen primárně do ateliéru!", IsDeleted = false, Img = "Sigma18-50_f918.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 15, Name = "Fotoaparát Canon EOS 70D", IsDeleted = false, Img = "EOS70D_b624.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 16, Name = "Fotoaparát Canon EOS 70D", Note = "Určen primárně do ateliéru!", IsDeleted = false, Img = "EOS70D_b624.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 17, Name = "Objektiv SIGMA 24-70 mm 1:2.8", Description = "FullFrame objektiv", IsDeleted = false, Img = "Sigma24-70_7bde.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 18, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 19, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 20, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 21, Name = "Videokamera Sony HDR-CX320", Description = "F1.8/f1.9-57 (černá)", IsDeleted = false, Img = "SonyHDR-CX320_6824.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 22, Name = "Bateriový grip Phottix BG 70D", IsDeleted = false, Img = "gripC70D_2982.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 23, Name = "Knoflíková baterie GP Alkaline A76 LR44 V13GA 1.5 V", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 24, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false, Img = "SDSanDisk64GB_715b.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 25, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false, Img = "SDSanDisk64GB_715b.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 26, Name = "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 27, Name = "Alkalická baterie Alkalisk 1.5 V LR03 AAA Ikea", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 28, Name = "Alkalická baterie Eneloop 1.2 V HR 3UTG AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 29, Name = "Alkalická baterie Eneloop 1.2 V HR 4UTG AAA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 30, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 31, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 32, Name = "Alkalická baterie LSD 1.2 V AAA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 33, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 34, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 35, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 36, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 37, Name = "Alkalická baterie LSD 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 38, Name = "MicroSD karta adapter SAMSUNG", Description = "Černý", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 39, Name = "SD karta Kingston SD10G3 32 GB", Description = "Černý", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 40, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 41, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 42, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 43, Name = "Pouzdro na SD kartu", Description = "Průhledné", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 44, Name = "Objektiv Canon ULTRASONIC 70-200 mm F4", Description = "FullFrame", IsDeleted = false, Img = "CanonEF70-200_b2f2.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 45, Name = "Fotoaparát Canon EOS 350D", Description = "Černý", IsDeleted = false, CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 46, Name = "Objektiv Canon 50 mm F1.8", Description = "FullFrame", IsDeleted = false, Img = "CanonEF50_0055.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 47, Name = "Baterie SONY NP FV70A 1900mAh", Description = "Černá", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 48, Name = "Baterie Canon LP E6N 1865mAh", Description = "Černá", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 49, Name = "Baterie Canon LP E6 1800mAh", Description = "Černá", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 50, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 51, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 52, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 53, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 54, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 55, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 56, Name = "Baterie Canon LP E6 1865mAh", Description = "Černá", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 57, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 58, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 59, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 60, Name = "Baterie Panasonic VW VBN130 1250mAh", Description = "náhradní akumulátor", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 61, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false, Img = "BattChgLC-E6E_27dd.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 62, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false, Img = "BattChgLC-E6E_27dd.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 63, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false, Img = "BattChgLC-E8E_02bb.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 64, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false, Img = "BattChgLC-E8E_02bb.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 65, Name = "Nabíječka baterií FK technics BC 450", Description = "Černá", IsDeleted = false, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 66, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 67, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 68, Name = "Testujeme s.r.o.", IsDeleted = true, CategoryId = 6 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 69, Name = "Objektiv SIGMA 18-200 mm 1:3.5-6.3", Description = "(Kz)", IsDeleted = false, Img = "Sigma18-200_cd74.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 70, Name = "Stativ Velbon C-600", Note = "xxx 2308", IsDeleted = false, Img = "StativVelbonC-600_b14c.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 71, Name = "Stativ Velbon C-600", IsDeleted = false, Img = "StativVelbonC-600_b14c.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 72, Name = "SD karta Lexar 64GB", Description = "95 MB/s", IsDeleted = false, Img = "SDLexar64GB_e4c2.jpg", CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 73, Name = "Ateliér B210", Description = "Rezervace místnosti se studiovými světly a odpalovačem.", IsDeleted = false, Img = "atelier_a27b.jpg", CategoryId = 6 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 74, Name = "Stativ Hama Star 62", Description = "Hlava má vůli; nevhodný na video", IsDeleted = false, Img = "hamaStar62_914d.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 75, Name = "Flycam HD-3000", Description = "Steadicam Camtree Wonder-3", IsDeleted = false, Img = "flycam_fd0f.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 76, Name = "Rekordér Zoom H1", IsDeleted = false, Img = "ZoomH1_2ffd.jpg", CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 77, Name = "Rekordér Zoom H1", IsDeleted = false, Img = "ZoomH1_2ffd.jpg", CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 78, Name = "stativ MiniTripod plochý", IsDeleted = false, Img = "MiniTripod_92cc.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 79, Name = "Objektiv Sigma 30mm/F1.4", IsDeleted = false, Img = "Sigma30_0caf.jpg", CategoryId = 2 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 80, Name = "Videokamera Panasonic HC-X920", Description = "F1,5 f2,84-34,1mm (včetně akumulátoru)", IsDeleted = false, Img = "Panasonic_HC-X920_7ce7.jpg", CategoryId = 1 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 81, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 82, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 83, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 84, Name = "Stativ video MS-007H", Description = "(kulová hlava)", Note = "poškozen zip", IsDeleted = false, Img = "StativMS-007H_3d14.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 85, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false, Img = "MikrofonKlopový_97b3.jpg", CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 86, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false, Img = "MikrofonKlopový_97b3.jpg", CategoryId = 5 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 87, Name = "Stativ Sony VCT-D680RM", IsDeleted = false, Img = "SonyVct-d680rm_9d51.jpg", CategoryId = 3 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 88, Name = "zkušební předmět", CategoryId = 6, Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "první předmět", IsDeleted = true, Img = "tn-techtips_f9dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 89, Name = "zkušební předmět", CategoryId = 6, Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "druhý předmět", IsDeleted = true, Img = "kitt-design-illustration_73ad.png" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 90, Name = "zkušební předmět", CategoryId = 6, Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true, Img = "tn-techtips_f9dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 91, Name = "Fiktivní předmět", CategoryId = 6, Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 92, Name = "Fiktivní předmět", CategoryId = 6, Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 93, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 94, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true, CategoryId = 4 });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 95, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true, CategoryId = 4 });

            //Obrázky
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "AsteroidSprite_ddff.png", OriginalName = "AsteroidSprite_ddff.png", ContentType = "image/png" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "atelier_a27b.jpg", OriginalName = "atelier_a27b.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "BattChgLC-E6E_27dd.jpg", OriginalName = "BattChgLC-E6E_27dd.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "BattChgLC-E8E_02bb.jpg", OriginalName = "BattChgLC-E8E_02bb.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "BattLPE6_03b5.jpg", OriginalName = "BattLPE6_03b5.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "BattLPE8_fd96.jpg", OriginalName = "BattLPE8_fd96.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "CanonEF50_0055.jpg", OriginalName = "CanonEF50_0055.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "CanonEF70-200_b2f2.jpg", OriginalName = "CanonEF70-200_b2f2.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "comica1_5b38.jpg", OriginalName = "comica1_5b38.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "comica2_ed64.jpg", OriginalName = "comica2_ed64.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "crane3Lab_1685.jpg", OriginalName = "crane3Lab_1685.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Cullmann3400_cd56.jpg", OriginalName = "Cullmann3400_cd56.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "cvm-d02_8d19.jpg", OriginalName = "cvm-d02_8d19.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "deity_v-mic-d3_e4bb.jpg", OriginalName = "deity_v-mic-d3_e4bb.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg", OriginalName = "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_940c.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg", OriginalName = "e890d2d1-ce7d-4b33-98ff-851e85ac4fd2_b249.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "EOS_RP_e3fe.jpg", OriginalName = "EOS_RP_e3fe.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "EOS650D_5a40.jpg", OriginalName = "EOS650D_5a40.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "EOS70D_b624.jpg", OriginalName = "EOS70D_b624.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "EOS90D_7dd9.jpg", OriginalName = "EOS90D_7dd9.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "flycam_fd0f.jpg", OriginalName = "flycam_fd0f.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "gripC70D_2982.jpg", OriginalName = "gripC70D_2982.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Group 1_0aad.jpg", OriginalName = "Group 1_0aad.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Group 612_3ac5.png", OriginalName = "Group 612_3ac5.png", ContentType = "image/png" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "hamaStar62_914d.jpg", OriginalName = "hamaStar62_914d.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "img_2_46b9.jpg", OriginalName = "img_2_46b9.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "img_2_de6d.jpg", OriginalName = "img_2_de6d.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "kitt-design-illustration_73ad.png", OriginalName = "kitt-design-illustration_73ad.png", ContentType = "image/png" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "MikrofonKlopový_97b3.jpg", OriginalName = "MikrofonKlopový_97b3.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "MiniTripod_92cc.jpg", OriginalName = "MiniTripod_92cc.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "moza_912f.jpg", OriginalName = "moza_912f.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Panasonic_HC-X920_7ce7.jpg", OriginalName = "Panasonic_HC-X920_7ce7.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "PanasonicVW-CT45E_cd22.jpg", OriginalName = "PanasonicVW-CT45E_cd22.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Placeholder.jpg", OriginalName = "Placeholder.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "rigHawk_8fd1.jpg", OriginalName = "rigHawk_8fd1.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "RigSpider_ea93.jpg", OriginalName = "RigSpider_ea93.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "SDLexar64GB_e4c2.jpg", OriginalName = "SDLexar64GB_e4c2.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "SDSanDisk64GB_2b06.jpg", OriginalName = "SDSanDisk64GB_2b06.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "SDSanDisk64GB_715b.jpg", OriginalName = "SDSanDisk64GB_715b.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Sigma18-200_cd74.jpg", OriginalName = "Sigma18-200_cd74.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Sigma18-50_f918.jpg", OriginalName = "Sigma18-50_f918.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Sigma24-70_7bde.jpg", OriginalName = "Sigma24-70_7bde.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Sigma30_0caf.jpg", OriginalName = "Sigma30_0caf.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "snoppa_4626.jpg", OriginalName = "snoppa_4626.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "SonyHDR-CX320_6824.jpg", OriginalName = "SonyHDR-CX320_6824.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "SonyVct-d680rm_9d51.jpg", OriginalName = "SonyVct-d680rm_9d51.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "StativMS-007H_3d14.jpg", OriginalName = "StativMS-007H_3d14.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "StativMS-007H_3d14_d8f7.png", OriginalName = "StativMS-007H_3d14_d8f7.png", ContentType = "image/png" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "stativ-svetlo_98ea.jpg", OriginalName = "stativ-svetlo_98ea.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "StativVelbonC-600_b14c.jpg", OriginalName = "StativVelbonC-600_b14c.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "tn-techtips_f9dd.jpg", OriginalName = "tn-techtips_f9dd.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "Viltrox_f0a5.jpg", OriginalName = "Viltrox_f0a5.jpg", ContentType = "image/jpeg" });
            modelBuilder.Entity<UploadedFile>().HasData(new UploadedFile { Id = "ZoomH1_2ffd.jpg", OriginalName = "ZoomH1_2ffd.jpg", ContentType = "image/jpeg" });

            //Přeslušenství předmětů
            //1002
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 13 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 13 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 14 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 14 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 17 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 17 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 24 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 24 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 25 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 25 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 46 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 46 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 48 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 48 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 49 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 49 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 50 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 50 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 51 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 51 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 52 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 52 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 63 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 63 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 64 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 64 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 11, AccessoryId = 69 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 12, AccessoryId = 69 });

            //1004
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 69 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 69 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 72 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 72 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 44 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 44 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 61 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 61 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 62 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 62 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 49 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 49 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 53 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 53 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 54 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 54 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 55 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 55 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 48 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 48 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 46 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 46 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 17 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 17 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 22 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 22 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 13 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 13 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 15, AccessoryId = 14 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 16, AccessoryId = 14 });

            //1006
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 18, AccessoryId = 47 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 19, AccessoryId = 47 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 20, AccessoryId = 47 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 18, AccessoryId = 57 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 19, AccessoryId = 57 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 20, AccessoryId = 57 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 18, AccessoryId = 58 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 19, AccessoryId = 58 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 20, AccessoryId = 58 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 18, AccessoryId = 59 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 19, AccessoryId = 59 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 20, AccessoryId = 59 });

            //1007
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 21, AccessoryId = 57 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 21, AccessoryId = 58 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 21, AccessoryId = 59 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 21, AccessoryId = 47 });

            //1010
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 24, AccessoryId = 40 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 25, AccessoryId = 40 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 24, AccessoryId = 41 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 25, AccessoryId = 41 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 24, AccessoryId = 42 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 25, AccessoryId = 42 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 24, AccessoryId = 43 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 25, AccessoryId = 43 });

            //1017
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 38, AccessoryId = 40 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 38, AccessoryId = 41 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 38, AccessoryId = 42 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 38, AccessoryId = 43 });

            //1018
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 39, AccessoryId = 40 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 39, AccessoryId = 41 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 39, AccessoryId = 42 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 39, AccessoryId = 43 });

            //1021
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 45, AccessoryId = 46 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 45, AccessoryId = 48 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 45, AccessoryId = 49 });

            //1035
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 61, AccessoryId = 53 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 62, AccessoryId = 53 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 61, AccessoryId = 54 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 62, AccessoryId = 54 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 61, AccessoryId = 55 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 62, AccessoryId = 55 });

            //1036
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 63, AccessoryId = 50 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 64, AccessoryId = 50 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 63, AccessoryId = 51 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 64, AccessoryId = 51 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 63, AccessoryId = 52 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 64, AccessoryId = 52 });

            //1037
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 26 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 27 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 28 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 29 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 93 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 94 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 95 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 30 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 31 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 32 });

            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 33 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 34 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 35 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 36 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 65, AccessoryId = 37 });

            //1047
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 76, AccessoryId = 78 });
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 77, AccessoryId = 78 });

            //1050
            modelBuilder.Entity<AccessoryItem>().HasData(new AccessoryItem { ItemId = 80, AccessoryId = 60 });
        }
    }
}