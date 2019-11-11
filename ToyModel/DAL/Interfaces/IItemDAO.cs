using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TownAttackRPG.Models.Items;
using TownAttackRPG.Models.Items.Equipment;

namespace TownAttackRPG.DAL.Interfaces
{
    public interface IItemDAO
    {
        Item CreateNewItem(int id, int count=1);
        Item CreateNewItem(string itemName, int count=1);

        EquipmentItem CreateNewEquipmentItem(int id, int count=1);
        EquipmentItem CreateNewEquipmentItem(string itemName, int count=1);
    }
}
