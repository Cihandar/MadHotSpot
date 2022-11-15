using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using tik4net;
using MadHotSpot.Models.ViewModel;
using tik4net.Objects;
using MadHotSpot.Interfaces;
using MadHotSpot.Models.Enum;

namespace MadHotSpot.Controllers
{
    public class LoginController : Controller
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;
        IStaffCrud _staffCrud;
        IStaffMikrotikCrud _staffMikrotikCrud;
        ILogCrud _logCrud;
        ICustomerInfo _customerInfo;

        public LoginController(OtelAppDbContext _context, UserManager<AppUser> userManager, IStaffCrud staffCrud, IStaffMikrotikCrud staffMikrotikCrud, ILogCrud logCrud, ICustomerInfo customerInfo)
        {
            context = _context;
            _userManager = userManager;
            _staffCrud = staffCrud;
            _staffMikrotikCrud = staffMikrotikCrud;
            _logCrud = logCrud;
            _customerInfo = customerInfo;
        }

        public IActionResult Index(string ClientMac, string ClientIp, string Lokasyon, Guid FirmaId)
        {
            ViewBag.ClientMac = ClientMac;
            ViewBag.ClientIp = ClientIp;
            ViewBag.Lokasyon = Lokasyon;

            var data = context.H_HotSpotAyar.FirstOrDefault(x => x.FirmaId == FirmaId);
            return View(data);
        }

        [HttpPost("LoginCheck")]
        public async Task<bool> LoginCheck(CustomerInfoViewModel customer)
        {
            ResultJson result = await _staffMikrotikCrud.CheckMikrotikUser(customer.UserName, customer.Password, customer.FirmaId);
            if (!result.Success)
                await _logCrud.SendErrorLogAsync(result.Message, "Login", customer.FirmaId, customer.Mac, customer.LocalIp);
            else
                _customerInfo.SendCustomerInfoAsync(customer);
            return result.Success;
        }

        //[HttpPost("LoginStaffCheck")]
        //public async Task<bool> LoginStaffCheck(CustomerInfoViewModel customer)
        //{
        //    ResultJson result = await _staffMikrotikCrud.CheckMikrotikUser(customer.UserName, customer.Password, customer.FirmaId);
        //    if (!result.Success) await _logCrud.SendErrorLogAsync(result.Message, "Login", customer.FirmaId,customer.Mac,customer.LocalIp);
        //    return result.Success;
        //}

        //[HttpPost("LoginMeetCheck")]
        //public async Task<bool> LoginMeetCheck(CustomerInfoViewModel customer)
        //{
        //    ResultJson result = await _staffMikrotikCrud.CheckMikrotikUser(customer.UserName, customer.Password, customer.FirmaId);
        //    if (!result.Success) await _logCrud.SendErrorLogAsync(result.Message, "Login", customer.FirmaId, customer.Mac, customer.LocalIp);
        //    else _customerInfo.SendCustomerInfoAsync(customer, LoginType.Meet);
        //    return result.Success;
        //}

        //[HttpPost("LoginSpaCheck")]
        //public async Task<bool> LoginSpaCheck(CustomerInfoViewModel customer)
        //{
        //    ResultJson result = await _staffMikrotikCrud.CheckMikrotikUser(customer.UserName, customer.Password, customer.FirmaId);
        //    if (!result.Success) await _logCrud.SendErrorLogAsync(result.Message, "Login", customer.FirmaId, customer.Mac, customer.LocalIp);
        //    else _customerInfo.SendCustomerInfoAsync(customer, LoginType.Spa);
        //    return result.Success;
        //}

        //[HttpPost("LoginDailyCheck")]
        //public async Task<bool> LoginSpaCheck(CustomerInfoViewModel customer)
        //{
        //    ResultJson result = await _staffMikrotikCrud.CheckMikrotikUser(customer.UserName, customer.Password, customer.FirmaId);
        //    if (!result.Success) await _logCrud.SendErrorLogAsync(result.Message, "Login", customer.FirmaId, customer.Mac, customer.LocalIp);
        //    else _customerInfo.SendCustomerInfoAsync(customer, LoginType.Spa);
        //    return result.Success;
        //}


    }
}
