using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Effects.EffectInterfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Effects.StatusConditions
{
    public class Poisoned : Effect, IPoison
    {
        public Poisoned(int duration, int dmg, Actor target)
        {
            EffectTags.Add("Status Condition");
            EffectTags.Add("Negative Effect");
            EffectTags.Add("Damage Over Time");

            EffectName = "Poisoned";
            EffectDescrip = "Taking poison damage over time.";
            Duration = duration;
            Target = target;
            DMG = dmg;
        }

        int DMG { get; }

        public void PoisonDamage(Actor target, int dmg)
        {
            target.Damaged(DMG, "poison", 0);
        }

        public override void TickAction()
        {
            PoisonDamage(Target, DMG);

            base.TickAction();
        }
    }
}
