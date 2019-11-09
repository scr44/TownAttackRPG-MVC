using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.VendorTrash;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Items.Equipment;

namespace TownAttackRPG.Models.Professions.DefaultProfessions
{
    public class Scholar : Profession
    {
        public Scholar(string gender = "M")
        {
            Title = "Scholar";
            Gender = GetGender(gender);
            ProfessionSummary = "The scholar is a quick learner and widely knowledgeable, but spends more" +
                "time reading books than on physical pursuits.";
            BaseHealth = 20;
            BaseStamina = 15;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 2 },
                { "DEX", 2 },
                { "SKL", 3 },
                { "APT", 9 },
                { "FOR", 2 },
                { "CHA", 3 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 1 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 1 },
                { "Engineering", 1 },
                { "History", 2 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { ItemDAO.CreateNewItem("Coins"), 400 },
                { new Diploma(), 1 }
            };
            StartingEquipmentDict = new Dictionary<string, Items.Equipment.EquipmentItem>()
            {
                { "MainHand", ItemDAO.CreateNewEquipmentItem("Bare Hand") },
                { "OffHand", ItemDAO.CreateNewEquipmentItem("History Tome") },
                { "Body", ItemDAO.CreateNewEquipmentItem("Clothing") },
                { "Charm 1", ItemDAO.CreateNewEquipmentItem("Quill and Inkwell") },
                { "Charm 2", ItemDAO.CreateNewEquipmentItem("Unadorned") }
            };
        }
    }
}
