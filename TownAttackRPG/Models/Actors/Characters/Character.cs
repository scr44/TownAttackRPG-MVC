using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Professions;
using TownAttackRPG.Models.Effects;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors.SkillCollections;
using TownAttackRPG.Models.Actors.ActorProperties;
using Newtonsoft.Json;

namespace TownAttackRPG.Models.Actors.Characters
{
    public class Character : Actor
    {
        [JsonConstructor]
        public Character(string name) : base(name)
        {
            // just here for json serialization
        }
        public Character(string name, Profession prof) : base(name, prof)
        {
            Name = name;
            Gender = prof.Gender ?? "Male";
            Profession = prof;
            Attributes = new Attributes(this, prof.StartingAttributesDict);
            Talents = new Talents(this, prof.StartingTalentsDict);
            Inventory = new Inventory(this, prof.StartingInventoryList);
            Equipment = new Equipment(this, prof.StartingEquipmentDict);
            Skillbar = new Skillbar(this, prof.StartingSkills);

            HP = new Health(this);
            SP = new Stamina(this);
            HP.AdjustHP(9999); // Start off at full health (account for equip mods).
            SP.AdjustSP(999); // Start off at full stamina (account for equip mods).
            XP = new Experience(this, 1);
        }

        #region Tags
        public string FirstName
        {
            get
            {
                string[] split = Name.Split(' ');
                if (split.Length > 1)
                {
                    return split[0];
                }
                else
                {
                    return Name;
                }
            }
        }
        public string LastName
        {
            get
            {
                string[] split = Name.Split(' ');
                if (split.Length > 1)
                {
                    return split[split.Length - 1];
                }
                else
                {
                    return Name;
                }
            }
        }
        public Profession Profession { get; }
        public List<string> CharacterTags { get; private set; } =
            new List<string>();
        #endregion

        #region Inventory
        /// <summary>
        /// The items currently held by the character.
        /// </summary>
        public Inventory Inventory { get; private set; }
        #endregion

        #region Equipment
        public Equipment Equipment { get; private set; }
        #endregion

        #region Attributes and Talents
        public Attributes Attributes { get; set; }
        public Talents Talents { get; set; }
        #endregion

        #region Health, Stamina, XP
        override public Health HP { get; protected set; }
        override public Stamina SP { get; protected set; }
        public Experience XP { get; private set; }
        #endregion

        #region Skills
        // public Skillbar Skillbar { get; private set; }
        // Skill Library
        #endregion

        #region Stat Modifiers
        public double EquipmentMod(string stat, Equipment equipment)
        {
            double mod = 0;
            foreach (KeyValuePair<string, EquipmentItem> item in equipment.Slot)
            {
                foreach (KeyValuePair<string, double> itemStat in item.Value.WeaponStats)
                {
                    if (itemStat.Key == stat)
                    {
                        mod += itemStat.Value;
                    }
                }
                foreach (KeyValuePair<string, double> itemStat in item.Value.ArmorStats)
                {
                    if (itemStat.Key == stat)
                    {
                        mod += itemStat.Value;
                    }
                }
                foreach (KeyValuePair<string, double> itemStat in item.Value.CharmStats)
                {
                    if (itemStat.Key == stat)
                    {
                        mod += itemStat.Value;
                    }
                }
            }
            return mod;
        }
        public double SingleEquipmentMod(string stat, EquipmentItem item)
        {
            double mod = 0;
            foreach (KeyValuePair<string, double> itemStat in item.WeaponStats)
            {
                if (itemStat.Key == stat)
                {
                    mod += itemStat.Value;
                }
            }
            foreach (KeyValuePair<string, double> itemStat in item.ArmorStats)
            {
                if (itemStat.Key == stat)
                {
                    mod += itemStat.Value;
                }
            }
            foreach (KeyValuePair<string, double> itemStat in item.CharmStats)
            {
                if (itemStat.Key == stat)
                {
                    mod += itemStat.Value;
                }
            }
            return mod;
        }
        #endregion

        #region Active Effects
        new public ActiveEffects ActiveEffects { get; private set; }
            = new ActiveEffects();
        override public double EffectMod(string stat)
        {
            double mod = 0;
            foreach (Effect effect in this.ActiveEffects.EffectList)
            {
                if (effect.StatMod.ContainsKey(stat))
                {
                    mod += effect.StatMod[stat];
                }
            }
            return mod;
        }
        #endregion

