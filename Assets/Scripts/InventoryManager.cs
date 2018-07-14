using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 using LitJson;

public class InventoryManager : MonoBehaviour
{
    public List<Item> itemList;


    public List<Item> demo;

    //public List<int> aaa;

    #region 单例模式
    private static InventoryManager _instance;

    public static InventoryManager Instance
    {
        get
        {
            if (null == _instance)
            {
                _instance = GameObject.Find("InventoryManager").GetComponent<InventoryManager>();
            }
            return _instance;
        }
    }
    #endregion

    // Use this for initialization
    void Start()
    {
        parseItemJson();
    }

    // Update is called once per frame
    void Update()
    {

    }

    // 解析item的json文件
    void parseItemJson() {
        itemList = new List<Item>();

        TextAsset itemText =  Resources.Load<TextAsset>("Items");

        string text = itemText.text;
        Debug.Log(text);

        demo = JsonMapper.ToObject<List<Item>>(text);
      //  Debug.Log(demo);

    }
}
