using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TownAttackRPG.Models.ViewModels.Main;

namespace TownAttackRPG.Controllers
{
    public class MainController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Options
        public IActionResult Options()
        {
            return View();
        }
        #endregion
    }
}