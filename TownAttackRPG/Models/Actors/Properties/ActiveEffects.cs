using TownAttackRPG.Models.Effects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace TownAttackRPG.Models.Actors.ActorProperties
{
    public class ActiveEffects
    {
        public List<Effect> EffectList { get; private set; } = new List<Effect>();
        public Dictionary<string,int> EffectInstanceCount
        {
            get
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                foreach(Effect effect in EffectList)
                {
                    if( !(dict.TryAdd(effect.EffectName,1)))
                    {
                        dict[effect.EffectName]++;
                    }
                }
                return dict;
            }
        }
        public Dictionary<string, int> EffectDurations
        {
            get
            {
                Dictionary<string, int> dict = new Dictionary<string, int>();
                foreach (Effect effect in EffectList)
                {
                    if (!(dict.TryAdd(effect.EffectName, effect.Duration)))
                    {
                        dict[effect.EffectName]+= effect.Duration;
                    }
                }
                return dict;
            }
        }

        #region Adding and Removing Effects (ad hoc)
        public bool AddEffect(Effect newEffect)
        {
            // If the effect can't stack, the duration stacks or refreshes rather than a new instance
            if (newEffect.EffectTags.Contains("Unique") 
                && EffectInstanceCount.ContainsKey(newEffect.EffectName))
            {
                if(newEffect.EffectTags.Contains("Stacking Duration"))
                {
                    // Stack the durations
                    foreach(Effect effect in EffectList)
                    {
                        if (effect.EffectName == newEffect.EffectName)
                        {
                            effect.Duration += newEffect.Duration;
                        }
                    }
                }
                else
                {   
                    // Refresh duration to new instance's starting duration
                    foreach (Effect effect in EffectList)
                    {
                        if (effect.EffectName == newEffect.EffectName)
                        {
                            effect.Duration = newEffect.Duration;
                        }
                    }
                }
            }
            // Otherwise, the effect starts
            else
            {
                EffectList.Add(newEffect);

                // If the effect has any triggers upon starting, activate them
                newEffect.NewEffectAction();
            }
            return true;
        }
        public bool RemoveEffect(int i, bool safeRemove=false)
        {
            try
            {
                if (EffectList[i].EffectTags.Contains("Trigger on Expiry") && safeRemove == false)
                {
                    EffectList[i].ExpiryAction();
                }
                EffectList.RemoveAt(i);
                return true;
            }
            catch (ArgumentOutOfRangeException)
            {
                return false;
            }
        }
        public void RemoveEffectsByKeyword(int numToRemove, string keyword)
        {
            for(int i = EffectList.Count - 1; i >= 0 && numToRemove > 0; i--)
            {
                if(EffectList[i].EffectTags.Contains(keyword))
                {
                    RemoveEffect(i);
                }

                numToRemove--;
            }
        }
        #endregion

        #region Tick and Expire Effects (every turn)
        // Ticks all effects, lowering duration by 1 and doing whatever other tick actions the individual effects have
        public void TickAllEffects()
        {
            foreach(Effect effect in EffectList)
            {
                effect.TickAction();
            }
            RemoveExpiredEffects();
        }
        public void RemoveExpiredEffects()
        {
            // go backward to make RemoveEffect safe for the list
            for (int i=EffectList.Count-1; i >= 0; i--)
            {
                if (EffectList[i].Duration == 0)
                {
                    RemoveEffect(i);
                }
            }
        }
        #endregion
    }
}
