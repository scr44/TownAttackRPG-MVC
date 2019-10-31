using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TownAttackRPG.Models;

namespace TownAttackRPG.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["SavedGames"] = 1; // TODO Main Menu: check for existing saved games
            // Splash Screen
            // Select new game, load game, options
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
