using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Actors.Enemies;
using TownAttackRPG.Models.Actors.Enemies.Unaffiliated;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.VendorTrash;
using TownAttackRPG.Models.Actors.Party;

namespace TownAttackRPG.Models.Scenarios.Tutorial.Combat
{
    public class Tutorial_Combat_01_TrainingDummy : CombatEvent
    {
        public Tutorial_Combat_01_TrainingDummy(Party playerParty) : base(playerParty)
        {
            EnemyParty = new Party();

            EnemyParty.AddPartyMember(new TrainingDummy(100, 0, 0, "Training Dummy 1"));

            EnemyParty.AddPartyMember(new TrainingDummy(100, 0, 0, "Training Dummy 2"));

            EnemyParty.AddPartyMember(new TrainingDummy(100, 0, 0, "Training Dummy 3",new Dictionary<Item, int>() { { new Rubbish(), 1 } }));

            CombatUI.SetTargetList(EnemyParty.PartyMembers);
        }

        override public void Run()
        {
            Actor Player = PlayerParty.PartyMembers[0];
            Actor Dummy1 = EnemyParty.PartyMembers[0];
            Actor Dummy2 = EnemyParty.PartyMembers[1];
            Actor Dummy3 = EnemyParty.PartyMembers[2];
            CombatUI.Display();

            #region UI tutorial
            Say("This is the combat menu.");
            Pause();

            Say("");
            Say("The box containing this text is the combat log. Here, you will see");
            Say("combat feedback, in-combat dialogue, and reward drops.");
            Pause();

            Say("");
            Say("On the right, you can see the list of enemies you're in combat with.");
            Pause();

            Say("");
            Say("The targeting indicator symbol < shows which enemy you are currently");
            Say("aiming at.");
            Pause();

            Say("");
            Say("Below the combat log are the HP and SP bars. These display your current");
            Say("health (HP) and stamina (SP). We'll go over these in detail later.");
            Pause();

            Say("");
            Say("Below the HP and SP bars is the skill bar. This contains your active");
            Say("skills. Skills are abilities that allow you to deal damage and cause");
            Say("other effects in combat. Tap the corresponding number key to use a skill.");
            Pause();

            Say("");
            Say("To the right of the skill bar is the equipment window. This displays");
            Say("which weapons you have equipped. Weapons determine the damage you deal,");
            Say("the skills you can use, and potentially have other effects.");
            Pause();

            Say("");
            Say("Now, let's take a closer look at those training dummies...");
            Pause();
            #endregion

            #region Turn 1
            Feedback("ENEMY TURN");
            Pause();

            Say("");
            Say("Attack!", Dummy1);
            Pause();

            Say("Raaaah!", Dummy2);
            Pause();

            Say("DEATH TO THE HUMANS!", Dummy3);
            Pause();

            Say("");
            Say("Look out!");
            Pause();

            ActorUsesSkill(Dummy1, 1, Player);
            Pause();
            #endregion

            #region Turn 2
            Say("PLAYER TURN");
            Pause();

            Say("");
            Say("Those things really hurt when they flail around in the wind! You'll need to");
            Say("defend yourself. Try using your first skill, Double Slash, by pressing the");
            Say("matching skill key (1).");
            while(true)
            {
                if (Console.ReadKey().Key == ConsoleKey.D1)
                {
                    break;
                }
                else
                {
                    continue;
                }
            }

            ActorUsesSkill(Player, 1, Dummy1);
            Pause();

            Say("");
            Say("Good work! Notice how the enemy's HP has dropped.");
            Pause();

            Say("");
            Say("Using skills takes stamina. You can use any number of skills during your");
            Say("turn as long as you have enough stamina; but many skills also have cooldown");
            Say("times, so use them wisely!");
            Pause();

            Say("Go ahead and finish off the dummy with double slash.");
            while(Dummy1.IsAlive)
            {
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                ActorUsesSkill(Player, 1, Dummy1);
            }
            EnemyKilled(Dummy1);
            Pause();

            Say("Nice job. Unfortunately, you don't have any stamina left to take out");
            Say("the other two dummies this turn. Brace yourself!");
            Pause();

            #endregion

            #region Turn 3
            Say("ENEMY TURN");
            Pause();

            Say("");
            Say("You'll pay for that!", Dummy2);
            Pause();

            Say("");
            Say("DEATH TO THE HUMANS!", Dummy3);
            Pause();

            ActorUsesSkill(Dummy2, 1, Player);
            ActorUsesSkill(Dummy3, 1, Player);
            #endregion

            #region Turn 4
            Say("PLAYER TURN");
            Pause();

            Player.SP.RegenTick();

            Say("");
            Say("Good thing you've got armor. Notice that some of your stamina has regenerated.");
            Say("Every turn, you will regain an amount of SP. The amount can be affected by");
            Say("your Dexterity stat, any charms you might have equipped, and any effects you may");
            Say("be under.");
            Pause();

            Say("");
            Say("Now, let's take down another enemy. First, change targets with the up and down");
            Say("arrow keys. You don't want to waste stamina beating a dead dummy.");

            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.DownArrow)
                {
                    CombatUI.ChangeTarget(keyPressed);
                    break;
                }
            }

