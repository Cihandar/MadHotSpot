using MadHotSpot.Dtos;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface IVisitorMikrotikCrud
    {
        public Task<ResultJson> AddUser(VisitorCrudDto model);
        public Task<ResultJson> UpdateUser(VisitorCrudDto model);
        public Task<ResultJson> SetDisabled(Visitor staff, bool status);
        public Task<ResultJson> DeleteUser(VisitorCrudDto model);
        public Task<ResultJson> CheckMikrotikUser(string username, string password, Guid FirmaId, string ClientMac, string ClientIp);
    }
}
