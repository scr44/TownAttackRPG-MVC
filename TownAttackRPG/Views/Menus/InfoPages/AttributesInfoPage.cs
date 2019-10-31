using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus.InfoPages
{
    static public class AttributesInfoPage
    {
        static public string Infopage
        {
            get
            {
                return @"


        ESC: Back            


        Strength (STR): Physical power. Affects crushing damage and weight carrying capacity. Required to use heavy weapons and armor.


        Dexterity (DEX): Speed and flexibility. Affects slashing damage and stamina regeneration. Required for many light weapons and armors.


        Skill (SKL): Precision and training. Affects piercing damage and armor penetration. Required for weapons that focus on finding weak points.


        Aptitude (APT): Ability to learn new things. Affects experience gained.


        Fortitude (FOR): Resilience and willpower. Improves defense against physical damage and many negative skill effects.


        Charisma (CHA): Charm and sociability. Opens many opportunities through persuasion.


        All Attributes can be used in certain events. Doing so may bring extra rewards... or challenges.
";
            }
        }

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
