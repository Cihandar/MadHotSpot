using MadHotSpot.Dtos;
using MadHotSpot.Models;
using MadHotSpot.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net.Objects.Ip.Hotspot;

namespace MadHotSpot.Interfaces
{
    public interface IMikrotikUserProfile
    {
        public Task<ResultJson> Add(HotspotUserProfile model,Guid FirmaId);
        public Task<ResultJson> Update(HotspotUserProfileDto model,Guid FirmaId);
        public Task<ResultJson> Delete(string Id,Guid FirmaId);
        public Task<ResultWithObject<HotspotUserProfile>> GetAll(Guid FirmaId);
        public Task<ResultWithObject<HotspotUserProfileDto>> GetById(string Id,Guid FirmaId);
    }
}
