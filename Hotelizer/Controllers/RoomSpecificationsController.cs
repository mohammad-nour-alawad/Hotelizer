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
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class RoomSpecificationsController : BaseController
    {
        public RoomSpecificationsController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }

        [Authorize]
        public ViewResult Index()
        {
            var roomSpecification = UnitOfWork.RoomSpecificationRepo.GetAll(includeProperties: "Room,Spec");
            return View(roomSpecification.ToList());
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roomSpecification = UnitOfWork.RoomSpecificationRepo.GetById(id);
            if (roomSpecification == null)
            {
                return NotFound();
            }
            return View(roomSpecification);
        }

        [Authorize]
        public IActionResult Create()
        {
            ViewData["RoomId"] = new SelectList(UnitOfWork.RoomRepo.GetAll(), "Id", "FloorNumber","Status");
            ViewData["SpecId"] = new SelectList(UnitOfWork.SpecificationRepo.GetAll(), "Id", "Type");
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,RoomId,SpecId")] RoomSpecification roomSpecification)
        {
            if (ModelState.IsValid)
            {
                UnitOfWork.RoomSpecificationRepo.Insert(roomSpecification);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoomId"] = new SelectList(UnitOfWork.RoomRepo.GetAll(), "Id", "Status", roomSpecification.RoomId);
            ViewData["SpecId"] = new SelectList(UnitOfWork.SpecificationRepo.GetAll(), "Id", "Type", roomSpecification.SpecId);
            return View(roomSpecification);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var roomSpecification =  UnitOfWork.RoomSpecificationRepo.GetById(id);
            if (roomSpecification == null)
            {
                return NotFound();
            }
            ViewData["RoomId"] = new SelectList(UnitOfWork.RoomRepo.GetAll(), "Id", "Status", roomSpecification.RoomId);
            ViewData["SpecId"] = new SelectList(UnitOfWork.SpecificationRepo.GetAll(), "Id", "Type", roomSpecification.SpecId);
            return View(roomSpecification);
        }

        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,RoomId,SpecId")] RoomSpecification roomSpecification)
        {
            if (id != roomSpecification.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    UnitOfWork.RoomSpecificationRepo.Update(roomSpecification);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!RoomSpecificationExists(roomSpecification.Id))
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
            ViewData["RoomId"] = new SelectList(UnitOfWork.RoomRepo.GetAll(), "Id", "Status", roomSpecification.RoomId);
            ViewData["SpecId"] = new SelectList(UnitOfWork.SpecificationRepo.GetAll(), "Id", "Type", roomSpecification.SpecId);
            return View(roomSpecification);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var roomSpecification = UnitOfWork.RoomSpecificationRepo.GetById(id);
            if (roomSpecification == null)
            {
                return NotFound();
            }
            return View(roomSpecification);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var roomSpecification =  UnitOfWork.RoomSpecificationRepo.GetById(id);
            UnitOfWork.RoomSpecificationRepo.Delete(roomSpecification);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool RoomSpecificationExists(int id)
        {
            return UnitOfWork.RoomSpecificationRepo.IsExist(id);
        }
    }
}
