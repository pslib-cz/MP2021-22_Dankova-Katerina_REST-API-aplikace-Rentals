﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Rentals.Context;

namespace Rentals.Migrations
{
    [DbContext(typeof(RentalsDbContext))]
    partial class RentalsDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.11")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Rentals.Models.DatabaseModel.AccessoryItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("AccessoryId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "AccessoryId");

                    b.HasIndex("AccessoryId");

                    b.ToTable("AccessoryItems");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.CartItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("CartItems");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Categories");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Name = "Fotoaparáty"
                        },
                        new
                        {
                            Id = 2,
                            Name = "Kamery"
                        });
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.CategoryItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("CategoryId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "CategoryId");

                    b.HasIndex("CategoryId");

                    b.ToTable("CategoryItems");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.FavouriteItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("FavouriteItems");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.InventoryItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("ItemId", "UserId");

                    b.HasIndex("UserId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Item", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Description")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Img")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("bit");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Items");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            Description = "Popis",
                            IsDeleted = true,
                            Name = "Jméno",
                            Note = "Poznámka",
                            State = 2
                        },
                        new
                        {
                            Id = 2,
                            Description = "Popis",
                            IsDeleted = false,
                            Name = "Jméno2",
                            Note = "Poznámka",
                            State = 1
                        },
                        new
                        {
                            Id = 3,
                            Description = "Popis",
                            IsDeleted = false,
                            Name = "Jméno3",
                            Note = "Poznámka",
                            State = 0
                        });
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Renting", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ApproverId")
                        .HasColumnType("int");

                    b.Property<DateTime>("End")
                        .HasColumnType("datetime2");

                    b.Property<string>("Note")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OwnerId")
                        .HasColumnType("int");

                    b.Property<DateTime>("Start")
                        .HasColumnType("datetime2");

                    b.Property<int>("State")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ApproverId");

                    b.HasIndex("OwnerId");

                    b.ToTable("Rentings");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ApproverId = 1,
                            End = new DateTime(2021, 10, 19, 17, 6, 25, 583, DateTimeKind.Local).AddTicks(6561),
                            OwnerId = 2,
                            Start = new DateTime(2021, 10, 14, 17, 6, 25, 586, DateTimeKind.Local).AddTicks(4651),
                            State = 0
                        });
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.RentingItem", b =>
                {
                    b.Property<int>("ItemId")
                        .HasColumnType("int");

                    b.Property<int>("RentingId")
                        .HasColumnType("int");

                    b.Property<bool>("Returned")
                        .HasColumnType("bit");

                    b.HasKey("ItemId", "RentingId");

                    b.HasIndex("RentingId");

                    b.ToTable("RentingItems");

                    b.HasData(
                        new
                        {
                            ItemId = 2,
                            RentingId = 1,
                            Returned = false
                        });
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("OauthId")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Trustfulness")
                        .HasColumnType("int");

                    b.Property<string>("Username")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            FirstName = "Admin",
                            Trustfulness = 0
                        },
                        new
                        {
                            Id = 2,
                            FirstName = "Jan",
                            LastName = "Novák",
                            Trustfulness = 0
                        });
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.AccessoryItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Accessory")
                        .WithMany()
                        .HasForeignKey("AccessoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Accessories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Accessory");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.CartItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Carts")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.User", "User")
                        .WithMany("Cart")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.CategoryItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Category", "Category")
                        .WithMany("Items")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Categories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Category");

                    b.Navigation("Item");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.FavouriteItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Favourites")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.User", "User")
                        .WithMany("Favourite")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.InventoryItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Inventories")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.User", "User")
                        .WithMany("Inventory")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("User");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Renting", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.User", "Approver")
                        .WithMany()
                        .HasForeignKey("ApproverId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.User", "Owner")
                        .WithMany("Rentings")
                        .HasForeignKey("OwnerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Approver");

                    b.Navigation("Owner");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.RentingItem", b =>
                {
                    b.HasOne("Rentals.Models.DatabaseModel.Item", "Item")
                        .WithMany("Rentings")
                        .HasForeignKey("ItemId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Rentals.Models.DatabaseModel.Renting", "Renting")
                        .WithMany("Items")
                        .HasForeignKey("RentingId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Item");

                    b.Navigation("Renting");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Category", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Item", b =>
                {
                    b.Navigation("Accessories");

                    b.Navigation("Carts");

                    b.Navigation("Categories");

                    b.Navigation("Favourites");

                    b.Navigation("Inventories");

                    b.Navigation("Rentings");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.Renting", b =>
                {
                    b.Navigation("Items");
                });

            modelBuilder.Entity("Rentals.Models.DatabaseModel.User", b =>
                {
                    b.Navigation("Cart");

                    b.Navigation("Favourite");

                    b.Navigation("Inventory");

                    b.Navigation("Rentings");
                });
#pragma warning restore 612, 618
        }
    }
}
