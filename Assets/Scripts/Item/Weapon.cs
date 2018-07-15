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

    public Weapon(Item item,
                   int damage,
            WeaponType weaponType) : base(item)
    {
        this.damage = damage;
        this.weaponType = weaponType;
    }
    public override string GetTooltipExtraText()
    {
        string wpTypeText = "";
        switch (weaponType)
        {
            case WeaponType.OffHand:
                wpTypeText = "副手武器";
                break;
            case WeaponType.MainHand:
                wpTypeText = "主手武器";
                break;
            default:
                break;
        }

        return string.Format("伤害：{0}\n部位：{1}\n", damage, wpTypeText);
        //return base.GetTooltipExtraText();
    }
}
