using MadHotSpot.Dtos;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface IMeetMikrotikCrud
    {
        public Task<ResultJson> AddUser(MeetCrudDto model);
        public Task<ResultJson> UpdateUser(MeetCrudDto model);
        public Task<ResultJson> SetDisabled(Meet Meet, bool status);
        public Task<ResultJson> DeleteUser(MeetCrudDto model);
        public Task<ResultJson> CheckMikrotikUser(string username, string password, Guid FirmaId);
    }
}
