using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;

namespace TownAttackRPG.DAL.DAOs.Json
{
    public class ItemJsonDAO : IItemDAO
    {
        readonly string JsonFolderPath;
        public ItemJsonDAO(string jsonFolderPath)
        {
            this.JsonFolderPath = jsonFolderPath;
        }

        public Item CreateNewItem(int id)
        {
            string path = JsonFolderPath + "/Item.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
                IEnumerable<Item> itemResult = from i in items
                                               where i.ItemID == id
                                               select i;
                if (itemResult.Count() == 0)
                {
                    throw new ArgumentException($"Argument Exception: ItemID {id} not found in item database.");
                }
                return itemResult.ToList<Item>()[0];
            }
        }
        public Item CreateNewItem(string itemName)
        {
            string path = JsonFolderPath + "/Item.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                List<Item> items = JsonConvert.DeserializeObject<List<Item>>(jsonData);
                IEnumerable<Item> itemResult = from i in items
                                               where i.ItemName == itemName
                                               select i;
                if (itemResult.Count() == 0)
                {
                    throw new ArgumentException($"Argument Exception: {itemName} not found in item database.");
                }
                return itemResult.ToList<Item>()[0];
            }
        }

        public EquipmentItem CreateNewEquipmentItem(int id)
        {
            string path = JsonFolderPath + "/Equipment.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                List<EquipmentItem> items = JsonConvert.DeserializeObject<List<EquipmentItem>>(jsonData);
                IEnumerable<EquipmentItem> itemResult = from i in items
                                                        where i.ItemID == id
                                                        select i;
                if (itemResult.Count() == 0)
                {
                    throw new ArgumentException($"Argument Exception: ItemID {id} not found in equipment database.");
                }

                return itemResult.ToList<EquipmentItem>()[0];
            }
        }
        public EquipmentItem CreateNewEquipmentItem(string itemName)
        {
            string path = JsonFolderPath + "/Equipment.json";
            using (StreamReader sr = new StreamReader(path))
            {
                string jsonData = sr.ReadToEnd();
                List<EquipmentItem> items = JsonConvert.DeserializeObject<List<EquipmentItem>>(jsonData);
                IEnumerable<EquipmentItem> itemResult = from i in items
                                                        where i.ItemName == itemName
                                                        select i;
                if (itemResult.Count() == 0)
                {
                    throw new ArgumentException($"Argument Exception: {itemName} not found in equipment database.");
                }
                return itemResult.ToList<EquipmentItem>()[0];
            }
        }
    }
}
