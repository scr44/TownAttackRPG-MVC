using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TownAttackRPG.Controllers
{
    public class LoadGameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}