using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Effects.EffectInterfaces
{
    interface IStatBuffOrDebuff
    {
        void AdjustStatMod(string stat, double points);
    }
}
