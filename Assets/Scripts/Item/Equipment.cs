using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EquipmentType
{
    Head,
    Chest,
    Neck,
    Ring,
    Leg,
    Bracer,
    Boots,
    Shoulder,
    Belt,
    OffHand
}


public class Equipment : Item
{

    public int strength;

    public int intellect;

    public int agility;

    public int stamina;

    public EquipmentType equipmentType;

    public Equipment(int id,
                  string name,
                ItemType itemType,
                 Quality quality,
                  string description,
                     int maxNum,
                     int buyPrice,
                     int sellPrice,
                  string sprite,
                     int strength,
                     int intellect,
                     int agility,
                     int stamina,
           EquipmentType equipmentType)
         : base(id, name, itemType, quality, description, maxNum, buyPrice, sellPrice, sprite)
    {
        this.strength = strength;
        this.intellect = intellect;
        this.agility = agility;
        this.stamina = stamina;
        this.equipmentType = equipmentType;
    }

    public Equipment(Item item,
                     int strength,
                     int intellect,
                     int agility,
                     int stamina,
           EquipmentType equipmentType) : base(item)
    {
        this.strength = strength;
        this.intellect = intellect;
        this.agility = agility;
        this.stamina = stamina;
        this.equipmentType = equipmentType;
    }


    public override string GetTooltipExtraText()
    {
        string typeText = "";
        switch (equipmentType)
        {
            case EquipmentType.Head:
                typeText = "头部";
                break;
            case EquipmentType.Chest:
                typeText = "胸部";
                break;
            case EquipmentType.Neck:
                typeText = "颈部";
                break;
            case EquipmentType.Ring:
                typeText = "戒指";
                break;
            case EquipmentType.Leg:
                typeText = "腿部";
                break;
            case EquipmentType.Bracer:
                typeText = "腕部";
                break;
            case EquipmentType.Boots:
                typeText = "鞋";
                break;
            case EquipmentType.Shoulder:
                typeText = "肩膀";
                break;
            case EquipmentType.Belt:
                typeText = "腰带";
                break;
            case EquipmentType.OffHand:
                typeText = "副手";
                break;
            default:
                break;
        }

        return string.Format("部位：{0}\n力量：{1}\n智力：{2}\n敏捷：{3}\n体力：{4}\n", typeText, strength, intellect, agility, stamina);
        //return base.GetTooltipExtraText();
    }
}
