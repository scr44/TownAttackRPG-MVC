using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Professions;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Skills
{
    abstract public class Skill
    {
        public Skill(Actor self=null)
        {
            this.Self = self;
            // set all the requirements in the constructor
        }
        public void SetUser(Actor self)
        {
            this.Self = self;
        }

        public Actor Self { get; protected set; }

        #region Tags and Metadata
        public string SkillName { get; protected set; }
        public string ShortDescrip { get; protected set; }
        public List<string> SkillTags { get; protected set; }

        abstract public string FullDescrip { get; } // interpolated with dmg amounts etc
        #endregion

        #region Skill Requirements
        public List<string> SkillProfessionReqs { get; protected set; }
        public bool Skill2HReq => SkillTags.Contains("Two-Handed");
        public Dictionary<string, int> SkillStatReqs { get; protected set; }
        public List<string> SkillEquipmentTagReqs { get; protected set; }

        public bool MeetsSkillReqs
        {
            get
            {
                Character self;

                // Don't bother checking reqs for Enemies and other non-Characters
                if (!(Self is Character))
                {
                    return true;
                }
                else
                {
                    self = (Character)Self; // cast Self to Character before checks
                }

                // Check for profession requirement
                if ( !(SkillProfessionReqs is null) &&
                    !(SkillProfessionReqs.Contains("any") || SkillProfessionReqs.Contains(self.Profession.Title))
                    ) { return false; }

                // Check for 2H requirement
                if (Skill2HReq == true && self.Equipment.Is2H == false)
                {
                    return false;
                }

                // Check each stat requirement
                if (!(SkillStatReqs is null))
                {
                    foreach (KeyValuePair<string, int> kvp in SkillStatReqs)
                    {
                        if ( // Attribute requirements
                            self.Attributes.ModdedValue.ContainsKey(kvp.Key) &&
                            kvp.Value > (self.Attributes.ModdedValue[kvp.Key])
                           )
                        {
                            return false;
                        }
                        if ( // Talent requirements
                            self.Talents.ModdedValue.ContainsKey(kvp.Key) &&
                            kvp.Value > (self.Talents.ModdedValue[kvp.Key])
                           )
                        {
                            return false;
                        }
                    }
                }

                // Check each equipment type requirement
                if (!(SkillEquipmentTagReqs is null))
                {
                    foreach (string req in SkillEquipmentTagReqs)
                    {
                        if (!self.Equipment.AllEquipmentTags.Contains(req))
                        {
                            return false;
                        }
                    }
                }

                // If it didn't fail any of the requirement checks, it passes
                return true;
            }
        }
        #endregion

        #region Targeting and Behavior
        virtual public void OnTarget(Actor target)
        { }
        virtual public void OnSelf()
        { }
        virtual public void OnMany(List<Actor> targetList)
        { }
        #endregion

        #region Cooldown and Stamina
        public int CooldownMax { get; protected set; }
        public int Cooldown { get; protected set; } = 0;
        public void StartCD()
        {
            Cooldown = CooldownMax;
        }
        public void TickCD()
        {
            if (Cooldown > 0)
            {
                Cooldown--;
            }
        }
        public int SPCost { get; protected set; }

        public bool Ready => (Cooldown == 0);
        public bool HasEnoughSP => (SPCost <= Self.SP.Current);
        public void ActivateSkill()
        {
            StartCD();
            Self.SP.AdjustSP(-SPCost);
        }
        #endregion

        protected double dmgFeedback { get; set; } = 0;
        protected double healFeedback { get; set; } = 0;
        
        /// <summary>
        /// Uses the skill. Leave args empty if the skill is only self-targeting.
        /// </summary>
        /// <param name="target">The target for single target effects.</param>
        /// <param name="targetList">The list of targets for multi-target effects.</param>
        virtual public double[] Use(Actor target = null, List<Actor> targetList = null)
        {
            if (!Ready)
            {
                throw new SkillNotReadyException();
            }
            else if (!HasEnoughSP)
            {
                throw new SkillNeedsSPException();
            }
            else if (!MeetsSkillReqs)
            {
                throw new SkillReqsNotMetException();
            }
            ActivateSkill();
            // Individual skill implementations happen after base.Use()

            return new double[] { dmgFeedback, healFeedback };
        }
    }
}
