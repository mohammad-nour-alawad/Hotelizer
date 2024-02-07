using BuisnessLogic.Dtos;
using BuisnessLogic.Pool;
using BuisnessLogic.ViewModels;
using Client.Models;
using DBConnection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, IServicePool _servicePool, UserManager<ApplicationUser> userManager)
            : base(_servicePool, userManager)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            ViewBag.rooms = servicePool.bookingService.GetMostBooked();
            ViewBag.users = servicePool.bookingService.GetTopUsers();

            var services = servicePool.roomService.GetServices();

            List<KeyValuePair<int, string>> services_pairs = new List<KeyValuePair<int, string>>();
            foreach (var ser in services)
            {
                services_pairs.Add(new KeyValuePair<int, string>(ser.Id, ser.Name));
            }
            ViewBag.services = services_pairs;

            return View();
        }

        public IActionResult About()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }



        [Route("home/getservices")]
        [Produces("application/json")]
        public JsonResult getServices()
        {
            var dataset = servicePool.bookingService.GetMostRequestedServices();

            string res = string.Empty;
            res = JsonConvert.SerializeObject(dataset, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });
            return Json(res);
        }

        [Route("home/getservicetopusers")]
        [Produces("application/json")]
        public JsonResult GetServiceTopUsers(int id)
        {
            var users = servicePool.bookingService.GetTopUsersForService(id);

            string res = string.Empty;
            res = JsonConvert.SerializeObject(users, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }
    }
}
