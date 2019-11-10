using TownAttackRPG.Models.Items;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.Characters.Stats
{
    public class Attributes
    {
        #region Constructors
        /// <summary>
        /// Sets all Attributes to the base level (5).
        /// </summary>
        public Attributes()
        {

        }
        /// <summary>
        /// Sets actor's base attributes to given levels.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="dex"></param>
        /// <param name="skl"></param>
        /// <param name="apt"></param>
        /// <param name="fort"></param>
        /// <param name="cha"></param>
        public Attributes(Character character, int str, int dex, int skl, int apt, int fort, int cha)
        {
            this.AttachedCharacter = character;
            BaseValue["STR"] = str;
            BaseValue["DEX"] = dex;
            BaseValue["SKL"] = skl;
            BaseValue["APT"] = apt;
            BaseValue["FOR"] = fort;
            BaseValue["CHA"] = cha;
        }
        public Attributes(Character character, Dictionary<string, int> initDict)
        {
            this.AttachedCharacter = character;
            this.BaseValue = initDict;
        }
        #endregion

        public Character AttachedCharacter { get; }

        /// <summary>
        /// Dictionary of Attributes
        /// </summary>
        public Dictionary<string, int> BaseValue { get; private set; } = new Dictionary<string, int>()
        {
            { "STR", 5 },
            { "DEX", 5 },
            { "SKL", 5 },
            { "APT", 5 },
            { "FOR", 5 },
            { "CHA", 5 }
        };
        /// <summary>
        /// Dictionary of Attributes + AttributeMods
        /// </summary>
        public Dictionary<string, int> ModdedValue
        {
            get
            {
                if (AttachedCharacter is null)
                {
                    return null;
                }
                return new Dictionary<string, int>()
                {
                    { "STR", BaseValue["STR"] + (int)AttachedCharacter.EquipmentMod("STR", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("STR") },
                    { "DEX", BaseValue["DEX"] + (int)AttachedCharacter.EquipmentMod("DEX", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("DEX") },
                    { "SKL", BaseValue["SKL"] + (int)AttachedCharacter.EquipmentMod("SKL", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("SKL") },
                    { "APT", BaseValue["APT"] + (int)AttachedCharacter.EquipmentMod("APT", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("APT") },
                    { "FOR", BaseValue["FOR"] + (int)AttachedCharacter.EquipmentMod("FOR", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("FOR") },
                    { "CHA", BaseValue["CHA"] + (int)AttachedCharacter.EquipmentMod("CHA", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("CHA") },
                };
            }
        }

        /// <summary>
        /// Changes an Attribute by the given points.
        /// </summary>
        /// <param name="stat">STR, DEX, SKL, APT, PER, or CHA</param>
        /// <param name="points">Positive points to increase, negative points to decrease.</param>
        public void AdjustAttribute(string stat, int points)
        {
            if (BaseValue.ContainsKey(stat))
            {
                stat = stat.ToUpper();
                BaseValue[stat] += points;
                if (BaseValue[stat] < 1)
                {
                    BaseValue[stat] = 1;
                }
                else if (BaseValue[stat] > 10)
                {
                    BaseValue[stat] = 10;
                }
            }
            else
            {
                throw new ArgumentException("Tried to change an invalid Attribute.");
            }
        }
        /// <summary>
        /// Sets the given stat's base level to the given points. Cannot go lower than 1 or higher than 10.
        /// </summary>
        /// <param name="stat"></param>
        /// <param name="points"></param>
        public void SetAttribute(string stat, int points)
        {
            if (BaseValue.ContainsKey(stat))
            {
                stat = stat.ToUpper();
                if (points < 1)
                {
                    points = 1;
                }
                else if (points > 10)
                {
                    points = 10;
                }
                BaseValue[stat] = points;
            }
            else
            {
                throw new ArgumentException("Tried to change an invalid Attribute.");
            }
        }
    }
}
