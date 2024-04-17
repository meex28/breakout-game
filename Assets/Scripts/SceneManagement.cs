using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour
{
    public int index;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            MoveToNextLevel();
        }
    }

    public void MoveToNextLevel()
    {
        Debug.Log("Move to next level: " + index);
        SceneManager.LoadScene(index);
    }
}