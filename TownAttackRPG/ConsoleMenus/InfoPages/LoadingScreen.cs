using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace TownAttackRPG.Views.Menus.InfoPages
{
    public static class LoadingScreen
    {
        static public void Display(int milliseconds)
        {
            Console.Clear();
            Console.WriteLine(
                @"
    __                ___                
   / /___  ____ _____/ (_)___  ____ _    
  / / __ \/ __ `/ __  / / __ \/ __ `/    
 / / /_/ / /_/ / /_/ / / / / / /_/ / _ _ 
/_/\____/\__,_/\__,_/_/_/ /_/\__, (_|_|_)
                            /____/       

"
                );
            Thread.Sleep(milliseconds);
        }
    }
}
