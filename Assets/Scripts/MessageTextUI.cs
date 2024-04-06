using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageTextUI: MonoBehaviour {
    public static void DisplayMessage(string text, float duration) 
    {
        GameObject messageObject = GameObject.FindWithTag("MessageText");
        if (messageObject != null) {
            Text messageText = messageObject.GetComponent<Text>();
            if (messageText != null) {
                messageText.text = text;
                MonoBehaviour monoBehaviour = messageObject.GetComponent<MonoBehaviour>();
                monoBehaviour.StartCoroutine(HideMessageAfterDuration(duration, messageText));
            }
        } else {
            Debug.LogWarning("Cannot find Message Text object");
        }
    }

    private static IEnumerator HideMessageAfterDuration(float duration, Text messageText) 
    {
        yield return new WaitForSeconds(duration);
        messageText.text = "";
    }
}
