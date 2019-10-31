using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Effects;
using TownAttackRPG.Models.Effects.Debuffs;
using TownAttackRPG.Models.Effects.EffectInterfaces;

namespace TownAttackRPG.Models.Effects.Buffs
{
    public class Berserk : Effect, IStatBuffOrDebuff
    {
        public Berserk(int duration, Actor target)
        {
            EffectTags.Add("Positive Effect");
            EffectTags.Add("Unique");
            EffectTags.Add("Stacking Duration");

            EffectName = "Berserk";
            EffectDescrip = "Consumed by a ritualistic, drug-fueled rage.";
            Duration = duration;
            Target = target;
        }

        public void AdjustStatMod(string stat, double points)
        {
            if (!StatMod.TryAdd(stat, points))
            {
                StatMod[stat] += points;
            }
        }

        public override void NewEffectAction()
        {
            AdjustStatMod("STR", 2);

            AdjustStatMod("crushMultiplier", .50);
            AdjustStatMod("pierceMultiplier", .30);
            AdjustStatMod("slashMultiplier", .30);
            AdjustStatMod("armorMultiplier", .40);

            // take double damage from physical sources
            AdjustStatMod("crushPROT", 2);
            AdjustStatMod("piercePROT", 2);
            AdjustStatMod("slashPROT", 2);
        }

        public override void ExpiryAction()
        {
            Target.ActiveEffects.AddEffect(new Exhausted(3, -5));
        }
    }
}
