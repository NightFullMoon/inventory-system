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
    Barcer,
    Boots,
    Trinket,
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

}
