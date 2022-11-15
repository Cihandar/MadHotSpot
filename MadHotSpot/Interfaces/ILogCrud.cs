using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface ILogCrud
    {
        public Task<ResultJson> AddLogAsync(Log model);
        public Task<List<Log>> GetListLogsAsync(Guid FirmaId);
        public Task<ResultJson> SendErrorLogAsync(string Message, string module, Guid FirmaId,string mac,string localIp);
        public Task<ResultJson> SendLogAsync(string Message, string module, Guid FirmaId, string mac, string localIp);

    }
}
