using AutoMapper;
using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.ElektraWeb
{
    public class ElektraWebSettingsCrud : IElektraWebSettingsCrud
    {
        OtelAppDbContext _context;
        IMapper _mapper;

        public ElektraWebSettingsCrud(IMapper mapper, OtelAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResultJson> Add(ElektraWebSettingsDto model)
        {
            var data = _mapper.Map<ElektraWebSetting>(model);
            await _context.H_ElektraWebSetting.AddAsync(data);
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Personel Kaydı Yapıldı", Id = data.Id };
        }

        public async Task<List<ElektraWebSettingsDto>> GetAll(Guid FirmaId)
        {
            var ElektraWebSettings = await _context.H_ElektraWebSetting.Where(x => x.FirmaId == FirmaId).ToListAsync();
            var result = _mapper.Map<List<ElektraWebSettingsDto>>(ElektraWebSettings);
            return result;
        }

        public async Task<ElektraWebSettingsDto> GetById(Guid Id)
        {
            var ElektraWebSettings = await _context.H_ElektraWebSetting.FindAsync(Id);
            var result = _mapper.Map<ElektraWebSettingsDto>(ElektraWebSettings);
            return result;
        }

        public async Task<ResultJson> SetActive(Guid Id, bool status)
        {
            var ElektraWebSettings = await _context.H_ElektraWebSetting.FindAsync(Id);
            return new ResultJson { Success = true, Message = "Başarılı", Id = ElektraWebSettings.Id };
        }

        public async Task<ResultJson> Update(ElektraWebSettingsDto model)
        {
            var ElektraWebSettings = await _context.H_ElektraWebSetting.FindAsync(model.Id);
            return new ResultJson { Success = true, Message = "Güncelleme Başarılı", Id = ElektraWebSettings.Id };
        }

        public async Task<ResultJson> Delete(Guid Id)
        {
            var model = await GetById(Id);
            return new ResultJson { Success = true, Message = "Kayıt Silindi" };
        }
    }
}
