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
            Animator animator = messageObject.GetComponent<Animator>();

            if (messageText != null && animator != null)
            {
                messageText.text = text;
                animator.SetTrigger("Display");

                MonoBehaviour monoBehaviour = messageObject.GetComponent<MonoBehaviour>();
                monoBehaviour.StartCoroutine(HideMessageAfterDuration(duration, messageText, animator));
            }
        }
        else
        {
            Debug.LogWarning("Cannot find Message Text object");
        }
    }

    private static IEnumerator HideMessageAfterDuration(float duration, Text messageText, Animator animator)
    {
        yield return new WaitForSeconds(duration);
        messageText.text = "";
        animator.SetTrigger("Hide");
    }
}