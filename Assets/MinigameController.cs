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

    private int currentTask = 0;

    void Start()
    {
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
        greenZoneController.GenerateNewZone();
    }
}