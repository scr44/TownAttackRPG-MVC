using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.VendorTrash;
using TownAttackRPG.Models.Skills.Abilities;

namespace TownAttackRPG.Models.Actors.Enemies.Unaffiliated
{
    public class TrainingDummy : Enemy
    {
        public TrainingDummy(int baseHP, int baseSP, int SPRegen, 
            string name="Training Dummy", Dictionary<Item, int> rewards = null) : 
            base(baseHP, baseSP, SPRegen, name, rewards)
        {
            // base.Skillbar fills here
            base.Skillbar = new SkillCollections.Skillbar(this);
            base.Skillbar.TryAdd(1, new DummyFlail(this));

            // combat rewards
            base.XPReward = 30;
        }

        public override double DMG(string dmgType)
        {
            return 0;
        }
        public override double PROT(string dmgType, bool weaponBlock)
        {
            return 0;
        }
    }
}
