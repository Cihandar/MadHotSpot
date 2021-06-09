using System;
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
        public OtelAppDbContext(DbContextOptions<OtelAppDbContext> options) : base(options) { }
        public OtelAppDbContext()
        {

 

        }
        public DbSet<Ayarlar> H_Ayarlar { get; set; }
        public DbSet<Kullanicilar> H_Kullanicilar { get; set; }
        public DbSet<Firmalar> H_Firmalar { get; set; }
        public DbSet<InternetSatis> H_InternetSatis { get; set; }
    }





}
 
