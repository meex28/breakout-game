using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    public AudioSource source;
    public Image soundOnIcon;
    public Image soundOffIcon;
    private bool muted = false;
    private void Awake()
    {
        source = GameObject.FindGameObjectWithTag("BackgroundMusic").GetComponent<AudioSource>();
        Load();
        HandleMusicUpdate();
    }

    public void ToggleMusic()
    {
        muted = !muted;
        HandleMusicUpdate();
        Save();
    }

    private void HandleMusicUpdate()
    {
        soundOnIcon.enabled = !muted;
        soundOffIcon.enabled = muted;
        if (muted)
        {
            source.Pause();
        }
        else
        {
            if (!source.isPlaying)
            {
                source.Play();
            }
        }
    }

    private void Load()
    {
        muted = PlayerPrefs.GetInt("music_muted") == 1;
    }

    private void Save()
    {
        PlayerPrefs.SetInt("music_muted", muted ? 1 : 0);
    }
}