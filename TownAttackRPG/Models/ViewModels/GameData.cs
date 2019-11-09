using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Scenarios;

namespace TownAttackRPG.Models.ViewModels.Game
{
    public class GameData
    {
        public int UserID { get; set; }
        public int SaveGameSlot { get; set; }
        public Scenario ActiveScenario { get; set; }
    }
}
