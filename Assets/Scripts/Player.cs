using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Player : MonoBehaviour
{

    int _money;

    public int money
    {
        get
        {
            return _money;
        }
        protected set
        {
            if (value == _money)
            {
                return;
            }

            if (value < 0)
            {
                _money = 0;
            }
            else
            {
                _money = value;
            }

            if (moneyText)
            {
                moneyText.text = money.ToString();
            }

        }
    }

    public Text moneyText;

    static public Player _instance;

    static public Player Instance()
    {
        return _instance;
    }

    //给角色添加钱
    public void Earn(int value)
    {

        if (value < 0)
        {
            return;
        }
        money += value;

    }

    //角色花钱
    public bool Cost(int value)
    {
        if (money < value)
        {
            return false;
        }
        money -= value;

        return true;
    }


    private void Awake()
    {
        money = 998;
        if (null == _instance)
        {
            _instance = this;
        }
    }
}
