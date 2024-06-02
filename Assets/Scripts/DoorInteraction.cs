using UnityEngine;

public class DoorInteraction : MonoBehaviour
{
    public GameObject player; // Drag your player GameObject here in the inspector
    public AudioSource sound;
    private SpriteRenderer playerSpriteRenderer;
    private bool isPlayerNear = false;
    public GameObject hidePrompt;

    private void Start()
    {
        playerSpriteRenderer = player.GetComponent<SpriteRenderer>();
        if (hidePrompt != null)
        {
            hidePrompt.SetActive(false);
        } else {
            Debug.LogWarning("HidePrompt not set.");
        }
    }

    private void Update()
    {
        if (isPlayerNear && Input.GetKeyDown(KeyCode.E)) // Assuming 'E' is the interaction key
        {
            ToggleInvisibility();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerNear = true;
            hidePrompt?.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject == player)
        {
            isPlayerNear = false;
            ResetInvisibility(); // Resets visibility when player leaves the door area
            hidePrompt?.SetActive(false);
        }
    }

    private void ToggleInvisibility()
    {
        bool isInvisible = playerSpriteRenderer.color.a == 0.3f;
        if (!isInvisible)
        {
            player.GetComponent<Player>().SetInvisible();
            sound.Play();
            DisableCollision();
        }
        else
        {
            ResetInvisibility();
            EnableCollision();
            sound.Play();
        }
    }

    private void ResetInvisibility()
    {
        // Reset player visibility
        player.GetComponent<Player>().SetVisible();
    }

    private void EnableCollision()
    {
    }

    private void DisableCollision()
    {
    }
}
