using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;

namespace TownAttackRPG.Models.Skills
{
    public class EmptySkill : Skill
    {
        public EmptySkill(Actor self=null) : base(self)
        {
            #region Tags and Metadata
            base.SkillName = "Empty";
            base.ShortDescrip = "No skill equipped in this slot.";
            base.SkillTags = new List<string>() {  };
            #endregion

            #region Cooldown and Stamina
            // none
            #endregion

            #region Requirements
            // none
            #endregion
        }
        public override string FullDescrip => "An empty skill slot";

        public override void OnMany(List<Actor> targetList)
        {
            
        }

        public override void OnSelf()
        {
            
        }

        public override void OnTarget(Actor target)
        {
            
        }

        public override double[] Use(Actor target = null, List<Actor> targetList = null)
        {
            // do nothing
            return new double[] { 0, 0 };
        }
    }
}
