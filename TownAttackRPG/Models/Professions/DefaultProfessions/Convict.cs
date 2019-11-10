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
    public class Convict : Profession
    {
        public Convict(string gender="F")
        {
            Title = "Convict";
            Gender = GetGender(gender);
            ProfessionSummary = "A thief arrested for attempting to steal horses, on their way " +
                "to the gallows. Weak, but exceptionally fast and clever.";
            BaseHealth = 20;
            BaseStamina = 20;
            BaseStaminaRegen = 20.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 2 },
                { "DEX", 9 },
                { "SKL", 5 },
                { "APT", 7 },
                { "FOR", 3 },
                { "CHA", 4 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 1 },
                { "Bestiary", 2 },
                { "Engineering", 2 },
                { "History", 0 }
            };
            StartingInventoryList = new List<Item>()
            {

            };
            StartingEquipmentDict = new Dictionary<string, Items.Equipment.EquipmentItem>()
            {
                { "MainHand", ItemDAO.CreateNewEquipmentItem("Bare Hand") },
                { "OffHand", ItemDAO.CreateNewEquipmentItem("Bare Hand") },
                { "Body", ItemDAO.CreateNewEquipmentItem("Naked") },
                { "Charm 1", ItemDAO.CreateNewEquipmentItem("Unadorned") },
                { "Charm 2", ItemDAO.CreateNewEquipmentItem("Unadorned") }
            };
        }
    }
}
