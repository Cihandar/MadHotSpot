using AutoMapper;
using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using MadHotSpot.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MadHotSpot.Applications.Staffs
{
    public class StaffCrud : IStaffCrud
    {

        OtelAppDbContext _context;
        IMapper _mapper;
        IStaffMikrotikCrud _staffMikrotik;

        public StaffCrud(IMapper mapper, OtelAppDbContext context)
        {
            _mapper = mapper;
            _context = context;
        }

        public async Task<ResultJson> Add(StaffCrudDto model)
        {
            var mikrotikresult = await  _staffMikrotik.AddUser(model);
            if (!mikrotikresult.Success) return new ResultJson { Success = false, Message = mikrotikresult.Message };
            var data = _mapper.Map<Staff>(model);
            data.MikrotikId = mikrotikresult.MikrotikId;
            await _context.H_Staffs.AddAsync(data);
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Personel Kaydı Yapıldı", Id = data.Id };
        }

        public async Task<List<StaffCrudDto>> GetAll(Guid FirmaId)
        {
            var staff = await _context.H_Staffs.Where(x => x.FirmaId == FirmaId).ToListAsync();
            var result = _mapper.Map<List<StaffCrudDto>>(staff);
            return result;
        }

        public async Task<StaffCrudDto> GetById(Guid Id)
        {
            var staff = await _context.H_Staffs.FindAsync(Id);
            var result = _mapper.Map<StaffCrudDto>(staff);
            return result;
        }

        public async Task<ResultJson> SetActive(Guid Id,string mikrotikId, bool status,Guid FirmaId)
        {
            var mikrotikResult = await _staffMikrotik.SetDisabled(FirmaId,mikrotikId, status);
            if(!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };
            var staff = await _context.H_Staffs.FindAsync(Id);
            staff.Active = status;
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Başarılı", Id = staff.Id };
        }

        public async Task<ResultJson> Update(StaffCrudDto model)
        {
            var mikrotikResult = await _staffMikrotik.UpdateUser(model);
            if(!mikrotikResult.Success) return new ResultJson { Success = false, Message = mikrotikResult.Message };

            var staff = await _context.H_Staffs.FindAsync(model.Id);
            if (staff == null) return new ResultJson { Success = false, Message = "Personelin kaydı bulunamadı", Id = model.Id };
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
            await _context.SaveChangesAsync();
            return new ResultJson { Success = true, Message = "Güncelleme Başarılı", Id = staff.Id };

        }
    }
}
