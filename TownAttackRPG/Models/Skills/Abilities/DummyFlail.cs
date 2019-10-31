using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;

namespace TownAttackRPG.Models.Skills.Abilities
{
    public class DummyFlail : Skill
    {
        public DummyFlail(Actor self = null) : base(self)
        {
            #region Tags and Metadata
            SkillName = "Dummy Flail";
            ShortDescrip = "The dummy flails in the wind.";
            SkillTags = new List<string>() { "Enemy" };
            #endregion

            #region Cooldown and Stamina
            base.CooldownMax = 0;
            base.SPCost = 0;
            #endregion

            #region Requirements
            // none
            #endregion
        }

        public override string FullDescrip => "The dummy flails for 10 damage.";

        #region Targeting and Behavior
        override public void OnTarget(Actor target)
        {
            this.dmgFeedback = 0;
            this.dmgFeedback += target.Damaged(10, "crush", Self.ArmorPiercing);
        }
        #endregion

        public override double[] Use(Actor target, List<Actor> targetList = null)
        {
            base.Use(); // takes stamina and starts cooldown
            OnTarget(target);
            return new double[] { dmgFeedback, healFeedback };
        }
    }
}
