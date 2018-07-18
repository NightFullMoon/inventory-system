using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerDownHandler
{

    protected Item _storedItem;

    //已经存放的Item
    public Item storedItem
    {
        get
        {
            //Debug.Log("aaa");
            return _storedItem;
            //return null;

        }
        set
        {
            if (_storedItem == value)
            {
                return;
            }
            _storedItem = value;
            if (null == _storedItem)
            {
                count = 0;
            }

            if (!tooltip || !tooltip.IsVisible())
            {
                return;
            }

            if (null == _storedItem || null != pickUpItem.storedItem)
            {
                tooltip.Hide();
            }
            else
            {
                tooltip.Show(_storedItem.GetTooltipText());
            }

            //return storedItem;
        }

    }

    public GameObject itemObject;

    public Image itemImage;

    public Text countText;

    private Tooltip tooltip;

    //已经存放的数量
    public int _count = 0;

    public int count
    {
        get
        {
            return _count;
        }
        set
        {
            if (_count == value)
            {
                return;
            }

            _count = value;
            countText.text = _count.ToString();
            if (0 == value)
            {
                storedItem = null;
            }
            else if (1 == value && null != storedItem && 1 == storedItem.maxNum)
            {
                countText.text = "";
            }
        }
    }

    void Awake()
    {
        // Debug.Log("slot Awake");
        // itemImage =  GetComponentInChildren<Image>();
        //countText = GetComponentInChildren<Text>();

        tooltip = FindObjectOfType<Tooltip>();
    }

    //public Item storedItem() { return _storedItem; }
    //存放指定的item，返回是否存放成功
    public bool storeItem(Item item, int count = 1)
    {
        if (null == item)
        {
            storedItem = null;
            this.count = 0;
            itemObject.SetActive(false);
            return true;
        }

        // Debug.Log(" 想要存放物品");
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
        //countText.text = count.ToString();

        itemObject.SetActive(true);

        //if (tooltip)
        //{
        //    tooltip.Show(item.GetTooltipText());
        //}
        //Debug.Log("存放了物品");
        return true;

    }

    public bool addCount(int count = 1)
    {
        Debug.Log("增加了数量");
        this.count += count;

        //countText.text = this.count.ToString();
        return true;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (null == storedItem || null != pickUpItem.storedItem)
        {
            return;
        }

        tooltip.Show(storedItem.GetTooltipText());
        //throw new System.NotImplementedException();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Hide();
        //  throw new System.NotImplementedException();
    }

    public virtual void OnPointerDown(PointerEventData eventData)
    {


        //Debug.Log();
        /*
         --|--  |--
         鼠标\当前|null|!null
         null| nothing|putDown
         !null|pickUp|swap or add

         */
        //Debug.Log("按下了鼠标");
        // throw new System.NotImplementedException();

        if (null == storedItem)
        {
            if (null != pickUpItem.storedItem)
            {
                PutDownItem(pickUpItem.storedItem, pickUpItem.count);
            }

            return;
        }

        if (eventData.button == PointerEventData.InputButton.Right)
        {
            OnRightClick();
            return;
        }


        if (null == pickUpItem.storedItem)
        {
            Debug.Log("捡起物品");
            PickUpItem();
            return;
        }

        //这个肯定是当前不为空
        if (pickUpItem.storedItem.id == storedItem.id)
        {
            PutDownItem(pickUpItem.storedItem, pickUpItem.count);
        }
        else
        {
            SwapWithSlot(pickUpItem.targetSlot);
        }
    }

    // 当前已经拾取的槽
    public PickupSlot pickUpItem;

    //主要是有以下几种操作情况:

    //捡起物品，将自身的物品递交给鼠标，根据ctrl键的状态决定数量
    void PickUpItem()
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            //拾取一半
            PickUpItem(0.5f);
        }
        else
        {
            //拾取全部
            PickUpItem(count);
        }
    }

    //捡起部分物品，将自身的物品递交给鼠标
    void PickUpItem(int count)
    {
        if (count < 1)
        {
            return;
        }

        int _count = count;
        if (this.count < count)
        {
            _count = this.count;
        }

        //pickUpItem.storedItem = this.storedItem;
        //pickUpItem.count = _count;
        bool isSuccess = pickUpItem.storeItem(storedItem, _count);
        if (!isSuccess)
        {
            return;
        }

        pickUpItem.gameObject.SetActive(true);

        pickUpItem.targetSlot = this;

        int newCount = this.count - _count;
        if (0 == newCount)
        {
            storeItem(null);
        }
        else
        {
            addCount(-_count);
        }
        tooltip.Hide();
    }

    //捡起部分物品，将自身的物品递交给鼠标，precent为0到1的数，如果不是正整数则向上取整，最少数量为1
    void PickUpItem(float precent)
    {
        if (1 < precent)
        {
            PickUpItem();
            return;
        }

        if (precent < 0)
        {
            PickUpItem(1);
            return;
        }

        int count = (int)((float)this.count * precent);

        if (count < 1)
        {
            PickUpItem(1);
            return;
        }
        PickUpItem(count);
        return;
    }

    //放下全部物品，将鼠标上的物品放置到Slot里面
    //void PutDownItem(Item item)
    //{
    //}
    //放下部分物品，将鼠标上的物品放置到Slot里面
    virtual public void PutDownItem(Item item, int allCount)
    {
        if (!isItemTypeCorrect(item))
        {
            return;
        }

        int addCount = allCount;

        if (Input.GetKey(KeyCode.LeftControl))
        {
            addCount = 1;
        }

        if (null == storedItem)
        {
          
            pickUpItem.count -= addCount;

            if (0 == pickUpItem.count)
            {
                pickUpItem.storeItem(null);
                pickUpItem.gameObject.SetActive(false);
            }

            storeItem(item, addCount);

            return;
        }

        //相同就叠加
        if (storedItem.id == item.id)
        {

            if (storedItem.maxNum < addCount + this.count)
            {
                addCount = storedItem.maxNum - this.count;
            }

            this.addCount(addCount);
            pickUpItem.addCount(-addCount);

            if (addCount == allCount)
            {
                pickUpItem.storeItem(null);
                pickUpItem.gameObject.SetActive(false);
            }
        }
    }

    //与对应插槽中的物品进行交换
    virtual public void SwapWithSlot(Slot slot)
    {
        if (null != slot.storedItem)
        {
            return;
        }
        //交换的顺序是这样的：鼠标->this;
        // this -> slot
        // null -> 鼠标

        //交换之前先去校验是否双方都能接受对方的物品类型
        if (!isItemTypeCorrect(pickUpItem.storedItem) || !slot.isItemTypeCorrect(this.storedItem))
        {
            return;
        }

        Debug.Log("交换物品");
        Item tempItem = storedItem;
        int tempCount = count;

        // this.storedItem = pickUpItem.storedItem;
        //this.count = pickUpItem.count;

        storeItem(null);
        storeItem(pickUpItem.storedItem, pickUpItem.count);

        slot.storeItem(null);
        slot.storeItem(tempItem, tempCount);

        pickUpItem.storeItem(null);
        pickUpItem.gameObject.SetActive(false);
    }


    //直接交换当前槽与目标槽的物品（忽略鼠标上物品的状态）
    // 目标槽允许为null
    virtual public void SwapWithSlotDirect(Slot slot)
    {
        //交换之前先去校验是否双方都能接受对方的物品类型
        if (!isItemTypeCorrect(slot.storedItem) || !slot.isItemTypeCorrect(this.storedItem))
        {
            return;
        }

        Item tempItem = storedItem;
        int tempCount = count;

        if (null == slot)
        {
            storeItem(null);
        }
        else
        {
            storeItem(null);
            storeItem(slot.storedItem, slot.count);
        }

        slot.storeItem(null);
        slot.storeItem(tempItem, tempCount);

    }


    //在槽上右键时候的操作
    virtual protected void OnRightClick()
    {
        storedItem.Use(this);
    }

    // 返回item的类型是否可以被接受
    virtual public bool isItemTypeCorrect(Item item)
    {
        return true;
    }
}
