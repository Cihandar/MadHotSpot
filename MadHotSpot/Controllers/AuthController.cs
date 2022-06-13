using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MadHotSpot.Models;
using MadHotSpot.Models.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using tik4net.Objects.User;
using MadHotSpot.Extentions;
namespace MadHotSpot.Controllers
{
    [AllowAnonymous]
    [Route("Auth")]
    public class AuthController : Controller
    {
        private readonly SignInManager<AppUser> _signInManager;
        private readonly UserManager<AppUser> _userManager;
        private readonly OtelAppDbContext _context;

        public AuthController(SignInManager<AppUser> signInManager, UserManager<AppUser> userManager , OtelAppDbContext context)
        {
            _signInManager = signInManager;
            _userManager = userManager;
            _context = context;
        }

        [Route("Login")]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("Login")]
        public async Task<ActionResult> Login(LoginViewModel request)
        {
            var ajaxResult = new Response();
            try
            {
                var user = _userManager.Users.FirstOrDefault(x => request.UserName == x.UserName);
                if (user == null)
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Girmiş olduğunuz email adresi ile sistemde kayıtlı kullanıcı bulunamadı.";
                    return Ok(ajaxResult);
                }

                var firma = _context.H_Firmalar.FirstOrDefault(x => x.Id  == user.FirmaId);
                if(firma.BitisTarihi<DateTime.Now)
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Lisans Süreniz Bitmiştir. Lütfen Lisansınızı Yenileyin. ";
                    return Ok(ajaxResult);
                }

                if (!firma.Aktif)
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Lisansınız Pasifleştirilmiştir.. Lütfen Bizimle İletişime Geçin..  ";
                    return Ok(ajaxResult);
                }            

                var result = await _signInManager.PasswordSignInAsync(user, request.Password, request.RememberMe, false);
                if (result.Succeeded)
                {
                    ajaxResult.Result = user;
                }
                else
                {
                    ajaxResult.Success = false;
                    ajaxResult.Message = "Şifreniz Yanlış..";
                }

                return Ok(ajaxResult);
            }
            catch (Exception e)
            {
                ajaxResult.ResultCode = ResultCode.ExceptionError;
            }

            return Ok(ajaxResult);
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(Firmalar request)
        {
            var retVal = new Response();

            try
            {
                var isUser = _userManager.Users.FirstOrDefault(x => request.Email == x.UserName);
                if(isUser != null)
                {
                    retVal.Success = false;
                    retVal.Message = "Bu email adresi kullanımda. lütfen giriş yapın veya başka bir mail adresi ile kayıt olun.";
                    return Ok(retVal);
                }

                request.BaslamaTarihi = DateTime.Now;
                request.BitisTarihi = DateTime.Now.AddMonths(1);
                request.Aktif = true;

                request.FirmaKodu= new Random().Next(1000, 9999);
                _context.H_Firmalar.Add(request);
                _context.SaveChanges();


                var UserAddSnc = new Response();
                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.Email,
                    PhoneNumber = request.Telefon,
                    FirmaId=request.Id
                };

                var result = await _userManager.CreateAsync(user, request.Password);
                if (!result.Succeeded)
                {
                    return Ok(new Response { Success = false, Message = result.Errors.Select(x => x.Description).FirstOrDefault() });
                }

                if (UserAddSnc.Success)
                {

                    _context.H_Ayarlar.Add(new Ayarlar { FirmaId = request.Id, GunlukFiyatTL = 10, GunlukFiyatEURO = 2, GunlukFiyatUSD = 3, SinirsizAktif=false, AdSoyadZorunlu=false });
                    _context.SaveChanges();
 
                    SendEmail mail = new SendEmail();
                    mail.Send(request.Email,"Online Hotspot Giriş Bilgileri","", request.Email, request.FirmaKodu.ToString(), request.Password, "IlkKayit");
                }

                retVal.Message = "Kaydınız Yapıldı. Yönlendiriliyorsunuz.";
            }
            catch (Exception ex)
            {
                retVal.ResultCode = ResultCode.ExceptionError;
            }

            return Ok(retVal);
        }

        [Route("Logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return View("index");
        }
    }
}
