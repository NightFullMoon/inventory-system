using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum WeaponType
{
    OffHand,
    MainHand
}

public class Weapon : Item
{

    public int damage;

    public WeaponType weaponType;

    public Weapon(int id,
                  string name,
                ItemType itemType,
                 Quality quality,
                  string description,
                     int maxNum,
                     int buyPrice,
                     int sellPrice,
                  string sprite,
                     int damage,
                     WeaponType weaponType)
    : base(id, name, itemType, quality, description, maxNum, buyPrice, sellPrice, sprite)
    {
        this.damage = damage;
        this.weaponType = weaponType;
    }
}
