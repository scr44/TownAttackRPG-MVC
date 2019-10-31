using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.Characters.Stats
{
    public class Experience
    {
        public Experience()
        {

        }
        public Experience(Character character, int initLvl = 1)
        {
            AttachedCharacter = character;
            Level = Math.Abs(initLvl);
        }

        public Character AttachedCharacter { get; }

        public int Current { get; private set; } = 0;
        public int Needed
        {
            get
            {
                return (int)(10 * (Math.Pow(Level, 2.5) + 50));
            }
        }
        public int Level { get; set; } = 1;
        public int AvailableAttributePts { get; private set; } = 0;
        public int AvailableTalentPts { get; private set; } = 0;

        public bool WillLevelUp(int points)
        {
            points *= (int)((0.1) * (AttachedCharacter.Attributes.ModdedValue["APT"] - 5) + 1);
            if (points + Current >= Needed)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void GainXP(int points)
        {
            points *= (int)((0.1) * (AttachedCharacter.Attributes.ModdedValue["APT"] - 5) + 1);
            Current += points;
            while(Current > Needed)
            {
                Current -= Needed;
                LevelUp();
            }
        }
        public string LevelUp()
        {
            Level += 1;
            AddLevelUpPts(1, 0); // Get one Att point every level
            if(Level % 2 == 0)
            {
                AddLevelUpPts(1, 0); // Get an additional Att point every 2nd level
            }
            if(Level % 3 == 0)
            {
                AddLevelUpPts(0, 1); // Get a Tal point every 3rd level
            }
            
            if(AvailableTalentPts == 0)
            {
                return $"{AttachedCharacter.Name} gained a level! " +
                    $"{AvailableAttributePts} Attribute point{(AvailableAttributePts > 1 ? "s" : "")} " +
                    $"available.";
            }
            else
            {
                return $"{AttachedCharacter.Name} gained a level! " +
                    $"{AvailableAttributePts} Attribute point{(AvailableAttributePts > 1 ? "s":"")} " +
                    $"and {AvailableTalentPts} Talent point{(AvailableTalentPts > 1 ? "s":"")} available.";
            }
        }
        public void AddLevelUpPts(int attPts = 0, int talPts = 0)
        {
            AvailableAttributePts += Math.Abs(attPts);
            AvailableTalentPts += Math.Abs(talPts);
        }
        public void SpendAttributePts(string att, int points)
        {
            if(Math.Abs(points) <= AvailableAttributePts) // no negative points allowed
            {
                AvailableAttributePts -= points;
                AttachedCharacter.Attributes.AdjustAttribute(att, points);
            }
        }
        public void SpendTalentPts(string att, int points)
        {
            if(Math.Abs(points) <= AvailableTalentPts)
            {
                AvailableTalentPts -= points;
                AttachedCharacter.Talents.AdjustTalent(att, points);
            }
        }
    }
}
