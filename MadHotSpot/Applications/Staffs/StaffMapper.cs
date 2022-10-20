using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MadHotSpot.Extentions.AutoMapper.Mapping;
using MadHotSpot.Dtos;
using MadHotSpot.Models;

namespace MadHotSpot.Applications.Staffs
{
    public class StaffMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configration)
        {
            configration.CreateMap<Staff, StaffCrudDto>();
            configration.CreateMap<StaffCrudDto, Staff>();
        }
    }
}
