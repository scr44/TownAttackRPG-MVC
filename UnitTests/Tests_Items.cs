using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.Models.Items;

namespace ItemsAndEquipment
{
    [TestClass]
    public class ItemDatabaseReading
    {

    }
    [TestClass]
    public class EquipmentItemCreation
    {
        [TestMethod]
        public void Longsword()
        {
            Item longsword = Item.CreateNew("Equipment", 3);
            Assert.AreEqual("Longsword", longsword.ItemName);

            longsword = Item.CreateNew("Equipment", "Longsword");
            Assert.AreEqual("Longsword", longsword.ItemName);
        }
        
    }
}
