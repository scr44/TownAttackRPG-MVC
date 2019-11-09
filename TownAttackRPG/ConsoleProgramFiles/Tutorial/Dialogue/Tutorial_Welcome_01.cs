using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Scenarios;

namespace TownAttackRPG.Models.Scenarios.Tutorial.Dialogue
{
    public class Tutorial_Welcome_01 : DialogueEvent
    {
        public void Run()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(
 "   Welcome to Town Attack RPG. Let's get started by going over the controls.\n");
            Console.WriteLine(
 "     (Press ENTER to continue after a text prompt)");
            Pause();

            Console.Clear();
            Console.WriteLine();
            Console.WriteLine(
 "   In this tutorial, you control a Knight named Guinevere.\n");
            Pause();
            Console.WriteLine(
@"   Knights are hardy warriors, wielding longswords with both hands and wearing heavy plate armor.
    
     Let's take a look at the combat menu.
");
            Pause();
        }
    }
}
