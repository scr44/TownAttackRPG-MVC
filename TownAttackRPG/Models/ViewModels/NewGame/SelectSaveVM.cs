using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.ViewModels.Game;

namespace TownAttackRPG.Models.ViewModels.NewGame
{
    public class SelectSaveVM
    {
        public List<GameData> SaveList { get; set; }
        public int Slot { get; set; }
    }
}
