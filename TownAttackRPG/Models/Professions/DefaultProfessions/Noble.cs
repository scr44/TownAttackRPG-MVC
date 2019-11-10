using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Items.VendorTrash;

namespace TownAttackRPG.Models.Professions.DefaultProfessions
{
    public class Noble : Profession
    {
        public Noble(string gender = "F")
        {
            Title = "Noble";
            Gender = GetGender(gender);
            if(Gender == "Male")
            {
                Title = "Nobleman";
            }
            else if(Gender == "Female")
            {
                Title = "Noblewoman";
            }
            ProfessionSummary = "Wealthy and fashionable, a member of the noble class well-educated in " +
                "the art of oration. Has some training as a duelist, but has never been in a real fight.";
            BaseHealth = 20;
            BaseStamina = 10;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 3 },
                { "DEX", 5 },
                { "SKL", 7 },
                { "APT", 5 },
                { "FOR", 3 },
                { "CHA", 8 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 0 },
                { "Engineering", 0 },
                { "History", 2 }
            };
            StartingInventoryList = new List<Item>()
            {
                ItemDAO.CreateNewItem("Coins",3000),
            };
            StartingEquipmentDict = new Dictionary<string, Items.Equipment.EquipmentItem>()
            {
                { "MainHand", ItemDAO.CreateNewEquipmentItem("Bare Hand") }, // Rapier
                { "OffHand", ItemDAO.CreateNewEquipmentItem("Bare Hand") }, // Bare Hand
                { "Body", ItemDAO.CreateNewEquipmentItem("Fancy Clothing") },
                { "Charm 1", ItemDAO.CreateNewEquipmentItem("Ladybug Brooch") },
                { "Charm 2", ItemDAO.CreateNewEquipmentItem("Heirloom Ring") }
            };
        }
    }
}
