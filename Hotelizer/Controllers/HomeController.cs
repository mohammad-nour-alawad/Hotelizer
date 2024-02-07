using DBConnection.Models;
using DBConnection.UnitOfWork;
using Hotelizer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotelizer.Controllers
{
    public class HomeController : BaseController
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
            _logger = logger;
        }

        //change language


        public IActionResult Index()
        {
            ViewBag.totalRooms = UnitOfWork.RoomRepo.GetTotalCounts();
            ViewBag.totalUsers = UnitOfWork.UserRepo.GetTotalCounts();
            ViewBag.totalServices = UnitOfWork.ServiceRepo.GetTotalCounts();
            ViewBag.totalCategories = UnitOfWork.CategoryRepo.GetTotalCounts();

            return View();
        }

        [Route("home/getincomes")]
        [Produces("application/json")]
        public JsonResult getIncomes()
        {
            float[] dataset = new float[12];
            var bookings = UnitOfWork.BookingRepo.GetAll();
            foreach (var book in bookings)
            {
                dataset[book.FromDate.Month - 1] += book.Bill;
            }
            string res = string.Empty;
            res = JsonConvert.SerializeObject(dataset, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

        [Route("home/getbookings")]
        [Produces("application/json")]
        public JsonResult getBookings()
        {
            int[] dataset = new int[12];
            var bookings = UnitOfWork.BookingRepo.GetAll();
            foreach (var book in bookings)
            {
                dataset[book.FromDate.Month - 1]++;
            }
            string res = string.Empty;
            res = JsonConvert.SerializeObject(dataset, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
