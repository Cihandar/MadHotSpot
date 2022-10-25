using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MadHotSpot.Interfaces;
using MadHotSpot.Dtos;

namespace MadHotSpot.Controllers
{
    public class StaffController : BaseController
    {

        IStaffCrud _staffCrud;
        IMikrotikUserProfile _userProfile;

        public StaffController(IStaffCrud staffCrud, IMikrotikUserProfile userProfile)
        {
            _staffCrud = staffCrud;
            _userProfile = userProfile;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(StaffCrudDto model)
        {
            model.FirmaId = FirmaId;
            var result = await _staffCrud.Add(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var listUserprofile = await _userProfile.GetAll(FirmaId);
            return PartialView("_FormPartial",new StaffCrudDto { HotspotUserProfilList = listUserprofile.ListData,Active=true });
        }

        [HttpPost]
        public async Task<IActionResult> Update(StaffCrudDto model)
        {
            var result = await _staffCrud.Update(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var listUserprofile = await _userProfile.GetAll(FirmaId);
            var result = await _staffCrud.GetById(Id);
            result.HotspotUserProfilList = listUserprofile.ListData;
            return PartialView("_FormPartial",result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _staffCrud.Delete(Id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _staffCrud.GetAll(FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _staffCrud.GetById(Id);
            return Json(result);
        }

        public async Task<IActionResult> SetActive(Guid Id,bool status)
        {
            var result = await _staffCrud.SetActive(Id, status);
            return Json(result);
        }
    }
}
