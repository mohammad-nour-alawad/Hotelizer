using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class FoodItemTypesController : BaseController
    {
        public FoodItemTypesController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        // GET: FoodItemTypes
        public IActionResult Index()
        {
            return View(UnitOfWork.FoodItemTypeRepo.GetAll().ToList());
        }

        // GET: FoodItemTypes/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemType = UnitOfWork.FoodItemTypeRepo
                .GetById(id);
            if (foodItemType == null)
            {
                return NotFound();
            }

            return View(foodItemType);
        }

        // GET: FoodItemTypes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: FoodItemTypes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Type")] FoodItemType foodItemType)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.FoodItemTypeRepo.Insert(foodItemType);
                await UnitOfWork.SaveAsync();

                return RedirectToAction(nameof(Index));
            }
            return View(foodItemType);
        }

        // GET: FoodItemTypes/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemType = UnitOfWork.FoodItemTypeRepo.GetById(id);
            if (foodItemType == null)
            {
                return NotFound();
            }
            return View(foodItemType);
        }

        // POST: FoodItemTypes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] FoodItemType foodItemType)
        {
            if (id != foodItemType.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.FoodItemTypeRepo.Update(foodItemType);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FoodItemTypeExists(foodItemType.Id))
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
            return View(foodItemType);
        }

        // GET: FoodItemTypes/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var foodItemType = UnitOfWork.FoodItemTypeRepo.GetById(id);
            if (foodItemType == null)
            {
                return NotFound();
            }

            return View(foodItemType);
        }

        // POST: FoodItemTypes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var foodItemType = UnitOfWork.FoodItemTypeRepo.GetById(id);
            UnitOfWork.FoodItemTypeRepo.Delete(foodItemType);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool FoodItemTypeExists(int id)
        {
            return UnitOfWork.FoodItemTypeRepo.IsExist(id);
        }
    }
}
