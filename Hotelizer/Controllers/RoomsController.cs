using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using System.Data;
using Hotelizer.Models;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class RoomsController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;
        public RoomsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }
        private async Task saveImage(Room room)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(room.Imagefile.FileName);
            string extension = Path.GetExtension(room.Imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yyyy-mm-ss-fff") + extension;
            string path = Path.Combine(wwwRootPath + "\\images\\rooms", filename);
            room.ImageUrl = Path.Combine("/images/rooms/", filename);
            using var fileStream = new FileStream(path, FileMode.Create);
            await room.Imagefile.CopyToAsync(fileStream);
        }
        private void deleteImage(string imagePath)
        {
            if (imagePath == null)
                return;
            string fullpath = Path.Combine(_hostEnvironment.WebRootPath + "\\", imagePath.Substring(1));
            if (System.IO.File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
        }

        [Authorize]
        public ViewResult Index()
        {
            var rooms = UnitOfWork.RoomRepo.GetAll (includeProperties: "Category,Hotel" );
            return View(rooms.ToList());
        }

        [Authorize]
        public ViewResult Details(int id)
        {
            var room = UnitOfWork.RoomRepo.GetById(id);
            var category = UnitOfWork.CategoryRepo.GetById(room.CategoryId);
            var hotel = UnitOfWork.HotelRepo.GetById(room.HotelId);

            ViewBag.catId = category.Name;
            ViewBag.hotIds = hotel.Name;

            return View(room);
        }
       
        [Authorize]
        public ActionResult Create()
        {
            var cats = UnitOfWork.CategoryRepo.GetAll();
            List<KeyValuePair<int, string>> category_pair = new List<KeyValuePair<int, string>>();
            foreach ( var cat in cats)
            {
                category_pair.Add(new KeyValuePair<int, string>(cat.Id, cat.Name));
            }
            ViewBag.catIds = category_pair;
            var hots = UnitOfWork.HotelRepo.GetAll();
            List<KeyValuePair<int, string>> hotel_pair = new List<KeyValuePair<int, string>>();
            foreach (var hot in hots)
            {
                hotel_pair.Add(new KeyValuePair<int, string>(hot.Id, hot.Name));
            }
            ViewBag.hotIds = hotel_pair;

            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FloorNumber,Status,CategoryId,HotelId,Imagefile")] Room room)
        {
            if (ModelState.IsValid)
            {
                if( room.Imagefile != null)
                    await saveImage(room);
                UnitOfWork.RoomRepo.Insert(room);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(room);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
    
            if (id == null)
            {
                return NotFound();
            }
            var room = UnitOfWork.RoomRepo.GetById(id);


            if (room == null)
            {
                return NotFound();
            }
            var cats = UnitOfWork.CategoryRepo.GetAll();
            List<KeyValuePair<int, string>> category_pair = new List<KeyValuePair<int, string>>();
            foreach (var cat in cats)
            {
                category_pair.Add(new KeyValuePair<int, string>(cat.Id, cat.Name));
            }
            ViewBag.catIds = category_pair;
            var hots = UnitOfWork.HotelRepo.GetAll();
            List<KeyValuePair<int, string>> hotel_pair = new List<KeyValuePair<int, string>>();
            foreach (var hot in hots)
            {
                hotel_pair.Add(new KeyValuePair<int, string>(hot.Id, hot.Name));
            }
            ViewBag.hotIds = hotel_pair;
            return View(room);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FloorNumber,Status,CategoryId,HotelId,Imagefile")] Room room)
        {
            if (id != room.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    Room room_old = UnitOfWork.RoomRepo.GetById(id);
                    if (room.Imagefile != null)
                    {
                        await saveImage(room);

                        deleteImage(room_old.ImageUrl);
                    }
                    else
                    {
                        room.ImageUrl = room_old.ImageUrl;
                    }
                    room.FloorNumber = room_old.FloorNumber;
                    UnitOfWork.RoomRepo.Update(room);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {

                    if (!RoomExists(room.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }

                }
                return RedirectToAction(nameof(Index));
            }
 
            return View(room);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var room = UnitOfWork.RoomRepo.GetById(id);
            var category = UnitOfWork.CategoryRepo.GetById(room.CategoryId);
            var hotel = UnitOfWork.HotelRepo.GetById(room.HotelId);

            ViewBag.catId = category.Name;
            ViewBag.hotIds = hotel.Name;

            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var room = UnitOfWork.RoomRepo.GetById(id);
            deleteImage(room.ImageUrl);
            UnitOfWork.RoomRepo.Delete(room);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomExists(int id)
        {
            return UnitOfWork.RoomRepo.IsExist(id);
        }
    }
}
