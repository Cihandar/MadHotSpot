﻿using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using MadHotSpot.Models;
 

namespace MadHotSpot.Models
{
    public class OtelAppDbContext :  IdentityDbContext<AppUser>
    {
        public OtelAppDbContext()
        {
        }
        public OtelAppDbContext(DbContextOptions<OtelAppDbContext> options) : base(options) { }
      
        public DbSet<Ayarlar> H_Ayarlar { get; set; }
        public DbSet<HotSpotAyar> H_HotSpotAyar { get; set; }
        public DbSet<Kullanicilar> H_Kullanicilar { get; set; }
        public DbSet<Firmalar> H_Firmalar { get; set; }
        public DbSet<InternetSatis> H_InternetSatis { get; set; }
        public DbSet<ViewKasa> H_ViewKasa { get; set; }
        public DbSet<Tarifeler> H_Tarifeler { get; set; }
        public DbSet<Rezervasyonlar> H_Rezervasyonlar { get; set; }
        public DbSet<CustomerInfo> H_CustomerInfo { get; set; }
        public DbSet<Staff> H_Staffs { get; set; }
        public DbSet<Meet> H_Meets { get; set; }
        public DbSet<Log> H_Logs { get; set; }
        public DbSet<Visitor> H_Visitors { get; set; }
        public DbSet<ElektraWebSetting> H_ElektraWebSetting { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("Server = 85.95.249.64\\MSSQLSERVER2016; Database = MadHotspotDb; User Id = aZ46e9n; Password = R5vlw93&; Application Name = OtelApp - Production; MultipleActiveResultSets = true");
            }
        }
    }
}
 
