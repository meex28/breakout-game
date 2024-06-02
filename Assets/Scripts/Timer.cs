using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public GUIStyle timerStyle; // Style for the timer text
    private static List<TimerData> timerDataList = new List<TimerData>(); // List to store timer data
    private static int nextID = 0; // Variable to assign unique IDs

    void Start()
    {
        timerStyle = new GUIStyle
        {
            alignment = TextAnchor.UpperRight, // Right-align the timer
            fontSize = 40 // Adjust font size as needed
        };
        timerStyle.normal.textColor = Color.white; // Set text color
    }

    public int AddTimer(string label, float duration)
    {
        int id = nextID++;
        TimerData newTimer = new TimerData(id, label, duration, Time.time);
        timerDataList.Add(newTimer);
        newTimer.coroutine = StartCoroutine(RunTimer(newTimer)); // Start coroutine and store it in TimerData
        return id; // Return the ID of the new timer
    }

    public void StopTimer(int id)
    {
        for (int i = 0; i < timerDataList.Count; i++)
        {
            if (timerDataList[i].id == id)
            {
                StopCoroutine(timerDataList[i].coroutine); // Stop the coroutine
                timerDataList.RemoveAt(i); // Remove the timer from the list
                break;
            }
        }
    }

    IEnumerator RunTimer(TimerData timerData)
    {
        yield return new WaitForSeconds(timerData.duration);
        timerDataList.Remove(timerData); // Remove completed timer from list
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
                    i--; // Adjust the index to account for the removed item
                }
            }
        }
    }
}

public class TimerData
{
    public int id;
    public string label;
    public float duration;
    public float startTime;
    public Coroutine coroutine; // Add coroutine reference

    public TimerData(int id, string label, float duration, float startTime)
    {
        this.id = id;
        this.label = label;
        this.duration = duration;
        this.startTime = startTime;
        this.coroutine = null; // Initialize coroutine as null
    }
}
