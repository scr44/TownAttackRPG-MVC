using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;

namespace TownAttackRPG.Models.Scenarios
{
    abstract public class Module
    {
        public string Name { get; protected set; }
        public List<Actor> PlayerParty { get; set; }
        public Character PlayerCharacter => (Character)PlayerParty[0];
        public List<Actor> Allies { get; protected set; } = new List<Actor>();
        public List<Actor> Enemies { get; protected set; } = new List<Actor>();
    }
}
