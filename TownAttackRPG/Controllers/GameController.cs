using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TownAttackRPG.Controllers
{
    public class GameController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        #region Dialogue
        [HttpGet]
        public IActionResult DialogueResponses(int DialogueVMIndex)
        {
            // make new DialogueVM with module dialogues
            return View();
        }
        [HttpPost]
        public IActionResult DialogueResponses(string placeholderForDialogueVM)
        {
            if(!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                int chosenResponse = 0; // pick response from module dialogue responses
                return RedirectToAction("DialogueResponses", chosenResponse);
            }
        }
        #endregion
    }
}