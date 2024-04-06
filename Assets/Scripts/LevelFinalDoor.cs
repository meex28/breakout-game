using UnityEngine;

public class LevelFinalDoor : Item
{
    public float messageDuration = 2f;

    override public void Interact() {
        bool playerHasKey = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.KEY);

        if(!playerHasKey) {
            MessageTextUI.DisplayMessage("Potrzebujesz klucza, by otworzyć drzwi!", messageDuration);
            return;
        }

        Debug.Log("Player goes to the next level! :)");
    }
}
