using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class OrdersController : BaseController
    {
        public OrdersController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }


        // GET: Orders
        public IActionResult Index()
        {
            var orders = UnitOfWork.OrderRepo.GetAll(includeProperties: "Booking,FoodItem");
            return View(orders.ToList());
        }

        // GET: Orders/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = UnitOfWork.OrderRepo.GetAll(e => e.Id == id, includeProperties: "Booking,FoodItem").ToList()[0];
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["BookingId"] = new SelectList(UnitOfWork.context.Booking, "Id", "Id");
            ViewData["FoodItemId"] = new SelectList(UnitOfWork.context.FoodItem, "Id", "Name");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookingId,FoodItemId,NumberOfItems")] Order order)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.OrderRepo.Insert(order);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["BookingId"] = new SelectList(UnitOfWork.context.Booking, "Id", "Id", order.BookingId);
            ViewData["FoodItemId"] = new SelectList(UnitOfWork.context.FoodItem, "Id", "Name", order.FoodItemId);
            return View(order);
        }

        // GET: Orders/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = UnitOfWork.OrderRepo.GetById(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["BookingId"] = new SelectList(UnitOfWork.context.Booking, "Id", "Id", order.BookingId);
            ViewData["FoodItemId"] = new SelectList(UnitOfWork.context.FoodItem, "Id", "Name", order.FoodItemId);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookingId,FoodItemId")] Order order)
        {
            if (id != order.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.OrderRepo.Update(order);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OrderExists(order.Id))
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
            ViewData["BookingId"] = new SelectList(UnitOfWork.context.Booking, "Id", "Id", order.BookingId);
            ViewData["FoodItemId"] = new SelectList(UnitOfWork.context.FoodItem, "Id", "Name", order.FoodItemId);
            return View(order);
        }

        // GET: Orders/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = UnitOfWork.OrderRepo.GetAll(e => e.Id == id, includeProperties: "Booking,FoodItem").ToList()[0];
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = UnitOfWork.OrderRepo.GetById(id);
            UnitOfWork.OrderRepo.Delete(order);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return UnitOfWork.OrderRepo.IsExist(id);
        }
    }
}
