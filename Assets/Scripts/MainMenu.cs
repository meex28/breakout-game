using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private string[] savedLevelsKeys = { "SavedLevel1", "SavedLevel2", "SavedLevel3" };
    public GameObject saveSlotsPanel;
    public GameObject mainPanel;
    public GameObject[] saveSlotButtons;

    public void PlayGame()
    {
        Debug.Log("Play Game");
        SceneManager.LoadScene(1);
    }

    public void QuitGame()
    {
        Debug.Log("Quit Game");
        Application.Quit();
    }


    // levelIndex from 1 to 4, slotIndex from 0 to 3
    public void Save(int slotIndex)
    {
        var levelIndex = SceneManager.GetActiveScene().buildIndex;
        Debug.Log("Save level " + levelIndex + " to slot " + slotIndex);
        PlayerPrefs.SetInt(savedLevelsKeys[slotIndex], levelIndex);
        string saveDatetime = DateTime.Now.ToString(); // Convert datetime to string
        PlayerPrefs.SetString(savedLevelsKeys[slotIndex] + "_Datetime", saveDatetime);
        PlayerPrefs.Save();
        HideSaveSlots();
    }

    public void LoadSavedLevel(int slotIndex)
    {
        var savedLevel = GetSavedLevelFromPrefs(slotIndex);
        Debug.Log("Load saved level " + savedLevel.levelIndex + " from slot " + slotIndex);
        SceneManager.LoadScene(savedLevel.levelIndex);
    }

    public SavedLevel[] GetSavedLevelFromPrefs()
    {
        SavedLevel[] savedLevels = new SavedLevel[savedLevelsKeys.Length];
        for (int i = 0; i < savedLevelsKeys.Length; i++)
        {
            savedLevels[i] = GetSavedLevelFromPrefs(i);
        }
        return savedLevels;
    }

    private SavedLevel GetSavedLevelFromPrefs(int slotIndex)
    {
        if (!PlayerPrefs.HasKey(savedLevelsKeys[slotIndex])) {
            return null;
        }
        int levelIndex = PlayerPrefs.GetInt(savedLevelsKeys[slotIndex]);
        string datetimeKey = savedLevelsKeys[slotIndex] + "_Datetime";
        string saveDatetimeStr = PlayerPrefs.HasKey(datetimeKey) ? PlayerPrefs.GetString(datetimeKey) : "";
        DateTime.TryParse(saveDatetimeStr, out DateTime saveDatetime);
        return new SavedLevel(levelIndex, saveDatetime);
    }

    public void ShowSaveSlots(bool isSaving = false)
    {
        var savedLevels = GetSavedLevelFromPrefs();
        for (int i = 0; i < savedLevels.Length; i++)
        {
            var savedLevel = savedLevels[i];

            string buttonText = savedLevel == null 
            ? "Pusty" 
            : "Poziom " + savedLevel.levelIndex + "\n(" + savedLevel.saveDatetime.ToString("yyyy-MM-dd HH:mm:ss") + ")";
            saveSlotButtons[i].GetComponentInChildren<UnityEngine.UI.Text>().text = buttonText;

            var isInteractable = savedLevel != null || isSaving;
            saveSlotButtons[i].GetComponent<UnityEngine.UI.Button>().interactable = isInteractable;
        }
        saveSlotsPanel.SetActive(true);
        mainPanel.SetActive(false);
    }

    public void HideSaveSlots()
    {
        saveSlotsPanel.SetActive(false);
        mainPanel.SetActive(true);
    }
}
