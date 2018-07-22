using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//用来存放秘方信息的类
public class Formula
{

    public Formula(ItemInfo compound, List<ItemInfo> itemInfoList)
    {
        this.itemInfoList = itemInfoList;
        this.compound = compound;

    }

    //合成所需的材料
    public List<ItemInfo> itemInfoList;

    //合成的产物
    public ItemInfo compound;


    //返回当前的物品是否满足合成的条件
    public bool isMatch(List<ItemInfo> itemInfoList)
    {
        foreach (ItemInfo itemInfo in this.itemInfoList)
        {
            bool isFind = false;

            //itemInfoList.Contains
            foreach (ItemInfo matchItemInfo in itemInfoList)
            {
                if (null == itemInfo.item)
                {
                    continue;
                }

                //int a = 0;

                //if (itemInfo.item.id == matchItemInfo.item.id) {

                //}

                if (itemInfo.item.id == matchItemInfo.item.id
                    && itemInfo.count <= matchItemInfo.count)
                {
                    isFind = true;
                    break;
                }
            }

            if (!isFind)
            {
                return false;
            }
        }

        return true;
    }

}
