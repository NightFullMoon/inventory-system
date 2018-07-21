using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : InventoryInstance
{
    static Knapsack _instance;

    public static Knapsack Instance()
    {
        //if (null == _instance)
        //{
        //    _instance = new Knapsack();
        //}

        return _instance;
    }

    override protected void Awake()
    {
        base.Awake();
        if (null == _instance)
        {
            _instance = this;
        }

    }

    protected void Start()
    {
        //base.Awake();
        for (int i = 1; i < 20; ++i)
        {
            storeItem(i);
        }
        storeItem(1, 20);
        storeItem(2, 5);
    }
}
