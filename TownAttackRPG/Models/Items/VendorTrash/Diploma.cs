using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Items.VendorTrash
{
    class Diploma : Item
    {
        public Diploma()
        {
            ItemName = "Diploma";
            ItemDescrip = "A very expensive piece of parchment. For you, at least.";
            Value = 0;
            Weight = 1.0;
        }
    }
}
