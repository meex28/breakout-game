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
        messages.Add("welcome", "Witaj w grze Breakout!\n Twoim zadaniem będzie ucieczka z podziemnego więzienia.\n \nPowodzenia!");
        messages.Add("items","W więzieniu można znaleźć różne przedmioty, które pomogą Ci w ucieczce. Możesz je podnieść, naciskając klawisz 'E'.");
        messages.Add("useItems", "W grze istnieją dwa rodzaje przedmiotów:\n1. Napój energetyczny - zwiększa prędkość poruszania\n2. Koszulka camo - chwilowo czyni cię niewykrywalnym\nIch dostępność jest widoczna w lewym górnym rogu ekranu.\nAby użyć przedmiotu, naciśnij klawisz 1 lub 2.");
        messages.Add("afterEnergyDrinkUsage", "W grze znajdują się także inne przedmioty. Znajdziesz je w następnych poziomach.");
        messages.Add("beforeGuards", "Podczas ucieczki na swojej drodze napotkasz strażników. Unikaj ich pola widzenia, aby nie zostać złapanym. W tym celu wykorzystaj otoczenie.");
        messages.Add("hide", "Kiedy znajdziesz się w miejscu odpowiednim do schowania, na ekranie pojawi się informacja. Naciśnij klawisz 'E', aby się schować.");
        messages.Add("finalDoors", "Aby dostać się na następne piętro musisz przejść przez drzwi. Niestety bez klucza się nie obejdzie. Straż trzyma go w sejfie.");
        messages.Add("lockpick", "Do otwarcia sejfu potrzebujesz wytrycha. Jest on schowany na każdym poziomie.");
        messages.Add("finalKey", "Próba otwarcia sejfy zaczcyna się po naciśnięciu klawisza 'E'.\n Aby otworzyc sejf musisz otworzyć trzy zabezpieczenia. Naciskaj klawisz 'E' w momencie kiedy wskazówka znajdzie się w zielonej strefie.");
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
