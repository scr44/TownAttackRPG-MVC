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
    public class Alchemist : Profession
    {
        public Alchemist(string gender="M")
        {
            Title = "Alchemist";
            Gender = GetGender(gender);
            ProfessionSummary = "Recently expelled from the university for burning down the " +
                "research hall, this pyromaniac may be more dangerous to their teammates than their enemies.";
            BaseHealth = 40;
            BaseStamina = 20;
            BaseStaminaRegen = 15.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 2 },
                { "DEX", 2 },
                { "SKL", 6 },
                { "APT", 7 },
                { "FOR", 3 },
                { "CHA", 1 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 3 },
                { "Veterancy", 0 },
                { "Bestiary", 0 },
                { "Engineering", 2 },
                { "History", 0 }
            };
            StartingInventoryList = new List<Item>()
            {
                ItemDAO.CreateNewItem("Coins", 250),
                // singed portrait
            };
            StartingEquipmentDict = new Dictionary<string, EquipmentItem>()
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
