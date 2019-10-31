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
    public class Barmaid : Profession
    {
        public Barmaid(string gender = "F")
        {
            Title = "Barmaid";
            Gender = GetGender(gender);
            if(gender == "Male")
            {
                Title = "Barkeep";
            }
            ProfessionSummary = $"The charming {(gender == "female" ? "young barmaid" : "barkeep")} from the local tavern. Strong and fast, with " +
                "a seemingly endless supply of drink, but no combat training.";
            BaseHealth = 40;
            BaseStamina = 20;
            BaseStaminaRegen = 15.0;
            StartingAttributesDict = new Dictionary<string, int>()
            {
                { "STR", 7 },
                { "DEX", 6 },
                { "SKL", 1 },
                { "APT", 4 },
                { "FOR", 3 },
                { "CHA", 8 }
            };
            StartingTalentsDict = new Dictionary<string, int>()
            {
                { "Medicine", 0 },
                { "Explosives", 0 },
                { "Veterancy", 0 },
                { "Bestiary", 0 },
                { "Engineering", 0 },
                { "History", 0 }
            };
            StartingInventoryDict = new Dictionary<Item, int>()
            {
                { Item.CreateNew("Item", "Coins"), 250 },
                //{ new BottomlessBeerMug(), 1 }
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
