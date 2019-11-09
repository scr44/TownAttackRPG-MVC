using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownAttackRPG.Models.Scenarios.Tutorial.Dialogue
{
    public class Tutorial_Dialogue_01_Welcome : Module, IDialogue
    {
        public Tutorial_Dialogue_01_Welcome()
        {
            Name = "Tutorial_Dialogue_01_Welcome";
        }

        public List<string> Dialogue { get; set; } = new List<string>()
        {
            
        };

        public Dictionary<int, Dictionary<int, string>> Responses { get; set; } = new Dictionary<int, Dictionary<int, string>>()
        {

        };

        
    }
}
