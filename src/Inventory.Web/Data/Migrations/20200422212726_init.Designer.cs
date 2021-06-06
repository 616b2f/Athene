﻿// <auto-generated />
using System;
using Athene.Inventory.Data.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Inventory.Web.Data.Migrations
{
    [DbContext(typeof(InventoryDbContext))]
    [Migration("20200422212726_init")]
    partial class init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.3");

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Address", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("City")
                        .HasColumnType("TEXT");

                    b.Property<string>("Country")
                        .HasColumnType("TEXT");

                    b.Property<string>("Street")
                        .HasColumnType("TEXT");

                    b.Property<string>("Zip")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Address");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Article", b =>
                {
                    b.Property<int>("ArticleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ImageUrl")
                        .HasColumnType("TEXT");

                    b.Property<string>("article_type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("ArticleId");

                    b.ToTable("Articles");

                    b.HasDiscriminator<string>("article_type").HasValue("Article");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EBookArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FullName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Info")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookArticleId");

                    b.HasIndex("EBookArticleId");

                    b.ToTable("Authors");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.BoardOfEducation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("BoardOfEducation");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Category", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("BookArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("EBookArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BookArticleId");

                    b.HasIndex("EBookArticleId");

                    b.ToTable("Categories");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("ArticleId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Barcode")
                        .HasColumnType("TEXT");

                    b.Property<int>("Condition")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ExternalId")
                        .HasColumnType("TEXT");

                    b.Property<decimal>("PurchasePrice")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("PurchasedAt")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("RentedAt")
                        .HasColumnType("TEXT");

                    b.Property<string>("RentedByUserId")
                        .HasColumnType("TEXT");

                    b.Property<int?>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StockLocationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("TEXT");

                    b.Property<int?>("InventoryItemId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Text")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InventoryItemId");

                    b.ToTable("ItemNote");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Language", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Languages");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Publisher", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Publisher");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.School", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BoardOfEducationId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<string>("ShortName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("AddressId");

                    b.HasIndex("BoardOfEducationId");

                    b.ToTable("Schools");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.SchoolClass", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<int>("SchoolId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("SchoolId");

                    b.ToTable("SchoolClass");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.StockLocation", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Corridor")
                        .HasColumnType("TEXT");

                    b.Property<string>("Hall")
                        .HasColumnType("TEXT");

                    b.Property<string>("Level")
                        .HasColumnType("TEXT");

                    b.Property<string>("Position")
                        .HasColumnType("TEXT");

                    b.Property<string>("Rack")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Hall", "Corridor", "Rack", "Level", "Position")
                        .IsUnique();

                    b.ToTable("StockLocations");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Student", b =>
                {
                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<int>("SchoolClassId")
                        .HasColumnType("INTEGER");

                    b.HasKey("StudentId");

                    b.HasIndex("SchoolClassId");

                    b.ToTable("Student");
                });

            modelBuilder.Entity("Athene.Inventory.User", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("AddressId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("Birthsday")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Gender")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Lastname")
                        .HasColumnType("TEXT");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<string>("MobilePhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("StudentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Surname")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
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

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.DeviceFlowCodes", b =>
                {
                    b.Property<string>("UserCode")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50000);

                    b.Property<string>("DeviceCode")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<DateTime?>("Expiration")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("SubjectId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.HasKey("UserCode");

                    b.HasIndex("DeviceCode")
                        .IsUnique();

                    b.HasIndex("Expiration");

                    b.ToTable("DeviceCodes");
                });

            modelBuilder.Entity("IdentityServer4.EntityFramework.Entities.PersistedGrant", b =>
                {
                    b.Property<string>("Key")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("ClientId")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<DateTime>("CreationTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Data")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50000);

                    b.Property<DateTime?>("Expiration")
                        .HasColumnType("TEXT");

                    b.Property<string>("SubjectId")
                        .HasColumnType("TEXT")
                        .HasMaxLength(200);

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.HasKey("Key");

                    b.HasIndex("Expiration");

                    b.HasIndex("SubjectId", "ClientId", "Type");

                    b.ToTable("PersistedGrants");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId1")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId1");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Book", b =>
                {
                    b.HasBaseType("Athene.Inventory.Abstractions.Models.Article");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("InternationalStandardBookNumber")
                        .HasColumnType("TEXT");

                    b.Property<int?>("LanguageId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("PublishedAt")
                        .HasColumnType("TEXT");

                    b.Property<int>("PublisherId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SubTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasIndex("LanguageId");

                    b.HasIndex("PublisherId");

                    b.HasDiscriminator().HasValue("book");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.EBook", b =>
                {
                    b.HasBaseType("Athene.Inventory.Abstractions.Models.Article");

                    b.Property<string>("SubTitle")
                        .HasColumnName("EBook_SubTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnName("EBook_Title")
                        .HasColumnType("TEXT");

                    b.HasDiscriminator().HasValue("ebook");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Article", b =>
                {
                    b.OwnsMany("Athene.Inventory.Abstractions.Models.Matchcode", "Matchcodes", b1 =>
                        {
                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("INTEGER");

                            b1.Property<int>("ArticleId")
                                .HasColumnType("INTEGER");

                            b1.Property<string>("Value")
                                .HasColumnType("TEXT");

                            b1.HasKey("Id");

                            b1.HasIndex("ArticleId");

                            b1.HasIndex("Value");

                            b1.ToTable("Matchcode");

                            b1.WithOwner()
                                .HasForeignKey("ArticleId");
                        });
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Author", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book", null)
                        .WithMany("Authors")
                        .HasForeignKey("BookArticleId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.EBook", null)
                        .WithMany("Authors")
                        .HasForeignKey("EBookArticleId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Category", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Book", null)
                        .WithMany("Categories")
                        .HasForeignKey("BookArticleId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.EBook", null)
                        .WithMany("Categories")
                        .HasForeignKey("EBookArticleId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.InventoryItem", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Article", "Article")
                        .WithMany("InventoryItems")
                        .HasForeignKey("ArticleId");

                    b.HasOne("Athene.Inventory.User", "RentedBy")
                        .WithMany()
                        .HasForeignKey("RentedByUserId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.School", null)
                        .WithMany("InventoryItems")
                        .HasForeignKey("SchoolId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.StockLocation", "StockLocation")
                        .WithMany("InventoryItems")
                        .HasForeignKey("StockLocationId");

                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany("RentedItems")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.ItemNote", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.InventoryItem", null)
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
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.SchoolClass", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.School", "School")
                        .WithMany("SchoolClasses")
                        .HasForeignKey("SchoolId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Student", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.SchoolClass", "SchoolClass")
                        .WithMany()
                        .HasForeignKey("SchoolClassId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
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
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany("Claims")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany("Logins")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany("Roles")
                        .HasForeignKey("UserId1");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Athene.Inventory.User", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Athene.Inventory.Abstractions.Models.Book", b =>
                {
                    b.HasOne("Athene.Inventory.Abstractions.Models.Language", "Language")
                        .WithMany()
                        .HasForeignKey("LanguageId");

                    b.HasOne("Athene.Inventory.Abstractions.Models.Publisher", "Publisher")
                        .WithMany()
                        .HasForeignKey("PublisherId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
