using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using MadHotSpot.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;

namespace MadHotSpot.Applications.Staffs
{
    public class StaffMikrotikCrud : IStaffMikrotikCrud
    {
        IMikrotikSettings _mikrotik;

        public StaffMikrotikCrud(IMikrotikSettings mikrotik)
        {
            _mikrotik = mikrotik;
        }

        public async Task<ResultJson> AddUser(StaffCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                var user = new HotspotUser()
                {
                    Profile = model.UserProfile,
                    Server = result.ayarlar.MikrotikHotspotAdi,
                    Name = model.IdNumber,
                    Password = model.Day + "." + model.Month + "." + model.Year,
                    Email = model.Email,
                    Disabled = !model.Active,
                    Comment = model.Comment
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

        public async Task<ResultJson> SetDisabled(Staff staff, bool status)
        {
            var result = await _mikrotik.GetMikrotikConnection(staff.FirmaId);
            if (result.result)
            {
                try
                {
                    using (var conn = result.tikConnection)
                    {
                        var user = conn.LoadById<HotspotUser>(staff.MikrotikId);
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

        public async Task<ResultJson> UpdateUser(StaffCrudDto model)
        {
            var result = await _mikrotik.GetMikrotikConnection(model.FirmaId);
            if (result.result)
            {
                try
                {
                    using (var conn = result.tikConnection)
                    {
                        var user = conn.LoadById<HotspotUser>(model.MikrotikId);
                        user.Disabled = !model.Active;
                        user.Name = model.IdNumber;
                        user.Email = model.Email;
                        user.Password = model.Day + "." + model.Month + "." + model.Year;
                        user.Profile = model.UserProfile;
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

        public async Task<ResultJson> DeleteUser(StaffCrudDto model)
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

        public async Task<ResultJson> CheckMikrotikUser(string username,string password,Guid FirmaId,string ClientMac,string ClientIp)
        {
            ResultMikrotikConnection resultMc = await _mikrotik.GetMikrotikConnection(FirmaId);
            ResultJson resultJson = new ResultJson();
            if(resultMc.result)
            {
                try
                {
                    using (var conn = resultMc.tikConnection)
                    {
                        var user = conn.LoadList<HotspotUser>().Where(x => x.Name.Equals(username) && x.Password.Equals(password)).FirstOrDefault();
                        if (user != null)
                        {
                            var resultActive = conn.CreateCommandAndParameters("/ip/hotspot/active/login", "user", username, "password", password, "mac-address", ClientMac, "ip", ClientIp).ExecuteScalarOrDefault();

                            resultJson.Success = true;
                        }
                        else
                        {
                            resultJson.Success = false;
                            resultJson.Message = "Geçersiz Şifre";
                        }
                    }
                }
                catch (Exception ex)
                {
                    resultJson.Success = false;
                    resultJson.Message = ex.Message;
                }
            }else
            {
                resultJson.Success = false;
                resultJson.Message = resultMc.Message;
            }

            return resultJson;
        }
    }
}
