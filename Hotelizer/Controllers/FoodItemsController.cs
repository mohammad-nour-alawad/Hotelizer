using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using Microsoft.AspNetCore.Hosting;
using DBConnection.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class FoodItemsController : BaseController
    {
        public FoodItemsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        // GET: FoodItems
        public IActionResult Index()
        {
            var foodItems = UnitOfWork.FoodItemRepo.GetAll(includeProperties: "FoodItemType").ToList();
            return View(foodItems);
        }

        // GET: FoodItems/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = UnitOfWork.FoodItemRepo.GetAll(e => e.Id == id, includeProperties: "FoodItemType").ToList()[0];
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // GET: FoodItems/Create
        public IActionResult Create()
        {
            ViewData["FoodItemTypeId"] = new SelectList(UnitOfWork.context.FoodItemType, "Id", "Type");
            return View();
        }

        // POST: FoodItems/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodItemTypeId,Name,Cost")] FoodItem foodItem)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.FoodItemRepo.Insert(foodItem);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["FoodItemTypeId"] = new SelectList(UnitOfWork.context.FoodItemType, "Id", "Type", foodItem.FoodItemTypeId);
            return View(foodItem);
        }

        // GET: FoodItems/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItem = UnitOfWork.FoodItemRepo.GetById(id);
            if (foodItem == null)
            {
                return NotFound();
            }
            ViewData["FoodItemTypeId"] = new SelectList(UnitOfWork.context.FoodItemType, "Id", "Type", foodItem.FoodItemTypeId);
            return View(foodItem);
        }

        // POST: FoodItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodItemTypeId,Name,Cost")] FoodItem foodItem)
        {
            if (id != foodItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.FoodItemRepo.Update(foodItem);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemExists(foodItem.Id))
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
            ViewData["FoodItemTypeId"] = new SelectList(UnitOfWork.context.FoodItemType, "Id", "Type", foodItem.FoodItemTypeId);
            return View(foodItem);
        }

        // GET: FoodItems/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var foodItem = UnitOfWork.FoodItemRepo.GetAll(e => e.Id == id, includeProperties: "FoodItemType").ToList()[0];
            //.Include(f => f.FoodItemType)
            //.FirstOrDefaultAsync(m => m.Id == id);
            if (foodItem == null)
            {
                return NotFound();
            }

            return View(foodItem);
        }

        // POST: FoodItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItem = UnitOfWork.FoodItemRepo.GetById(id);
            UnitOfWork.FoodItemRepo.Delete(foodItem);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemExists(int id)
        {
            return UnitOfWork.FoodItemRepo.IsExist(id);
        }
    }
}
