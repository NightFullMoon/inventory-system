using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class Tooltip : MonoBehaviour {

    private Text text;

    private Text contentText;

    private CanvasGroup canvasGroup;

    private Canvas canvas;

	// Use this for initialization
	void Start () {
        text = GetComponent<Text>();
        contentText = transform.Find("Content").GetComponent<Text>();

        canvasGroup = GetComponent<CanvasGroup>();

        canvas = GameObject.Find("Canvas").GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {

        if (0 == canvasGroup.alpha)
        {
            return;
        }

        Vector2 position;

        RectTransformUtility.ScreenPointToLocalPointInRectangle(canvas.transform as RectTransform, Input.mousePosition, null, out position);

        SetPosition(position);


    }

    public void Show(string text) {
        this.text.text = text;
        contentText.text = text;

        canvasGroup.alpha = 1;


    }

    public void Hide() {
        canvasGroup.alpha = 0;
    }

    public void SetPosition(Vector3 position) {
        //Vector2 offset = new Vector2(20, 20);
        position.x += 20;
        position.y -= 20;

        transform.localPosition = position  ;
    }
}
