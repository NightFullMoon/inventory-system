﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;



public class InventoryManager
{
    public List<Item> itemList;

    #region 单例模式
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = new InventoryManager();
                _instance.init();
            }
            //init();
            return _instance;
        }
    }
    #endregion

    // Use this for initialization
    //void Awake()
    //{
    //    init();
    //}

    private void init()
    {
        parseItemJson();
    }

    // Update is called once per frame


    // 解析item的json文件
    void parseItemJson()
    {
        itemList = new List<Item>();

        TextAsset itemText = Resources.Load<TextAsset>("Items");

        string text = itemText.text;
        Debug.Log(text);

        //text = @"
        //  {
        //    ""album"" : {
        //      ""name""   : ""The Dark Side of the Moon"",
        //      ""artist"" : ""Pink Floyd"",
        //      ""year""   : 1973,
        //      ""tracks"" : [
        //        ""Speak To Me"",
        //        ""Breathe"",
        //        ""On The Run""
        //      ]
        //    }
        //  }
        //";

        JsonData demo = JsonMapper.ToObject(text);
        //Debug.Log(demo["consumable"][0]["name"]);

        //Debug.Log((demo["consumable"].Count));
        for (int index = 0, count = demo.Count; index < count; ++index)
        {
            JsonData item = demo[index];
            Item baseItem = new Item((int)item["id"],
                (string)item["name"],
                ItemType.Consumable,
                (Quality)System.Enum.Parse(typeof(Quality), (string)item["quality"]),
                (string)item["description"],
                (int)item["maxNum"],
                (int)item["buyPrice"],
                (int)item["sellPrice"],
                (string)item["sprite"]);

            ItemType type = (ItemType)System.Enum.Parse(typeof(ItemType), (string)item["itemType"]);
            switch (type)
            {
                case ItemType.Consumable:
                    itemList.Add(new Consumable(
                        baseItem,
                        (int)item["hp"],
                        (int)item["mp"]));
                    break;
                case ItemType.Equipment:
                    itemList.Add(new Equipment(
                       baseItem,
                       (int)item["strength"],
                       (int)item["intellect"],
                       (int)item["agility"],
                       (int)item["stamina"],
                       (EquipmentType)System.Enum.Parse(typeof(EquipmentType), (string)item["equipmentType"])
                       ));

                    break;
                case ItemType.Weapon:

                    itemList.Add(new Weapon(baseItem,
                        (int)item["damage"],
                        (WeaponType)System.Enum.Parse(typeof(WeaponType), (string)item["weaponType"])
                        ));
                    break;
                case ItemType.Material:
                    itemList.Add(new Material(baseItem));
                    break;
                default:
                    break;
            }


            //Debug.Log(index);  



        }
        // Debug.Log(itemList.Count);

    }

    //根据id去查找对应的item对象，如果该对象不存在，则返回null;
    public Item getItemById(int itemId)
    {
        if (null == itemList)
        {
            Debug.LogWarning("试图通过id获取对应物品，但物品列表尚未初始化");
            return null;
        }
        foreach (Item item in itemList)
        {
            if (itemId == item.id)
            {
                return item;
            }
        }
        return null;

    }

    /*需要完善的地方：
     * 槽的逻辑比较混乱
     * 售卖的时候，没有走原先的流程，而是直接新起了一个流程，导致左Ctrl卖出1个的逻辑失效
     */
}


