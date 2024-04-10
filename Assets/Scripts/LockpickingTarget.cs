using UnityEngine;

public class LockipingTarget : Item
{
    public float messageDuration = 2f;

    override public void Interact() {
        bool playerHasKey = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.PICKLOCK);

        if(!playerHasKey) {
            MessageTextUI.DisplayMessage("Potrzebujesz wytrycha, by to otworzyć!", messageDuration);
            return;
        }

        Debug.Log("Otwierasz fiu fiu");
    }
}
