using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;

namespace MadHotSpot.Extentions.AutoMapper.Mapping
{
    public interface IHaveCustomMapping
    {
        void CreateMappings(Profile configration);
    }
}
