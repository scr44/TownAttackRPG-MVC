using TownAttackRPG.Models.Actors;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Effects
{
    abstract public class Effect
    {
        public List<string> EffectTags { get; protected set; } = new List<string>();

        public string EffectName { get; protected set; }
        public string EffectDescrip { get; protected set; }

        /// <summary>
        /// Number of ticks an effect has remaining.
        /// </summary>
        public int Duration { get; set; }
        /// <summary>
        /// The target of the effect.
        /// </summary>
        public Actor Target { get; protected set; }

        /// <summary>
        /// Dictionary of the Attribute, Talent, Charm, and other stats modified by the Effect.
        /// </summary>
        public Dictionary<string, double> StatMod { get; protected set; } = new Dictionary<string, double>();

        public virtual void TriggerAction()
        {
            // TODO Effects: build an Effect with a triggered function (eg next attack skill costs less AP)
        }
        public virtual void TickAction()
        {
            Duration -= 1;
            if(Duration <= 0)
            {
                ExpiryAction();
            }
        }
        public virtual void NewEffectAction()
        {

        }
        public virtual void ExpiryAction()
        {

        }
    }
}