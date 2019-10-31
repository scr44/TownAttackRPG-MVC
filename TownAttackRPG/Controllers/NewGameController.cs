using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TownAttackRPG.Controllers
{
    public class NewGameController : Controller
    {
        public IActionResult CharacterCreation()
        {
            return View();
        }
        public IActionResult Scenario()
        {
            return View();
        }
    }
}