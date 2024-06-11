using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerDisplay : MonoBehaviour
{
    public GUIStyle timerStyle;
    public Font font;
    private static List<TimerData> timerDataList = new List<TimerData>();
    private static int nextID = 0;

    void Start()
    {
        timerStyle = new GUIStyle
        {
            alignment = TextAnchor.UpperRight,
            font = font,
            fontSize = 24,
        };
        timerStyle.normal.textColor = Color.white;
    }

    public int AddTimer(string label, float duration)
    {
        int id = nextID++;
        TimerData newTimer = new TimerData(id, label, duration, Time.time);
        timerDataList.Add(newTimer);
        newTimer.coroutine = StartCoroutine(RunTimer(newTimer));
        return id;
    }

    public void StopTimer(int id)
    {
        for (int i = 0; i < timerDataList.Count; i++)
        {
            if (timerDataList[i].id == id)
            {
                StopCoroutine(timerDataList[i].coroutine);
                timerDataList.RemoveAt(i);
                break;
            }
        }
    }

    IEnumerator RunTimer(TimerData timerData)
    {
        yield return new WaitForSeconds(timerData.duration);
        timerDataList.Remove(timerData);
    }

    void OnGUI()
    {
        if (timerDataList.Count > 0)
        {
            int verticalOffset = 0;
            int padding = 20;
            int rightPadding = 50;
            int horizontalSpacing = 40; // Increased distance between the timer and the message
            for (int i = 0; i < timerDataList.Count; i++)
            {
                float remainingTime = timerDataList[i].duration - (Time.time - timerDataList[i].startTime);

                if (remainingTime > 0f)
                {
                    string timeLeftText = remainingTime.ToString("F1") + "s";
                    string labelText = timerDataList[i].label + ": ";

                    Rect labelRect = new Rect(Screen.width - rightPadding - 150 - horizontalSpacing, 10 + verticalOffset + padding, 100, 20);
                    Rect timeRect = new Rect(Screen.width - rightPadding - 50, 10 + verticalOffset + padding, 100, 20);

                    DrawOutlinedLabel(labelRect, labelText, timerStyle, Color.black);
                    DrawOutlinedLabel(timeRect, timeLeftText, timerStyle, Color.black);

                    verticalOffset += 30;
                }
                else
                {
                    timerDataList.RemoveAt(i);
                    i--;
                }
            }
        }
    }

    void DrawOutlinedLabel(Rect rect, string text, GUIStyle style, Color outlineColor)
    {
        var backupStyle = new GUIStyle(style);
        var backupColor = style.normal.textColor;

        int outlineThickness = Mathf.CeilToInt(style.fontSize / 4.0f);

        style.normal.textColor = outlineColor;

        for (int x = -outlineThickness; x <= outlineThickness; x++)
        {
            for (int y = -outlineThickness; y <= outlineThickness; y++)
            {
                if (x != 0 || y != 0)
                {
                    GUI.Label(new Rect(rect.x + x, rect.y + y, rect.width, rect.height), text, style);
                }
            }
        }

        style.normal.textColor = backupColor;
        GUI.Label(rect, text, style);

        style.normal.textColor = backupColor;
    }
}

public class TimerData
{
    public int id;
    public string label;
    public float duration;
    public float startTime;
    public Coroutine coroutine;

    public TimerData(int id, string label, float duration, float startTime)
    {
        this.id = id;
        this.label = label;
        this.duration = duration;
        this.startTime = startTime;
        this.coroutine = null;
    }
}