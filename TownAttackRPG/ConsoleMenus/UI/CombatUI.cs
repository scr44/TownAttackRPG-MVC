using TownAttackRPG.Models.Actors;
using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Skills;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus.Combat
{
    public class CombatUI
    {
        public CombatUI(Character player)
        {
            Player = player;
            for (int i=0; i<LogHeight; i++)
            {
                CombatLog.Add("");
            }
        }
        Character Player { get; }

        #region Bars and Equipment
        int MaxBarLength = 40;
        string HPbar
        {
            get
            {
                int fillLength = (int)(MaxBarLength * Player.HP.Percent * 0.01);
                string bar = new string('=', fillLength) + new string(' ', MaxBarLength - fillLength);
                return bar;
            }
        }
        string SPbar
        {
            get
            {
                int fillLength = (int)(MaxBarLength * Player.SP.Percent * 0.01);
                string bar = new string(' ', MaxBarLength - fillLength) + new string('=', fillLength);
                return bar;
            }
        }
        string MainHand
        {
            get
            {
                return Player.Equipment.Slot["MainHand"].ItemName;
            }
        }
        string OffHand
        {
            get
            {
                return Player.Equipment.Slot["OffHand"].ItemName;
            }
        }
        List<string> skillNames
        {
            get
            {
                List<string> names = new List<string>();
                foreach (KeyValuePair<int, string> skill in Player.Skillbar.SkillNames)
                {
                    names.Add(skill.Value);
                }
                return names;
            }
        }
        #endregion

        #region Combat Log
        private const int LogHeight = 35;
        List<string> CombatLog { get; set; } = new List<string>();
        public void WriteToLog(string text, int line=LogHeight)
        {
            if (line == LogHeight)
            {
                CombatLog.RemoveAt(0);
                CombatLog.Add(text);
                CombatLog.RemoveAt(0);
                CombatLog.Add("");
            }
            else
            {
                CombatLog[line] = text;
            }
            Display();
        }
        #endregion

        #region Targeting UI
        public int TargetIndex { get; set; } = 1;
        public List<Actor> TargetList { get; private set; }
        public List<string> TargetHPList
        {
            get
            {
                var list = new List<string>();
                foreach (Actor target in TargetList)
                {
                    if (target.IsAlive)
                    {
                        list.Add($"HP: {target.HP.Current}/{target.HP.Max}");
                    }
                    else
                    {
                        list.Add($"    DEAD");
                    }
                }
                return list;
            }
        }
        public void SetTargetList(List<Actor> list)
        {
            TargetList = list;
        }
        public List<string> TargetingMenu
        {
            get
            {
                List<string> list = new List<string>(LogHeight);
                for (int i = 0; i < LogHeight; i++)
                {
                    list.Add("");
                }

                int j = 0;
                int nextNameIndex = 2;
                for (int i = 1; j < TargetList.Count; i++)
                {
                    if (i == nextNameIndex && j < TargetList.Count)
                    {
                        list[i - 1] = TargetList[j].Name;
                        list[i] = TargetHPList[j];

                        if (j + 1 == TargetIndex)
                        {
                            list[i - 1] += " <";
                        }
                        
                        j++;
                        nextNameIndex += 3;
                    }
                }
                return list;
            }
        }
        public void ChangeTarget(ConsoleKey direction)
        {
            if (direction == ConsoleKey.UpArrow)
            {
                TargetIndex--;
                if (TargetIndex < 1)
                {
                    TargetIndex = TargetList.Count;
                }
            }
            else if (direction == ConsoleKey.DownArrow)
            {
                TargetIndex++;
                if (TargetIndex > TargetList.Count)
                {
                    TargetIndex = 1;
                }
            }
            Display();
        }
        #endregion

        public string AssembleHUD()
        {
            string HUD = "";
            HUD += "     Inventory: I     Skills: K     Character: C     Flee: F     Game Menu: F10" + "\n";
            HUD +=  "+------------------------------------------------------------------------------------------------------+" + "\n";
            for (int i=0; i<LogHeight; i++)
            {
                HUD += $"| {CombatLog[i],-80}" + " | " + $"{TargetingMenu[i],-18}|\n";
            }
            HUD +=  "+------------------------------------------------------------------------------------------------------+" + "\n";
            HUD += $"{new string(' ', 20)}HP{new string(' ', 40)}SP" + "\n";
            HUD += $"+----------------------------------------+ +----------------------------------------+ +----------------+" + "\n";
            HUD += $"|{HPbar,-40}" +                        $"| |{SPbar,-40}|" +                        $" | {Player.FirstName,-15}|" + "\n";
            HUD += $"+----------------------------------------+ +----------------------------------------+ | -Main Hand-    |" + "\n";
            HUD += $"+-----------------------------------------------------------------------------------+ | {MainHand,15}|" + "\n";
            HUD += $"|      1      |      2      |      3      |      4      |      5      |      6      | | -Off Hand-     | " + "\n";
            HUD += $"|{skillNames[0],-13}|{skillNames[1],-13}|{skillNames[2],-13}|{skillNames[3],-13}|{skillNames[4],-13}|{skillNames[5],-13}| | {OffHand,15}|" + "\n";
            HUD += $"+-----------------------------------------------------------------------------------+ +----------------+" + "\n";
            return HUD;
        }
        public void Display()
        {
            Console.Clear();
            Console.WriteLine(AssembleHUD());
        }
    }
}
