using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.VendorTrash;
using TownAttackRPG.Models.Skills;
using TownAttackRPG.Models.Skills.Techniques.Swords;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Items.Equipment;

namespace TownAttackRPG.Models.Professions.DefaultProfessions
{
    public class Knight : Profession
    {
        public Knight(string gender="M")
        {
            Title = "Knight";
            Gender = GetGender(gender);
            ProfessionSummary = "Knights are masters of the longsword clad in sturdy plate armor; but " +
                "they often neglect their academic studies in favor of drinking and skirt-chasing.";
            BaseHealth = 20;
            BaseStamina = 20;
            BaseStaminaRegen = 15.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 7 },
                { "DEX", 7 },
                { "SKL", 7 },
                { "APT", 2 },
                { "FOR", 6 },
                { "CHA", 4 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 2 },
                { "Bestiary", 0 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { Item.CreateNew("Item", "Coins"), 800 },
                { new Memento(), 1 }
            };
            StartingEquipmentDict = new Dictionary<string, Items.Equipment.EquipmentItem>()
            {
                { "MainHand", (EquipmentItem)Item.CreateNew("Equipment","Longsword") },
                { "OffHand", (EquipmentItem)Item.CreateNew("Equipment","Two-handing") },
                { "Body", (EquipmentItem)Item.CreateNew("Equipment","Plate Armor") },
                { "Charm 1", (EquipmentItem)Item.CreateNew("Equipment","Lover's Locket") },
                { "Charm 2", (EquipmentItem)Item.CreateNew("Equipment","Unadorned") }
            };
            StartingSkills = new List<Skill>(6) { new DoubleSlash(), null, null, null, null, null };
        }
    }
}
