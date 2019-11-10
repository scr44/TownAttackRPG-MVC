using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Scenarios;
using TownAttackRPG.Models.ViewModels.Game;
using TownAttackRPG.Models.ViewModels.Main;
using TownAttackRPG.Models.ViewModels.NewGame;

namespace TownAttackRPG.Controllers
{
    public class NewGameController : Controller
    {
        private readonly ISaveGameDAO SaveGameDAO;
        public NewGameController(ISaveGameDAO saveGameDAO)
        {
            this.SaveGameDAO = saveGameDAO;
        }

        public IActionResult Index()
        {
            return RedirectToAction("SelectScenario");
        }

        #region Scenario Selection
        [HttpGet]
        public IActionResult SelectScenario()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SelectScenario(ScenarioVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }
            else
            {
                try
                {
                    HttpContext.Session.SetString("Scenario", vm.ScenarioChoice);
                    return RedirectToAction("NameAndGender");
                }
                catch (ArgumentException e)
                {
                    if (e.Message == "Invalid Scenario chosen")
                    {
                        return View();
                    }
                    else
                    {
                        throw e;
                    }
                }
            }
        }
        #endregion

        #region Character Creation
        [HttpGet]
        public IActionResult NameAndGender()
        {
            return View();
        }
        [HttpPost]
        public IActionResult NameAndGender(NameAndGenderVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                HttpContext.Session.SetString("Name",vm.Name);
                HttpContext.Session.SetString("Gender", vm.Gender);
                return RedirectToAction("Profession");
            }
        }

        [HttpGet]
        public IActionResult Profession()
        {
            ProfessionVM vm = new ProfessionVM();
            return View(vm);
        }
        [HttpPost]
        public IActionResult Profession(ProfessionVM vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }
            else
            {
                HttpContext.Session.SetString("Profession", vm.ProfessionChoice);
                return RedirectToAction("SelectSaveSlot");
            }
        }

        [HttpGet]
        public IActionResult SelectSaveSlot()
        {
            List<GameData> SavedGames = new List<GameData>();
            for (int i=1;i<=3;i++)
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
                return RedirectToAction("StartGame");
            }
        }
        #endregion

        public IActionResult StartGame()
        {
            int slot = (int)TempData["slot"];
            string name = HttpContext.Session.GetString("Name");
            string gender = HttpContext.Session.GetString("Gender");
            string profChoice = HttpContext.Session.GetString("Profession");
            Profession prof = (new ProfessionVM() { ProfessionChoice = profChoice, Gender = gender }).Profession;
            Character PlayerCharacter = new Character(name, prof);
            ScenarioVM scenarioVM = new ScenarioVM()
            {
                ScenarioChoice = HttpContext.Session.GetString("Scenario")
            };
            Scenario scenario = scenarioVM.Scenario;

            scenario.ActiveModule = scenario.Modules[scenario.StartingModuleIndex];
            scenario.PlayerParty[0] = PlayerCharacter;
            scenario.UpdateModuleParty();

            GameData gameData = new GameData()
            {
                UserID = 1, // TODO: implement userID
                SaveGameSlot = slot,
                ActiveScenario = scenario
            };
            SaveGameDAO.SaveGame(gameData.UserID, slot, gameData);

            HttpContext.Session.SetString("UserID", "1"); // TODO: remove when userID implemented

            return RedirectToAction("Index", "Game", slot);
        }
    }
}