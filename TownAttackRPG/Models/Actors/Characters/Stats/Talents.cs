using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.Characters.Stats
{
    public class Talents
    {
        #region Talent Descriptions
        /* 
        Medicine: The art of healing wounds and diagnosing injuries.

        Explosives: The knowledge to craft throwable weapons.

        Veterancy: Skill at identifying human enemies.Bonuses against human opponents, and can unlock special event actions.

        Bestiary: Skill at identifying beasts. Bonuses against beast enemies, and can unlock special event actions.

        Engineering: Skill at operating and understanding machines. Bonuses against mechanical enemies, and can unlock special event actions.

        History: Skill at identifying ancient relics. Can unlock special event actions.
        */
        #endregion

        #region Constructors
        public Talents() { }
        public Talents(Character character, int med, int expl, int vet, int best, int eng, int hist)
        {
            this.AttachedCharacter = character;
            BaseValue["Medicine"] = med;
            BaseValue["Explosives"] = expl;
            BaseValue["Veterancy"] = vet;
            BaseValue["Bestiary"] = best;
            BaseValue["Engineering"] = eng;
            BaseValue["History"] = hist;
        }
        public Talents(Character character, Dictionary<string,int> initDict)
        {
            this.AttachedCharacter = character;
            this.BaseValue = initDict;
        }
        #endregion

        public Character AttachedCharacter { get; }

        /// <summary>
        /// Dictionary of Talents.
        /// </summary>
        public Dictionary<string, int> BaseValue { get; private set; }
            = new Dictionary<string, int>()
                {
                    { "Medicine", 0 },
                    { "Explosives", 0 },
                    { "Veterancy", 0 },
                    { "Bestiary", 0 },
                    { "Engineering", 0 },
                    { "History", 0 }
                };
        /// <summary>
        /// Dictionary of Attributes + AttributeMods
        /// </summary>
        public Dictionary<string, int> ModdedValue
        {
            get
            {
                return new Dictionary<string, int>()
                {
                    { "Medicine", BaseValue["Medicine"] + (int)AttachedCharacter.EquipmentMod("Medicine", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("Medicine") },
                    { "Explosives", BaseValue["Explosives"] + (int)AttachedCharacter.EquipmentMod("Explosives", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("Explosives") },
                    { "Veterancy", BaseValue["Veterancy"] + (int)AttachedCharacter.EquipmentMod("Veterancy", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("Veterancy") },
                    { "Bestiary", BaseValue["Bestiary"] + (int)AttachedCharacter.EquipmentMod("Bestiary", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("Bestiary") },
                    { "Engineering", BaseValue["Engineering"] + (int)AttachedCharacter.EquipmentMod("Engineering", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("Engineering") },
                    { "History", BaseValue["History"] + (int)AttachedCharacter.EquipmentMod("History", AttachedCharacter.Equipment) + (int)AttachedCharacter.EffectMod("History") },
                };
            }
        }

        /// <summary>
        /// Changes a Talent by the given number of points.
        /// </summary>
        /// <param name="stat">Medicine, Speech, Herbalim, BombCrafting, Bestiary, Veterancy, Engineering, or History.</param>
        /// <param name="points">Positive points to increase, negative points to decrease.</param>
        public void AdjustTalent(string stat, int points)
        {
            if (BaseValue.ContainsKey(stat))
            {
                BaseValue[stat] += points;
            }
            else
            {
                throw new ArgumentException("Tried to change an invalid Talent.");
            }
        }
    }
}
