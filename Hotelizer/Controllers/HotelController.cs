using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using DBConnection;
using Microsoft.AspNetCore.Authorization;
using DataTables.AspNet.AspNetCore;
using DataTables.AspNet.Core;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class HotelController : BaseController
    {

        public HotelController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        [Authorize]
        [HttpPost("GetHotels")]
        public IActionResult GetHotels()
        {
            try
            {
                var draw = Request.Form["draw"].FirstOrDefault();
                var start = Request.Form["start"].FirstOrDefault();
                var length = Request.Form["length"].FirstOrDefault();
                var sortColumn = Request.Form["columns[" + Request.Form["order[0][column]"].FirstOrDefault() + "][name]"].FirstOrDefault();
                var sortColumnDirection = Request.Form["order[0][dir]"].FirstOrDefault();
                var searchValue = Request.Form["search[value]"].FirstOrDefault();
                int pageSize = length != null ? Convert.ToInt32(length) : 0;
                int skip = start != null ? Convert.ToInt32(start) : 0;
                int recordsTotal = 0;
                var userData = (from tempuser in UnitOfWork.context.Hotel select tempuser);


                if (!(string.IsNullOrEmpty(sortColumn) && string.IsNullOrEmpty(sortColumnDirection)))
                {
                    userData = userData.OrderBy(s => sortColumn + " " + sortColumnDirection);
                }
                if (!string.IsNullOrEmpty(searchValue))
                {
                    userData = userData.Where(m => m.Location.Contains(searchValue)
                                                || m.Name.Contains(searchValue));
                }


                recordsTotal = userData.Count();
                var data = userData.Skip(skip).Take(pageSize).ToList();
                var jsonData = new { draw = draw, recordsFiltered = recordsTotal, recordsTotal = recordsTotal, data = data };
                return Ok(jsonData);
            }
            catch
            {
                throw;
            }
        }





        [Authorize]

        public ViewResult Index()
        {
            var hotel = UnitOfWork.HotelRepo.GetAll();
            return View(hotel.ToList());
        }


        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel = UnitOfWork.HotelRepo.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Location")] Hotel hotel)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.HotelRepo.Insert(hotel);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(hotel);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel = UnitOfWork.HotelRepo.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,Id,Location")] Hotel hotel)
        {
            if (id != hotel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.HotelRepo.Update(hotel);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HotelExists(hotel.Id))
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
            return View(hotel);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var hotel = UnitOfWork.HotelRepo.GetById(id);
            if (hotel == null)
            {
                return NotFound();
            }
            return View(hotel);
        }
        [HttpPost, ActionName("Delete")]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var hotel = UnitOfWork.HotelRepo.GetById(id);
            UnitOfWork.HotelRepo.Delete(hotel);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HotelExists(int id)
        {
            return UnitOfWork.HotelRepo.IsExist(id);
        }
    }
}
