using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Actors.SkillCollections;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.DAL.DAOs.Json;

namespace TownAttackRPG.Models.Professions
{
    abstract public class Profession
    {
        #region Constructor
        const string jsonPath = @"./DAL/JsonData";
        protected IItemDAO ItemDAO { get; set; }

        public Profession()
        {
            ItemDAO = new ItemJsonDAO(jsonPath);
        }
        #endregion

        #region Tags
        public string Title { get; protected set; }
        public string Gender { get; set; }
        public string GetGender(string gender)
        {
            gender = gender.ToLower();
            if (gender == "male" || gender == "m")
            {
                Gender = "Male";
            }
            else if (gender == "female" || gender == "f")
            {
                Gender = "Female";
            }
            else
            {
                Gender = "Unknown";
            }
            return Gender;
        }
        #endregion

        #region Profession Stats
        public string ProfessionSummary { get; protected set; }
        public int BaseHealth { get; protected set; }
        public int BaseStamina { get; protected set; }
        public double BaseStaminaRegen { get; protected set; }
        #endregion

        #region Initalizers
        public Dictionary<string, int> StartingAttributesDict { get; protected set; } =
            new Dictionary<string, int>()
            {
                { "STR", 5 },
                { "DEX", 5 },
                { "SKL", 5 },
                { "APT", 5 },
                { "FOR", 5 },
                { "CHA", 5 }
            };
        public Dictionary<string, int> StartingTalentsDict { get; protected set; } =
        new Dictionary<string, int>()
        {
            { "Medicine", 0 },
            { "Explosives", 0 },
            { "Veterancy", 0 },
            { "Bestiary", 0 },
            { "Engineering", 0 },
            { "History", 0 }
        };
        public Dictionary<Item, int> StartingInventoryDict { get; protected set; }
        public Dictionary<string, EquipmentItem> StartingEquipmentDict { get; protected set; }
        public List<Skill> StartingSkills = new List<Skill>(6) { null, null, null, null, null, null };
        #endregion
    }
}
