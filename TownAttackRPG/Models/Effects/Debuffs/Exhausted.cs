using TownAttackRPG.Models.Effects.EffectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Effects.Debuffs
{
    public class Exhausted : Effect, IStatBuffOrDebuff
    {
        /// <summary>
        /// Overexertion has resulted in slower stamina regeneration.
        /// </summary>
        /// <param name="duration">Turns remaining.</param>
        /// <param name="exhaustion">Points of reduced SP Regen. Use negative numbers.</param>
        public Exhausted(int duration, double exhaustion)
        {
            EffectTags.Add("Negative Effect");
            EffectTags.Add("Stamina Degen");
            EffectTags.Add("Stacking Instances");

            EffectName = "Exhausted";
            EffectDescrip = "Overexertion has resulted in slower stamina regeneration.";
            Duration = duration;
            Exhaustion = exhaustion;
        }

        double Exhaustion { get; }

        public void AdjustStatMod(string stat, double points)
        {
            if (!StatMod.TryAdd(stat, points))
            {
                StatMod[stat] += points;
            }
        }

        public override void ExpiryAction()
        {
            base.ExpiryAction();
        }

        public override void NewEffectAction()
        {
            base.NewEffectAction();
            AdjustStatMod("staminaRegen", -5.0);
        }

        public override void TickAction()
        {
            base.TickAction();
        }

        public override void TriggerAction()
        {
            base.TriggerAction();
        }
    }
}
