using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using System;
using System.Collections.Generic;
using System.Text;

namespace TownAttackRPG.Models.Actors.ActorProperties
{
    public class InventoryOLD
    {
        public InventoryOLD() { }
        public InventoryOLD(Dictionary<Item,int> initItems)
        {
            foreach(var item in initItems)
            {
                for (int i = 1; i <= item.Value; i++)
                {
                    this.AddItem(item.Key);
                }
            }
        }
        public InventoryOLD(Character character, Dictionary<Item, int> initItems)
        {
            AttachedCharacter = character;
            foreach (var item in initItems)
            {
                for (int i = 1; i <= item.Value; i++)
                {
                    this.AddItem(item.Key);
                }
            }
        }

        public Character AttachedCharacter { get; }
        public double WeightCapacity
        {
            get
            {
                if (AttachedCharacter is null)
                {
                    return 9999; // don't bother for non-Character entities
                }

                double moddedSTR = AttachedCharacter.Attributes.BaseValue["STR"]
                    + AttachedCharacter.EquipmentMod("STR", AttachedCharacter.Equipment) 
                    + AttachedCharacter.EffectMod("STR");
                if (moddedSTR <= 10)
                {
                    // 15 points of Weight Cap for every STR up to 10
                    return moddedSTR * 15;
                }
                else
                {
                    // Diminishing max Weight Capacity returns for STR > 10
                    return (moddedSTR - 10) * 5 + (10 * 15);
                }

            }
        }
        public double WeightLoad
        {
            get
            {
                if (AttachedCharacter is null)
                {
                    return 0; // don't bother for non-Character entities
                }

                double weight = 0;
                foreach (KeyValuePair<string, Item> item in this.InventoryContents)
                {
                    // weight of all items in inventory
                    weight += this.InventoryContents[item.Key].Weight
                        * this.InventoryCounts[item.Key];
                }
                foreach (var item in AttachedCharacter.Equipment.Slot)
                {
                    // weight of all equipped items
                    weight += AttachedCharacter.Equipment.Slot[item.Key].Weight;
                }
                return weight;
            }
        }
        public bool IsOverburdened
        {
            // If held weight exceeds the weight capacity.
            get
            {
                return WeightLoad > WeightCapacity;
            }
        } 

        /// <summary>
        /// The contents of a character's inventory.
        /// </summary>
        public Dictionary<string, Item> InventoryContents { get; private set; } =
            new Dictionary<string, Item>();
        /// <summary>
        /// The counts of a character's inventory.
        /// </summary>
        public Dictionary<string, int> InventoryCounts { get; private set; } =
            new Dictionary<string, int>();

        /// <summary>
        /// Adds the given item to the inventory.
        /// </summary>
        /// <param name="item"></param>
        public void AddItem(Item item)
        {
            if (InventoryContents.TryAdd(item.ItemName, item))
            {
                InventoryCounts.Add(item.ItemName, 1);
            }
            else
            {
                InventoryCounts[item.ItemName]++;
            }
        }
        public void AddItem(Item item, int count)
        {
            for (int i = 0; i < count; i++)
            {
                this.AddItem(item);
            }
        }
        /// <summary>
        /// Removes an item from the inventory and returns it to the caller.
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void RemoveItem(string itemName)
        {
            if (InventoryCounts[itemName] > 1)
            {
                InventoryCounts[itemName]--;
            }
            else if (InventoryCounts[itemName] == 1)
            {
                InventoryContents.Remove(itemName);
                InventoryCounts.Remove(itemName);
            }
            else
            {
                throw new ArgumentOutOfRangeException("An inventory cannot have a negative number of items.");
            }
        }
        public void RemoveItem(string itemName, int count)
        {
            for(int i = 0; i < count; i++)
            {
                this.RemoveItem(itemName);
            }
        }
        /// <summary>
        /// Transfers an item from this inventory to the target inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        public void GiveItem(Item item, Inventory target)
        {
            target.AddItem(item);
            this.RemoveItem(item.ItemName);
        }
        /// <summary>
        /// Transfers an item from the target inventory to this one.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        public void TakeItem(Item item, Inventory target)
        {
            this.AddItem(item);
            target.RemoveItem(item.ItemName);
        }
    }
}
