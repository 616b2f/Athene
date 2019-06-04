﻿// <auto-generated />
using System;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Athene.Inventory.Data.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20190603204956_ChangeLanguageIsOptional")]
    partial class ChangeLanguageIsOptional
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.2.4-servicing-10062")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

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
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Discriminator")
                        .IsRequired();

                    b.Property<string>("ImageUrl");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Article");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("BookArticleId");

                    b.Property<string>("FullName");

                    b.Property<string>("Info");

                    b.HasKey("Id");

                    b.HasIndex("BookArticleId");

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

                    b.Property<int?>("BookArticleId");

                    b.Property<string>("Name");

                    b.HasKey("Id");

                    b.HasIndex("BookArticleId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("ArticleId");

                    b.Property<string>("Barcode");

                    b.Property<int>("Condition");

                    b.Property<string>("ExternalId");

                    b.Property<decimal>("PurchasePrice");

                    b.Property<DateTime?>("PurchasedAt");

                    b.Property<DateTime?>("RentedAt");

                    b.Property<string>("RentedByUserId");

                    b.Property<int?>("SchoolId");

                    b.Property<int?>("StockLocationId");

                    b.Property<string>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ArticleId");

                    b.HasIndex("Barcode");

                    b.HasIndex("ExternalId");

                    b.HasIndex("RentedByUserId");

                    b.HasIndex("SchoolId");

                    b.HasIndex("StockLocationId");

                    b.HasIndex("UserId");

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

            modelBuilder.Entity("Athene.Inventory.User", b =>
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

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("UserId1");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.Property<string>("UserId1");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.Property<string>("UserId1");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId1");

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

                    b.Property<int?>("LanguageId");

                    b.Property<DateTime?>("PublishedAt");

                    b.Property<int>("PublisherId");

                    b.Property<string>("SubTitle");

                    b.Property<string>("Title");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublisherId");

                    b.HasDiscriminator().HasValue("Book");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Article", b =>
                {
                    b.OwnsMany("Athene.Inventory.Abstractions.Models.Matchcode", "Matchcodes", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd();

                            b1.Property<int>("ArticleId");

                            b1.Property<string>("Value");

                            b1.HasKey("Id");

                            b1.HasIndex("ArticleId");

                            b1.HasIndex("Value");

                            b1.ToTable("Matchcode");

                            b1.HasOne("Athene.Inventory.Abstractions.Models.Article")
                                .WithMany("Matchcodes")
                                .HasForeignKey("ArticleId")
                                .OnDelete(DeleteBehavior.Cascade);
                        });
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book")
                        .WithMany("Authors")
                        .HasForeignKey("BookArticleId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Category", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book")
                        .WithMany("Categories")
                        .HasForeignKey("BookArticleId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Article", "Article")
                        .WithMany("InventoryItems")
                        .HasForeignKey("ArticleId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.School")
                        .WithMany("InventoryItems")
                        .HasForeignKey("SchoolId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.StockLocation", "StockLocation")
                        .WithMany("InventoryItems")
                        .HasForeignKey("StockLocationId");

                    b.HasOne("Athene.Inventory.User")
                        .WithMany("RentedItems")
                        .HasForeignKey("UserId");
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

            modelBuilder.Entity("Athene.Inventory.User", b =>
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
                    b.HasOne("Athene.Inventory.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Athene.Inventory.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("Athene.Inventory.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Athene.Inventory.User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Book", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
