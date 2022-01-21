﻿using Microsoft.EntityFrameworkCore;
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
        public DbSet<UploadedFile> Files { get; set; }

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
            modelBuilder.Entity<Category>().HasData(new Category { Id = 1, Name = "Přístroje" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 2, Name = "Objektivy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 3, Name = "Stativy" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 4, Name = "Příslušenství" });
            modelBuilder.Entity<Category>().HasData(new Category { Id = 5, Name = "Audiotechnika" });
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
            modelBuilder.Entity<Item>().HasData(new Item { Id = 11, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false, Img = "EOS650D_5a40.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 12, Name = "Fotoaparát Canon EOS 650D", Note = "Bez očnice", IsDeleted = false, Img = "EOS650D_5a40.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 13, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Prstenec transfokátoru má vůli", IsDeleted = false, Img = "Sigma18-50_f918.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 14, Name = "Objektiv SIGMA 17-50 mm 1:2.8", Note = "Určen primárně do ateliéru!", IsDeleted = false, Img = "Sigma18-50_f918.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 15, Name = "Fotoaparát Canon EOS 70D", IsDeleted = false, Img = "EOS70D_b624.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 16, Name = "Fotoaparát Canon EOS 70D", Note = "Určen primárně do ateliéru!", IsDeleted = false, Img = "EOS70D_b624.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 17, Name = "Objektiv SIGMA 24-70 mm 1:2.8", Description = "FullFrame objektiv", IsDeleted = false, Img = "Sigma24-70_7bde.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 18, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 19, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 20, Name = "Videokamera Sony 1.9/2.1-57", Description = "Stříbrná", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 21, Name = "Videokamera Sony HDR-CX320", Description = "F1.8/f1.9-57 (černá)", IsDeleted = false, Img = "SonyHDR-CX320_6824.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 22, Name = "Bateriový grip Phottix BG 70D", IsDeleted = false, Img = "gripC70D_2982.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 23, Name = "Knoflíková baterie GP Alkaline A76 LR44 V13GA 1.5 V", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 24, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false, Img = "SDSanDisk64GB_715b.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 25, Name = "SD karta SanDisk 64 GB", Description = "90 MB/s", IsDeleted = false, Img = "SDSanDisk64GB_715b.jpg" });
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
            modelBuilder.Entity<Item>().HasData(new Item { Id = 44, Name = "Objektiv Canon ULTRASONIC 70-200 mm F4", Description = "FullFrame", IsDeleted = false, Img = "CanonEF70-200_b2f2.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 45, Name = "Fotoaparát Canon EOS 350D", Description = "Černý", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 46, Name = "Objektiv Canon 50 mm F1.8", Description = "FullFrame", IsDeleted = false, Img = "CanonEF50_0055.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 47, Name = "Baterie SONY NP FV70A 1900mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 48, Name = "Baterie Canon LP E6N 1865mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 49, Name = "Baterie Canon LP E6 1800mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 50, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 51, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 52, Name = "Baterie Canon LP E8 1120mAh", Description = "k 650D (šedá)", IsDeleted = false, Img = "BattLPE8_fd96.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 53, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 54, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 55, Name = "Baterie Canon LP E6 1800mAh", Description = "k C70D, C5D", IsDeleted = false, Img = "BattLPE6_03b5.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 56, Name = "Baterie Canon LP E6 1865mAh", Description = "Černá", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 57, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 58, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 59, Name = "Baterie SONY NP FV30 500mAh", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 60, Name = "Baterie Panasonic VW VBN130 1250mAh", Description = "náhradní akumulátor", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 61, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false, Img = "BattChgLC-E6E_27dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 62, Name = "Nabíječka baterií Canon LC E6E", Description = "k C70D, C5D", IsDeleted = false, Img = "BattChgLC-E6E_27dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 63, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false, Img = "BattChgLC-E8E_02bb.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 64, Name = "Nabíječka baterií Canon LC E8E", Description = "k C650D", IsDeleted = false, Img = "BattChgLC-E8E_02bb.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 65, Name = "Nabíječka baterií FK technics BC 450", Description = "Černá", IsDeleted = false });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 66, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 67, Name = "Napájecí kabel I SHENG 033", Description = "Černý", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 68, Name = "Testujeme s.r.o.", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 69, Name = "Objektiv SIGMA 18-200 mm 1:3.5-6.3", Description = "(Kz)", IsDeleted = false, Img = "Sigma18-200_cd74.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 70, Name = "Stativ Velbon C-600", Note = "xxx 2308", IsDeleted = false, Img = "StativVelbonC-600_b14c.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 71, Name = "Stativ Velbon C-600", IsDeleted = false, Img = "StativVelbonC-600_b14c.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 72, Name = "SD karta Lexar 64GB", Description = "95 MB/s", IsDeleted = false, Img = "SDLexar64GB_e4c2.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 73, Name = "Ateliér B210", Description = "Rezervace místnosti se studiovými světly a odpalovačem.", IsDeleted = false, Img = "atelier_a27b.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 74, Name = "Stativ Hama Star 62", Description = "Hlava má vůli; nevhodný na video", IsDeleted = false, Img = "hamaStar62_914d.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 75, Name = "Flycam HD-3000", Description = "Steadicam Camtree Wonder-3", IsDeleted = false, Img = "flycam_fd0f.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 76, Name = "Rekordér Zoom H1", IsDeleted = false, Img = "ZoomH1_2ffd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 77, Name = "Rekordér Zoom H1", IsDeleted = false, Img = "ZoomH1_2ffd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 78, Name = "stativ MiniTripod plochý", IsDeleted = false, Img = "MiniTripod_92cc.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 79, Name = "Objektiv Sigma 30mm/F1.4", IsDeleted = false, Img = "Sigma30_0caf.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 80, Name = "Videokamera Panasonic HC-X920", Description = "F1,5 f2,84-34,1mm (včetně akumulátoru)", IsDeleted = false, Img = "Panasonic_HC-X920_7ce7.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 81, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 82, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 83, Name = "Stativ Rig Spider FT-10", IsDeleted = false, Img = "RigSpider_ea93.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 84, Name = "Stativ video MS-007H", Description = "(kulová hlava)", Note = "poškozen zip", IsDeleted = false, Img = "StativMS-007H_3d14.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 85, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false, Img = "MikrofonKlopový_97b3.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 86, Name = "Mikrofon klopový", Description = "bez předzesilovače, induktivní", IsDeleted = false, Img = "MikrofonKlopový_97b3.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 87, Name = "Stativ Sony VCT-D680RM", IsDeleted = false, Img = "SonyVct-d680rm_9d51.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 88, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "první předmět", IsDeleted = true, Img = "tn-techtips_f9dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 89, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", Note = "druhý předmět", IsDeleted = true, Img = "kitt-design-illustration_73ad.png" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 90, Name = "zkušební předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché až primitivní konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true, Img = "tn-techtips_f9dd.jpg" });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 91, Name = "Fiktivní předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 92, Name = "Fiktivní předmět", Description = "Tento výrobek je zkonstruován z tenkého plátu ocelového plechu, který má patinovaný povrch. Díky tomuto hrubému povrchu působí výrobek surově, ale zároveň kvůli jednoduché konstrukci nabývá minimalistického a elegantního vzhledu. ", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 93, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 94, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true });
            modelBuilder.Entity<Item>().HasData(new Item { Id = 95, Name = "Alkalická baterie GP 2700 1.2 V AA", IsDeleted = true });

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


            //Kategorie předmětů
            //-přístroje (to jako fotoaparáty a videokamery)
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 1 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 2 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 5 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 6 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 7 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 8 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 9 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 10 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 11 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 12 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 15 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 16 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 18 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 19 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 20 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 21 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 45 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 1, ItemId = 80 });

            //-objektivy
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 13 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 14 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 17 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 44 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 46 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 69 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 2, ItemId = 79 });

            //-stativy (stativ, gimbal, flycam, rigy...)
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 70 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 71 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 74 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 75 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 78 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 81 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 82 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 83 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 84 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 3, ItemId = 87 });

            //-příslušenství (sd karty, akumulátory, nabíječky brašny)
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 22 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 23 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 24 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 25 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 26 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 27 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 28 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 29 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 30 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 31 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 32 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 33 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 34 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 35 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 36 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 37 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 38 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 39 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 40 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 41 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 42 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 43 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 47 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 48 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 49 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 50 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 51 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 52 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 53 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 54 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 55 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 56 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 57 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 58 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 59 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 60 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 61 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 62 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 63 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 64 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 65 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 66 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 67 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 4, ItemId = 72 });

            //-audiotechnika (mikrofon, rekordér)
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 3 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 4 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 76 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 77 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 85 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 5, ItemId = 86 });

            //ostatní (ateliér a co se "nevejde" jinam)
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 68 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 73 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 88 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 89 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 90 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 91 });
            modelBuilder.Entity<CategoryItem>().HasData(new CategoryItem { CategoryId = 6, ItemId = 92 });
        }
    }
}
