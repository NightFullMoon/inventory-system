﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 物品类型
public enum ItemType
{
    //消耗品
    Consumable,

    //装备
    Equipment,

    //武器
    Weapon,

    //材料
    Material
}

//品质
public enum Quality
{
    //普通
    Common,

    //优良
    Uncommon,

    //稀有
    Rare,

    //史诗
    Epic,

    //传说
    Legendary,

    //远古
    Artifact

}

// 物品的基类
public class Item
{

    public int id;

    public string name;

    public ItemType itemType;

    public Quality quality;

    //描述
    public string description;

    public int maxNum;

    public int buyPrice;

    public int sellPrice;

    public string sprite;

    public Item()
    {
        this.id = -1;
    }


    public Item(int id, string name, ItemType itemType, Quality quality, string description, int maxNum, int buyPrice, int sellPrice, string sprite)
    {
        this.id = id;

        this.name = name;

        this.itemType = itemType;

        this.quality = quality;

        //描述
        this.description = description;

        this.maxNum = maxNum;

        this.buyPrice = buyPrice;

        this.sellPrice = sellPrice;

        this.sprite = sprite;

    }

    public Item(Item item)
    {
        this.id = item.id;
        this.name = item.name;
        this.itemType = item.itemType;
        this.quality = item.quality;
        this.description = item.description;
        this.maxNum = item.maxNum;
        this.buyPrice = item.buyPrice;
        this.sellPrice = item.sellPrice;
        this.sprite = item.sprite;
    }

    // 获取显示在面板上的信息
    public virtual string GetTooltipText()
    {
        string color = "white";

        switch (quality)
        {
            case Quality.Common:
                break;
            case Quality.Uncommon:
                color = "lime";
                break;
            case Quality.Rare:
                color = "navy";
                break;
            case Quality.Epic:
                color = "magenta";
                break;
            case Quality.Legendary:
                color = "orange";
                break;
            case Quality.Artifact:
                color = "red";
                break;
            default:
                break;
        }
        string extraText = GetTooltipExtraText();

        string text = string.Format("<color={0}>{1}</color>\n{5}购买价格:{2}\n卖出价格:{3}\n\n{4}", color, name, buyPrice, sellPrice, description, extraText);
        return text;
    }

    // 用来获取额外的属性信息
    protected virtual string GetTooltipExtraText()
    {
        return "";
    }

    //对该物品的使用
    //inventory :当前物品所在的仓库类对象实例
    // slot ： 当前物品所在的Slot对象实例
    virtual public void Use( Slot slot)
    {

    }



}
