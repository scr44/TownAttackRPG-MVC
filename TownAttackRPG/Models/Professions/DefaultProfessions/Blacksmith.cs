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
    public class Blacksmith : Profession
    {
        public Blacksmith(string gender="M")
        {
            Title = "Blacksmith";
            Gender = GetGender(gender);
            ProfessionSummary = "The town blacksmith is incredibly strong and knows all the weaknesses " +
                "of every kind of armor, but slow and gruff.";
            BaseHealth = 40;
            BaseStamina = 20;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 9 },
                { "DEX", 2 },
                { "SKL", 7 },
                { "APT", 3 },
                { "FOR", 6 },
                { "CHA", 3 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 1 },
                { "Veterancy", 0 },
                { "Bestiary", 0 },
                { "Engineering", 3 },
                { "History", 0 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { Item.CreateNew("Item", "Coins"), 1200 },
            };
            StartingEquipmentDict = new Dictionary<string, Items.Equipment.EquipmentItem>()
            {
                { "MainHand", (EquipmentItem)Item.CreateNew("Equipment","Bare Hand") },
                { "OffHand", (EquipmentItem)Item.CreateNew("Equipment","Bare Hand") },
                { "Body", (EquipmentItem)Item.CreateNew("Equipment","Naked") },
                { "Charm 1", (EquipmentItem)Item.CreateNew("Equipment","Unadorned") },
                { "Charm 2", (EquipmentItem)Item.CreateNew("Equipment","Unadorned") }
            };
        }
    }
}
