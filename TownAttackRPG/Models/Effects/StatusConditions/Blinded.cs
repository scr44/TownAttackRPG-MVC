using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Effects;
using TownAttackRPG.Models.Effects.EffectInterfaces;

namespace TownAttackRPG.Models.Effects.StatusConditions
{
    public class Blinded : Effect, IStatBuffOrDebuff
    {
        public Blinded(int duration, Actor target)
        {
            EffectTags.Add("Status Condition");
            EffectTags.Add("Unique"); // unique but no stacking duration
            EffectTags.Add("Negative Effect");
            // Note: status conditions are not considered
            //       stat debuffs for the purposes of removal

            EffectName = "Blinded";
            EffectDescrip = "Has severely reduced accuracy.";
            Duration = duration;
            Target = target;
        }

        double AccuracyDebuff = -.90;

        public void AdjustStatMod(string stat, double points)
        {
            if(!StatMod.TryAdd(stat, points))
            {
                StatMod[stat] += points;
            }
        }

        public override void NewEffectAction()
        {
            AdjustStatMod("accuracy", AccuracyDebuff);
        }
    }
}
