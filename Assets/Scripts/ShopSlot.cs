using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShopSlot : Slot
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Left && null != pickUpItem.storedItem)
        {
            SellItem(pickUpItem);
            pickUpItem.gameObject.SetActive(false);
        }
        else if (eventData.button == PointerEventData.InputButton.Right && null == pickUpItem.storedItem)
        {
            Debug.Log("点击了右键");
            OnRightClick();
        }

    }

    //购买该物品
    protected override void OnRightClick()
    {
        if (null == storedItem)
        {
            return;
        }
        int price = storedItem.buyPrice * 1;

        if (Player.Instance().money < price || !Knapsack.Instance().couldStoreItem(storedItem))
        {
            return;
        }
        bool isSuccess = Player.Instance().Cost(price);

        if (!isSuccess)
        {
            Debug.LogWarning("判定为可购买但是扣款失败");
            return;
        }

        Knapsack.Instance().storeItem(storedItem, 1);
        Debug.Log("购买成功");
    }

    //override 
    //public override void SwapWithSlot(Slot slot)
    //{
    //    //base.SwapWithSlot(slot);
    //    SellItem(slot);
    //}

    //卖掉某样物品
    void SellItem(Slot slot)
    {
        if (null == slot)
        {
            return;
        }

        Item item = slot.storedItem;
        int count = slot.count;

        if (null == item || 0 == count)
        {
            return;
        }
        slot.storeItem(null);

        int price = item.sellPrice * count;
        Player.Instance().Earn(price);
    }
}
