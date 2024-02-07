using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;
using Hotelizer.Models;

namespace Hotelizer.Controllers
{
    public class ReviewBookingsController : BaseController
    {

        public ReviewBookingsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        [Authorize]
        public ViewResult Index()
        {
            var booking = UnitOfWork.BookingRepo.GetAll(x => x.Status.ToLower() == "pending", includeProperties: "Room,ApplicationUser");
            return View(booking.ToList());
        }


        //_________________________________________________________________________________________
        [Route("reviewbookings/bookingsFilters")]
        [Produces("application/json")]
        public async Task<JsonResult> bookingsFilters(DateTime From, DateTime To)
        {
            List<Booking> booking = UnitOfWork.BookingRepo.GetAll(includeProperties: "Room,ApplicationUser").ToList();
            List<Booking> s = new List<Booking>();
            foreach (var b in booking)
            {
                DateTime fromP = b.FromDate;
                DateTime toP = b.ToDate;
                if (From <= fromP && To >= toP)
                {
                    s.Add(b);
                }
            }

            var data = s;

            string res = string.Empty;
            res = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
            
        }
        //_________________________________________________________________________________________

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var booking = UnitOfWork.BookingRepo.GetById(id);
            if (booking == null)
            {
                return NotFound();
            }
            return View(booking);
        }

        public IActionResult ReviewServices()
        {
            var all = UnitOfWork.ServiceCategoryBookingRepo.GetAll(x => x.Booking.Status.ToLower() == "accepted", includeProperties: "Booking,ServiceCategory,ServiceCategory.Service,Booking.ApplicationUser,Booking.Room").ToList();
            Dictionary<int, ServiceCategoryBooking> scb = new Dictionary<int, ServiceCategoryBooking>();
            for (int i = 0; i < all.Count; i++)
            {
                var book = all[i];
                if (!scb.ContainsKey(book.BookingId))
                {
                    scb.Add(book.BookingId, book);
                }
                else
                {
                    scb[book.BookingId].ServiceCategory.Service.Name = scb[book.BookingId].ServiceCategory.Service.Name + "," + book.ServiceCategory.Service.Name;
                }
            }
            return View(scb.Values.ToList());
        }


        [Authorize]
        public IActionResult Edit(int id)
        {
            var services = UnitOfWork.ServiceCategoryBookingRepo.GetAll(x => x.BookingId == id, includeProperties: "Booking,ServiceCategory,ServiceCategory.Service,Booking.ApplicationUser,Booking.Room").ToList();
            ViewBag.services = services;
            var book = UnitOfWork.BookingRepo.GetAll(x => x.Id == id, includeProperties: "Room,ApplicationUser").ToList()[0];//
            ViewBag.book = book;
            return View();
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public IActionResult Edit( ServicesVM ser)
        {
            if (ModelState.IsValid)
            {
                var tem = ser.services;
                if (tem != null)
                {
                    if (tem.Contains(','))
                    {
                       var str = ser.services.Split(',');
                        foreach (var st in str)
                        {
                            int id = 0;
                            int.TryParse(st, out id);
                            if (id == 0)
                            {
                                continue;
                            }
                            var services = UnitOfWork.ServiceCategoryBookingRepo.GetAll(x => x.BookingId == ser.BookId && x.ServiceCategory.ServiceId == id, includeProperties: "ServiceCategory").ToList()[0];
                            services.isConsumed = true;
                            UnitOfWork.ServiceCategoryBookingRepo.Update(services);
                        }
                        UnitOfWork.Save();

                        return RedirectToAction(nameof(ReviewServices));

                    }
                    else
                    {
                        var str = ser.services.ToString();
                        int id = 0;
                            int.TryParse(str, out id);
                            
                            var services = UnitOfWork.ServiceCategoryBookingRepo.GetAll(x => x.BookingId == ser.BookId && x.ServiceCategory.ServiceId == id, includeProperties: "ServiceCategory").ToList()[0];
                            services.isConsumed = true;
                            UnitOfWork.ServiceCategoryBookingRepo.Update(services);
                        
                        UnitOfWork.Save();

                        return RedirectToAction(nameof(ReviewServices)); 

                    }
                }
            }
            return RedirectToAction(nameof(Edit),ser.BookId);
        }

     
        public async Task AvailablePeriod( int bookingID)
        {
            var book = UnitOfWork.BookingRepo.GetById(bookingID);
            List<Booking> allBookings = UnitOfWork.BookingRepo.GetAll(x=>  x.Status.ToLower() == "pending"  && x.RoomId == book.RoomId && x.Id != bookingID, includeProperties: "Room,ApplicationUser").ToList();
            DateTime start = book.FromDate;
            DateTime end = book.ToDate;

            if (allBookings.Count != 0)
            { 
                foreach (var bo in allBookings)
                {

                    DateTime fromP = bo.FromDate;
                    DateTime toP = bo.ToDate;

                    if ((start >= fromP && start <= toP) || (end >= fromP && end <= toP)||(fromP >= start && toP <=end))
                    {
                        bo.Status = "Rejected";
                        UnitOfWork.BookingRepo.Update(bo);
                        UnitOfWork.Save();
                    }
                }
            }
            book.Status = "Accepted";
            UnitOfWork.BookingRepo.Update(book);
            await UnitOfWork.SaveAsync();

            return ;
        }

        public async Task reject(int bookingID)
        {
            var book = UnitOfWork.BookingRepo.GetById(bookingID);
            book.Status = "Rejected";
            UnitOfWork.BookingRepo.Update(book);
            await UnitOfWork.SaveAsync();

        }


        //______________________________________________

        [Route("reviewbookings/getBookings")]
        [Produces("application/json")]
        public async Task<JsonResult> GetBookings( int bookingID)
        {
            
            await AvailablePeriod(bookingID);
         

            var all = UnitOfWork.BookingRepo.GetAll(x => x.Status.ToLower() == "pending" , includeProperties: "Room,ApplicationUser").ToList();
    
            var data = all.ToList();

            string res = string.Empty;
            res = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

        [Route("reviewbookings/reject")]
        [Produces("application/json")]
        public async Task<JsonResult> Reject(int bookingID)
        {

            await reject(bookingID);


            var all = UnitOfWork.BookingRepo.GetAll(x => x.Status.ToLower() == "pending", includeProperties: "Room,ApplicationUser").ToList();

            var data = all.ToList();

            string res = string.Empty;
            res = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

    }


}
