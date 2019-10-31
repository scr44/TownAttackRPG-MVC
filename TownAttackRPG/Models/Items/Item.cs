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

        /// <summary>
        /// Creates a new item based on the item category and ID.
        /// </summary>
        /// <param name="category">Accepts "Item" or "Equipment"</param>
        /// <param name="id">The item ID in the given database.</param>
        /// <returns></returns>
        public static Item CreateNew(string category, int id)
        {
            if (!(category == "Item" || category == "Equipment"))
            {
                throw new ArgumentException("Cannot create item: invalid category.");
            }

            string file =
                @"../../../../TownAttackRPG/bin/Debug/netcoreapp2.2/Models/Items/Database/" 
                + category + ".json";
            using (StreamReader sr = new StreamReader(file))
            {
                if (category == "Equipment")
                {
                    string json = sr.ReadToEnd();
                    List<EquipmentItem> items = JsonConvert.DeserializeObject<List<EquipmentItem>>(json);
                    IEnumerable<EquipmentItem> itemResult = from i in items
                                                            where i.ItemID == id
                                                            select i;

                    return (Item)itemResult.ToList<EquipmentItem>()[0];
                }
                else
                {
                    string json = sr.ReadToEnd();
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                    IEnumerable<Item> itemResult = from i in items
                                     where i.ItemID == id
                                     select i;
                    return itemResult.ToList<Item>()[0];
                }
            }
        }
        /// <summary>
        /// Creates a new item based on item category and name.
        /// </summary>
        /// <param name="category">Accepts "Item" or "Equipment"</param>
        /// <param name="itemName">The item name in the given database.</param>
        /// <returns></returns>
        public static Item CreateNew(string category, string itemName)
        {
            if (!(category == "Item" || category == "Equipment"))
            {
                throw new ArgumentException("Cannot create item: invalid category.");
            }

            string file =
                @"../../../../TownAttackRPG/bin/Debug/netcoreapp2.2/Models/Items/Database/"
                + category + ".json";
            using (StreamReader sr = new StreamReader(file))
            {
                if (category == "Equipment")
                {
                    string json = sr.ReadToEnd();
                    List<EquipmentItem> items = JsonConvert.DeserializeObject<List<EquipmentItem>>(json);
                    IEnumerable<EquipmentItem> itemResult = from i in items
                                                            where i.ItemName == itemName
                                                            select i;

                    return (Item)itemResult.ToList<EquipmentItem>()[0];
                }
                else
                {
                    string json = sr.ReadToEnd();
                    List<Item> items = JsonConvert.DeserializeObject<List<Item>>(json);
                    IEnumerable<Item> itemResult = from i in items
                                                   where i.ItemName == itemName
                                                   select i;
                    return itemResult.ToList<Item>()[0];
                }
            }
        }
    }
}
