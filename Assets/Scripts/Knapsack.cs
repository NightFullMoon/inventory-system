using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knapsack : Inventory
{
    override protected void Start()
    {
        base.Start();
        for (int i = 1; i < 20; ++i)
        {
            storeItem(i);
        }
        storeItem(1, 20);
        storeItem(2, 12);
    }
}
