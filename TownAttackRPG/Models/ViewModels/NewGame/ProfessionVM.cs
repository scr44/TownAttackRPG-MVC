using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Professions.DefaultProfessions;

namespace TownAttackRPG.Models.ViewModels.NewGame
{
    public class ProfessionVM
    {
        public string ProfessionChoice { get; set; }
        public string Gender { get; set; } = "M";
        public Profession Profession
        {
            get
            {
                switch(ProfessionChoice.ToLower())
                {
                    case "knight":
                        return new Knight(Gender);
                    case "alchemist":
                        return new Alchemist(Gender);
                    case "barmaid":
                        return new Barmaid(Gender);
                    case "blacksmith":
                        return new Blacksmith(Gender);
                    case "constable":
                        return new Constable(Gender);
                    case "convict":
                        return new Convict(Gender);
                    case "footman":
                        return new Footman(Gender);
                    case "huntress":
                        return new Huntress(Gender);
                    case "noble":
                        return new Noble(Gender);
                    case "plaguedoctor":
                        return new PlagueDoctor(Gender);
                    case "scholar":
                        return new Scholar(Gender);
                    case "squire":
                        return new Squire(Gender);
                    case "secret":
                        return new SecretProfession(Gender);
                    default:
                        throw new ArgumentException("Invalid profession chosen");
                }
            }
        }
        public List<Profession> ProfessionList => new List<Profession>()
        {
            new Knight(),
            new Alchemist(),
            new Barmaid(),
            new Blacksmith(),
            new Constable(),
            new Convict(),
            new Footman(),
            new Huntress(),
            new Noble(),
            new PlagueDoctor(),
            new Scholar(),
            new Squire(),
            new SecretProfession()
        };
    }
}
