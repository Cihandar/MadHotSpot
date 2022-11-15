using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.Logs
{
    public class LogCrud : ILogCrud
    {
        OtelAppDbContext _context;

        public LogCrud(OtelAppDbContext context)
        {
            _context = context;
        }

        public Task<ResultJson> AddLogAsync(Log model)
        {
            return null;
        }

        public Task<List<Log>> GetListLogsAsync(Guid FirmaId)
        {
            throw new NotImplementedException();
        }

        public  async Task<ResultJson> SendErrorLogAsync(string Message, string module,Guid FirmaId, string mac, string localIp)
        {
            Log log = new Log
            {
                Date = DateTime.Now,
                Message = Message,
                Module = module,
                Error = true,
                FirmaId = FirmaId
            };
            ResultJson resultJson = await AddLogAsync(log);
            return resultJson;
        }

        public async Task<ResultJson> SendLogAsync(string Message, string module,Guid FirmaId, string mac, string localIp)
        {
            Log log = new Log
            {
                Date = DateTime.Now,
                Message = Message,
                Module = module,
                Error = false,
                FirmaId = FirmaId
            };

            ResultJson resultJson = await AddLogAsync(log);
            return resultJson;
        }
    }
}
