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
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;

namespace Hotelizer.Controllers
{
    public class ServiceCategoriesController : BaseController
    {

        public ServiceCategoriesController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        [Authorize]
        public ViewResult Index()
        {
            var serviceCategory = UnitOfWork.ServiceCategoryRepo.GetAll(includeProperties: "Category,Service");
            return View(serviceCategory.ToList());
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var serviceCategory = UnitOfWork.ServiceCategoryRepo.GetById(id);
            if (serviceCategory == null)
            {
                return NotFound();
            }

            return View(serviceCategory);
        }


        [Authorize]
        public IActionResult Create()
        {
            ViewData["CategoryId"] = new SelectList(UnitOfWork.CategoryRepo.GetAll(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(UnitOfWork.ServiceRepo.GetAll(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,ServiceId,CategoryId,Cost")] ServiceCategory serviceCategory)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.ServiceCategoryRepo.Insert(serviceCategory);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryId"] = new SelectList(UnitOfWork.CategoryRepo.GetAll(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(UnitOfWork.ServiceRepo.GetAll(), "Id", "Name");
            return View(serviceCategory);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCategory = UnitOfWork.ServiceCategoryRepo.GetById(id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryId"] = new SelectList(UnitOfWork.CategoryRepo.GetAll(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(UnitOfWork.ServiceRepo.GetAll(), "Id", "Name");
            return View(serviceCategory);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ServiceId,CategoryId,Cost")] ServiceCategory serviceCategory)
        {
            if (id != serviceCategory.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.ServiceCategoryRepo.Update(serviceCategory);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceCategoryExists(serviceCategory.Id))
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
            ViewData["CategoryId"] = new SelectList(UnitOfWork.CategoryRepo.GetAll(), "Id", "Name");
            ViewData["ServiceId"] = new SelectList(UnitOfWork.ServiceRepo.GetAll(), "Id", "Name");
            return View(serviceCategory);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var serviceCategory = UnitOfWork.ServiceCategoryRepo.GetById(id);
            if (serviceCategory == null)
            {
                return NotFound();
            }
            return View(serviceCategory);
        }

        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var serviceCategory = UnitOfWork.ServiceCategoryRepo.GetById(id);
            UnitOfWork.ServiceCategoryRepo.Delete(serviceCategory);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceCategoryExists(int id)
        {
            return UnitOfWork.ServiceCategoryRepo.IsExist(id);
        }
    }
}
