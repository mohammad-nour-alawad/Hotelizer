using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotelizer.Controllers
{
    [Authorize(Roles = "Admin, SuperAdmin")]
    public class BaseController : Controller
    {
        protected readonly IUnitOfWork UnitOfWork;
        protected readonly IConfiguration Configuration;
        protected readonly UserManager<ApplicationUser> userManager;
        protected readonly IWebHostEnvironment hostEnvironment;
        
        public BaseController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment)
        {
            hostEnvironment = _hostEnvironment;
            Configuration = configuration;
            UnitOfWork = unitOfWork;
            this.userManager = userManager;
        }
        public IActionResult ChangeLanguage(string culture)
        {
            if (culture == "en-US")
            {
                TempData["lang"] = "ltr";
            }
            else
            {
                TempData["lang"] = "rtl";
            }
            Response.Cookies.Append(CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions() { Expires = DateTimeOffset.UtcNow.AddYears(1) });

            return Redirect(Request.Headers["Referer"].ToString());
        }
    }
}
