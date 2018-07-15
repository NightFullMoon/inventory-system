using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Consumable : Item
{

    public int hp;
    public int mp;

    public Consumable() { }

    public Consumable(int id, string name, ItemType itemType, Quality quality, string description, int maxNum, int buyPrice, int sellPrice, string sprite, int hp, int mp)
    : base(id, name, itemType, quality, description, maxNum, buyPrice, sellPrice, sprite)
    {
        this.hp = hp;
        this.mp = mp;
    }

    public Consumable(Item item, int hp, int mp) : base(item)
    {
        this.hp = hp;
        this.mp = mp;
    }

}
