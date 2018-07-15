using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Slot : MonoBehaviour
{

    //已经存放的Item
    public Item storedItem = null;

    public GameObject itemObject;

    public Image itemImage;

    public Text countText;

    //已经存放的数量
    public int count = 0;

    void Awake()
    {
        // Debug.Log("slot Awake");
        // itemImage =  GetComponentInChildren<Image>();
        //countText = GetComponentInChildren<Text>();
    }

    //public Item storedItem() { return _storedItem; }
    //存放指定的item，返回是否存放成功
    public bool storeItem(Item item, int count = 1)
    {
        if (null == item)
        {
            return false;
        }

        Debug.Log(" 想要存放物品");
        if (null != storedItem)
        {
            if (item.id != storedItem.id)
            {
                return false;
            }
            return addCount(count);
        }

        // 放入一个新的item
        storedItem = item;
        this.count = count;

        itemImage.sprite = Resources.Load<Sprite>(item.sprite);
        countText.text = count.ToString();

        itemObject.SetActive(true);
        //Debug.Log("存放了物品");
        return true;

    }

    public bool addCount(int count = 1)
    {
        Debug.Log("增加了数量");
        this.count += count;

        countText.text = this.count.ToString();
        return true;
    }



}
