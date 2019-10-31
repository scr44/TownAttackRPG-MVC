using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Actors.SkillCollections;
using TownAttackRPG.Models.Effects;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors
{
    public abstract class Actor
    {
        public Actor(string name)
        {
            Name = name;
        }
        public string Name { get; protected set; }
        public string Gender { get; set; }
        #region Pronouns
        // Pronouns for string interpolation in events and combat
        public string HisHer
        {
            get
            {
                if (Gender == "Male")
                {
                    return "His";
                }
                else if (Gender == "Female")
                {
                    return "Her";
                }
                else
                {
                    return "Its";
                }
            }
        }
        public string HisHers
        {
            get
            {
                if (Gender == "Male")
                {
                    return "His";
                }
                else if (Gender == "Female")
                {
                    return "Hers";
                }
                else
                {
                    return "Its";
                }
            }
        }
        public string HeShe
        {
            get
            {
                if (Gender == "Male")
                {
                    return "He";
                }
                else if (Gender == "Female")
                {
                    return "She";
                }
                else
                {
                    return "It";
                }
            }
        }
        #endregion

        #region Health, Stamina, XP
        public bool IsAlive
        {
            get
            {
                return HP.Current > 0;
            }
        }
        abstract public Health HP { get; protected set; }
        abstract public Stamina SP { get; protected set; }

        public double Damaged(double dmgRaw, string dmgType, double dmgAP = 0, bool weaponBlock = false)
        {
            // PROT reduces damage multiplicatively
            double reducedDmg = dmgRaw * (1 - PROT(dmgType, weaponBlock));
            // A portion of the blocked damage gets through with the armor piercing multiplier
            double armorPiercingDmg = (dmgRaw - reducedDmg) * dmgAP;
            // calculate the total amount of damage the character will actually take
            double totalDmgTaken = -1 * (reducedDmg + armorPiercingDmg);

            // take the damage
            HP.AdjustHP(totalDmgTaken);

            return totalDmgTaken;
        }
        #endregion

        #region Skillbar
        public Skillbar Skillbar { get; protected set; }
        #endregion

        #region Active Effects
        public ActiveEffects ActiveEffects { get; protected set; } = new ActiveEffects();
        virtual public double EffectMod(string stat)
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
        abstract public double DMG(string dmgType);
        public double ArmorPiercing = 0;
        #endregion

        #region Defense
        abstract public double PROT(string dmgType, bool weaponBlock);
        #endregion

        #region Rewards
        public int XPReward { get; protected set; }
        public Inventory LootDrops { get; protected set; }
        #endregion
    }
}
