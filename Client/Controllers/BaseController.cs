using BuisnessLogic.Pool;
using DBConnection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class BaseController : Controller
    {

        protected IServicePool servicePool;

        protected readonly UserManager<ApplicationUser> userManager;

        public BaseController(IServicePool _servicePool, UserManager<ApplicationUser> userManager)
        {
            servicePool = _servicePool;
            this.userManager = userManager;
        }
        public bool IsAuthenticated
        {
            get
            {
                return HttpContext.User.Identity.IsAuthenticated;
            }
        }
    }

}
