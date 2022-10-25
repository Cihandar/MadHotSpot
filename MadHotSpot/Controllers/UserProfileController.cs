using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using tik4net.Objects.Ip.Hotspot;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using MadHotSpot.Interfaces;
using MadHotSpot.Dtos;
using MadHotSpot.Models.ViewModel;

namespace MadHotSpot.Controllers
{
    public class UserProfileController : BaseController
    {

        IMikrotikUserProfile _userProfile;

        public UserProfileController(IMikrotikUserProfile userProfile)
        {
            _userProfile = userProfile;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HotspotUserProfile model)
        {       
            var result = await _userProfile.Add(model,FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return PartialView("_FormPartial",null);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ResultWithObject<HotspotUserProfileDto> model)
        {
            var result = await _userProfile.Update(model.Data,FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(string Id)
        {
            var result = await _userProfile.GetById(Id,FirmaId);             
            return PartialView("_FormPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(string Id)
        {
            var result = await _userProfile.Delete(Id,FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _userProfile.GetAll(FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(string Id)
        {
            var result = await _userProfile.GetById(Id,FirmaId);
            return Json(result);
        }
 
    }
}
