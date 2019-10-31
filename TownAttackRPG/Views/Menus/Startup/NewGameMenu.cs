using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TownAttackRPG.Views.Menus.Startup
{
    public class NewGameMenu : Menu
    {
        #region Title and Options strings
        string Title = @"





       ::::::::::: ::::::::  :::       ::: ::::    :::          ::: ::::::::::: ::::::::::: :::      ::::::::  :::    ::: 
           :+:    :+:    :+: :+:       :+: :+:+:   :+:        :+: :+:   :+:         :+:   :+: :+:   :+:    :+: :+:   :+:  
           +:+    +:+    +:+ +:+       +:+ :+:+:+  +:+       +:+   +:+  +:+         +:+  +:+   +:+  +:+        +:+  +:+   
           +#+    +#+    +:+ +#+  +:+  +#+ +#+ +:+ +#+      +#++:++#++: +#+         +#+ +#++:++#++: +#+        +#++:++    
           +#+    +#+    +#+ +#+ +#+#+ +#+ +#+  +#+#+#      +#+     +#+ +#+         +#+ +#+     +#+ +#+        +#+  +#+   
           #+#    #+#    #+#  #+#+# #+#+#  #+#   #+#+#      #+#     #+# #+#         #+# #+#     #+# #+#    #+# #+#   #+#  
           ###     ########    ###   ###   ###    ####      ###     ### ###         ### ###     ###  ########  ###    ### 



                                                  :::::::::  :::::::::   :::::::: 
                                                 :+:    :+: :+:    :+: :+:    :+: 
                                                +:+    +:+ +:+    +:+ +:+         
                                               +#++:++#:  +#++:++#+  :#:          
                                              +#+    +#+ +#+        +#+   +#+#    
                                             #+#    #+# #+#        #+#    #+#     
                                            ###    ### ###         ########   ";
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
        string MenuOptions = @"
                                                  Select a Pre-Made Profession
                                                         (Recommended)                  
         
                                                       Custom Profession
         
                                                             Back
";
        string MenuOptions1 = @"
                                                > Select a Pre-Made Profession <
                                                         (Recommended)                  
         
                                                       Custom Profession
         
                                                             Back
";
        string MenuOptions2 = @"
                                                  Select a Pre-Made Profession
                                                         (Recommended)                  
         
                                                     > Custom Profession <
         
                                                             Back
";
        string MenuOptions3 = @"
                                                  Select a Pre-Made Profession
                                                         (Recommended)                  
         
                                                       Custom Profession
         
                                                           > Back <
";
        #endregion

        override public string Selection { get; set; } = "Run New Game Menu";

        override public string DisplayOptions()
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
        override public ConsoleKey DisplayCursor(int cursor)
        {
            switch(cursor)
            {
                case 1:
                    Console.WriteLine(MenuOptions1);
                    break;
                case 2:
                    Console.WriteLine(MenuOptions2);
                    break;
                case 3:
                    Console.WriteLine(MenuOptions3);
                    break;
            }
            return Console.ReadKey().Key;
        }
        override public int MoveCursor(ConsoleKey keyPressed, int cursor, int maxCursor=3)
        {
            switch(keyPressed)
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
        override public void SelectOption(int cursor)
        {
            switch(cursor)
            {
                case 1:
                    Selection = "Select Pre-Made Profession";
                    break;
                case 2:
                    Selection = "Custom Profession";
                    break;
                case 3:
                    Selection = "Back";
                    break;
            }
        }
        //public override void CursorMoveBeep()
        //{
        //    Console.Beep(500, 100);
        //}
        //public override void CursorSelectBeep()
        //{
        //    Console.Beep(900, 100);
        //    Console.Beep(1200, 80);
        //}
    }
}
