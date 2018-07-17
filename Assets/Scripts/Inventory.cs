using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Inventory : MonoBehaviour {

    static InventoryInstance _instance;

    static public InventoryInstance Instance()
    {
        // static Inventory ins = new Inventory();
        if (null == _instance)
        {
            _instance = new InventoryInstance();
        }

        return _instance;
    }
}

/* 这里要说明的一点是，因为 InventoryInstance这个类（之前叫Inventory）承担了两个角色，
 * 
 * 基类和单例，所以导致了一些问题
 * 这里就先将这两个职责分开，新的Inventory类充当 单例的角色，
 * InventoryInstance类充当基类的角色，
 * 而目前的问题是，Inventory没有达到原有的目的（仍然可以实例一个InventoryInstance对象）
 * 而C#又没有友元的操作导致。。。反正目前是没想出办法解决这个问题
 */


public class InventoryInstance : MonoBehaviour
{
    //protected InventoryInstance() { }


    protected Slot[] slots;//= Instance().slots;

    // Use this for initialization
   virtual protected void Awake()
    {
        slots = GetComponentsInChildren<Slot>();
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        //storeItem(3);
       

        if (null == slots)
        {
            slots = new Slot[0];
            Debug.LogWarning("警告，没有找到Slot对象");
        }

        Debug.Log("slots 被初始化，数量：" + slots.Length);

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

    //private void Awake()
    //{
      
    //}

    void Update()
    {
        if (Input.GetKeyDown(displayKey))
        {
            Debug.Log("切换显示隐藏");
            toggleDisplay();
        }
    }
}
