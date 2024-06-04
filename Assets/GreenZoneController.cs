using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GreenZoneController : MonoBehaviour
{
    public RectTransform bar;
    public float percentage = 0.2f;

    void Start()
    {
        GenerateNewZone();
    }

    public void GenerateNewZone()
    {
                RectTransform rectTransform = GetComponent<RectTransform>();
        float barWidth = bar.rect.width;
        float greenZoneWidth = barWidth * percentage;

        float randomStartPosition = Random.Range(-barWidth / 2 + greenZoneWidth /2, barWidth / 2 - greenZoneWidth / 2);
        rectTransform.anchoredPosition = new Vector2(greenZoneWidth, rectTransform.sizeDelta.y);
        rectTransform.anchoredPosition = new Vector2(randomStartPosition, 0);
    }
}
