﻿using MadHotSpot.Dtos;
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
        public Task<ResultJson> SetDisabled(Staff staff, bool status);
        public Task<ResultJson> DeleteUser(StaffCrudDto model);
        public Task<ResultJson> CheckMikrotikUser(string username, string password, Guid FirmaId, string ClientMac, string ClientIp);
    }
}
