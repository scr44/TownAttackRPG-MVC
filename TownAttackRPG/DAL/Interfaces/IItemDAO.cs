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
        Item CreateNewItem(int id);
        Item CreateNewItem(string itemName);

        EquipmentItem CreateNewEquipmentItem(int id);
        EquipmentItem CreateNewEquipmentItem(string itemName);
    }
}
