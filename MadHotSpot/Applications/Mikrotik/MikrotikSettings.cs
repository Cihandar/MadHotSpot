using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using tik4net;
using MadHotSpot.Models.ViewModel;

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
                return new ResultMikrotikConnection { result = true, tikConnection = conn , ayarlar =ayar };
            }
            catch (TikConnectionException ex) 
            {
                return new ResultMikrotikConnection { Message = ex.Message, result = false,ayarlar=ayar };
            
            }catch(Exception ex)
            {
                return new ResultMikrotikConnection { Message = ex.Message, result = false, ayarlar = ayar };
            }    
      
          
        }


       
    }
}
