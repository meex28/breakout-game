using System.Collections;
using UnityEngine;

public class LevelFinalDoor : Item
{
    public float noKeyMessageduration = 1f;
    public float doorOpenedMessageDuration = 1f;

    override public void Interact() {
        bool playerHasKey = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.KEY);

        if(!playerHasKey) {
            MessageTextUI.DisplayMessage("Potrzebujesz klucza, by otworzyć drzwi!", noKeyMessageduration);
            return;
        }

        Debug.Log("Player goes to the next level! :)");
        MessageTextUI.DisplayMessage("W końcu możesz się stąd wydostać. Przechodzisz do następnego poziomu!", doorOpenedMessageDuration);
        StartCoroutine(MoveToNextLevelAfterDelay(doorOpenedMessageDuration));
    }

    IEnumerator MoveToNextLevelAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GameObject.FindWithTag("GameManager").GetComponent<NextLevel>().MoveToNextLevel();
    }}
