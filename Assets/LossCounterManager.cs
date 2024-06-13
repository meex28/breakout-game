using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LossCounterManager : MonoBehaviour
{
    public static LossCounterManager Instance { get; private set; }

    public int LossCount { get; private set; } = 0;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    public void IncrementLossCount()
    {
        LossCount++;
    }

    public void ResetLossCount()
    {
        LossCount = 0;
    }

    public void SetLossCount(int count)
    {
        LossCount = count;
    }

    public int GetLossCount()
    {
        return LossCount;
    }
}