using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Scenario
{
    public class Event
    {
        public void EventText(string text)
        {
            // TODO Event: center text
            Console.WriteLine(text);
        }

        public void Pause()
        {
            ConsoleKey key = ConsoleKey.A;
            while (key != ConsoleKey.Enter)
            {
                key = Console.ReadKey().Key;
            }
        }
    }
}
