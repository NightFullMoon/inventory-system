using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    private Slot[] slots;

    // Use this for initialization
    virtual protected void Start()
    {
        slots = GetComponentsInChildren<Slot>();
        //storeItem(3);


    }

    // 存放入指定id的物品，如果存放失败，则返回false
    protected bool storeItem(int itemId, int count = 1)
    {
        Item item = InventoryManager.Instance.getItemById(itemId);

        if (null == item)
        {

            Debug.LogWarning("试图放入不存在的物品：【id：" + itemId + "】");
            return false;
        }

        return storeItem(item, count);
    }

    protected bool storeItem(Item item, int count = 1)
    {
        if (null == item)
        {
            return false;
        }

        Slot slot = findSlotWithItem(item);

        /* if (null != slot) {
             slot.storeItem(item, count);
             //if ((1 == item.maxNum && 1 == count) || slot.count + count <= item.maxNum)
             //{
             //    slot.count += count;
             //    return true;
             //}
         }
         */

        if (null == slot)
        {
            slot = findEmptySlot();
        }

        if (null == slot)
        {
            //所有的槽都已经满了
            Debug.Log("所有的槽都已经满了");
            return false;
        }
        slot.storeItem(item, count);
        return true;


    }

    //找到一个空的物品槽，如果没有找到，则返回null 
    Slot findEmptySlot()
    {
        foreach (Slot slot in slots)
        {
            if (null == slot.storedItem)
            {
                return slot;
            }
        }
        return null;

    }

    //找到存放着某个item的Slot，如果没有找到该Slot，则返回null
    Slot findSlotWithItem(Item item)
    {
        foreach (Slot slot in slots)
        {
            if (null == slot.storedItem)
            {
                continue;
            }

            if (item.id == slot.storedItem.id)
            {
                return slot;
            }
        }
        return null;
    }
    public KeyCode displayKey;

    private CanvasGroup canvasGroup;

    //切换显示隐藏面板
    void toggleDisplay()
    {
        if (null == canvasGroup)
        {
            Debug.LogWarning("没有找到CanvasGroup组件，无法提供切换显示隐藏的功能");
            return;
        }

        if (0 == canvasGroup.alpha)
        {
            canvasGroup.alpha = 1;
            canvasGroup.interactable = true;
            canvasGroup.blocksRaycasts = true;
        }
        else
        {
            canvasGroup.alpha = 0;
            canvasGroup.interactable = false;
            canvasGroup.blocksRaycasts = false;
        }
    }

    private void Awake()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
    }

    void Update()
    {
        if (Input.GetKeyDown(displayKey))
        {
            Debug.Log("切换显示隐藏");
            toggleDisplay();
        }
    }
}
