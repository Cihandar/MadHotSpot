using MadHotSpot.Dtos;
using MadHotSpot.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MadHotSpot.Interfaces
{
    public interface IElektraWebSettingsCrud
    {
        public Task<ResultJson> Add(ElektraWebSettingsDto model);
        public Task<ResultJson> Update(ElektraWebSettingsDto model);
        public Task<List<ElektraWebSettingsDto>> GetAll(Guid FirmaId);
        public Task<ElektraWebSettingsDto> GetById(Guid Id);
        public Task<ResultJson> SetActive(Guid Id, bool status);
        public Task<ResultJson> Delete(Guid Id);
    }
}