            Say("");
            Say("Now, with Training Dummy 2 targeted, use your double slash skill to destroy it.");
            while (true)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.DownArrow)
                {
                    CombatUI.ChangeTarget(keyPressed);
                }
                else if (keyPressed == ConsoleKey.D1 && CombatUI.TargetIndex == 2)
                {
                    if (Player.Skillbar.Skills[0].HasEnoughSP)
                    {
                        ActorUsesSkill(Player, 1, Dummy2);
                    }
                    else
                    {
                        Say("");
                        ActorUsesSkill(Player, 1, Dummy2);
                        Pause();
                        Say("");
                        Say("You'll have to wait for your stamina to regenerate again. To end your turn");
                        Say("at any time during combat, press SPACE.");
                    }
                }
                else if (keyPressed == ConsoleKey.Spacebar && !Player.Skillbar.Skills[0].HasEnoughSP)
                {
                    break;
                }
            }
            #endregion

            #region Turn 5
            Say("ENEMY TURN");
            Pause();
            ActorUsesSkill(Dummy2, 1, Player);
            ActorUsesSkill(Dummy3, 1, Player);
            #endregion

            #region Turn 6
            Say("PLAYER TURN");
            Pause();

            Player.SP.RegenTick();

            Say("");
            Say("Finish that dummy off!");
            while (Dummy2.IsAlive)
            {
                while (true)
                {
                    if (Console.ReadKey().Key == ConsoleKey.D1)
                    {
                        break;
                    }
                    else
                    {
                        continue;
                    }
                }

                ActorUsesSkill(Player, 1, Dummy2);
            }
            EnemyKilled(Dummy2);
            Pause();
            Say("");
            Say("You don't have enough stamina to kill the third dummy this turn. Instead,");
            Say("pass the turn with SPACE and conserve your stamina for a more focused");
            Say("set of attacks next turn.");
            while (Console.ReadKey().Key != ConsoleKey.Spacebar)
            {
                continue;
            }
            #endregion

            #region Turn 7
            Say("ENEMY TURN");
            Say("AHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHAHA!", Dummy3);
            ActorUsesSkill(Dummy3, 1, Player);
            #endregion

            #region Turn 8
            Say("PLAYER TURN");
            Player.SP.RegenTick();

            Say("");
            Say("Target the final dummy and put a stop to all this flailing.");
            Pause();

            while (Dummy3.IsAlive)
            {
                ConsoleKey keyPressed = Console.ReadKey().Key;
                if (keyPressed == ConsoleKey.UpArrow || keyPressed == ConsoleKey.DownArrow)
                {
                    CombatUI.ChangeTarget(keyPressed);
                }
                else if (keyPressed == ConsoleKey.D1 && CombatUI.TargetIndex == 3)
                {
                    ActorUsesSkill(Player, 1, Dummy3);
                }
            }
            EnemyKilled(Dummy3);
            #endregion

            Pause();
            Say("");
            Say("You've killed them all! Well, knocked them over, anyway. Time to see your");
            Say("rewards.");
            Pause();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("    After combat, you'll be taken to the victory screen. This screen displays\n");
            Console.WriteLine("    all items dropped by the enemies in combat and any experience gained.");
            Console.ReadLine();

            VictoryScreen();

            Console.WriteLine();
            Console.WriteLine("End of module, closing program.");
            Console.ReadLine();
            Environment.Exit(0);
        }
    }
}
