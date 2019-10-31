using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus.InfoPages
{
    static public class TalentsInfoPage
    {
        static public string Infopage = @"


        ESC: Back         


        Medicine: The art of healing and diagnosing injuries. Improves effectiveness of healing items and resistance to bleeding.

        
        Explosives: Use of bombs and fire. Improves damage with bombs and resistance to fire damage.


        Veterancy: Experience in combat. Improves damage and armor penetration against human enemies.


        Bestiary: Experience hunting beasts. Improves damage against beast enemies and resistance against poisons and 
                  venoms.

        Engineering: Skill at repair and knowledge of machinery. Improves damage against mechanical enemies and armor   
                     penetration.

        History: Knowledge of culture, politics, and the past. Allows the identification of items, providing more loot 
                 from enemies and chests.

        All talents can be used in events, opening new dialogue, combat, and rewards.
";
        static public void Display()
        {
            bool showInfo = true;
            while (showInfo)
            {
                Console.Clear();
                Console.WriteLine(Infopage);
                if (Console.ReadKey().Key == ConsoleKey.Escape)
                { showInfo = false; }
            }
        }
    }
}
