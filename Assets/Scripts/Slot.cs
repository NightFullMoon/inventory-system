using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler,IPointerDownHandler
{

    //已经存放的Item
    public Item storedItem = null;

    public GameObject itemObject;

    public Image itemImage;

    public Text countText;

    private Tooltip tooltip;

    //已经存放的数量
    public int count = 0;

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

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (null == storedItem || null!= pickUpItem.storedItem) {
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

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("按下了鼠标");
        // throw new System.NotImplementedException();

        if (null == storedItem)
        {
            //todo:放下等逻辑
            return;
        }
        //先实现捡起的逻辑
        if (Input.GetKey(KeyCode.LeftControl)) {
            //拾取一半
            PickUpItem(0.5f);
        }
        else {
            //拾取全部
            PickUpItem();
        }

    }

    // 当前已经拾取的槽
    public Slot pickUpItem;

    //主要是有以下几种操作情况:

    //捡起全部物品，将自身的物品递交给鼠标
    void PickUpItem() {
       PickUpItem(count);
    }
    //捡起部分物品，将自身的物品递交给鼠标
    void PickUpItem(int count) {
      

        if (count < 1) {
            return;
        }
        int _count = count;
        if (this.count < count) {
            _count = this.count;
        }

        //pickUpItem.storedItem = this.storedItem;
        //pickUpItem.count = _count;
       bool isSuccess = pickUpItem.storeItem(storedItem, _count);
        if (!isSuccess) {
            return;
        }
        tooltip.Hide();

        int newCount = this.count - _count;
        if (0 == newCount)
        {
            storeItem(null);
        }
        else {
            addCount(-_count);
        }
       

    }
    //捡起部分物品，将自身的物品递交给鼠标，precent为0到1的数，如果不是正整数则向上取整，最少数量为1
    void PickUpItem(float precent) {
        if (1 < precent) {
            PickUpItem();
            return;
        }

        if (precent < 0) {
            PickUpItem(1);
            return;
        }

        int count = (int)((float)this.count * precent);

        if (count < 1) {
            PickUpItem(1);
            return;
        }
        PickUpItem(count);
        return;
    }

    //放下全部物品，将鼠标上的物品放置到Slot里面
    void PutDownItem(Item item)
    {

    }
    //放下部分物品，将鼠标上的物品放置到Slot里面
    void PutDownItem(Item item, int count) {

    }

    //与对应插槽中的物品进行交换
    void SwapWithSlot(Slot slot) {

    }
       
}
