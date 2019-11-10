using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Characters.Stats;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Professions;

namespace TownAttackRPG.Models.Actors.Enemies
{
    abstract public class Enemy : Actor
    {
        public Enemy(int baseHP, int baseSP, int SPRegen, string name, List<Item> rewards=null) : base(name)
        {
            HP = new Health(this, baseHP);
            SP = new Stamina(this, baseSP, SPRegen);
            if (!(rewards is null))
            {
                LootDrops = new Inventory(rewards);
            }
        }

        // Not all Actors need to have a full stat list and equipment. Enemies are simple actors with some combat stats and skills.

        public override Health HP { get; protected set; }
        public override Stamina SP { get; protected set; }

        public override double DMG(string dmgType)
        {
            throw new NotImplementedException();
        }

        public override double PROT(string dmgType, bool weaponBlock)
        {
            throw new NotImplementedException();
        }
    }
}
