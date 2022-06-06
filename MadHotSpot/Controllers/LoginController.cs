using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Identity;
using tik4net;

namespace MadHotSpot.Controllers
{
    public class LoginController : Controller
    {

        public OtelAppDbContext context;
        private readonly UserManager<AppUser> _userManager;

        public LoginController(OtelAppDbContext _context, UserManager<AppUser> userManager)
        {
            context = _context;
            _userManager = userManager;
        }

        public IActionResult Index()
        { 
            return View(); 
        }
    }
}
