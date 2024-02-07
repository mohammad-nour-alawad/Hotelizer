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
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;

namespace Hotelizer.Controllers
{
    public class CatergoriesController : BaseController
    {
        private readonly IWebHostEnvironment _hostEnvironment;

        public CatergoriesController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
            this._hostEnvironment = _hostEnvironment;
        }

        [Authorize]
        public ViewResult Index()
        {
            var category = UnitOfWork.CategoryRepo.GetAll();
            return View(category.ToList());
        }

        [Authorize]
        public IActionResult Details(int? id)
        {
            if (!id.HasValue)
            {
                return RedirectToAction(actionName: nameof(Index), controllerName: "Catergories");
            }

            var category = UnitOfWork.CategoryRepo.GetById(id);
            if (category == null)
            {
                return RedirectToAction(actionName: nameof(Index), controllerName: "Catergories");
            }

            return View(category);
        }

        private async Task saveImage(Catergory category)
        {

            string wwwRootPath = _hostEnvironment.WebRootPath;
            string filename = Path.GetFileNameWithoutExtension(category.Imagefile.FileName);
            string extension = Path.GetExtension(category.Imagefile.FileName);
            //+DateTime.Now.ToString("yyyy-mm-ss-fff")
            filename = filename  + extension;
            string path = Path.Combine(wwwRootPath + "\\images\\categories", filename);
            category.ImageUrl = Path.Combine("/images/categories/", filename);
            using var fileStream = new FileStream(path, FileMode.Create);
            await category.Imagefile.CopyToAsync(fileStream);
        }
        private void deleteImage(string imagePath)
        {
            if (imagePath == null || _hostEnvironment == null)
                return;
            string fullpath = Path.Combine(_hostEnvironment.WebRootPath + "\\", imagePath.Substring(1));
            if (System.IO.File.Exists(fullpath))
            {
                System.IO.File.Delete(fullpath);
            }
        }


        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,Id,Imagefile")] Catergory category)
        {
            if (ModelState.IsValid)
            {
                // saving file
                if (category.Imagefile != null)
                    await saveImage(category);

                UnitOfWork.CategoryRepo.Insert(category);
                UnitOfWork.Save();

                var model = UnitOfWork.CategoryRepo.GetAll().ToList();
                return Ok(model);
                //RedirectToAction(actionName: nameof(Index), controllerName: "Catergories");
            }
            return View(category);
        }


        [Authorize]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var catergory = UnitOfWork.CategoryRepo.GetById(id);
            if (catergory == null)
            {
                return NotFound();
            }
            return View(catergory);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Catergory catergory)
        {

            if (ModelState.IsValid)
            {
                
                Catergory cat_old = UnitOfWork.CategoryRepo.GetById(catergory.Id);
                if ( catergory.Imagefile != null)
                {
                    await saveImage(catergory);
                    deleteImage(cat_old.ImageUrl);
                }
                else
                {
                    catergory.ImageUrl = cat_old.ImageUrl;
                }


                var new_category = UnitOfWork.CategoryRepo.Update(catergory);
                await UnitOfWork.SaveAsync();

                return Ok(new_category);
            }
            return View(catergory);
        }
        [Authorize]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var category = UnitOfWork.CategoryRepo.GetById(id);
            if (category == null)
            {
                return NotFound(id);
            }
            return View(category);
        }


        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var category = UnitOfWork.CategoryRepo.GetById(id);
            deleteImage(category.ImageUrl);
            UnitOfWork.CategoryRepo.Delete(category);
            UnitOfWork.Save();

            var model = UnitOfWork.CategoryRepo.GetAll().ToList();
            return Ok(model);
        }

        private bool CatergoryExists(int id)
        {

            return UnitOfWork.CategoryRepo.IsExist(id);
        }
    }
}
