using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net;
using tik4net.Objects;

using MadHotSpot.Models.ViewModel;
using tik4net.Objects.Ip.Hotspot;

namespace MadHotSpot.Applications.Mikrotik
{
    public class MikrotikSettings : IMikrotikSettings
    {
        OtelAppDbContext _context;

        public MikrotikSettings(OtelAppDbContext context)
        {
            _context = context;
        }
        public async Task<ResultMikrotikConnection> GetMikrotikConnection(Guid FirmaId)
        {

            var ayar = _context.H_Ayarlar.Where(x => x.FirmaId == FirmaId).FirstOrDefault();

            try
            {
                var conn = ConnectionFactory.OpenConnection(TikConnectionType.Api_v2, ayar.MikrotikIp, int.Parse(ayar.MikrotikPort), ayar.MikrotikUser, ayar.MikrotikPass);
                return new ResultMikrotikConnection { result = true, tikConnection = conn, ayarlar = ayar };
            }
            catch (TikConnectionException ex)
            {
                return new ResultMikrotikConnection { Message = ex.Message, result = false, ayarlar = ayar };

            }
            catch (Exception ex)
            {
                return new ResultMikrotikConnection { Message = ex.Message, result = false, ayarlar = ayar };
            }
        }

        public async Task<ResultJson> DeleteExpireTimeUsers(Guid FirmaId)
        {
            try
            {
                var model = _context.H_InternetSatis.Where(x => x.FirmaId.Equals(FirmaId) && x.BitisTarihi < DateTime.Now.AddDays(-2)).ToList();

                var conn = await GetMikrotikConnection(FirmaId);
                if (!conn.result) return new ResultJson { Message = conn.Message, Success = conn.result };

                using (var con = conn.tikConnection)
                {
                    int dltCounter = 0;
                
                    foreach (var x in model)
                    {
                        var entity = new HotspotUser();
                        try
                        {
                              entity = con.LoadByName<HotspotUser>(x.Sifre);
             
                        }
                        catch  (TikNoSuchItemException ex)
                        {
                            continue;
                        }catch (Exception ex)
                        {
                            return new ResultJson { Message = ex.Message, Success = false };
                        }               
                         con.Delete(entity); dltCounter++; 
                    }
                }
                return new ResultJson { Message = "Eski Kayıtlar Mikrotik tarafında silindi.", Success = true };
            }
            catch (Exception ex)
            {
                return new ResultJson { Message = ex.Message, Success = false };
            }
        }
    }
}
