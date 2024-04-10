using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LockpickingTarget : Item
{
    public GameObject successBarUI;
    public float messageDuration = 1f;
    private int successCount = 0;
    private bool isLockpicking = false;

override public void Interact()
{
    bool playerHasKey = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.PICKLOCK);
    if (!playerHasKey)
    {
        return;
    }

    StartCoroutine(StartLockpicking());
}


    private IEnumerator StartLockpicking()
    {
        isLockpicking = true;
        successBarUI.SetActive(true);
        successCount = 0;
        Time.timeScale = 0;

        while (isLockpicking)
        {
            yield return StartCoroutine(DoQTE());

            successBarUI.GetComponent<Slider>().value = successCount;

            if (successCount >= 3)
            {
                isLockpicking = false;
            }
        }

        successBarUI.SetActive(false);
        Time.timeScale = 1;
    }

    private IEnumerator DoQTE()
    {
        bool hasPressed = false;
        float timer = 1.0f;
        float startTime = Time.realtimeSinceStartup;

        while (Time.realtimeSinceStartup - startTime < timer)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                hasPressed = true;
                break;
            }
            yield return null;
        }

        if (hasPressed)
        {
            successCount++;
        }
        else
        {
            successCount = 0;
            yield return new WaitForSecondsRealtime(1);
        }
    }
}
