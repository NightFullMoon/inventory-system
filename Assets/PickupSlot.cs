using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupSlot : Slot
{

    private Canvas canvas;

    // Use this for initialization
    void Start()
    {
        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

        SetPosition(position);
    }

    public void SetPosition(Vector3 position)
    {
        //Vector2 offset = new Vector2(20, 20);
        position.x += 40;
        position.y -= 40;

        transform.localPosition = position;
    }
}
