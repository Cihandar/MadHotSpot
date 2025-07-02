using AutoMapper;
using MadHotSpot.Dtos;
using MadHotSpot.Extentions.AutoMapper.Mapping;
using MadHotSpot.Models;

namespace MadHotSpot.Applications.Visitors
{
    public class VisitorMapper : IHaveCustomMapping
    {
        public void CreateMappings(Profile configration)
        {
            configration.CreateMap<Visitor, VisitorCrudDto>();
            configration.CreateMap<VisitorCrudDto, Visitor>();
        }
    }
}
