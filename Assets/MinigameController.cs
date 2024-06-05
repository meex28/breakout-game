using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MinigameController : MonoBehaviour
{
    public RectTransform indicator;
    public RectTransform greenZone;
    public Image[] lockImages;
    public Sprite openLockSprite;
    public Sprite closedLockSprite;
    public GreenZoneController greenZoneController;
    public Canvas gameCanvas;
    public GameObject cabinet;
    public GameObject key;
    public GameObject openText;
    public GameObject bar;

    private int currentTask = 0;
    private Vector2 initialGreenZoneSize;

    void Start()
    {
        initialGreenZoneSize = greenZone.sizeDelta;
        ResetLocks();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            CheckIndicatorPosition();
        }
    }

    void CheckIndicatorPosition()
    {
        float indicatorLeft = indicator.anchoredPosition.x - indicator.rect.width / 2;
        float indicatorRight = indicator.anchoredPosition.x + indicator.rect.width / 2;
        float greenZoneLeft = greenZone.anchoredPosition.x - greenZone.rect.width / 2;
        float greenZoneRight = greenZone.anchoredPosition.x + greenZone.rect.width / 2;

        if (indicatorRight > greenZoneLeft && indicatorLeft < greenZoneRight)
        {
            Debug.Log("Success! Indicator is within the green zone.");
            lockImages[currentTask].sprite = openLockSprite;
            currentTask++;

            if (currentTask < lockImages.Length)
            {
                ShrinkGreenZone();
                greenZoneController.GenerateNewZone();
            }
            else
            {
                Debug.Log("All tasks completed!");
                EndGame();
            }
        }
        else
        {
            Debug.Log("Fail! Indicator is outside the green zone.");
            ResetLocks();
        }
    }

    void EndGame()
    {
        Debug.Log("Congratulations! You have completed all tasks!");
        indicator.gameObject.SetActive(false);
        greenZone.gameObject.SetActive(false);
        bar.gameObject.SetActive(false);
        foreach(Image image in lockImages) {
            image.gameObject.SetActive(false);
        }
        openText.gameObject.SetActive(true);
        StartCoroutine(DisableCanvasAfterDelay(1.0f));
    }

    IEnumerator DisableCanvasAfterDelay(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameCanvas.gameObject.SetActive(false);
        cabinet.gameObject.SetActive(false);
        key.gameObject.SetActive(true);
    }

    void ResetLocks()
    {
        foreach (var lockImage in lockImages)
        {
            lockImage.sprite = closedLockSprite;
        }
        currentTask = 0;
        greenZone.sizeDelta = initialGreenZoneSize;
        greenZoneController.GenerateNewZone();
    }

    void ShrinkGreenZone()
    {
        greenZone.sizeDelta *= 0.9f;
    }
}