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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class SpecificationsController : BaseController
    {
        public SpecificationsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {

        }

        [Authorize]
        public ViewResult Index()
        {
            var specification = UnitOfWork.SpecificationRepo.GetAll();
            return View(specification.ToList());
        }

        [Authorize]
        public ViewResult Details(int? id)
        {
            Specification specification = UnitOfWork.SpecificationRepo.GetById(id);
            return View(specification);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Type")] Specification specification)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.SpecificationRepo.Insert(specification);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(specification);

        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = UnitOfWork.SpecificationRepo.GetById(id);
            if (specification == null)
            {
                return NotFound();
            }
            return View(specification);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Type")] Specification specification)
        {
            if (id != specification.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.SpecificationRepo.Update(specification);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!SpecificationExists(specification.Id))
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
            return View(specification);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var specification = UnitOfWork.SpecificationRepo.GetById(id);
               
            if (specification == null)
            {
                return NotFound();
            }

            return View(specification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var specification = UnitOfWork.SpecificationRepo.GetById(id);
            UnitOfWork.SpecificationRepo.Delete(specification);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }


        private bool SpecificationExists(int id)
        {
            return UnitOfWork.SpecificationRepo.IsExist(id);
        }
    }
}