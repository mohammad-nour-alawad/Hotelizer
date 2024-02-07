using DBConnection.Models;
using DBConnection.UnitOfWork;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotelizer.Controllers
{
    [Authorize (Roles = "Admin,SuperAdmin")]
    public class UsersController : BaseController
    {

        public UsersController(IUnitOfWork unitOfWork, IConfiguration configuration, UserManager<ApplicationUser> userManager
            , IWebHostEnvironment _hostEnvironment) : base(unitOfWork, configuration, userManager, _hostEnvironment)
        {
        }
        [Authorize]
        public IActionResult Index()
        {
            var users = UnitOfWork.UserRepo.GetAll();
            return View(users);
        }


        [Authorize(Roles = "SuperAdmin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize (Roles = "SuperAdmin")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,Email,Password")] ApplicationUser user)
        {
            if (ModelState.IsValid)
            {
                ApplicationUser appUser = new ApplicationUser
                {
                    FirstName = user.FirstName,
                    Email = user.Email,
                    LastName = user.LastName,
                    UserName = user.Email,
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
                    NormalizedEmail = user.Email,
                    AccessFailedCount = 0,
                    LockoutEnabled = false,
                    LockoutEnd = null,
                    SecurityStamp = Guid.NewGuid().ToString("D"),
                    PasswordHash = userManager.PasswordHasher.HashPassword(null, user.Password)
                };

                UnitOfWork.UserRepo.Insert(appUser);
                await UnitOfWork.SaveAsync();

                if (Request.Form["role"] == "1") {
                    var res = userManager.AddToRoleAsync(appUser, "Admin").Result;

                    if (res.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        foreach (IdentityError error in res.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }
                if (Request.Form["role"] == "2") {
                    var res = userManager.AddToRoleAsync(appUser, "User").Result;

                    if (res.Succeeded)
                        return RedirectToAction(nameof(Index));
                    else
                    {
                        foreach (IdentityError error in res.Errors)
                            ModelState.AddModelError("", error.Description);
                    }
                }

                UnitOfWork.Save();
            }
            return View(user);
        }

        [Authorize]
        public IActionResult Edit(string id)
        {
            if (id == "")
            {
                return NotFound();
            }
            var user = UnitOfWork.UserRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Delete(string id)
        {
            if (id == "")
            {
                return NotFound();
            }
            var user = UnitOfWork.UserRepo.GetById(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }
        [Authorize]
        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            ApplicationUser user = await userManager.FindByIdAsync(id);
            if (user != null)
            {
                IdentityResult result = await userManager.DeleteAsync(user);
                if (result.Succeeded)
                    return RedirectToAction("Index");
            }
            else
                ModelState.AddModelError("", "User Not Found");
            return RedirectToAction(nameof(Index));

        }
    }
}
