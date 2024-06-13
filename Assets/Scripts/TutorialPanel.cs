using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanel : MonoBehaviour
{
    void Update() {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Close();
        }
    }

    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void FinishTutorial()
    {
        LossCounterManager.Instance.ResetLossCount();
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}