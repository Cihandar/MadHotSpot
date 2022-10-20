using MadHotSpot.Dtos;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface IStaffMikrotikCrud
    {
        public Task<ResultJson> AddUser(StaffCrudDto model);
        public Task<ResultJson> UpdateUser(StaffCrudDto model);
        public Task<ResultJson> SetDisabled(Guid FirmaId, string mikrotikId, bool status);
    }
}
