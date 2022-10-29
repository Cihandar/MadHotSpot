using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Interfaces;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;
using MadHotSpot.Models;
using MadHotSpot.Models.ViewModel;
using AutoMapper;
using MadHotSpot.Dtos;

namespace MadHotSpot.Applications.Mikrotik
{
    public class MikrotikUserProfile : IMikrotikUserProfile
    {
        IMikrotikSettings _mikrotikSetting;
        OtelAppDbContext _context;
        IMapper _mapper;

        public MikrotikUserProfile(IMikrotikSettings mikrotikSetting, OtelAppDbContext context, IMapper mapper)
        {
            _mikrotikSetting = mikrotikSetting;
            _context = context;
            _mapper = mapper;
        }

        public async Task<ResultJson> Add(HotspotUserProfile model, Guid FirmaId)
        {
            var connection = await GetConnection(FirmaId);
            if (!connection.result) return new ResultJson { Success = false, Message = connection.Message };

            try
            {
                using (var conn = connection.tikConnection)
                {
                    conn.Save<HotspotUserProfile>(model);
                }
                return new ResultJson { Success = true, Message = "Profile Mikrotike Kaydedildi",UserProfileName=model.Name };
            }
            catch (Exception ex)
            {
              return new  ResultJson { Success = false, Message = ex.Message};
            }
        }

        public async Task<ResultJson> Update(HotspotUserProfileDto model, Guid FirmaId)
        {
            var connection = await GetConnection(FirmaId);
            if (!connection.result) return new ResultJson { Success = false, Message = connection.Message };

            try
            {
                using (var conn = connection.tikConnection)
                {
                    var userProfil = conn.LoadById<HotspotUserProfile>(model.Id);
                    userProfil.Name = model.Name;
                    userProfil.SharedUsers = model.SharedUsers;
                    userProfil.RateLimit = model.RateLimit;
                    userProfil.AddMacCookie = model.AddMacCookie;
                    userProfil.MacCookieTimeout = model.MacCookieTimeout;

                    conn.Save(userProfil);
                }
                return new ResultJson { Success = true, Message = "Profile Mikrotike Kaydedildi" };
            }
            catch (Exception ex)
            {
                return new ResultJson { Success = false, Message = ex.Message };
            }

        }

        public async Task<ResultJson> Delete(string Id, Guid FirmaId)
        {
            var connection = await GetConnection(FirmaId);
            if (!connection.result) return new ResultJson { Success = false, Message = connection.Message };

            try
            {
                using (var conn = connection.tikConnection)
                {
                    var model = conn.LoadById<HotspotUserProfile>(Id);
                    conn.Delete<HotspotUserProfile>(model);
                }
                return new ResultJson { Success = true, Message = "Seçili Profile Mikrotikten silindi" };
            }
            catch (Exception ex)
            {
                return new ResultJson { Success = false, Message = ex.Message };
            }
        }

        public async Task<ResultWithObject<HotspotUserProfile>> GetAll(Guid FirmaId)
        {
            var connection = await GetConnection(FirmaId);
            if (!connection.result) return new ResultWithObject<HotspotUserProfile> { Status = false, Message = connection.Message };

            try
            {
                using (var conn = connection.tikConnection)
                {
                    var result = conn.LoadList<HotspotUserProfile>().ToList();
                    return new ResultWithObject<HotspotUserProfile> { ListData = result, Status = true };
                }                 
            }
            catch (Exception ex)
            {
                return new ResultWithObject<HotspotUserProfile> { Status = false, Message = ex.Message };
            }
        }

        public async Task<ResultWithObject<HotspotUserProfileDto>> GetById(string Id, Guid FirmaId)
        {
            var connection = await GetConnection(FirmaId);
            if (!connection.result) return new ResultWithObject<HotspotUserProfileDto> { Status = false, Message = connection.Message };

            try
            {
                using (var conn = connection.tikConnection)
                {
                    var model = conn.LoadById<HotspotUserProfile>(Id);
                    var result = _mapper.Map<HotspotUserProfileDto>(model);
                    return new ResultWithObject<HotspotUserProfileDto> { Data = result, Status = true };
                }
            }
            catch (Exception ex)
            {
                return new ResultWithObject<HotspotUserProfileDto> { Status = false, Message = ex.Message };
            }
        }   

        private async Task<ResultMikrotikConnection> GetConnection(Guid FirmaId)
        {
            return await _mikrotikSetting.GetMikrotikConnection(FirmaId);
        }





    }
}
