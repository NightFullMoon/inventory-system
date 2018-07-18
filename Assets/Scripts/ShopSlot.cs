using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class ShopSlot : Slot
{

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            Debug.Log("点击了右键");            OnRightClick();
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


}
