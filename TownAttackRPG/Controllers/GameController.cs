using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TownAttackRPG.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index(string scenario=null, string character=null)
        {
            return View();
        }
    }
}