using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorController : MonoBehaviour
{
    public RectTransform bar;
    public float speed = 0.5f;
    private RectTransform rectTransform;
    private bool movingRight = true;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    void Update()
    {
        float step = speed * Time.deltaTime;
        if (movingRight)
        {
            rectTransform.anchoredPosition += new Vector2(step, 0);
            if (rectTransform.anchoredPosition.x >= bar.rect.width / 2)
            {
                movingRight = false;
            }
        }
        else
        {
            rectTransform.anchoredPosition -= new Vector2(step, 0);
            if (rectTransform.anchoredPosition.x <= -bar.rect.width / 2)
            {
                movingRight = true;
            }
        }
    }
}
