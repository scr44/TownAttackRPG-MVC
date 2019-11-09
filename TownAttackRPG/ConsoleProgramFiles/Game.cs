using TownAttackRPG.Models.Actors.Characters;
using System;
using System.Collections.Generic;
using TownAttackRPG.Models.Scenarios.Campaign.Tutorial;
using TownAttackRPG.Views.Menus.Startup;
using TownAttackRPG.Views.Menus.InfoPages;

namespace TownAttackRPG
{
    public class Game
    {
        #region Menu Objects
        SplashScreen SplashScreen = new SplashScreen();
        MainMenu MainMenu = new MainMenu();
        ScenarioSelect ScenarioMenu = new ScenarioSelect();
        ProfessionSelect ProfessionMenu = new ProfessionSelect();
        #endregion

        #region Menu Selections
        public void BootUpConsole()
        {
            Console.SetWindowSize(130, 50);
            //Console.SetWindowSize(130, 40);
        }
        public void RunSplashScreen()
        {
            SplashScreen.FlashingPrompt();
            SplashScreen.AscendingTitle();
            Console.ReadKey();
        }
        public string RunMainMenu()
        {
            while (Selection == "Main Menu")
            {
                Selection = MainMenu.DisplayOptions();
                switch (Selection)
                {
                    case "New Game":
                        Selection = "Scenario Menu";
                        break;

                    case "Continue":
                        FeatureIncompleteInfoPage.Display();
                        Selection = "Main Menu";
                        break;

                    case "Options":
                        FeatureIncompleteInfoPage.Display();
                        Selection = "Main Menu";
                        break;

                    case "Exit Program":
                        Selection = "Exit Program";
                        break;

                    default:
                        Selection = "Main Menu";
                        break;
                }
            }
            return Selection;
        }
        public string RunScenarioSelect()
        {
            while (Selection == "Scenario Menu")
            {
                Selection = ScenarioMenu.DisplayOptions();
                switch (Selection)
                {
                    case "Begin Scenario: Tutorial":
                        Selection = "Scenario: Tutorial";
                        break;

                    case "Begin Scenario: Town Attack Classic":
                        Selection = "Scenario: Town Attack Classic";

                        FeatureIncompleteInfoPage.Display();
                        Selection = "Scenario Menu";
                        break;

                    default:
                        Selection = "Main Menu";
                        break;
                }
            }
            return Selection;
        }
        public string RunProfessionMenu()
        {
            while (Selection == "Profession Menu")
            {
                Selection = ProfessionMenu.DisplayOptions();
                switch (Selection)
                {
                    case "Knight":
                        Selection = "Main Menu";
                        break;

                    case "Scholar":
                        Selection = "Main Menu";
                        break;

                    default:
                        Selection = "Scenario Menu";
                        break;
                }
            }
            return Selection;
        }
        #endregion

        public string Selection { get; private set; } = "Main Menu";

        public void Start()
        {
            BootUpConsole();
            RunSplashScreen();
            while(true)
            {
                switch (Selection)
                {
                    case "Main Menu":
                        Selection = RunMainMenu();
                        break;
                    case "Scenario Menu":
                        Selection = RunScenarioSelect();
                        break;
                    case "Character Creation":

                        break;
                    //case "Profession Menu":

                    //    break;
                    case "Scenario: Tutorial":
                        LoadingScreen.Display(500);
                        Tutorial Tutorial = new Tutorial();
                        break;
                    case "Exit Program":
                        Environment.Exit(0);
                        break;
                }
            }
        }

       
    }
}
