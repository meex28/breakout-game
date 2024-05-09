using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialPanel : MonoBehaviour
{
    public void Close()
    {
        Time.timeScale = 1f;
        gameObject.SetActive(false);
    }

    public void FinishTutorial()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(2);
    }
}
