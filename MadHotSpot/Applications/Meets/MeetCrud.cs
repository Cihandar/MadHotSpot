using AutoMapper;
using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.Meets
{
    public class MeetCrud : IMeetCrud
    {

        OtelAppDbContext _context;
        IMapper _mapper;
        IMeetMikrotikCrud _MeetMikrotik;

        public MeetCrud(IMapper mapper, OtelAppDbContext context, IMeetMikrotikCrud MeetMikrotik)
        {
            _mapper = mapper;
            _context = context;
            _MeetMikrotik = MeetMikrotik;
        }

        public async Task<ResultJson> Add(MeetCrudDto model)
        {
            var mikrotikresult = await  _MeetMikrotik.AddUser(model);
            if (!mikrotikresult.Success) return new ResultJson { Success = false, Message = mikrotikresult.Message };
            var data = _mapper.Map<Meet>(model);
            data.MikrotikId = mikrotikresult.MikrotikId;
            await _context.H_Meets.AddAsync(data);
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Personel Kaydı Yapıldı", Id = data.Id };
        }

        public async Task<List<MeetCrudDto>> GetAll(Guid FirmaId)
        {
            var Meet = await _context.H_Meets.Where(x => x.FirmaId == FirmaId).ToListAsync();
            var result = _mapper.Map<List<MeetCrudDto>>(Meet);
            return result;
        }

        public async Task<MeetCrudDto> GetById(Guid Id)
        {
            var Meet = await _context.H_Meets.FindAsync(Id);
            var result = _mapper.Map<MeetCrudDto>(Meet);
            return result;
        }

        public async Task<ResultJson> SetActive(Guid Id,bool status)
        {
            var Meet = await _context.H_Meets.FindAsync(Id);
            var mikrotikResult = await _MeetMikrotik.SetDisabled(Meet, status);
            if(!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };         
            Meet.Silindi = status;
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Başarılı", Id = Meet.Id };
        }

        public async Task<ResultJson> Update(MeetCrudDto model)
        {
            var mikrotikResult = await _MeetMikrotik.UpdateUser(model);
            if(!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            var Meet = await _context.H_Meets.FindAsync(model.Id);
            if (Meet == null) return new ResultJson { Success = false, Message = "Personelin kaydı bulunamadı", Id = model.Id };
            Meet.Name = model.Name;
            Meet.Password = model.Password;
            Meet.PasswordExpire = model.PasswordExpire;
            Meet.RateLimit = model.RateLimit;
            Meet.AccessLimit = model.AccessLimit;
            Meet.CompanyName = model.CompanyName;
            Meet.Silindi = model.Silindi;
            Meet.MikrotikId = mikrotikResult.MikrotikId;
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Güncelleme Başarılı", Id = Meet.Id };

        }

        public async Task<ResultJson> Delete(Guid Id)
        {
            var model = await GetById(Id);
            var mikrotikResult = await _MeetMikrotik.DeleteUser(model);
            if (!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            var entityModel = _mapper.Map<Meet>(model);
            _context.H_Meets.Remove(entityModel);
            _context.SaveChanges();
            return new ResultJson { Success = true, Message = "Kayıt Silindi"};
        }
    }
}
