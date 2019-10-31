using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Skills;

namespace TownAttackRPG.Models.Skills.Techniques.Swords
{
    public class DoubleSlash : Skill
    {
        public DoubleSlash(Actor self=null) : base(self)
        {
            #region Tags and Metadata
            base.SkillName = "Double Slash";
            base.ShortDescrip = "Make two slashes in quick succession.";
            base.SkillTags = new List<string>() { "Sword", "Technique", "Two-Handed", "Physical", "Attack" };
            #endregion

            #region Cooldown and Stamina
            base.CooldownMax = 0;
            base.SPCost = 5;
            #endregion

            #region Requirements
            base.SkillProfessionReqs = new List<string>() { "Knight" };
            base.SkillStatReqs = new Dictionary<string, int>()
            {
                { "STR", 5 },
                { "DEX", 5 },
            };
            base.SkillEquipmentTagReqs = new List<string>() { "Sword" };
            #endregion
        }

        #region Interpolated Description
        public override string FullDescrip 
            => $"Two-handed Knightly Sword Technique: " +
            $"Attack twice, dealing {1 * Self.DMG("slash")} slashing damage each time.";
        #endregion

        #region Targeting and Behavior
        override public void OnTarget(Actor target)
        {
            this.dmgFeedback = 0;
            this.dmgFeedback += target.Damaged(1 * Self.DMG("slash"), "slash", Self.ArmorPiercing);
            this.dmgFeedback += target.Damaged(1 * Self.DMG("slash"), "slash", Self.ArmorPiercing);
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
