﻿using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Items.VendorTrash;

namespace TownAttackRPG.Models.Professions.DefaultProfessions
{
    public class Huntress : Profession
    {
        public Huntress(string gender="F")
        {
            Title = "Huntress";
            Gender = GetGender(gender);
            if (gender == "Male")
            {
                Title = "Hunter";
            }
            ProfessionSummary = $"A dangerous {(gender == "female" ? "huntress" : "hunter")} from the wildling " +
                $"tribes. Knows how to hunt nearly any creature, but has no social graces.";
            BaseHealth = 20;
            BaseStamina = 20;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 4 },
                { "DEX", 8 },
                { "SKL", 7 },
                { "APT", 5 },
                { "FOR", 4 },
                { "CHA", 1 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 3 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryList = new List<Item>()
            {
                // Mushroom Brew
                // Herbal Poultice
                // Witch's Ointment
                // Pungent Nuts
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
