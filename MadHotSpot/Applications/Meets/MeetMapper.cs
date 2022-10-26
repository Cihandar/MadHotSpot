using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MadHotSpot.Extentions.AutoMapper.Mapping;
using MadHotSpot.Dtos;
using MadHotSpot.Models;

namespace MadHotSpot.Applications.Meets
{
    public class MeetMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configration)
        {
            configration.CreateMap<Meet, MeetCrudDto>();
            configration.CreateMap<MeetCrudDto, Meet>();
        }
    }
}
