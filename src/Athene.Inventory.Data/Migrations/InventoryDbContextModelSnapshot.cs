﻿// <auto-generated />
using Athene.Inventory.Abstractions.Models;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using System;

namespace Athene.Inventory.Data.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    partial class InventoryDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452");

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("City");

                    b.Property<string>("Country");

                    b.Property<string>("Street");

                    b.Property<string>("Zip");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Article", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ImageUrl");

                    b.HasKey("Id");

                    b.ToTable("Articles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Article");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookId");

                    b.Property<string>("FullName");

                    b.Property<string>("Info");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.BoardOfEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("BoardOfEducation");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BookId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<int?>("ArticleId");

                    b.Property<int?>("ArticleId1");

                    b.Property<string>("Barcode");

                    b.Property<int>("Condition");

                    b.Property<string>("ExternalId");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<DateTime?>("PurchasedAt");

                    b.Property<DateTime?>("RentedAt");

                    b.Property<string>("RentedByUserId");

                    b.Property<int?>("SchoolId");

                    b.Property<int?>("StockLocationId");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("ArticleId");

                    b.HasIndex("ArticleId1");

                    b.HasIndex("SchoolId");

                    b.HasIndex("StockLocationId");

                    b.ToTable("InventoryItems");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.ItemNote", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<int?>("InventoryItemId");

                    b.Property<string>("Text");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("ItemNote");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<int>("BoardOfEducationId");

                    b.Property<string>("Name");

                    b.Property<string>("ShortName");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("BoardOfEducationId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name");

                    b.Property<int>("SchoolId");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("SchoolClass");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.StockLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Corridor");

                    b.Property<string>("Hall");

                    b.Property<string>("Level");

                    b.Property<string>("Position");

                    b.Property<string>("Rack");

                    b.HasKey("Id");

                    b.HasIndex("Hall", "Corridor", "Rack", "Level", "Position")
                        .IsUnique();

                    b.ToTable("StockLocations");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("SchoolClassId");

                    b.HasKey("StudentId");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Athene.Inventory.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("Birthsday");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<int>("Gender");

                    b.Property<string>("Lastname");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("MobilePhoneNumber");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("StudentId");

                    b.Property<string>("Surname");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("StudentId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ApplicationUserId");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("ApplicationUserId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("ApplicationUserId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Book", b =>
                {
                    b.HasBaseType("Athene.Inventory.Abstractions.Models.Article");

                    b.Property<string>("Description");

                    b.Property<string>("InternationalStandardBookNumber");

                    b.Property<int>("LanguageId");

                    b.Property<DateTime?>("PublishedAt");

                    b.Property<int>("PublisherId");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublisherId");

                    b.ToTable("Book");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Category", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book")
                        .WithMany("Categories")
                        .HasForeignKey("BookId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany("RentedItems")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.Article", "Article")
                        .WithMany()
                        .HasForeignKey("ArticleId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.Article")
                        .WithMany("InventoryItems")
                        .HasForeignKey("ArticleId1");

                    b.HasOne("Athene.Inventory.Abstractions.Models.School")
                        .WithMany("InventoryItems")
                        .HasForeignKey("SchoolId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.StockLocation", "StockLocation")
                        .WithMany("InventoryItems")
                        .HasForeignKey("StockLocationId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.ItemNote", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.InventoryItem")
                        .WithMany("Notes")
                        .HasForeignKey("InventoryItemId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.School", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.BoardOfEducation", "BoardOfEducation")
                        .WithMany()
                        .HasForeignKey("BoardOfEducationId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.SchoolClass", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.School", "School")
                        .WithMany("SchoolClasses")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Student", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Athene.Inventory.ApplicationUser", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.Student", "Student")
                        .WithMany()
                        .HasForeignKey("StudentId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("ApplicationUserId");

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Athene.Inventory.ApplicationUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Book", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.Abstractions.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
