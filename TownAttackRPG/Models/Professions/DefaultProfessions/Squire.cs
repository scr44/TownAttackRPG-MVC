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
    public class Squire : Profession
    {
        public Squire(string gender = "M")
        {
            Title = "Squire";
            Gender = GetGender(gender);
            ProfessionSummary = "A young farmboy from one of the surrounding villages, who's passing through " +
                "on his way to the castle to become a squire. Energetic, but clumsy.";
            BaseHealth = 20;
            BaseStamina = 30;
            BaseStaminaRegen = 15.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 5 },
                { "DEX", 5 },
                { "SKL", 2 },
                { "APT", 4 },
                { "FOR", 5 },
                { "CHA", 5 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 2 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryList = new List<Item>()
            {
                ItemDAO.CreateNewItem("Coins",100),
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
