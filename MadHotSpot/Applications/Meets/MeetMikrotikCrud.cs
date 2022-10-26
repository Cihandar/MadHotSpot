using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;

namespace MadHotSpot.Applications.Meets
{
    public class MeetMikrotikCrud : IMeetMikrotikCrud
    {
        IMikrotikSettings _mikrotik;

        public MeetMikrotikCrud(IMikrotikSettings mikrotik)
        {
            _mikrotik = mikrotik;
        }

        public async Task<ResultJson> AddUser(MeetCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                var user = new HotspotUser()
                {
                    
                };
                try
                {
                    using (var conn = result.tikConnection)
                    {
                        conn.Save(user);
                    }
                    return new ResultJson { Success = true, MikrotikId = user.Id };
                }
                catch (Exception ex)
                {
                    return new ResultJson { Success = false, Message = ex.Message };
                }
            }
            else return new ResultJson { Success = false, Message = result.Message };
        }

        public async Task<ResultJson> SetDisabled(Meet Meet, bool status)
        {
            var result = await _mikrotik.GetMikrotikConnection(Meet.FirmaId);
            if (result.result)
            {
                try
                {
                    using (var conn = result.tikConnection)
                    {
                        var user = conn.LoadById<HotspotUser>(Meet.MikrotikId);
                        user.Disabled = !status;
                        conn.Save(user);
                    }
                    return new ResultJson { Success = true };
                }
                catch (Exception ex)
                {
                    return new ResultJson { Success = false, Message = ex.Message };
                }
            }
            else return new ResultJson { Success = false, Message = result.Message };
        }

        public async Task<ResultJson> UpdateUser(MeetCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                try
                {
                    using (var conn = result.tikConnection)
                    {
                        var user = conn.LoadById<HotspotUser>(model.MikrotikId);
                        user.Disabled = !model.Silindi;
                        conn.Save(user);
                        return new ResultJson { Success = true, MikrotikId = user.Id };
                    }
               
                }
                catch (TikNoSuchItemException ex)
                {
                    var newresult = await AddUser(model);
                    return new ResultJson { Success = newresult.Success, Message = newresult.Message, MikrotikId = newresult.MikrotikId };
                    
                }
                catch (Exception ex)
                {
                    return new ResultJson { Success = false, Message = ex.Message };
                }
            
            }
            else return new ResultJson { Success = false, Message = result.Message };
        }

        public async Task<ResultJson> DeleteUser(MeetCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                try
                {
                    using (var conn = result.tikConnection)
                    {                   
                        var user = conn.LoadById<HotspotUser>(model.MikrotikId); 
                        conn.Delete(user);
                    }
                    return new ResultJson { Success = true };
                }
                catch (Exception ex)
                {
                    return new ResultJson { Success = false, Message = ex.Message };
                }
            }
            else return new ResultJson { Success = false, Message = result.Message };
        }
    }
}
