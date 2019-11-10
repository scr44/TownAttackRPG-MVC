using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Scenarios;
using TownAttackRPG.Models.ViewModels.Game;
using TownAttackRPG.Models.ViewModels.NewGame;

namespace TownAttackRPG.Controllers
{
    public class LoadGameController : Controller
    {
        private readonly ISaveGameDAO SaveGameDAO;
        public LoadGameController(ISaveGameDAO saveGameDAO)
        {
            this.SaveGameDAO = saveGameDAO;
        }

        [HttpGet]
        public IActionResult SelectSaveSlot()
        {
            List<GameData> SavedGames = new List<GameData>();
            for (int i = 1; i <= 3; i++)
            {
                SavedGames.Add(SaveGameDAO.LoadGame(1, i));
                SavedGames[i - 1].SaveGameSlot = i;
            }
            SelectSaveVM vm = new SelectSaveVM() { SaveList = SavedGames };
            return View(vm);
        }
        [HttpPost]
        public IActionResult SelectSaveSlot(SelectSaveVM vm)
        {
            int slot = vm.Slot;
            int[] validSlots = new int[3] { 1, 2, 3 };
            if (!ModelState.IsValid || !validSlots.Contains(slot))
            {
                List<GameData> SavedGames = new List<GameData>();
                for (int i = 1; i <= 3; i++)
                {
                    SavedGames.Add(SaveGameDAO.LoadGame(1, i));
                    SavedGames[i - 1].SaveGameSlot = i;
                }
                vm.SaveList = SavedGames;
                return View(vm);
            }
            else
            {
                TempData["slot"] = slot;
                return RedirectToAction("Index", "Game", slot);
            }
        }
    }
}