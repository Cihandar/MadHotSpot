using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Dtos;
using MadHotSpot.Models;

namespace MadHotSpot.Interfaces
{
    public interface IVisitorCrud
    {
        public Task<ResultJson> Add(VisitorCrudDto model);
        public Task<ResultJson> Update(VisitorCrudDto model);
        public Task<List<VisitorCrudDto>> GetAll(Guid FirmaId);
        public Task<VisitorCrudDto> GetById(Guid Id);
        public Task<ResultJson> SetActive(Guid Id, bool status);
        public Task<ResultJson> Delete(Guid Id);
    }
}
