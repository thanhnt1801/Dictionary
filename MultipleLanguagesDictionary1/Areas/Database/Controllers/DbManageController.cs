using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using App.Data;
using App.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MultipleLanguagesDictionary1.Data;

namespace App.Areas.Database.Controllers
{
    [Area("Database")]
    [Route("/database-manage/[action]")]
    //[Authorize(Roles = RoleName.Administrator)]
    public class DbManageController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbManageController(ApplicationDbContext dbContext, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _dbContext = dbContext;
            _userManager = userManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult DeleteDb()
        {
            // TODO: Your code here
            return View();
        }

        [TempData]
        public string StatusMessage { get; set; }


        [HttpPost]
        public async Task<IActionResult> DeleteDbAsync()
        {
            var success = await _dbContext.Database.EnsureDeletedAsync();

            StatusMessage = success ? "Db deleted SUCCESSFULLY" : "Db deleted UNSUCCESSFULLY";

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Migrate()
        {
            await _dbContext.Database.MigrateAsync();
            StatusMessage = "Migrations updated";

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> SeedDataAsync()
        {
            var roleNames = typeof(RoleName).GetFields().ToList();
            foreach (var r in roleNames)
            {
                var roleName = (string)r.GetRawConstantValue();
                var rfound = await _roleManager.FindByNameAsync(roleName);
                if (rfound == null)
                {
                    await _roleManager.CreateAsync(new IdentityRole(roleName));
                }
            }

            // admin, pwd=admin123, admin@example.com
            var userAdmin = await _userManager.FindByNameAsync("admin");
            if (userAdmin == null)
            {
                userAdmin = new IdentityUser()
                {
                    UserName = "admin",
                    Email = "admin@example.com",
                    EmailConfirmed = true
                };

                await _userManager.CreateAsync(userAdmin, "admin123");
                await _userManager.AddToRoleAsync(userAdmin, RoleName.Administrator);
            }

            StatusMessage = "Database has been seeded";
            return RedirectToAction("Index");
        }
    }
}