        #region Offense
        public double EquipmentDmgMultiplier(string dmgType)
        {
            string dmgMult = dmgType + "Multiplier";
            return EquipmentMod(dmgMult,this.Equipment);
        }
        public double EffectDmgMultiplier(string dmgType)
        {
            string dmgMult = dmgType + "Multiplier";
            return EffectMod(dmgMult);
        }
        [JsonIgnore]
        public Dictionary<string, double> DmgScaling
        {
            get
            {
                return new Dictionary<string, double>()
                {
                    { "slash", 0.2 * (Attributes.ModdedValue["DEX"] - 5) },
                    { "pierce", 0.2 * (Attributes.ModdedValue["SKL"] - 5) },
                    { "crush", 0.2 * (Attributes.ModdedValue["STR"] - 5) },

                    { "poison", 0.1 * Talents.ModdedValue["Bestiary"] },
                    { "bleed", 0.1 * Talents.ModdedValue["Medicine"] },
                    { "fire", 0.1 * Talents.ModdedValue["Explosives"] },
                    { "acid", 0.1 * Talents.ModdedValue["Engineering"] },
                };
            }
        } // Attack scaling is additive
        public double Bonus2HScaling(string dmgType, bool is2H)
        {
            if (is2H && (
                   dmgType == "slash"
                || dmgType == "pierce"
                || dmgType == "crush"
                ))
            {
                return 0.2;
            }
            else
            {
                return 0.0;
            }
        }
        public new double ArmorPiercing => EquipmentMod("armorPiercing", Equipment); // consider making this single item--it's a bit silly right now

        override public double DMG(string dmgType)
        {
            return EquipmentDmgMultiplier(dmgType)
                + EffectDmgMultiplier(dmgType)
                + DmgScaling[dmgType] 
                + Bonus2HScaling(dmgType, this.Equipment.Is2H);
        }
        #endregion

        #region Defense
        public bool AttemptBlock(EquipmentItem equipment)
        {
            Random rand = new Random();
            if (equipment.ArmorStats["blockChance"] * .01 >= rand.NextDouble())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public double EquipmentPROT(string dmgType, bool weaponBlock = false)
        {
            double prot = 0;
            string dmgProt = dmgType + "PROT";
            if (weaponBlock)
            {
                prot += EquipmentMod(dmgProt, this.Equipment);
            }
            else
            {
                prot += SingleEquipmentMod(dmgProt, this.Equipment.Slot["Body"]);
                prot += SingleEquipmentMod(dmgProt, this.Equipment.Slot["Charm 1"]);
                prot += SingleEquipmentMod(dmgProt, this.Equipment.Slot["Charm 2"]);
            }

            return prot;
        }
        public double EffectPROT(string dmgType)
        {
            string dmgPROT = dmgType + "PROT";
            return EffectMod(dmgPROT);
        }
        [JsonIgnore]
        public Dictionary<string, double> PROTScaling
        {
            get
            {
                return new Dictionary<string, double>()
                {
                    { "slash", 0.05 * (Attributes.ModdedValue["FOR"] - 5) },
                    { "pierce", 0.05 * (Attributes.ModdedValue["FOR"] - 5)},
                    { "crush", 0.05 * (Attributes.ModdedValue["FOR"] - 5)},

                    { "poison", 0.25 * Talents.BaseValue["Bestiary"] },
                    { "bleed", 0.25 * Talents.BaseValue["Medicine"] },
                    { "fire", 0.25 * Talents.BaseValue["Explosives"] },
                    { "acid", 0.25 * Talents.BaseValue["Engineering"] },
                };
            }
        }

        override public double PROT(string dmgType, bool weaponBlock = false)
        {
            // Defense scaling is multiplicative with equipment PROT, 
            // otherwise becoming invincible would be too easy. (it's still pretty easy)
                return 
                    1 - (1 - EquipmentPROT(dmgType, weaponBlock))
                      * (1 - EffectPROT(dmgType))
                      * (1 - PROTScaling[dmgType]);
        }
        #endregion
    }
}
