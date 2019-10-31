using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Views.Menus.Startup
{
    public class ScenarioSelect : Menu
    {
        #region Title & Option strings
        string TitleAlt = @"





       ::::::::::: ::::::::  :::       ::: ::::    :::          ::: ::::::::::: ::::::::::: :::      ::::::::  :::    ::: 
           :+:    :+:    :+: :+:       :+: :+:+:   :+:        :+: :+:   :+:         :+:   :+: :+:   :+:    :+: :+:   :+:  
           +:+    +:+    +:+ +:+       +:+ :+:+:+  +:+       +:+   +:+  +:+         +:+  +:+   +:+  +:+        +:+  +:+   
           +#+    +#+    +:+ +#+  +:+  +#+ +#+ +:+ +#+      +#++:++#++: +#+         +#+ +#++:++#++: +#+        +#++:++    
           +#+    +#+    +#+ +#+ +#+#+ +#+ +#+  +#+#+#      +#+     +#+ +#+         +#+ +#+     +#+ +#+        +#+  +#+   
           #+#    #+#    #+#  #+#+# #+#+#  #+#   #+#+#      #+#     #+# #+#         #+# #+#     #+# #+#    #+# #+#   #+#  
           ###     ########    ###   ###   ###    ####      ###     ### ###         ### ###     ###  ########  ###    ### 



                                                  :::::::::  :::::::::   :::::::: 
                                                 :+:    :+:  :+:    :+:  :+:    :+: 
                                                 +:+    +:+  +:+    +:+  +:+         
                                                +#++:++#:    +#++:++#+    :#:          
                                               ++#    ++#    +#+           #++   ##++    
                                               +#+    +#+    #+#           +#+    +#+     
                                              ###    ###     ###             ########   ";

        string TutorialSelected = @"
                                                           > Tutorial <         

                                                        Town Attack Classic  

                                                               Back
";
        string TACSelected = @"
                                                             Tutorial           

                                                      > Town Attack Classic <

                                                               Back
";
        string BackSelected = @"
                                                             Tutorial           

                                                        Town Attack Classic  

                                                             > Back <
";
        #endregion

        override public string Selection { get; set; } = "Scenario Menu";

        public override string DisplayOptions()
        {
            int cursor = 1;
            while (cursor != 0)
            {
                Console.Clear();
                Console.WriteLine(TitleAlt);

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine();
                }

                ConsoleKey keyPressed = DisplayCursor(cursor);

                cursor = MoveCursor(keyPressed, cursor);
            }
            return Selection;
        }
        public override ConsoleKey DisplayCursor(int cursor)
        {
            switch (cursor)
            {
                case 1:
                    Console.WriteLine(TutorialSelected);
                    break;
                case 2:
                    Console.WriteLine(TACSelected);
                    break;
                case 3:
                    Console.WriteLine(BackSelected);
                    break;
            }
            return Console.ReadKey().Key;
        }
        public override int MoveCursor(ConsoleKey keyPressed, int cursor, int maxCursor = 3)
        {
            switch (keyPressed)
            {
                case ConsoleKey.UpArrow:
                    CursorMoveBeep();
                    if (cursor == 1)
                    { return maxCursor; }
                    else
                    { return cursor -= 1; }

                case ConsoleKey.DownArrow:
                    CursorMoveBeep();
                    if (cursor == maxCursor)
                    { return 1; }
                    else
                    { return cursor += 1; }

                case ConsoleKey.Enter:
                    CursorSelectBeep();
                    SelectOption(cursor);
                    return 0;

                case ConsoleKey.Escape:
                    CursorSelectBeep();
                    Selection = "Back";
                    return 0;

                default:
                    return cursor;
            }
        }
        public override void SelectOption(int cursor)
        {
            switch (cursor)
            {
                case 1:
                    Selection = "Begin Scenario: Tutorial";
                    break;
                case 2:
                    Selection = "Begin Scenario: Town Attack Classic";
                    break;
                case 3:
                    Selection = "Back";
                    break;
            }
        }
    }
}
