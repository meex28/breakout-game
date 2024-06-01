using System.Collections;
using UnityEngine;

public class LevelFinalDoor : Item
{
    public float noKeyMessageDuration = 1f;
    public float doorOpenedMessageDuration = 2f;
    public GameObject endTransistion;
    public GameObject textTransistion;
    public GameObject startTransistion;

    private void Start()
    {
        // Start the coroutine to deactivate startTransistion after 1 second
        StartCoroutine(DeactivateStartTransistion());
    }

    override public void Interact() {
        bool playerHasKey = GameObject.FindWithTag("Player").GetComponent<InventorySystem>().IncludesItem(Items.KEY);

        if (!playerHasKey) {
            MessageTextUI.DisplayMessage("Potrzebujesz klucza, by otworzyć drzwi!", noKeyMessageDuration);
            return;
        }

        Debug.Log("Player goes to the next level! :)");
        endTransistion.SetActive(true);
        textTransistion.SetActive(true);
        StartCoroutine(MoveToNextLevelAfterDelay(doorOpenedMessageDuration));
    }

    IEnumerator MoveToNextLevelAfterDelay(float delay) {
        yield return new WaitForSeconds(delay);
        GameObject.FindWithTag("GameManager").GetComponent<NextLevel>().MoveToNextLevel();
    }

    IEnumerator DeactivateStartTransistion()
    {
        yield return new WaitForSeconds(1f);
        startTransistion.SetActive(false);
    }
}