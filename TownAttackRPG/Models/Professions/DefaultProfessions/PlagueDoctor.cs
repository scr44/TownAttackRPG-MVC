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
    public class PlagueDoctor : Profession
    {
        public PlagueDoctor(string gender = "F")
        {
            Title = "Plague Doctor";
            Gender = GetGender(gender);
            ProfessionSummary = "This traveling surgeon wears an unsettling beak-shaped mask and " +
                "goggles. Her cures are of dubious merit, but the efficacy of her poisons is inarguable.";
            BaseHealth = 20;
            BaseStamina = 20;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 4 },
                { "DEX", 3 },
                { "SKL", 6 },
                { "APT", 5 },
                { "FOR", 6 },
                { "CHA", 2 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 3 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 2 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryList = new List<Item>()
            {
                ItemDAO.CreateNewItem("Coins",900),
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
