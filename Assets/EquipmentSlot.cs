using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquipmentSlot : Slot
{
    //接受的武器类型
    public WeaponType weaponType = WeaponType.None;
    //接受的装备类型
    public EquipmentType equipmentType = EquipmentType.None;


    // 返回指定的item是否可以放置在当前槽内
    bool isItemTypeCorrect(Item item) {

        if (null == item)
        {
            return false;
        }
        Weapon targetItem = item as Weapon;
        Equipment targetEquipment = item as Equipment;

        if ((null == targetItem || targetItem.weaponType != weaponType) &&
                 (null == targetEquipment || targetEquipment.equipmentType != equipmentType))
        {
            return false;
        }

        return true;
    }

    //放下部分物品，将鼠标上的物品放置到Slot里面
    override protected void PutDownItem(Item item, int allCount)
    {

        if (false == isItemTypeCorrect(item)) {
            return;
        }
        base.PutDownItem(item, allCount);
    }

    //与对应插槽中的物品进行交换
    override protected void SwapWithSlot(Slot slot)
    {
        if (false == isItemTypeCorrect(pickUpItem.storedItem))
        {
            return;
        }
        base.SwapWithSlot(slot);
    }


}
