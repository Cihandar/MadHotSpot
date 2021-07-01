using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models.Enum;
using Microsoft.AspNetCore.Identity;

namespace MadHotSpot.Models
{

    public class AppUser : IdentityUser
    {
        public string AvatarUrl { get; set; }
        public byte Status { get; set; }
        public Guid FirmaId { get; set; }
        public PermissionEnum Yetki { get; set; }
        public bool admin { get; set; }
    }
}
