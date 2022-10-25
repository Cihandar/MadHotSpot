using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MadHotSpot.Extentions.AutoMapper.Mapping;
using MadHotSpot.Dtos;
using MadHotSpot.Models;
using tik4net.Objects.Ip.Hotspot;

namespace MadHotSpot.Applications.Mikrotik
{
    public class MikrotikUserProfileMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configration)
        {
            configration.CreateMap<HotspotUserProfile,HotspotUserProfileDto>();
            configration.CreateMap<HotspotUserProfileDto,HotspotUserProfile>();
        }
    }
}
