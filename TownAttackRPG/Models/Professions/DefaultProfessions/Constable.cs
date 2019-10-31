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
    public class Constable : Profession
    {
        public Constable(string gender = "M")
        {
            Title = "Constable";
            Gender = GetGender(gender);
            ProfessionSummary = "The town's venerable lawman. Knows his way around locks and is " +
                "well-versed in breaking up brawls. This stiffness of age is beginning to set in.";
            BaseHealth = 20;
            BaseStamina = 20;
            BaseStaminaRegen = 10.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 6 },
                { "DEX", 3 },
                { "SKL", 6 },
                { "APT", 4 },
                { "FOR", 5 },
                { "CHA", 5 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 1 },
                { "Bestiary", 0 },
                { "Engineering", 1 },
                { "History", 0 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { Item.CreateNew("Item", "Coins"), 1000 },
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
