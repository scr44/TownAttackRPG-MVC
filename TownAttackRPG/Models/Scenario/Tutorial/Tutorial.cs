using TownAttackRPG.Views.Menus.Combat;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Party;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using TownAttackRPG.Models.Scenario.Tutorial.Combat;
using TownAttackRPG.Models.Scenario.Tutorial.Dialogue;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Scenario.Campaign.Tutorial
{
    public class Tutorial
    {
        public Tutorial()
        {
            PC = new Character("Guinevere", new Knight("F"));
            Party.AddPartyMember((Actor)PC);
            CombatUI = new CombatUI(PC);
            Tutorial_Combat_01_TrainingDummy = new Tutorial_Combat_01_TrainingDummy(Party);
        }
        Character PC { get; }
        Party Party { get; set; } = new Party();
        CombatUI CombatUI { get; set; } = null;

        #region Modules
        Tutorial_Welcome_01 Tutorial_Welcome_01 { get; set; } = new Tutorial_Welcome_01();
        Tutorial_Combat_01_TrainingDummy Tutorial_Combat_01_TrainingDummy { get; set; } 
        #endregion

        public void RunScenario()
        {
            string Module = "Tutorial_Welcome_01";
            while (true)
            {
                switch(Module)
                {
                    case "Tutorial_Welcome_01":
                        Tutorial_Welcome_01.Run();
                        Module = "Tutorial_Combat_01_TrainingDummy";
                        break;
                    case "Tutorial_Combat_01_TrainingDummy":
                        Tutorial_Combat_01_TrainingDummy.Run();
                        break;
                    default:
                        return;
                }
            }
        }
    }
}
