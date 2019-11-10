using TownAttackRPG.Models.Actors.Characters;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TownAttackRPG.DAL.Interfaces;
using TownAttackRPG.DAL.DAOs.Json;

namespace TownAttackRPG.Models.Actors.ActorProperties
{
    public class Inventory
    {
        IItemDAO ItemDAO { get; set; } = new ItemJsonDAO(@"./DAL/JsonData"); // TODO DAO: make this more elegant and robust
        public Inventory() { }
        public Inventory(List<Item> initItems)
        {
            this.Items = initItems;
        }
        public Inventory(Character character, List<Item> initItems)
        {
            AttachedCharacter = character;
            this.Items = initItems;
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
                foreach (Item item in Items)
                {
                    // weight of all items in inventory
                    weight += item.Weight * item.StackSize;
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

        public List<Item> Items { get; protected set; } = new List<Item>();
        public Dictionary<string,int> ItemCounts
        {
            get
            {
                Dictionary<string, int> result = new Dictionary<string, int>();
                foreach(Item item in Items)
                {
                    if (!result.TryAdd(item.ItemName, item.StackSize))
                    {
                        result[item.ItemName] += item.StackSize;
                    }
                }
                return result;
            }
        }
        private List<Item> NonMaxedStacks
        {
            get
            {
                List<Item> nonMaxStacks = (from i in Items
                                           where i.StackSize < i.MaxStackSize
                                           select i).ToList<Item>();
                List<Item> result = nonMaxStacks ?? new List<Item>();
                return result;
            }
        }

        public bool Contains(string itemName)
        {
            foreach (Item item in Items)
            {
                if (item.ItemName == itemName)
                {
                    return true;
                }
            };
            return false;
        }
        public Item GetFirstItemInstance(string itemName)
        {
            int index = Items.FindIndex(i => i.ItemName == itemName);
            return Items[index];
        }
        /// <summary>
        /// Adds the given item(s) to the inventory.
        /// </summary>
        /// <param name="item">The item object to add.</param>
        /// <param name="count">The number of items to add.</param>
        public void AddItem(Item item, int count=1)
        {
            while (count > 0)
            {
                if (count >= item.MaxStackSize)
                {
                    // First, create as many full stacks as we can
                    item.StackSize = item.MaxStackSize;
                    Items.Add(item);
                    count -= item.MaxStackSize;
                }
                else
                {
                    // Now check whether any partial stacks exist
                    if (NonMaxedStacks.Contains(item))
                    {
                        // If partial stacks exist, find the first one's index in the main list
                        int index = Items.FindIndex(i => (i.ItemID == item.ItemID) 
                            && (i.ItemName == item.ItemName)
                            && (i.StackSize < i.MaxStackSize));
                        
                        // Calculate how much room it has left in the stack
                        int remainingSpace = Items[index].MaxStackSize - Items[index].StackSize;

                        // If the remaining count won't completely fit, fill the stack and run the loop again
                        if (count > remainingSpace)
                        {
                            count -= remainingSpace;
                            Items[index].StackSize += remainingSpace;
                        }
                        else
                        {
                            // If it does fit, add the rest of the count to the stack and end
                            Items[index].StackSize += count;
                            count = 0;
                        }

                    }
                    else
                    {
                        // If no partial stack exists, create a new one and end
                        item.StackSize = count;
                        Items.Add(item);
                        count = 0;
                    }
                }
            }
        }
        /// <summary>
        /// Removes item(s) from the inventory.
        /// </summary>
        /// <param name="item">Item to remove</param>
        public void RemoveItem(Item item, int count=1)
        {
            while (count > 0)
            {
                // Make sure there are enough items to fill the remove request
                if (ItemCounts[item.ItemName] < count)
                {
                    throw new NotEnoughItemsException($"Not enough {item.ItemName} items in {AttachedCharacter.Name}'s inventory to meet remove request.");
                }

                // Find the first instance of the item in the main list
                int index = Items.FindIndex(i => (i.ItemID == item.ItemID)
                            && (i.ItemName == item.ItemName));

                if (count > Items[index].StackSize)
                {
                    // If the remove count exceeds the stack size, deplete stack and remove
                    count -= Items[index].StackSize;
                    Items.RemoveAt(index);
                    // Then repeat loop to look for another stack
                }
                else
                {
                    // Otherwise, remove the appropriate number of items from the stack and end
                    Items[index].StackSize -= count;
                    count = 0;
                }

                // Dangerous list multiremoval in a single loop
                for (int j = Items.Count() - 1; j > 0; j--)
                {
                    if (Items[j].StackSize == 0)
                    {
                        Items.RemoveAt(j);
                    }
                }
            }
        }
        /// <summary>
        /// Transfers an item from this inventory to the target inventory.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        public void GiveItem(Item item, Inventory target, int count=1)
        {
            target.AddItem(item, count);
            this.RemoveItem(item, count);
        }
        /// <summary>
        /// Transfers an item from the target inventory to this one.
        /// </summary>
        /// <param name="item"></param>
        /// <param name="target"></param>
        public void TakeItem(Item item, Inventory target, int count=1)
        {
            this.AddItem(item, count);
            target.RemoveItem(item, count);
        }
    }
    public class NotEnoughItemsException : Exception
    {
        public NotEnoughItemsException(string message) : base(message)
        {

        }
    }
}
