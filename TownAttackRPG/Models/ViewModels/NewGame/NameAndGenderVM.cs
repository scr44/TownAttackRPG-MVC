using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using TownAttackRPG.Models.Scenarios;

namespace TownAttackRPG.Models.ViewModels.Main
{
    public class NameAndGenderVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Gender { get; set; }
    }
}
