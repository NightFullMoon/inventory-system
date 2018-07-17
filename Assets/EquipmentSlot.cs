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
    bool isItemTypeCorrect(Item item)
    {

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
    override public void PutDownItem(Item item, int allCount)
    {

        if (false == isItemTypeCorrect(item))
        {
            return;
        }
        base.PutDownItem(item, allCount);
    }

    //与对应插槽中的物品进行交换
    override public void SwapWithSlot(Slot slot)
    {


        if (false == isItemTypeCorrect(pickUpItem.storedItem))
        {
            return;
        }
        base.SwapWithSlot(slot);
    }

    override public void SwapWithSlotDirect(Slot slot)
    {

        if (false == isItemTypeCorrect(slot.storedItem))
        {
            return;
        }
        base.SwapWithSlotDirect(slot);
    }

    // .Use的话，要么是swap，要么是putdown，不管是swap还是putdown，都需要原来槽的信息


}
