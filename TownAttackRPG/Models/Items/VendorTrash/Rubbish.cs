using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Items.VendorTrash
{
    public class Rubbish : Item
    {
        public Rubbish()
        {
            ItemName = "Rubbish";
            ItemDescrip = "Worthless junk.";
            Value = 0;
            Weight = 1;
        }
    }
}
