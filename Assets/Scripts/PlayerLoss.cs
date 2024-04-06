using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerLoss : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public AudioClip loseSound;
    public Text messageText;
    private bool isPlayerDetected = false; // use flag to prevent from raising action multiple times

    private void Start()
    {
        // Subscribe to the event
        EnemyAI.PlayerLost += HandlePlayerLost;
    }

    private void HandlePlayerLost(object sender, PlayerLostEvent e)
    {
        if (isPlayerDetected) return;

        Debug.Log("Handle Player Lost: " + e.message);
        isPlayerDetected = true;

        if (loseSound != null)
        {
            AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);
            StartCoroutine(MovePlayerToRespawnPointAfterSound());
        }
        else
        {
            Debug.LogWarning("No lose sound assigned.");
            MovePlayerToRespawnPoint();
        }

        // Display message on screen
        if (messageText != null)
        {
            messageText.text = e.message;
        }
    }

    private IEnumerator MovePlayerToRespawnPointAfterSound()
    {
        yield return new WaitForSeconds(loseSound.length);
        MovePlayerToRespawnPoint();
    }

    // use little delay to prevent running lossing second time - idk why its happening
    private IEnumerator ResetPlayerDetectionFlag()
    {
        yield return new WaitForSeconds(1f);
        isPlayerDetected = false;
    }

    private void MovePlayerToRespawnPoint()
    {
        if (player != null && respawnPoint != null)
        {
            player.transform.position = respawnPoint.position;
            StartCoroutine(ResetPlayerDetectionFlag());
        }
        else
        {
            Debug.LogWarning("Player or respawn point not assigned.");
        }
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        EnemyAI.PlayerLost -= HandlePlayerLost;
    }
}
