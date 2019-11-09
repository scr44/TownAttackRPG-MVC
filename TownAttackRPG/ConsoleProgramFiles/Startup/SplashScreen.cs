using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TownAttackRPG.Views.Menus.Startup
{
    public class SplashScreen
    {
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
        string pressEnter = @"
                                                          Press Enter
";

        #region Title split into words
        string TOWN = @"
::::::::::: ::::::::  :::       ::: ::::    ::: 
    :+:    :+:    :+: :+:       :+: :+:+:   :+: 
    +:+    +:+    +:+ +:+       +:+ :+:+:+  +:+ 
    +#+    +#+    +:+ +#+  +:+  +#+ +#+ +:+ +#+ 
    +#+    +#+    +#+ +#+ +#+#+ +#+ +#+  +#+#+# 
    #+#    #+#    #+#  #+#+# #+#+#  #+#   #+#+# 
    ###     ########    ###   ###   ###    #### 
";
        string ATTACK = @"
    ::: ::::::::::: ::::::::::: :::      ::::::::  :::    ::: 
  :+: :+:   :+:         :+:   :+: :+:   :+:    :+: :+:   :+:  
 +:+   +:+  +:+         +:+  +:+   +:+  +:+        +:+  +:+   
+#++:++#++: +#+         +#+ +#++:++#++: +#+        +#++:++    
+#+     +#+ +#+         +#+ +#+     +#+ +#+        +#+  +#+   
#+#     #+# #+#         #+# #+#     #+# #+#    #+# #+#   #+#  
###     ### ###         ### ###     ###  ########  ###    ###
";
        #endregion

        public void DisplayTitle(bool showPrompt)
        {
            Console.Write(TitleAlt);
            for (int i = 0; i < 8; i++)
            {
                Console.WriteLine();
            }
            if (showPrompt)
            {
                Console.WriteLine(pressEnter);
            }
        }
        public void FlashingPrompt()
        {
            Console.Clear();
            for (int i = 0; i < 12; i++)
            {
                Console.WriteLine();
            }
            DisplayTitle(false);
            bool showPrompt = true;
            TitleJingle();
            while(!Console.KeyAvailable)
            {
                Console.Clear();
                for (int i = 0; i < 12; i++)
                {
                    Console.WriteLine();
                }
                DisplayTitle(showPrompt);
                for (int i = 0; i < 8; i++)
                {
                    Console.WriteLine();
                }
                Thread.Sleep(500);
                showPrompt = !showPrompt;
            }
        }
        public void AscendingTitle()
        {
            PressedEnterJingle();
            for(int j = 11; j > 5; j--)
            {
                Console.Clear();
                for (int i = 0; i < j; i++)
                {
                    Console.WriteLine();
                }
                DisplayTitle(false);
                Thread.Sleep(25);
            }
        }
        public void TitleJingle()
        {
            Console.Beep(600, 100);
            Console.Beep(700, 100);
            Console.Beep(650, 300);
            Console.Beep(500, 200);
            Console.Beep(550, 150);
        }
        public void PressedEnterJingle()
        {
            Console.Beep(800, 90);
            Console.Beep(1400, 100);
            Console.Beep(1200, 80);
            Console.Beep(1400, 100);
        }

    }
}
