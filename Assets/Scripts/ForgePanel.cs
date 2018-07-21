using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//用来存放Item的信息

public struct ItemInfo
{
    public Item item;
    public int count;

    public ItemInfo(int itemID, int count = 1)
    {
        item = InventoryManager.Instance.getItemById(itemID);
        this.count = count;
    }

    public ItemInfo(Item item, int count = 1)
    {
        this.item = item;
        this.count = count;
    }
}

public class ForgePanel : InventoryInstance
{
    //合成配方列表
    static List<Formula> formulaList;

    protected override void Awake()
    {
        base.Awake();
        //todo:这里要去解析JSON文件，以获取真正的物品信息
        if (null == formulaList)
        {
            formulaList = new List<Formula>();

            List<ItemInfo> tempItemInfoList = new List<ItemInfo>();
            tempItemInfoList.Add(new ItemInfo(1));
            tempItemInfoList.Add(new ItemInfo(2));

            formulaList.Add(new Formula(new ItemInfo(16), tempItemInfoList));

        }
    }

    //用来获取槽里面的物品信息
    List<ItemInfo> GetItemInfoList()
    {

        List<ItemInfo> result = new List<ItemInfo>();

        foreach (Slot slot in slots)
        {
            if (null == slot.storedItem)
            {
                continue;
            }
            result.Add(new ItemInfo(slot.storedItem, slot.count));
        }

        return result;
    }

    //根据当前槽里的物品，返回匹配到的，能够合成的配方
    Formula FindFormula(List<ItemInfo> itemInfoList)
    {
        foreach (Formula formula in formulaList)
        {
            if (formula.isMatch(itemInfoList))
            {
                return formula;
            }
        }

        return null;
    }

    //将槽里的物品合成一个新的物品
    public void Compound()
    {
        List<ItemInfo> itemInfoList = GetItemInfoList();
        Formula formula = FindFormula(itemInfoList);

        if (null == formula)
        {
            // 没有找到可以合成的物品
            Debug.Log("没有找到可以合成的物品");
            return;
        }

        if (formula.isMatch(itemInfoList))
        {
            ItemInfo result = formula.compound;

            //先检查是否能够存放目标物品
            bool couldStore =  Knapsack.Instance().couldStoreItem(result.item, result.count);

            if (!couldStore) {
                return;
            }

            //把每个槽里面，消耗掉的物品都减去对应的数量
            foreach (Slot slot in slots)
            {
                if (null == slot.storedItem) {
                    continue;
                }
               
                foreach (ItemInfo itemInfo in formula.itemInfoList)
                {
                    if (itemInfo.item.id == slot.storedItem.id) {
                        slot.addCount(-itemInfo.count);
                        if (0 == slot.count) {
                            slot.itemObject.SetActive(false);
                        }
                        break;
                    }
                }
            }
            //放进新的物品
            Knapsack.Instance().storeItem(result.item, result.count);
            Debug.Log("合成物品成功！");
            return;
        }
        Debug.LogWarning("匹配到配方却无法合成，可能是判定函数不正确");
    }
}
