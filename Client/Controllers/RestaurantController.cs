using AutoMapper;
using BuisnessLogic.Dtos;
using BuisnessLogic.Pool;
using Client.Models;
using DBConnection.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Client.Controllers
{
    public class RestaurantController : BaseController
    {
        public RestaurantController(IServicePool _servicePool, UserManager<ApplicationUser> userManager) : base(_servicePool, userManager)
        {
        }
        public IActionResult Index()
        {
            var fooditems = servicePool.restaurantService.GetAllFooditems(includeProperties: "FoodItemType");
            ViewBag.FoodItems = fooditems;
            ViewBag.error =  "no";
            ViewBag.booking = servicePool.bookingService.GetBookingforuser(userManager.GetUserId(HttpContext.User));
            return View();
        }
        [Authorize]
        public IActionResult MyOrders()
        {
            var userid = userManager.GetUserId(HttpContext.User);
            var orders = servicePool.restaurantService.GetAll(x => x.Booking.UserId == userid , includeProperties: "Booking,FoodItem,FoodItem.FoodItemType").AsEnumerable();
            return View(orders);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateOrder([Bind("fooditemsids,fooditemsQuantity,bookingid")] orderViewModel fooditems)
        {
            try
            {
                if(!string.IsNullOrEmpty( fooditems.fooditemsids) && !string.IsNullOrEmpty(fooditems.fooditemsQuantity) && fooditems.fooditemsids.Length == fooditems.fooditemsQuantity.Length)
                {
                    string[] ids = fooditems.fooditemsids.Split(',');
                    string[] Quantities = fooditems.fooditemsQuantity.Split(',');
                    int i = 0;
                    foreach (string id in ids)
                    {
                        OrderDto order = new OrderDto();
                        order.Id = 0;
                        order.BookingId = fooditems.bookingid;
                        order.FoodItemId = int.Parse(id.Trim());
                        order.NumberOfItems = int.Parse(Quantities[i]);
                        servicePool.restaurantService.Create(order);
                        i++;
                    }


                    return RedirectToAction(nameof(MyOrders));
                }
                throw new Exception();
                
            }
            catch
            {
                ViewBag.error = "Not Cool";
                return RedirectToAction(nameof(Index));
            }
           
           
        }


        [Route("restaurant/search")]
        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var searchChoices = new List<string>();
                var foodtypes = new List<string>();
                if (!string.IsNullOrEmpty(term))
                {
                    searchChoices = servicePool.restaurantService.FoodRepo.GetAll(m => m.Name.ToLower().Contains(term.ToLower()) )
                                                                        .Select(m => m.Name)
                                                                        .ToList();
                    foodtypes = servicePool.restaurantService.FoodTypeRepo.GetAll(m => m.Type.ToLower().Contains(term.ToLower()))
                                                                        .Select(m => m.Type)
                                                                        .ToList();
                }
                    

                return Ok(searchChoices.Concat(foodtypes));
            }
            catch
            {
                return BadRequest();
            }
        }

        [Route("restaurant/getFood")]
        [Produces("application/json")]
        public JsonResult getFood(string searchValue)
        {
            var foodItems = servicePool.restaurantService.FoodRepo.GetAll(includeProperties: "FoodItemType");
            int money = 0;
            int.TryParse(searchValue, out money);
            if (!string.IsNullOrEmpty(searchValue))
            {
                foodItems = foodItems.Where(m => m.FoodItemType.Type.ToLower().Contains(searchValue.ToLower())
                                                || m.Name.ToLower().Contains(searchValue.ToLower())
                                                || m.Cost == money).ToList();
            }

            var recordsTotal = foodItems.Count();
            var data = foodItems.ToList();

            string res = string.Empty;
            res = JsonConvert.SerializeObject(data, Formatting.Indented, new JsonSerializerSettings {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }

    }
}
