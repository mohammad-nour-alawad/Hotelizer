using BuisnessLogic.Dtos;
using BuisnessLogic.Pool;
using DBConnection.Models;
using Client.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Diagnostics;

namespace Client.Controllers
{
    public class RoomController : BaseController
    {
        public RoomController(IServicePool _servicePool, UserManager<ApplicationUser> userManager) : base(_servicePool, userManager)
        {

        }
        public IActionResult Index()
        {
            List<RoomSpecificationDto> roomspecifications = servicePool.roomService.GetAllRooms();

            ViewBag.rooms = roomspecifications;
            return View();
        }

        public ViewResult Details(int id)
        {
            try
            {
                RoomSpecificationDto room = servicePool.roomService.getRoomDetails(id);

                ViewBag.specID = room.Spec.Id;
                ViewBag.catID = room.Room.CategoryId;
                List<RoomSpecificationDto> Similar_rooms = servicePool.roomService.getSimilarRooms(room);
                ViewBag.room = room;
                ViewBag.similar_rooms = Similar_rooms;
                return View();
            }
            catch
            {
                return View( "Error", new ErrorViewModel { RequestId = "error 404"});
            }
            
        }

        [Route ("room/search")]
        [Produces("application/json")]
        [HttpGet("search")]
        public IActionResult Search()
        {
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                var searchChoices = new List<string>();
                var res = new List<SearchViewModel>();
                if (!string.IsNullOrEmpty(term))
                {
                    searchChoices = servicePool.roomService.GetAllSpecifications(m => m.Type.Contains(term)).Select(m => m.Type).ToList();
                    foreach( string spec in searchChoices)
                    {
                        res.Add(new SearchViewModel(spec, "specifications"));
                    }

                    var cats = servicePool.roomService.GetAllCategories(m => m.Name.Contains(term)).Select(m => m.Name).ToList();
                    foreach (string cat in cats)
                        res.Add(new SearchViewModel(cat, "categories"));

                }
                return Ok(res);
            }
            catch
            {
                return BadRequest();
            }
        }


        [Route("room/getrooms")]
        [Produces("application/json")]
        public JsonResult GetRooms(string searchValue)
        {
            var roomsData = new List<RoomSpecificationDto>();
            int floornumber = 0;
            int.TryParse(searchValue, out floornumber);
            if (!string.IsNullOrEmpty(searchValue))
            {
                roomsData = servicePool.roomService.SearchRooms(searchValue, floornumber);
            }

            string res = string.Empty;
            res = JsonConvert.SerializeObject(roomsData, Formatting.Indented, new JsonSerializerSettings
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore
            });

            return Json(res);
        }


    }
}
