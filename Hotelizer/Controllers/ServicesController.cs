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
using System.IO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace Hotelizer.Controllers
{
    public class ServicesController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public ServicesController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }
        private async Task saveImage(Service service)
        {
            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(service.Imagefile.FileName);
            string extension = Path.GetExtension(service.Imagefile.FileName);
            filename = filename + DateTime.Now.ToString("yyyy-mm-ss-fff") + extension;
            string path = Path.Combine(wwwRootPath + "\\images\\services", filename);
            service.ImageUrl = Path.Combine("/images/services/", filename);
            using var fileStream = new FileStream(path, FileMode.Create);
            await service.Imagefile.CopyToAsync(fileStream);
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
            var service = UnitOfWork.ServiceRepo.GetAll();
            return View(service.ToList());
        }

        [Authorize]
        public ViewResult Details(int? id)
        {
            var service = UnitOfWork.ServiceRepo.GetById(id);
            return View(service);
        }

        [Authorize]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Create([Bind("Id,Name,Imagefile")] Service service)
        {
            if (ModelState.IsValid)
            {
                if (service.Imagefile != null)
                    await saveImage(service);
                UnitOfWork.ServiceRepo.Insert(service);
                await UnitOfWork.SaveAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(service);
        }

        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = UnitOfWork.ServiceRepo.GetById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Imagefile")] Service service)
        {
            if (id != service.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    Service service_old = UnitOfWork.ServiceRepo.GetById(id);
                    if (service.Imagefile != null)
                    {
                        await saveImage(service);

                        deleteImage(service_old.ImageUrl);
                    }
                    else
                    {
                        service.ImageUrl = service_old.ImageUrl;
                    }
                    UnitOfWork.ServiceRepo.Update(service);
                    await UnitOfWork.SaveAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ServiceExists(service.Id))
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
            return View(service);
        }

        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var service = UnitOfWork.ServiceRepo.GetById(id);
            if (service == null)
            {
                return NotFound();
            }
            return View(service);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = UnitOfWork.ServiceRepo.GetById(id);
            deleteImage(service.ImageUrl);
            UnitOfWork.ServiceRepo.Delete(service);
            await UnitOfWork.SaveAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ServiceExists(int id)
        {
            return UnitOfWork.ServiceRepo.IsExist(id);
        }
    }
}
