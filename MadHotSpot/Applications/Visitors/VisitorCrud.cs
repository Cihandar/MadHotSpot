
using AutoMapper;
using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace MadHotSpot.Applications.Visitors
{
    public class VisitorCrud : IVisitorCrud
    {

        OtelAppDbContext _context;
        IMapper _mapper;
        IVisitorMikrotikCrud _visitorMikrotik;

        public VisitorCrud(IMapper mapper, OtelAppDbContext context, IVisitorMikrotikCrud visitorMikrotik)
        {
            _mapper = mapper;
            _context = context;
            _visitorMikrotik = visitorMikrotik;
        }

        public async Task<ResultJson> Add(VisitorCrudDto model)
        {
            var mikrotikresult = await _visitorMikrotik.AddUser(model);
            if (!mikrotikresult.Success) return new ResultJson { Success = false, Message = mikrotikresult.Message };
            var data = _mapper.Map<Visitor>(model);
            data.MikrotikId = mikrotikresult.MikrotikId;
            await _context.H_Visitors.AddAsync(data);
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Misafir Kaydı Yapıldı", Id = data.Id };
        }

        public async Task<List<VisitorCrudDto>> GetAll(Guid FirmaId)
        {
            var staff = await _context.H_Visitors.Where(x => x.FirmaId == FirmaId).ToListAsync();
            var result = _mapper.Map<List<VisitorCrudDto>>(staff);
            return result;
        }

        public async Task<VisitorCrudDto> GetById(Guid Id)
        {
            var staff = await _context.H_Visitors.FindAsync(Id);
            var result = _mapper.Map<VisitorCrudDto>(staff);
            return result;
        }

        public async Task<ResultJson> SetActive(Guid Id, bool status)
        {
            var staff = await _context.H_Visitors.FindAsync(Id);
            var mikrotikResult = await _visitorMikrotik.SetDisabled(staff, status);
            if (!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            staff.Active = status;
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Başarılı", Id = staff.Id };
        }

        public async Task<ResultJson> Update(VisitorCrudDto model)
        {
            var mikrotikResult = await _visitorMikrotik.UpdateUser(model);
            if (!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            var staff = await _context.H_Visitors.FindAsync(model.Id);
            if (staff == null) return new ResultJson { Success = false, Message = "Misafirin kaydı bulunamadı", Id = model.Id };
            staff.IdNumber = model.IdNumber;
            staff.Month = model.Month;
            staff.Name = model.Name;
            staff.Surname = model.Surname;
            staff.PhoneNumber = model.PhoneNumber;
            staff.Email = model.Email;
            staff.Day = model.Day;
            staff.Year = model.Year;
            staff.UserProfile = model.UserProfile;
            staff.Active = model.Active;
            staff.MikrotikId = mikrotikResult.MikrotikId;
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Güncelleme Başarılı", Id = staff.Id };

        }

        public async Task<ResultJson> Delete(Guid Id)
        {
            var model = await GetById(Id);
            var mikrotikResult = await _visitorMikrotik.DeleteUser(model);
            if (!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            var entityModel = _mapper.Map<Visitor>(model);
            _context.H_Visitors.Remove(entityModel);
            _context.SaveChanges();
            return new ResultJson { Success = true, Message = "Kayıt Silindi" };
        }
    }
}
