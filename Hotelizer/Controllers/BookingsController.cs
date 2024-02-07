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

namespace Hotelizer.Controllers
{
    public class BookingsController : BaseController
    {

        public BookingsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        [Authorize]
        public ViewResult Index()
        {
            var booking = UnitOfWork.BookingRepo.GetAll(includeProperties: "Room,ApplicationUser");
            return View(booking.ToList());

        }


       
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

        [Authorize]
        // GET: Bookings/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = new SelectList(UnitOfWork.context.Users, "Id", "Email");
            ViewData["RoomId"] = new SelectList(UnitOfWork.context.Room, "Id", "FloorNumber");
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,UserId,FromDate,ToDate,Status,Bill")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.BookingRepo.Insert(booking);
                await UnitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            ViewData["UserId"] = new SelectList(UnitOfWork.context.Users, "Id", "Email", booking.UserId);
            ViewData["RoomId"] = new SelectList(UnitOfWork.context.Room, "Id", "FloorNumber", booking.RoomId);
            return View(booking);
        }

        [Authorize]
        // GET: Bookings/Edit/5
        public IActionResult Edit(int? id)
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
            ViewData["UserId"] = new SelectList(UnitOfWork.context.Users, "Id", "Email", booking.UserId);
            ViewData["RoomId"] = new SelectList(UnitOfWork.context.Room, "Id", "FloorNumber", booking.RoomId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,UserId,FromDate,ToDate,Status,Bill")] Booking booking)
        {
            if (id != booking.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.BookingRepo.Update(booking);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BookingExists(booking.Id))
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
            ViewData["UserId"] = new SelectList(UnitOfWork.context.Users, "Id", "Email", booking.UserId);
            ViewData["RoomId"] = new SelectList(UnitOfWork.context.Room, "Id", "FloorNumber", booking.RoomId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        [Authorize]
        public IActionResult Delete(int? id)
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

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var booking = UnitOfWork.BookingRepo.GetById(id);
            UnitOfWork.BookingRepo.Delete(booking);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool BookingExists(int id)
        {
            return UnitOfWork.BookingRepo.IsExist(id);
        }
    }
}
