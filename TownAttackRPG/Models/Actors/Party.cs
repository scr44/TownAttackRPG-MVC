using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.Party
{
    public class Party
    {
        public string Name { get; private set; }
        public List<Actor> PartyMembers { get; private set; } = new List<Actor>();
        public Inventory SharedInventory { get; } = new Inventory();
        
        public void AddPartyMember(Actor member)
        {
            PartyMembers.Add(member);
        }
    }
}
