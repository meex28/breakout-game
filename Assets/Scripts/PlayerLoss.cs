using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
        playerLostScreen.SetActive(true);
        LossCounterManager.Instance.IncrementLossCount();

        if (loseSound != null)
        {
            AudioSource.PlayClipAtPoint(loseSound, Camera.main.transform.position);
            StartCoroutine(ReloadSceneAfterSound());
        }
        else
        {
            Debug.LogWarning("No lose sound assigned.");
            ReloadScene();
        }
    }

    private IEnumerator ReloadSceneAfterSound()
    {
        yield return new WaitForSeconds(loseSound.length);
        ReloadScene();
    }

    private IEnumerator ResetPlayerDetectionFlag()
    {
        yield return new WaitForSeconds(1f);
        isPlayerDetected = false;
    }

    private void ReloadScene()
    {
        StopAlarmIfActive();

        if (player != null)
        {
            player.GetComponent<InventorySystem>().ClearInventory();
            StartCoroutine(ResetPlayerDetectionFlag());

            Scene currentScene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(currentScene.name);
        }
        else
        {
            Debug.LogWarning("Player not assigned.");
        }
    }

    private void StopAlarmIfActive()
    {
        var alarm = GameObject.FindGameObjectWithTag("AlarmStartPoint")?.GetComponent<Alarm>();

        if (alarm == null)
        {
            Debug.LogWarning("Alarm script not found.");
            return;
        }

        alarm.StopAlarm();
    }

    private void OnDestroy()
    {
        EnemyAI.PlayerLost -= HandlePlayerLost;
        Alarm.PlayerLost -= HandlePlayerLost;
    }
}