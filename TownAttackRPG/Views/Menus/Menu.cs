using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus
{
    abstract public class Menu
    {
        public string Title { get; private set; }
        public string MenuOptions { get; private set; }
        abstract public string Selection { get; set; }

        abstract public string DisplayOptions();
        abstract public ConsoleKey DisplayCursor(int cursor);
        abstract public int MoveCursor(ConsoleKey keyPressed, int cursor, int maxCursor);
        abstract public void SelectOption(int cursor);
        public void CursorMoveBeep()
        {
            Console.Beep(500, 100);
        }
        public void CursorSelectBeep()
        {
            Console.Beep(900, 100);
            Console.Beep(1200, 80);
        }
    }
}
