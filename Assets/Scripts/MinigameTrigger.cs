using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MinigameTrigger : MonoBehaviour
{
    public GameObject minigameController;
    private bool playerInRange = false;


    void Update()
    {
        if (playerInRange && Input.GetKeyDown(KeyCode.E))
        {
            StartMinigame();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
        }
    }

    void StartMinigame()
    {
        bool hasLockpick = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.PICKLOCK);
        if(hasLockpick)
        {
            minigameController.SetActive(true);
        }
        else
        {
            MessageTextUI.DisplayMessage("Potrzebujesz wytrycha, aby otworzyć sejf!", 1f);
        }
    }
}