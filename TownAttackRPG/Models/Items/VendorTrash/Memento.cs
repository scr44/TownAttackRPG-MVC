using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Items.VendorTrash
{
    public class Memento : Item
    {
        public Memento()
        {
            ItemName = "Memento";
            ItemDescrip = "A trinket to commemorate a victorious battle long ago.";
            Value = 20;
            Weight = 2.0;
        }
    }
}
