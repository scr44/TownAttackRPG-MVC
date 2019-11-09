using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Scenarios;
using TownAttackRPG.Models.Scenarios.Campaign.Tutorial;

namespace TownAttackRPG.Models.ViewModels.NewGame
{
    public class ScenarioVM
    {
        [Required]
        public string ScenarioChoice { get; set; }
        public Scenario Scenario
        {
            get
            {
                switch(ScenarioChoice.ToLower())
                {
                    case "tutorial":
                        return new Tutorial();
                    case "townattackclassic":
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentException("Invalid Scenario chosen");
                }
            }
        }
    }
}
