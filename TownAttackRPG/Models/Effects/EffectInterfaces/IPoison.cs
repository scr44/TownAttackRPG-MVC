using TownAttackRPG.Models.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Effects.EffectInterfaces
{
    interface IPoison
    {
        void PoisonDamage(Actor target, int dmg);
    }
}
