using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class MessageTextUI : MonoBehaviour
{
    public static void DisplayMessage(string text, float duration)
    {
        GameObject messageObject = GameObject.FindWithTag("MessageText");
        if (messageObject != null)
        {
            Text messageText = messageObject.GetComponent<Text>();
            Animation animation = messageObject.GetComponent<Animation>();
            if (messageText != null && animation != null && !animation.isPlaying)
            {
                messageText.text = text;
                animation.Play();
                MonoBehaviour monoBehaviour = messageObject.GetComponent<MonoBehaviour>();
                monoBehaviour.StartCoroutine(HideMessageAfterDuration(duration, messageText));
            }
        }
        else
        {
            Debug.LogWarning("Cannot find Message Text object");
        }
    }

    private static IEnumerator HideMessageAfterDuration(float duration, Text messageText)
    {
        yield return new WaitForSeconds(duration);
        messageText.text = "";
    }
}