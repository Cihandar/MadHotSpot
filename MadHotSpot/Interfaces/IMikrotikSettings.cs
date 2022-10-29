using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models;
using MadHotSpot.Models.ViewModel;
using tik4net;
namespace MadHotSpot.Interfaces
{
    public interface IMikrotikSettings
    {
        public Task<ResultMikrotikConnection> GetMikrotikConnection(Guid FirmaId);
        public Task<ResultJson> DeleteExpireTimeUsers(Guid FirmaId);
    }
}
