using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using MadHotSpot.Models;
using tik4net;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;

namespace MadHotSpot.Controllers
{

    public class BaseController : Controller
    {

        public Guid FirmaId { get; set; }
        public Guid UserId { get; set; }

        public override void OnActionExecuting(ActionExecutingContext context)
        {

            //Guid guidid;

            //if (Guid.TryParse(context.HttpContext.Session.GetString("FirmaId"),out guidid))
            //{
            //    FirmaId = guidid;
            //   if( Guid.TryParse(context.HttpContext.Session.GetString("UserId"), out guidid))
            //    {
            //        UserId = guidid;
            //    }


            //} 
             


          
        }

    }

 
}
