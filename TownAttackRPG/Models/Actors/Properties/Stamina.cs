using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Professions;
using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace TownAttackRPG.Models.Actors.Characters.Stats
{
    public class Stamina
    {
        /*
         Stamina or SP (stamina points) measures how much energy your character has available to use skills.
         Stamina will regenerate on its own in combat, but can also be restored by certain skills or items.
        */
        [JsonConstructor]
        public Stamina(Actor actor)
        {
            if (actor is Character)
            {
                Character = (Character)actor;
                Base = Character.Profession.BaseStamina;
                BaseRegen = Character.Profession.BaseStaminaRegen;
                Current = Max;
            }
        }
        public Stamina(Actor actor, int baseSP, int SPRegen)
        {
            Actor = actor;
            Base = baseSP;
            BaseRegen = SPRegen;
            Current = Max;
        }

        #region Attached objects
        private Actor Actor { get; } = null;
        private Character Character { get; } = null;
        #endregion

        public int Current { get; private set; } = 0;
        public int Max
        {
            get
            {
                if (Character is null)
                {
                    return Base + (int)Math.Round(Actor.EffectMod("addMaxSP"));
                }
                else
                {
                    return Base
                    + (int)Math.Round(Character.EquipmentMod("addMaxSP", Character.Equipment))
                    + (int)Math.Round(Character.EffectMod("addMaxSP"));
                }
            }
        }
        public double Percent
        {
            get
            {
                return Math.Round((((double)Current / Max * 100)));
            }
        }
        public int Base { get; private set; }
        public double BaseRegen { get; private set; }

        public bool AdjustSP(double points)
        {
            // You can't use stamina if your current SP is below 0
            if (points < 0 && Current <= 0)
            {
                return false;
            }

            if (0 < points && points < 1)
            {
                points += 1; // since SP is an integer, any increase must be at least 1 SP.
            }
            else if (-1 < points && points < 0)
            {
                points -= 1; // Likewise for losing SP.
            }

            Current += (int)Math.Round(points);

            if (Current > Max)
            {
                Current = Max;
            }
            else if (Current <= -5)
            {
                Current = -5; // you can "overcast" your stamina, to mitigate high-SP skill spam
            }
            return true;
        }
        public void AdjustBase(int points)
        {
            // Base stamina can be adjusted in events.
            Base += points;
            if (Base < 0)
            {
                Base = 0; // can't have less than 0 base energy.
            }
        }
        public void AdjustBaseRegen(int points)
        {
            // Base regen can be adjusted in events.
            Base += points;
            if (Base < 1)
            {
                Base = 1; // can't have less than 1 base regen.
            }
        }
        public void RegenTick()
        {
            if (Character is null)
            {
                AdjustSP(BaseRegen + Actor.EffectMod("staminaRegen"));
            }
            else
            {
                AdjustSP(
                BaseRegen
                + Character.EquipmentMod("staminaRegen", Character.Equipment)
                + Character.EffectMod("staminaRegen")
                + Character.Attributes.ModdedValue["DEX"] * .2
                );
            }
            
        }
    }
}
