using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using TownAttackRPG.DAL.DAOs.Json;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.Models.Items;

namespace ItemsAndEquipment
{
    [TestClass]

    public class ItemCreation
    {
        const string jsonPath = @"./DAL/JsonData";
        protected IItemDAO ItemDAO { get; set; }
        public ItemCreation()
        {
            ItemDAO = new ItemJsonDAO(jsonPath);
        }

        [TestMethod]
        public void CreateItem()
        {
            Item coins = ItemDAO.CreateNewItem(0);
            Assert.AreEqual("Coins", coins.ItemName);

            coins = ItemDAO.CreateNewItem("Coins");
            Assert.AreEqual("Coins", coins.ItemName);
        }

        [TestMethod]
        public void CreateEquipmentItem()
        {
            Item longsword = ItemDAO.CreateNewEquipmentItem(3);
            Assert.AreEqual("Longsword", longsword.ItemName);

            longsword = ItemDAO.CreateNewEquipmentItem("Longsword");
            Assert.AreEqual("Longsword", longsword.ItemName);
        }
        
    }
}
