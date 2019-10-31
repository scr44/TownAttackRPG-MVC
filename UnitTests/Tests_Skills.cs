using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Enemies;
using TownAttackRPG.Models.Actors.Enemies.Unaffiliated;
using TownAttackRPG.Models.Professions.DefaultProfessions;
using TownAttackRPG.Models.Skills;
using TownAttackRPG.Models.Skills.Techniques.Swords;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Items.Equipment;
using TownAttackRPG.Models.Items;

namespace Skills
{
    [TestClass]
    public class Activation
    {
        [TestMethod]
        public void UseAttackSkill()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // attach the "double slash" skill directly to the character (not going through skillbar here)
            DoubleSlash knightSkill = new DoubleSlash();
            knightSkill.SetUser(Guinevere);
            Assert.IsTrue(knightSkill.SkillTags.Contains("Attack"), "Double Slash should be an Attack skill");

            // make a training dummy
            TrainingDummy dummy = new TrainingDummy(100,10,10);

            // knight uses skill to attack the dummy
            try
            {
                knightSkill.Use(dummy);
                Assert.IsTrue(true, "Attack skill should activate successfully.");
            }
            catch(Exception e)
            {
                Assert.IsFalse(true, e.Message);
            }
        }
        [TestMethod]
        public void AttackSkillDamage()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // attach the "double slash" skill directly to the character (not going through skillbar here)
            DoubleSlash knightSkill = new DoubleSlash(Guinevere);

            // make a training dummy
            TrainingDummy dummy = new TrainingDummy(100, 10, 10);

            // attack the dummy
            knightSkill.Use(dummy);

            // dummy HP should be lower
            Assert.IsTrue(dummy.HP.Current < dummy.HP.Max, "Dummy should have lost health.");
        }
    }

    [TestClass]
    public class Requirements
    {
        [TestMethod]
        public void CheckProfession()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // create a footman
            Character Gilliam = new Character("Gilliam", new Footman("M"));

            // attach the "double slash" skill directly to the characters (not going through skillbar here)
            DoubleSlash knightSkill = new DoubleSlash(Guinevere);
            DoubleSlash footmanSkill = new DoubleSlash(Gilliam);

            // "double slash" should be a Knight-restricted skill
            Assert.IsTrue(knightSkill.SkillProfessionReqs.Contains("Knight"), "Double Slash should be a Knight-only skill.");

            // have the knight use the skill on the footman
            try
            {
                knightSkill.Use(Gilliam);
                Assert.IsTrue(true, "Knight should use the skill successfully.");
            }
            catch(SkillReqsNotMetException)
            {
                Assert.IsFalse(true, "Knight should meet skill requirements.");
            }

            // have the footman try to use the skill on the knight
            try
            {
                footmanSkill.Use(Guinevere);
                Assert.IsFalse(true, "Footman should be unable to use the skill.");
            }
            catch(SkillReqsNotMetException)
            {
                Assert.IsTrue(true, "Footman should not meet the skill requirements.");
            }
        }

        [TestMethod]
        public void CheckStatReq()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // create another knight and make him too weak to meet the req
            Character Gilliam = new Character("Gilliam", new Knight("M"));
            Gilliam.Attributes.SetAttribute("STR", 1);

            // attach the "double slash" skill directly to the characters (not going through skillbar)
            DoubleSlash strongSkill = new DoubleSlash(Guinevere);
            DoubleSlash weakSkill = new DoubleSlash(Gilliam);

            // strong knight should be able to use the skill
            try
            {
                strongSkill.Use(Gilliam);
                Assert.IsTrue(true, "Strong should use the skill successfully.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsFalse(true, "Strong should meet skill requirements.");
            }

            // weak knight should not be able to use the skill
            try
            {
                weakSkill.Use(Guinevere);
                Assert.IsFalse(true, "Weak should be unable to use the skill.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsTrue(true, "Weak should not meet the skill requirements.");
            }
        }
        [TestMethod]
        public void Check2HReq()
        {
            // create a knight and a training dummy
            Character Guinevere = new Character("Guinevere", new Knight("F"));
            TrainingDummy Dummy = new TrainingDummy(100, 0, 0);

            // knight should be able to use double slash
            try
            {
                Guinevere.Skillbar.Skills[0].Use(Dummy);
                Assert.IsTrue(true, "Knight should use the skill successfully.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsFalse(true, "Knight should meet skill requirements.");
            }

            // Toggle 2H off and try to use it again, should fail
            Guinevere.Equipment.Toggle2H();
            try
            {
                Guinevere.Skillbar.Skills[0].Use(Dummy);
                Assert.IsFalse(true, "Knight should fail to use the skill successfully.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsTrue(true, "Knight should not meet the 2H requirements.");
            }

        }
        // TODO Test: Check Equipment Req
        [TestMethod]
        public void CheckEquipReq()
        {
            // create a knight and a training dummy
            Character Guinevere = new Character("Guinevere", new Knight("F"));
            TrainingDummy Dummy = new TrainingDummy(100, 0, 0);

            // knight should be able to use double slash
            try
            {
                Guinevere.Skillbar.Skills[0].Use(Dummy);
                Assert.IsTrue(true, "Knight should use the skill successfully.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsFalse(true, "Knight should meet skill requirements.");
            }

            // replace the knight's sword with a book and 2H it (lol)
            Guinevere.Inventory.AddItem((EquipmentItem)Item.CreateNew("Equipment", "History Tome"));
            Guinevere.Equipment.Equip("MainHand",Guinevere.Inventory.InventoryContents["History Tome"]);
            Guinevere.Equipment.Toggle2H();

            // try to use double slash, should fail because the book doesn't have a "sword" tag
            try
            {
                Guinevere.Skillbar.Skills[0].Use(Dummy);
                Assert.IsFalse(true, "Book should not meet the skill's Sword requirement.");
            }
            catch (SkillReqsNotMetException)
            {
                Assert.IsTrue(true, "Book should not meet the skill's Sword requirement.");
            }
        }
    }
    
    [TestClass]
    public class SkillbarMethods
    {
        [TestMethod]
        public void AddSkill()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // knight should have double slash in her skillbar
            Assert.IsTrue(Guinevere.Skillbar.Skills[0] is DoubleSlash);
            Assert.IsTrue(Guinevere.Skillbar.SkillNames[1] == "Double Slash");

            // try to add another copy of double slash and fail
            Assert.IsFalse(Guinevere.Skillbar.TryAdd(2, new DoubleSlash(Guinevere)));
        }
        [TestMethod]
        public void RemoveSkill()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // remove double slash from the bar
            Assert.IsTrue(Guinevere.Skillbar.Remove(1) is DoubleSlash);
        }
        [TestMethod]
        public void SwapSkills()
        {
            // create a knight
            Character Guinevere = new Character("Guinevere", new Knight("F"));

            // skill 1 should be double slash, skill 2 should be empty
            // TODO Profession: update knight skill tests with new defaults
            Assert.IsTrue(Guinevere.Skillbar.Skills[0] is DoubleSlash);
            Assert.IsTrue(Guinevere.Skillbar.Skills[1] is EmptySkill);

            // swap the skills
            Guinevere.Skillbar.Swap(1, 2);
            Assert.IsTrue(Guinevere.Skillbar.Skills[1] is DoubleSlash);
            Assert.IsTrue(Guinevere.Skillbar.Skills[0] is EmptySkill);
        }
    }
}
