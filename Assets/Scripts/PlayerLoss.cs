using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;  // Add this line to use SceneManager

public class PlayerLoss : MonoBehaviour
{
    public Transform respawnPoint;
    public GameObject player;
    public AudioClip loseSound;
    public GameObject playerLostScreen;
    private bool isPlayerDetected = false;

    private void Start()
    {
        EnemyAI.PlayerLost += HandlePlayerLost;
        Alarm.PlayerLost += HandlePlayerLost;
    }

    private void HandlePlayerLost(object sender, PlayerLostEvent e)
    {
        if (isPlayerDetected) return;

        Debug.Log("Handle Player Lost: " + e.message);
        isPlayerDetected = true;
        StopAlarmIfActive();

        if (loseSound != null)
        {
            AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);
            playerLostScreen.SetActive(true);
            StartCoroutine(MovePlayerToRespawnPointAfterSound());
        }
        else
        {
            Debug.LogWarning("No lose sound assigned.");
            MovePlayerToRespawnPoint();
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
            player.GetComponent<InventorySystem>().ClearInventory();
            StartCoroutine(ResetPlayerDetectionFlag());

            // Reload the current scene
            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);

            playerLostScreen.SetActive(false);
        }
        else
        {
            Debug.LogWarning("Player or respawn point not assigned.");
        }
    }

    private void StopAlarmIfActive()
    {
        var alarm = GameObject.FindGameObjectWithTag("AlarmStartPoint")?.GetComponent<Alarm>();

        if(alarm == null) 
        {
            Debug.LogWarning("Alarm script not found.");
            return;
        }

        alarm.StopAlarm();
    }

    private void OnDestroy()
    {
        // Unsubscribe from the event
        EnemyAI.PlayerLost -= HandlePlayerLost;
        Alarm.PlayerLost -= HandlePlayerLost;
    }
}