﻿// <auto-generated />
using System;
using MadHotSpot.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace MadHotSpot.Migrations
{
    [DbContext(typeof(OtelAppDbContext))]
    [Migration("20210701190140_UpdateAppUserAddAdmin")]
    partial class UpdateAppUserAddAdmin
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.0-rtm-30799")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("MadHotSpot.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("AvatarUrl");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<Guid>("FirmaId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<byte>("Status");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.Property<int>("Yetki");

                    b.Property<bool>("admin");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("MadHotSpot.Models.Ayarlar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("AdSoyadZorunlu");

                    b.Property<Guid>("FirmaId");

                    b.Property<int>("FirmaKodu");

                    b.Property<double>("GunlukFiyatEURO");

                    b.Property<double>("GunlukFiyatTL");

                    b.Property<double>("GunlukFiyatUSD");

                    b.Property<string>("MikrotikDefaultSifre");

                    b.Property<string>("MikrotikHotspotAdi");

                    b.Property<string>("MikrotikIp");

                    b.Property<string>("MikrotikPass");

                    b.Property<string>("MikrotikPort");

                    b.Property<string>("MikrotikProfilAdi");

                    b.Property<string>("MikrotikUser");

                    b.Property<bool>("Silindi");

                    b.Property<bool>("SinirsizAktif");

                    b.HasKey("Id");

                    b.ToTable("H_Ayarlar");
                });

            modelBuilder.Entity("MadHotSpot.Models.Firmalar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Aktif");

                    b.Property<DateTime>("BaslamaTarihi");

                    b.Property<DateTime>("BitisTarihi");

                    b.Property<string>("Email");

                    b.Property<string>("FirmaAdi");

                    b.Property<Guid>("FirmaId");

                    b.Property<int>("FirmaKodu");

                    b.Property<string>("Password");

                    b.Property<bool>("Silindi");

                    b.Property<string>("Telefon");

                    b.Property<string>("YetkiliAdSoyad");

                    b.HasKey("Id");

                    b.ToTable("H_Firmalar");
                });

            modelBuilder.Entity("MadHotSpot.Models.InternetSatis", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AdSoyad");

                    b.Property<bool>("Aktarildi");

                    b.Property<DateTime>("BaslamaTarihi");

                    b.Property<DateTime>("BitisTarihi");

                    b.Property<string>("Doviz");

                    b.Property<Guid>("FirmaId");

                    b.Property<int>("FirmaKodu");

                    b.Property<int>("Gun");

                    b.Property<bool>("Iade");

                    b.Property<string>("IadeEdilenDoviz");

                    b.Property<double>("IadeEdilenTutar");

                    b.Property<string>("Odano");

                    b.Property<string>("Sifre");

                    b.Property<bool>("Silindi");

                    b.Property<string>("Telefon");

                    b.Property<double>("Tutar");

                    b.Property<string>("VatNo");

                    b.HasKey("Id");

                    b.ToTable("H_InternetSatis");
                });

            modelBuilder.Entity("MadHotSpot.Models.Kullanicilar", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Email");

                    b.Property<Guid>("FirmaId");

                    b.Property<int>("FirmaKodu");

                    b.Property<string>("KullaniciKodu");

                    b.Property<string>("Password");

                    b.Property<bool>("Silindi");

                    b.Property<int>("Yetki");

                    b.HasKey("Id");

                    b.ToTable("H_Kullanicilar");
                });

            modelBuilder.Entity("MadHotSpot.Models.ViewKasa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<double>("Bakiye");

                    b.Property<string>("Doviz");

                    b.Property<Guid>("FirmaId");

                    b.Property<int>("FirmaKodu");

                    b.Property<double>("Iade");

                    b.Property<double>("Satis");

                    b.Property<bool>("Silindi");

                    b.Property<DateTime>("Tarih");

                    b.HasKey("Id");

                    b.ToTable("H_ViewKasa");
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
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

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
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

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

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("MadHotSpot.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("MadHotSpot.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("MadHotSpot.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("MadHotSpot.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
