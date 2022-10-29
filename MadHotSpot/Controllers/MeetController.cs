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
    public class MeetController : BaseController
    {

        IMeetCrud _MeetCrud;
        IMikrotikUserProfile _userProfile;

        public MeetController(IMeetCrud MeetCrud, IMikrotikUserProfile userProfile)
        {
            _MeetCrud = MeetCrud;
            _userProfile = userProfile;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(MeetCrudDto model)
        {
            model.FirmaId = FirmaId;
            var result = await _MeetCrud.Add(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new MeetCrudDto();
            model.PasswordExpire = DateTime.Now.AddDays(1);
            return PartialView("_FormPartial",model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(MeetCrudDto model)
        {
            var result = await _MeetCrud.Update(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var listUserprofile = await _userProfile.GetAll(FirmaId);
            var result = await _MeetCrud.GetById(Id);
            return PartialView("_FormPartial",result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _MeetCrud.Delete(Id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _MeetCrud.GetAll(FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _MeetCrud.GetById(Id);
            return Json(result);
        }

        public async Task<IActionResult> SetActive(Guid Id,bool status)
        {
            var result = await _MeetCrud.SetActive(Id, status);
            return Json(result);
        }
    }
}
