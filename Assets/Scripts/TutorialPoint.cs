using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialPoint : MonoBehaviour
{
    public GameObject tutorialPanel;
    public Dictionary<string, string> messages = new Dictionary<string, string>();
    public string messageKey = "";
    public float activationRange = 5f;
    private bool playerInRange = false;
    private bool messageShown = false;

    void Start()
    {
        // initialize messages
        messages.Add("welcome", "Witaj w tutorialu Breakout! Twoim zadaniem będzie pokonanie kolejnych poziomów i ucieczka z więzienia. Idź dalej, by poznać kolejne elementy gry. Powodzenia!");
        messages.Add("energyDrinkPickedUp", "Podniosłeś pierwszy przedmiot - napój energetyczny. Użyj przycisku '1', by go wypróbować!");
        messages.Add("afterEnergyDrinkUsage", "W grze znajdują się także inne przedmioty. Znajdziesz je w następnych poziomach.");
        messages.Add("beforeGuards", "Unikaj wizji strażników, by nie zostać złapanym. Użyj skrytek, żeby ich ominąć.");
        messages.Add("finalDoors", "Aby wydostać się z poziomu musisz przejść przez finałowe drzwi. Znajdź klucz, by je otworzyć. Znajduje się on w sejfie na końcu korytarza.");
        messages.Add("lockpick", "By otworzyć sejf potrzebujesz wytrycha, podnieś go.");
        messages.Add("finalKey", "Otwórz sejf, aby zdobyć klucz. Gdy to zrobisz, będziesz mógł otworzyć drzwi.");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!playerInRange)
            {
                ShowTutorialMessage();
            }
            playerInRange = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    private void ShowTutorialMessage()
    {
        if (messageShown)
        {
            return;
        }
        messageShown = true;
        if (tutorialPanel != null)
        {
            Time.timeScale = 0f;
            tutorialPanel.SetActive(true);
            Text messageText = tutorialPanel.transform.Find("Message").gameObject.GetComponent<Text>();
            if (messageText != null)
            {
                if (messages.ContainsKey(messageKey))
                {
                    messageText.text = messages[messageKey];
                }
                else
                {
                    Debug.LogWarning("Message key not found.");
                }
            }
            else
            {
                Debug.LogWarning("Text component not found in TutorialPanel.");
            }
        }
        else
        {
            Debug.LogWarning("TutorialPanel GameObject not found.");
        }
    }
}
