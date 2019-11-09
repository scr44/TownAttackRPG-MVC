using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using TownAttackRPG.Models.Scenarios.Tutorial.Dialogue;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Scenarios.Campaign.Tutorial
{
    public class Tutorial : Scenario
    {
        public Tutorial()
        {
            Name = "Tutorial";
            StartingModuleIndex = 0;
            Modules = new List<Module>()
            {
                new Tutorial_Dialogue_01_Welcome(),
                // TODO Tutorial Modules: Training Dummy Battle
                // TODO Tutorial Modules: Victory Screen
                // TODO Tutorial Modules: Character Menu
                // TODO Tutorial Modules: Inventory Menu
                // TODO Tutorial Modules: Level Up Menu
            };
        }
    }
}
