using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus.InfoPages
{
    public static class FeatureIncompleteInfoPage
    {
        static public string Infopage
        {
            get
            {
                return @"


        Sorry, this feature is incomplete. Press ESC to go back.";
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
