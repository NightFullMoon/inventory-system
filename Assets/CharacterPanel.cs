﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterPanel : InventoryInstance
{
    #region 单例
    static CharacterPanel _instance;

    static public CharacterPanel Instance()
    {
        //// static Inventory ins = new Inventory();
        //if (null == _instance)
        //{
        //    Debug.Log("初始化 CharacterPanel _instance ");
        //    _instance = CharacterPanel();
        //}

        //fixme:这里不知道为什么new一个对象会导致。。。slots为null

        return _instance;
    }

    // private CharacterPanel() { }
    #endregion

    override protected void Awake()
    {
        base.Awake();
        //Debug.Log()
        //base.Awake();
        //Debug.Log("slots 在子类被，数量：" + slots.Length);
        //Debug.Log(slots == null);
        //Debug.Log(base.slots == null);
        _instance = this;
    }

    //装备指定的槽
    public bool Equip(Equipment equipment, Slot slot)
    {
        EquipmentSlot targetSlot = FindSolt(equipment.equipmentType);

        if (null == targetSlot)
        {
            Debug.LogWarning("未找到指定类型[" + equipment.equipmentType + "]的装备插槽");
            return false;
        }

        if (null == targetSlot.storedItem)
        {
            targetSlot.PutDownItem(equipment, 1);
            slot.storeItem(null);
        }
        else
        {
            Debug.Log("交换");
            targetSlot.SwapWithSlotDirect(slot);
        }
        return true;
    }


    //找到指定装备类型的槽，如果当前没有该类型的槽，则返回null
    EquipmentSlot FindSolt(EquipmentType equipmentType)
    {
        if (null == base.slots)
        {
            Debug.LogWarning("警告，slots为空");
            return null;
        }

        foreach (Slot slot in slots)
        {

            EquipmentSlot equiSlot = slot as EquipmentSlot;

            if (null == equiSlot)
            {
                continue;
            }

            //Debug.Log(equiSlot.equipmentType);
            if (equiSlot.equipmentType == equipmentType)
            {
                return equiSlot;
            }

        }

        return null;
    }


}