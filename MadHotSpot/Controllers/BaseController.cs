﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using Microsoft.AspNetCore.Authorization;
using tik4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace MadHotSpot.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        public Guid FirmaId { get; set; }
        public bool admin { get; set; }
        public override void OnActionExecuting(ActionExecutingContext context)
        {

            if (context.HttpContext.User.Identity.IsAuthenticated)
            {
                var ssee =  HttpContext.RequestServices.GetService<UserManager<AppUser>>();
                var _userManager = context.HttpContext.RequestServices.GetService<UserManager<AppUser>>();
                var user = _userManager.FindByEmailAsync(context.HttpContext.User.Identity.Name).Result;
               
                if (user == null)
                {
                    user = _userManager.FindByNameAsync(context.HttpContext.User.Identity.Name).Result;

                    if(user!=null)
                    {
                        FirmaId = user.FirmaId;
                        ViewBag.Admin = user.admin;
                        admin = user.admin;
                        return;
                    }

                    context.Result = new RedirectToRouteResult("Home");
                    return;
                }
                else
                {
                    FirmaId = user.FirmaId;
                    ViewBag.PermissionEnum = user.Yetki;
                    ViewBag.Admin = user.admin;
                    admin = user.admin;
                    //_onlineUser.NameSurname = user.Name;
                    //_onlineUser.ProfilePicture = user.AvatarUrl;
                    //_onlineUser.RestaurantId = user.RestorantId;
                }
            }
        }

    }
}
