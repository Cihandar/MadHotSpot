using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Dtos;
using MadHotSpot.Models;

namespace MadHotSpot.Interfaces
{
    public interface IMeetCrud
    {
        public Task<ResultJson> Add(MeetCrudDto model);
        public Task<ResultJson> Update(MeetCrudDto model);
        public Task<List<MeetCrudDto>> GetAll(Guid FirmaId);
        public Task<MeetCrudDto> GetById(Guid Id);
        public Task<ResultJson> SetActive(Guid Id, bool status);
        public Task<ResultJson> Delete(Guid Id);
    }
}
