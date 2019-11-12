using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;

namespace TownAttackRPG.Models.Scenarios
{
    abstract public class Scenario
    {
        public string Name { get; protected set; }
        [JsonProperty]
        public List<Actor> PlayerParty { get; set; } = new List<Actor>();
        public Character PlayerCharacter => (Character)PlayerParty[0];
        public void UpdateScenarioParty()
        {
            PlayerParty = ActiveModule.PlayerParty;
        }
        public void UpdateModuleParty()
        {
            ActiveModule.PlayerParty = PlayerParty;
        }

        #region Modules
        public int StartingModuleIndex { get; set; }
        public Module ActiveModule { get; set; }
        public List<Module> Modules { get; set; }
        public void ChangeModule(int index)
        {
            UpdateScenarioParty();
            ActiveModule = Modules[index];
            ActiveModule.PlayerParty = PlayerParty;
        }
        public void RandomCombatModule()
        {
            // TODO: go to random module that implements ICombat
        }
        #endregion
    }
}
