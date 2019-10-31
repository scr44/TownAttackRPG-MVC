using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Effects;
using TownAttackRPG.Models.Effects.Buffs;
using TownAttackRPG.Models.Effects.StatusConditions;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;

namespace Effects
{
    [TestClass]
    public class Generic_InitializeAndExpire
    {
        [TestMethod]
        public void NewEffect()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // new Knight has no starting poison PROT
            Assert.AreEqual(0, Guinevere.PROT("poison"));

            // poison the knight (they really don't pay her enough for this)
            Guinevere.ActiveEffects.AddEffect(new Poisoned(5, 5, Guinevere));
            bool isPoisoned = false;
            foreach (Effect effect in Guinevere.ActiveEffects.EffectList)
            {
                if (effect is Poisoned)
                {
                    isPoisoned = true;
                    break;
                }
            }
            Assert.IsTrue(isPoisoned);
        }
        [TestMethod]
        public void EffectExpiration()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // poison the knight
            Guinevere.ActiveEffects.AddEffect(new Poisoned(1, 5, Guinevere));

            // tick effect to get duration to 0
            Guinevere.ActiveEffects.TickAllEffects();

            // poison should have expired
            Assert.AreEqual(0, Guinevere.ActiveEffects.EffectList.Count);
        }
    }

    [TestClass]
    public class Generic_StatModifiers
    {
        [TestMethod]
        public void AttributeMod()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // buff her with berserk
            Guinevere.ActiveEffects.AddEffect(new Berserk(2, Guinevere));

            // strength should be up by 2
            int baseSTR = Guinevere.Attributes.BaseValue["STR"];
            int modSTR = Guinevere.Attributes.ModdedValue["STR"];
            Assert.AreEqual(baseSTR + 2, modSTR);
        }
        [TestMethod]
        public void AccuracyMod()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // blind them for 3 turns
            Guinevere.ActiveEffects.AddEffect(new Blinded(3, Guinevere));

            // her accuracy should be lowered by 90
            double baseAcc = Guinevere.Equipment.Slot["MainHand"].WeaponStats["accuracy"];
            double modAcc = Math.Round(baseAcc + Guinevere.EffectMod("accuracy"), 2); // for some reason it gives .04999... instead of .05
            Assert.AreEqual(.05, modAcc, "Accuracy should be modded down by .90");
        }
    }

    [TestClass]
    public class Status_Poison
    {
        [TestMethod]
        public void PoisonNewTickAndExpire()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // poison the knight
            Guinevere.ActiveEffects.AddEffect(new Poisoned(5, 5, Guinevere));

            // knight should have 5 turns/ticks of poison remaining
            Assert.AreEqual(5, Guinevere.ActiveEffects.EffectDurations["Poisoned"]);

            // knight should have taken no damage yet
            Assert.AreEqual(Guinevere.HP.Max, Guinevere.HP.Current);

            // tick the damage effect of the poison
            Guinevere.ActiveEffects.TickAllEffects();

            // knight should take 5 damage
            Assert.AreEqual(Guinevere.HP.Max - 5, Guinevere.HP.Current);

            // poison duration should have dropped to 4 turns/ticks
            Assert.AreEqual(4, Guinevere.ActiveEffects.EffectDurations["Poisoned"]);
        }
        [TestMethod]
        public void PoisonRemove()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // poison them
            Guinevere.ActiveEffects.AddEffect(new Poisoned(5, 5, Guinevere));

            // remove poison
            Assert.IsTrue(Guinevere.ActiveEffects.RemoveEffect(0), "Poison should successfully remove");
        }
    }

    [TestClass]
    public class Status_Blind
    {
        [TestMethod]
        public void BlindNewTickAndExpire()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // blind them for 3 turns
            Guinevere.ActiveEffects.AddEffect(new Blinded(3, Guinevere));

            // she should have 3 turns/ticks of blind remaining
            Assert.AreEqual(3, Guinevere.ActiveEffects.EffectDurations["Blinded"]);

            // her accuracy should be lowered by 90
            double baseAcc = Guinevere.Equipment.Slot["MainHand"].WeaponStats["accuracy"];
            double modAcc = Math.Round(baseAcc + Guinevere.EffectMod("accuracy"),2); // for some reason it gives .04999... instead of .05
            Assert.AreEqual(.05, modAcc, "Accuracy should be modded down by .90");

            // tick effects 3 times to run out timer on blind
            for(int i=0; i<3; i++)
            {
                Guinevere.ActiveEffects.TickAllEffects();
            }

            // Blind should have expired
            Assert.AreEqual(0, Guinevere.ActiveEffects.EffectList.Count, "blind should have expired.");
        }
        [TestMethod]
        public void BlindRemove()
        {
            // create a new character
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // blind them for 3 turns
            Guinevere.ActiveEffects.AddEffect(new Blinded(3, Guinevere));

            // remove the blind
            Assert.IsTrue(Guinevere.ActiveEffects.RemoveEffect(0), "Blind should successfully remove");
        }
    }
}
