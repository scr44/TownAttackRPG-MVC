using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using TownAttackRPG.Views.Menus;

namespace TownAttackRPG.Views.Menus.Startup
{
    public class MainMenu : Menu
    {
        #region Title and Options
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
        string MenuOptions1 = @"
                                                          > New Game <

                                                            Continue      

                                                             Option

                                                              Quit
";
        string MenuOptions2= @"
                                                            New Game

                                                          > Continue <    

                                                             Option

                                                              Quit
";
        string MenuOptions3= @"
                                                            New Game

                                                            Continue    

                                                           > Option <

                                                              Quit
";
        string MenuOptions4= @"
                                                            New Game

                                                            Continue    

                                                             Option

                                                            > Quit <
";
        #endregion

        override public string Selection { get; set; }

        override public string DisplayOptions()
        {
            int cursor = 1;
            while(cursor != 0)
            {
                Console.Clear();
                Console.WriteLine(TitleAlt);

                for (int i = 0; i < 10; i++)
                {
                    Console.WriteLine();
                }

                ConsoleKey keyPressed = DisplayCursor(cursor);

                cursor = MoveCursor(keyPressed, cursor); // Selection happens in here
            }
            return Selection;
        } // Main loop
        override public ConsoleKey DisplayCursor(int cursor)
        {
            if (cursor == 1)
            {
                Console.WriteLine(MenuOptions1);
            }
            else if (cursor == 2)
            {
                Console.WriteLine(MenuOptions2);
            }
            else if (cursor == 3)
            {
                Console.WriteLine(MenuOptions3);
            }
            else if (cursor == 4)
            {
                Console.WriteLine(MenuOptions4);
            }

            return Console.ReadKey().Key;
        }
        override public int MoveCursor(ConsoleKey keyPressed, int cursor, int maxCursor=4)
        {
            if(keyPressed == ConsoleKey.UpArrow)
            {
                CursorMoveBeep();
                if (cursor == 1)
                { return maxCursor; }
                else
                { return cursor -= 1; }
            }
            else if(keyPressed == ConsoleKey.DownArrow)
            {
                CursorMoveBeep();
                if (cursor == maxCursor)
                { return 1; }
                else
                { return cursor += 1; }
            }
            else if(keyPressed == ConsoleKey.Enter)
            {
                CursorSelectBeep();
                SelectOption(cursor);
                return 0;
            }
            else
            {
                return cursor;
            }
        }
        override public void SelectOption(int cursor)
        {
            switch(cursor)
            {
                case 1:
                    Selection = "New Game";
                    break;
                case 2:
                    Selection = "Continue";
                    break;
                case 3:
                    Selection = "Options";
                    break;
                default:
                    Selection = "Exit Program";
                    break;
            }
        }
        
    }
}
