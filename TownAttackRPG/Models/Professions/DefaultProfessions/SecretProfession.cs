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
    public class SecretProfession : Profession
    {
        public SecretProfession(string gender="F")
        {
            Title = "Pony";
            Gender = GetGender(gender);
            ProfessionSummary = "You've had about enough of these bandits and their horsing around.";
            BaseHealth = 100;
            BaseStamina = 40;
            BaseStaminaRegen = 20.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 8 },
                { "DEX", 8 },
                { "SKL", 1 },
                { "APT", 5 },
                { "FOR", 5 },
                { "CHA", 9 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 1 },
                { "Bestiary", 3 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { Item.CreateNew("Item", "Coins"), 500 },
                // singed portrait
            };
            StartingEquipmentDict = new Dictionary<string, EquipmentItem>()
            {
                { "MainHand", (EquipmentItem)Item.CreateNew("Equipment","Bare Hand") }, // Hoof
                { "OffHand", (EquipmentItem)Item.CreateNew("Equipment","Bare Hand") }, // Hoof
                { "Body", (EquipmentItem)Item.CreateNew("Equipment","Naked") }, // Shiny Fur Coat
                { "Charm 1", (EquipmentItem)Item.CreateNew("Equipment","Unadorned") }, // Flower Ornament
                { "Charm 2", (EquipmentItem)Item.CreateNew("Equipment","Unadorned") } // Tail Bow
            };
        }
    }
}
