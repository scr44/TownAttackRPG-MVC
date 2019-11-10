using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TownAttackRPG.Models.Items.Equipment;

namespace TownAttackRPG.Models.Items
{
    public class Item
    {
        public int ItemID { get; set; }
        public string ItemName { get; set; }
        public string ItemDescrip { get; set; }
        public int Value { get; set; }
        public double Weight { get; set; }
        public int MaxStackSize { get; set; } = 250;
        public int StackSize { get; set; }
    }
}
