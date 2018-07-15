using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using LitJson;

public struct JsonDataFormat
{
    public List<Consumable> consumable;
}


public class InventoryManager : MonoBehaviour
{
    public List<Item> itemList;


    // public List<Item> demo;

    //public List<int> aaa;

    #region 单例模式
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (null == _instance)
            {
                //_instance = this;
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion

    // Use this for initialization
    void Awake()
    {
        parseItemJson();


    }

    // Update is called once per frame
    void Update()
    {

    }

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

        Debug.Log((demo["consumable"].Count));
        for (int index = 0, count = demo["consumable"].Count; index < count; ++index)
        {
            Debug.Log(index);
            JsonData item = demo["consumable"][index];
            itemList.Add(new Consumable((int)item["id"],
                (string)item["name"],
                ItemType.Consumable,
                (Quality)System.Enum.Parse(typeof(Quality), (string)item["quality"]),
                (string)item["description"],
                (int)item["maxNum"],
                (int)item["buyPrice"],
                (int)item["sellPrice"],
                (string)item["sprite"],
                (int)item["hp"],
                (int)item["mp"]));
        }
        Debug.Log(itemList.Count);
        
    }

    //根据id去查找对应的item对象，如果该对象不存在，则返回null;
    public Item getItemById(int itemId)
    {
        if (null == itemList)
        {
            Debug.Log("itemList 为 null");
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
}
