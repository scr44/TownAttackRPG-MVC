using TownAttackRPG.Models.Actors.Enemies;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Professions;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TownAttackRPG.Models.Actors.Characters.Stats
{
    public class Health
    {
        /*
         Health or HP (health points) measures how close a character is to death. Character HP is determined
         by their profession, while Enemy and NPC HP is set at Actor intialization. Most armor increases 
         max health, and many skill effects can temporarily grant extra max HP or restore lost HP.
        */
        [JsonConstructor]
        public Health(Actor actor)
        {
            if (actor is Character)
            {
                Character = (Character)actor;
                Base = Character.Profession.BaseHealth;
                Current = Max;
            }
        }
        public Health(Actor actor, int baseHP)
        {
            Actor = actor;
            Base = baseHP;
            Current = Base;
        }

        #region Linked objects for stat modifier calculation
        private Actor Actor { get; } = null;
        private Character Character { get; } = null;
        #endregion

        public int Current { get; set; }
        public int Max
        {
            get
            {
                if ( !(Character is null) )
                {
                    return Base
                    + (int)Math.Round(Character.EquipmentMod("healthBonus", Character.Equipment))
                    + (int)Math.Round(Character.EffectMod("healthBonus"));
                }
                else
                {
                    return Base + (int)Math.Round(Actor.EffectMod("healthBonus")); ;
                }
            }
        }
        /// <summary>
        /// Percent of HP remaining (formatted as 15 == 15%
        /// </summary>
        public double Percent => Math.Round((((double)Current / Max * 100)));
        public int Base { get; private set; }
        public double BaseRegen { get; private set; } = 0; // Actors don't start with any implicit base health regen
        public bool IsAlive
        {
            get
            {
                return this.Current > 0;
            }
        }

        public bool AdjustHP(double points)
        {
            if (!IsAlive)
            {
                return false;
            }

            if(0 < points && points < 1)
            {
                Current += 1; // since HP is an integer, any increase must be at least 1 HP.
            }
            else if (-1 < points && points < 0)
            {
                Current -= 1; // Likewise for losing HP.
            }

            Current += (int)Math.Round(points);

            if (Current > Max)
            {
                Current = Max; // Cannot have more HP than max. Max itself can be adjusted by buffs, however.
            }
            else if(Current < 0)
            {
                Current = 0; // Die if you hit 0 HP.
            }

            return true;
        }
        public bool Resurrect(double points)
        {
            if (IsAlive)
            {
                return false;
            }
            else
            {
                Current = (int)points;
                return true;
            }
        }
        public void AdjustBase(int points)
        {
            // Base health can be adjusted in events.
            Base += points;
            if(Base <= 0)
            {
                Base = 1; // ensures that you'll never die simply from taking your armor off
            }
        }
        public void AdjustBaseRegen(int points)
        {
            // Base regen can be adjusted in events.
            BaseRegen += points;
            if (BaseRegen < 0)
            {
                BaseRegen = 0; // can't have base negative regen, though effects can cause negatives.
            }
        }
        public void RegenTick()
        {
            AdjustHP(
                BaseRegen
                + Character.EquipmentMod("healthRegen", Character.Equipment)
                + Character.EffectMod("healthRegen")
                - Character.EffectMod("healthDegen")
                );
        }
    }
}
