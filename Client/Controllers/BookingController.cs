using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BuisnessLogic.Dtos;
using BuisnessLogic.Pool;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using Client.Models;
using DBConnection.Models;
using Microsoft.AspNetCore.Identity;
using System.Net;

namespace Client.Controllers
{
    public class BookingController : BaseController
    {
        private readonly IServicePool _servicePool;

        public BookingController(IServicePool servicePool, UserManager<ApplicationUser>userManager) : base(servicePool, userManager) => _servicePool = servicePool;
        


        public IActionResult reservation(BookViewModel booking = null)
        {            
            var books = servicePool.bookingService.GetAll();
            ViewBag.books = books;
            
            var category = servicePool.roomService.GetAllCategories();
            List<KeyValuePair<int, string>> category_pair = new List<KeyValuePair<int, string>>();
            foreach (var cat in category)
            {
                category_pair.Add(new KeyValuePair<int, string>(cat.Id, cat.Name));
            }
            ViewBag.catIds = category_pair;

            var specification = servicePool.roomService.GetAllSpecifications();
            List<KeyValuePair<int, string>> specification_pair = new List<KeyValuePair<int, string>>();
            foreach (var spe in specification)
            {
                specification_pair.Add(new KeyValuePair<int, string>(spe.Id, spe.Type));
            }
            ViewBag.speIds = specification_pair;


            if (booking == null)
                return View();
            else
            {
                return View(booking);
            }
                
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create( BookViewModel booking )
        {
            if (ModelState.IsValid)
            {
                var sss = servicePool.roomService.getRoomDetails(booking.RoomId);
                float sum = sss.Room.Category.BasePrice;
                if (booking.services != null)
                {
                    string[] servicesIds = booking.services.Split(',');
                    foreach (string ser in servicesIds)
                    {
                        int sId = 0;
                        int.TryParse(ser, out sId);
                        int cId = sss.Room.CategoryId;
                        ServiceCategoryDto sc = servicePool.bookingService.GetServiceCategory(sId, cId);
                        sum += sc.Cost;
                    }
                }
                BookingDto book = new BookingDto()
                {
                    FromDate = booking.FromDate,
                    RoomId = booking.RoomId,
                    Status = "Pending",
                    ToDate = booking.ToDate,
                    Id = 0,
                    Bill = sum,
                    UserId = userManager.GetUserId(HttpContext.User)
                };
                var newbook = servicePool.bookingService.Create(book);

                if (booking.services != null)
                {
                    string[] servicesIds = booking.services.Split(',');

                    foreach (string ser in servicesIds)
                    {
                        int sId = 0;
                        int.TryParse(ser, out sId);
                        int cId = sss.Room.CategoryId;
                        ServiceCategoryDto sc = servicePool.bookingService.GetServiceCategory(sId, cId);
                        int scid = sc.Id;
                        var scb = new ServiceCategoryBookingDto()
                        {
                            BookingId = newbook.Id,
                            ServiceCategoryId = scid
                        };
                        servicePool.bookingService.CreateServiceCategoryBooking(scb);
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return RedirectToAction(nameof(reservation), booking);
        }



        public IActionResult Index()
        {

            var userid = userManager.GetUserId(HttpContext.User);
            var all = servicePool.bookingService.GetAllServiceCategory(x => x.Booking.UserId == userid, includeProperties: "Booking,ServiceCategory,ServiceCategory.Service,Booking.ApplicationUser,Booking.Room").ToList();
            Dictionary<int, ServiceCategoryBookingDto> scb = new Dictionary<int, ServiceCategoryBookingDto>();
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

        public ActionResult Edit(int id)
        {
            var services = servicePool.bookingService.GetAllServiceCategory(x => x.BookingId == id, includeProperties: "Booking,ServiceCategory,ServiceCategory.Service,Booking.ApplicationUser,Booking.Room").ToList();
            ViewBag.oldServices = services;

            var idCat = services[0].ServiceCategory.CategoryId;
            var allServices = servicePool.bookingService.GetAllServiceCategoryForCatId(idCat).ToList();

            List<ServiceCategoryDto> rest = new List<ServiceCategoryDto>(allServices);

            foreach(var s in allServices)
            {
                foreach(var ss in services)
                {
                    if (s.ServiceId == ss.ServiceCategory.ServiceId)
                    {
                        rest.Remove(s);
                        break;
                    }
                }
            }

            ViewBag.newServices = rest;

            var book = servicePool.bookingService.GetAllBookings(x => x.Id == id, includeProperties: "Room,ApplicationUser").ToList()[0];//
            ViewBag.book = book;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(ServicesViewModel svm)
        {
            if (!string.IsNullOrEmpty(svm.tocreate))
            {
                var sc = svm.tocreate.Split(',');
                foreach(var s in sc)
                {
                    int id = 0;
                    int.TryParse(s, out id);
                    if(id != 0)
                    {
                        var scb = new ServiceCategoryBookingDto()
                        {
                            BookingId = svm.bookid,
                            ServiceCategoryId = id
                        };
                        servicePool.bookingService.CreateServiceCategoryBooking(scb);
                    }
                    
                }
            }
            if (!string.IsNullOrEmpty(svm.todelete))
            {
                var sc = svm.todelete.Split(',');
                foreach (var s in sc)
                {
                    int id = 0;
                    int.TryParse(s, out id);
                    if (id != 0)
                    {
                        servicePool.bookingService.DeleteServiceCategoryBooking(id);
                    }
                }
            }
            
            servicePool.bookingService.UpdateBill(svm.bookid);

            return RedirectToAction("Index");
        }


        [Route("booking/getrooms")]
        [Produces("application/json")]
        public JsonResult GetRooms(int cat_id , int spec_id , DateTime From , DateTime To)
        {
            var roomsData = servicePool.roomService.AvailablePeriod(From, To , cat_id , spec_id);
            var data = roomsData.ToList();

            string res = string.Empty;
            res = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

        [Route("booking/getroomDetails")]
        [Produces("application/json")]
        public JsonResult GetRoomDetails(string roomid)
        {
            int id = 0;
            int.TryParse(roomid , out id);
            if( id != 0)
            {
                var roomsData = servicePool.roomService.getRoomDetails(id);

                string res = string.Empty;
                res = JsonConvert.SerializeObject(roomsData, Formatting.Indented, new JsonSerializerSettings
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });

                return Json(res);
            }
            else
            {
                return new JsonResult("")
                {
                    StatusCode = (int)HttpStatusCode.InternalServerError
                };
            }
            
        }

        [Route("booking/getServiceCategory")]
        [Produces("application/json")]
        public JsonResult GetServiceCategory(string cat_id)
        {
            int id = 0;
            int.TryParse(cat_id, out id);
            var services = servicePool.roomService.GetAllServices( x=> x.CategoryId == id );

            string res = string.Empty;
            res = JsonConvert.SerializeObject(services, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }
    }
}
