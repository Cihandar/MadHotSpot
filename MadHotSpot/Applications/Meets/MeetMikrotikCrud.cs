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
        IMikrotikUserProfile _userProfile;

        public MeetMikrotikCrud(IMikrotikSettings mikrotik, IMikrotikUserProfile userProfile)
        {
            _mikrotik = mikrotik;
            _userProfile = userProfile;
        }

        public async Task<ResultJson> AddUser(MeetCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                var time = (int)Math.Round((model.PasswordExpire - DateTime.Now).TotalDays, 0);
                var user = new HotspotUser()
                {
                    Name = model.Name,
                    Password = model.Password,
                    Comment = model.CompanyName,
                    LimitUptime = time.ToString() + ":00:00"
                };

                var userProfil = new HotspotUserProfile
                {
                    Name = Guid.NewGuid().ToString(),
                    RateLimit = model.RateLimit,
                    SharedUsers = model.AccessLimit
                };

                user.Profile = userProfil.Name;

                try
                {
                    using (var conn = result.tikConnection)
                    {
                        conn.Save(userProfil);
                        try
                        {
                            using (var conn2 = result.tikConnection)
                            {
                                conn2.Save(user);
                            }
                            return new ResultJson { Success = true, MikrotikId = user.Id, UserProfileName = userProfil.Name };
                        }
                        catch (Exception ex)
                        {
                            return new ResultJson { Success = false, Message = ex.Message };
                        }
                    }
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
                        var profile = new HotspotUserProfile();
                        try
                        {                            
                            profile = conn.LoadByName<HotspotUserProfile>(model.UserProfileName);                 
                        }
                        catch (TikNoSuchItemException)
                        {
                            profile.Name = Guid.NewGuid().ToString();
                        }
                        profile.RateLimit = model.RateLimit;
                        profile.SharedUsers = model.AccessLimit;
                        conn.Save(profile);

                        var time = (int)Math.Round((DateTime.Now - model.PasswordExpire).TotalDays, 0);
                        var user = conn.LoadById<HotspotUser>(model.MikrotikId);
                        user.Name = model.Name;
                        user.Password = model.Password;
                        user.LimitUptime = time.ToString() + ":00:00";
                        user.Profile = profile.Name;
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
                        var profile = conn.LoadByName<HotspotUserProfile>(model.UserProfileName);
                        conn.Delete(profile);

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
