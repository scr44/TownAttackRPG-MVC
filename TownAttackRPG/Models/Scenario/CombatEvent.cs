using TownAttackRPG.Views.Menus.Combat;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.ActorProperties;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Actors.Party;
using TownAttackRPG.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Scenario
{
    abstract public class CombatEvent : Event
    {
        public CombatEvent(Party playerParty)
        {
            // TODO Combat Event: generate turn order
            PlayerParty = playerParty;
            CombatUI = new CombatUI((Character)playerParty.PartyMembers[0]);
        }
        
        public CombatUI CombatUI { get; protected set; }
        public Party PlayerParty { get; protected set; }
        public Party EnemyParty { get; protected set; }
        public Party AllyParty { get; protected set; }
        public Inventory Rewards { get; protected set; } = new Inventory();

        /// <summary>
        /// Format combat dialogue to output to the combat menu terminal.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <returns></returns>
        public string CombatDialogue(string dialogue, Actor speaker=null)
        {
            // format combat dialogue to output to the combat menu terminal
            if (!(speaker is null))
            {
                return $"{speaker.Name} says \"{dialogue}\"";
            }
            else
            {
                return dialogue;
            }
        }
        /// <summary>
        /// Write dialogue or combat feedback to the combat log.
        /// </summary>
        /// <param name="dialogue"></param>
        /// <param name="speaker"></param>
        public void Say(string dialogue, Actor speaker = null)
        {
            string text = CombatDialogue(dialogue, speaker);
            CombatUI.WriteToLog(text);
            CombatUI.Display();
        }
        /// <summary>
        /// Write feedback to combat log.
        /// </summary>
        /// <param name="text"></param>
        public void Feedback(string text)
        {
            Say(text);
        }

        /// <summary>
        /// An actor uses a skill on the given target(s).
        /// </summary>
        /// <param name="user"></param>
        /// <param name="skill"></param>
        /// <param name="target"></param>
        /// <param name="targetList"></param>
        public void ActorUsesSkill(Actor user, int skillslot, Actor singleTarget = null, List<Actor> multiTarget = null)
        {
            Skill skill = user.Skillbar.Skills[skillslot - 1];

            #region Use skill
            double[] feedbackArray;
            try
            {
               feedbackArray = skill.Use(singleTarget, multiTarget);
            }
            catch (SkillReqsNotMetException)
            {
                Feedback($"Cannot use {skill.SkillName}, requirements not met.");
                return;
            }
            catch (SkillNotReadyException)
            {
                Feedback($"{skill.SkillName} is not ready yet ({skill.Cooldown} turns left on cooldown).");
                return;
            }
            catch (SkillNeedsSPException)
            {
                Feedback($"Not enough stamina to use {skill.SkillName}.");
                return;
            }
            #endregion

            #region Usage feedback
            feedbackArray[0] = -Math.Round(feedbackArray[0]);
            Say("");
            if (feedbackArray[0] != 0)
            {
                if (!(singleTarget is null) && !(multiTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName}. {feedbackArray[0]} damage!");
                }
                else if (!(singleTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName} on {singleTarget.Name}. {feedbackArray[0]} damage!");
                }
                else if (!(multiTarget is null))
                {
                    foreach (Actor actor in multiTarget)
                    {
                        Feedback($"{user.Name} used {skill.SkillName} on {actor.Name}. {feedbackArray[0]} damage!");
                    }
                }
            }
            if (feedbackArray[1] != 0)
            {
                if (!(singleTarget is null) && !(multiTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName}. {feedbackArray[1]} healed!");
                }
                else if (!(singleTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName} on {singleTarget.Name}. {feedbackArray[1]} healed!");
                }
                else if (!(multiTarget is null))
                {
                    foreach (Actor actor in multiTarget)
                    {
                        Feedback($"{user.Name} used {skill.SkillName} on {actor.Name}. {feedbackArray[1]} healed!");
                    }
                }
            }
            if (feedbackArray[0] == 0 && feedbackArray[1] == 0)
            {
                if (!(singleTarget is null) && !(multiTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName}.");
                }
                else if (!(singleTarget is null))
                {
                    Feedback($"{user.Name} used {skill.SkillName} on {singleTarget.Name}.");
                }
                else if (!(multiTarget is null))
                {
                    foreach (Actor actor in multiTarget)
                    {
                        Feedback($"{user.Name} used {skill.SkillName} on {actor.Name}.");
                    }
                }
            }
            
            #endregion
        }
        /// <summary>
        /// Removes enemy from target list and drops rewards/XP.
        /// </summary>
        /// <param name="enemyActor"></param>
        public void EnemyKilled(Actor enemyActor)
        {
            int xp = enemyActor.XPReward;
            Say($"{enemyActor.Name} has been struck down. The party gains {xp} XP.");
            
            // award XP to party
            foreach (Actor member in PlayerParty.PartyMembers)
            {
                if (member is Character)
                {
                    Character memberChar = (Character)member;
                    if (memberChar.XP.WillLevelUp(xp))
                    {
                        Say($"{memberChar.Name} leveled up!");
                    }
                }
            }
            // drop loot into the "rewards" inventory
            Inventory loot = enemyActor.LootDrops;
            if (!(loot is null))
            {
                foreach (KeyValuePair<string, Item> item in loot.InventoryContents)
                {
                    Item itemToAdd = item.Value;
                    int numberToAdd = enemyActor.LootDrops.InventoryCounts[item.Key];
                    for (int i = 1; i <= numberToAdd; i++)
                    {
                        Rewards.AddItem(itemToAdd, numberToAdd);
                        Say($"{enemyActor.Name} drops {item.Key} (x{numberToAdd})");
                    }
                }
            }
            Say("");
        }
        public void AllyKilled(Actor ally)
        {

        }
        public void PlayerKilled(Actor player)
        {

        }

        public abstract void Run();
        public void VictoryScreen()
        {

        }
    }
}