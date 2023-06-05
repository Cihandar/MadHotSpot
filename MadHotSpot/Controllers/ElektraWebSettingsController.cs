using MadHotSpot.Dtos;
using MadHotSpot.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MadHotSpot.Controllers
{
    public class ElektraWebSettingsController : BaseController
    {

        IElektraWebSettingsCrud _ElektraWebSettingsCrud;

        public ElektraWebSettingsController(IElektraWebSettingsCrud MeetCrud)
        {
            _ElektraWebSettingsCrud = MeetCrud;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ElektraWebSettingsDto model)
        {
            model.FirmaId = FirmaId;
            var result = await _ElektraWebSettingsCrud.Add(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = new ElektraWebSettingsDto();
            return PartialView("_FormPartial", model);
        }

        [HttpPost]
        public async Task<IActionResult> Update(ElektraWebSettingsDto model)
        {
            var result = await _ElektraWebSettingsCrud.Update(model);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> Update(Guid Id)
        {
            var result = await _ElektraWebSettingsCrud.GetById(Id);
            return PartialView("_FormPartial", result);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid Id)
        {
            var result = await _ElektraWebSettingsCrud.Delete(Id);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var result = await _ElektraWebSettingsCrud.GetAll(FirmaId);
            return Json(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetById(Guid Id)
        {
            var result = await _ElektraWebSettingsCrud.GetById(Id);
            return Json(result);
        }

        public async Task<IActionResult> SetActive(Guid Id, bool status)
        {
            var result = await _ElektraWebSettingsCrud.SetActive(Id, status);
            return Json(result);
        }
    }
}
