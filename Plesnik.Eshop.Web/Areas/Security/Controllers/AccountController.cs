using Microsoft.AspNetCore.Mvc;
using Plesnik.Eshop.Web.Controllers;
using Plesnik.Eshop.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Plesnik.Eshop.Web.Areas.Security.Controllers
{
    [Area("Security")]
    public class AccountController : Controller
    {
        public IActionResult Register()
        {
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel loginViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { area = "" });
            }

            return View(loginViewModel);
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel registerViewModel)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction(nameof(HomeController.Index), nameof(HomeController).Replace("Controller", ""), new { area = "" });
            }

            return View(registerViewModel);
        }
    }
}
