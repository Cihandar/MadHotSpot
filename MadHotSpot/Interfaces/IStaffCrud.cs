using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Dtos;
using MadHotSpot.Models;

namespace MadHotSpot.Interfaces
{
    public interface IStaffCrud
    {
        public Task<ResultJson> Add(StaffCrudDto model);
        public Task<ResultJson> Update(StaffCrudDto model);
        public Task<List<StaffCrudDto>> GetAll(Guid FirmaId);
        public Task<StaffCrudDto> GetById(Guid Id);
        public Task<ResultJson> SetActive(Guid Id, string mikrotikId, bool status, Guid FirmaId);
    }
}
