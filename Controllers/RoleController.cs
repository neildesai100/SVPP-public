using System;

using System.Collections.Generic;

using System.Linq;

using System.Threading.Tasks;

using Microsoft.AspNetCore.Identity;

using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Authorization;



namespace SVPP.Controllers
{
    public class RoleController : Controller
    {
        RoleManager<IdentityRole> roleManager;
        //Injecting Role Manager 

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        [Authorize(Policy = "adminonlypolicy")]
        public IActionResult Index()
        {

            var roles = roleManager.Roles.ToList();

            return View(roles);

        }

        [Authorize(Policy = "adminonlypolicy")]
        public IActionResult Create()
        {

            return View(new IdentityRole());

        }

        [HttpPost]
        public async Task<IActionResult> Create(IdentityRole role)
        {

            await roleManager.CreateAsync(role);

            return RedirectToAction(nameof(Index));

        }

    }

}