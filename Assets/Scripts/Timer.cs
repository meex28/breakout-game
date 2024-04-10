using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public GUIStyle timerStyle; // Style for the timer text
    private static List<TimerData> timerDataList = new List<TimerData>(); // List to store timer data

    void Start()
    {
        timerStyle = new GUIStyle
        {
            alignment = TextAnchor.UpperRight, // Right-align the timer
            fontSize = 40 // Adjust font size as needed
        };
        timerStyle.normal.textColor = Color.white; // Set text color
    }

    public void AddTimer(string label, float duration)
    {
        timerDataList.Add(new TimerData(label, duration, Time.time));
        StartCoroutine(RunTimer(timerDataList.Count - 1)); // Start coroutine with list index
    }

    IEnumerator RunTimer(int index)
    {
        yield return new WaitForSeconds(timerDataList[index].duration);
        timerDataList.RemoveAt(index); // Remove completed timer from list
    }

    void OnGUI()
    {
        if (timerDataList.Count > 0) // Check if any timers are active
        {
            int verticalOffset = 0;
            for (int i = 0; i < timerDataList.Count; i++)
            {
                float remainingTime = timerDataList[i].duration - (Time.time - timerDataList[i].startTime); // Calculate remaining time

                if (remainingTime > 0f) // Check if timer is still active
                {
                    string timeLeftText = remainingTime.ToString("F1") + "s"; // Format remaining time
                    string labelText = timerDataList[i].label + ": ";

                    // Adjust positioning for each timer in a column
                    GUI.Label(new Rect(Screen.width - 150, 10 + verticalOffset, 100, 20), labelText, timerStyle);
                    GUI.Label(new Rect(Screen.width - 50, 10 + verticalOffset, 100, 20), timeLeftText, timerStyle);

                    verticalOffset += 25; // Adjust vertical offset for each timer
                }
                else // Timer is finished, remove it from the list
                {
                    timerDataList.RemoveAt(i);
                }
            }
        }
    }
}

public class TimerData
{
    public string label;
    public float duration;
    public float startTime;
    public TimerData(string label, float duration, float startTime)
    {
        this.label = label;
        this.duration = duration;
        this.startTime = startTime;
    }
}
