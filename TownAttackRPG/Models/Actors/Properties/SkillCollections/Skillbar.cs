using TownAttackRPG.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.SkillCollections
{
    public class Skillbar
    {
        public Skillbar(Actor self)
        {
            Self = self;
            Skill Empty = new EmptySkill(Self);
            Skills = new List<Skill>(6) { Empty, Empty, Empty, Empty, Empty, Empty };
        }
        public Skillbar(Actor self, List<Skill> skills)
        {
            Self = self;
            for (int i=0; i<Skills.Count; i++)
            {
                if (skills[i] is null)
                {
                    Skills[i] = new EmptySkill(self);
                }
                else
                {
                    Skills[i] = skills[i];
                    Skills[i].SetUser(self);
                }
            }
        }

        public Actor Self { get; }

        public List<Skill> Skills { get; private set; } = new List<Skill>(6) { null, null, null, null, null, null };
        public Dictionary<int,string> SkillNames
        {
            get
            {
                return new Dictionary<int, string>()
                {
                    { 1, Skills[0].SkillName },
                    { 2, Skills[1].SkillName },
                    { 3, Skills[2].SkillName },
                    { 4, Skills[3].SkillName },
                    { 5, Skills[4].SkillName },
                    { 6, Skills[5].SkillName },
                };
            }
        }

        /// <summary>
        /// Checks for duplicate skill active on bar
        /// </summary>
        /// <param name="skill"></param>
        /// <returns></returns>
        public bool NoDuplicates(Skill skill)
        {
            foreach(Skill item in Skills)
            {
                if (item.SkillName != "Empty" && item.SkillName == skill.SkillName)
                {
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// Tries to add a skill to the bar. If the skill is not unique, returns false and doesn't add.
        /// </summary>
        /// <param name="slot">The integer slot to add the skill to.</param>
        /// <param name="skill">The skill to add.</param>
        /// <returns></returns>
        public bool TryAdd(int slot, Skill skill)
        {
            if (NoDuplicates(skill))
            {
                Skills[slot - 1] = skill;
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Takes a skill out of the bar and returns it.
        /// </summary>
        /// <param name="slot">The slot to empty.</param>
        /// <returns></returns>
        public Skill Remove(int slot)
        {
            Skill skill = Skills[slot - 1];
            Skills[slot - 1] = new EmptySkill(Self);
            return skill;
        }
        /// <summary>
        /// Swaps two skills on the bar.
        /// </summary>
        /// <param name="slot1"></param>
        /// <param name="slot2"></param>
        public void Swap(int slot1, int slot2)
        {
            Skill skill1 = Remove(slot1);
            Skill skill2 = Remove(slot2);
            TryAdd(slot1, skill2);
            TryAdd(slot2, skill1);
        }
    }
}
