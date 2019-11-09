using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TownAttackRPG.Models.Scenarios
{
    interface IDialogue
    {
        List<string> Dialogue { get; set; } // TODO: make a dialogue class to hide some of the complexity
        Dictionary<int, Dictionary<int, string>> Responses { get; set; }
    }
}
