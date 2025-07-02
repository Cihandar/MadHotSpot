using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MadHotSpot.Controllers
{
    public class VisitorController : BaseController
    {

        IVisitorCrud _staffCrud;
        IMikrotikUserProfile _userProfile;

        public VisitorController(IVisitorCrud staffCrud, IMikrotikUserProfile userProfile)
        {
            _staffCrud = staffCrud;
            _userProfile = userProfile;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(VisitorCrudDto model)
        {
            model.FirmaId = FirmaId;
            var result = await _staffCrud.Add(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new VisitorCrudDto();
            return PartialView("_FormPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(VisitorCrudDto model)
        {
            var result = await _staffCrud.Update(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var listUserprofile = await _userProfile.GetAll(FirmaId);
            var result = await _staffCrud.GetById(Id);
            return PartialView("_FormPartial", result);
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

        public async Task<IActionResult> SetActive(Guid Id, bool status)
        {
            var result = await _staffCrud.SetActive(Id, status);
            return Json(result);
        }
    }
}
