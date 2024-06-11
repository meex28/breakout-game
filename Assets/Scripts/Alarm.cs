using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Alarm : MonoBehaviour
{
    public int alarmDuration = 100;
    public AudioSource alarmSound;
    public float alarmInterval = 5f;
    public float alarmStartedMessageDuration = 2f;
    public GameObject alarmAnimation;
    private int timerId = -1;


    private bool alarmActive = false;

    public static event EventHandler<PlayerLostEvent> PlayerLost;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            StartAlarm();
        }
    }

    private void StartAlarm() 
    {
        if(alarmActive) {
            return;
        }
        
        Debug.Log("Alarm started!");
        alarmAnimation.gameObject.SetActive(true);
        alarmActive = true;
        timerId = GameObject.FindWithTag("GameManager").GetComponent<TimerDisplay>().AddTimer("Czas na ucieczkę", alarmDuration);
        alarmSound.Play();
        StartCoroutine(PlayAlarmSound());
        StartCoroutine(Countdown());
    }

    private IEnumerator PlayAlarmSound()
    {
        while (alarmActive)
        {
            alarmSound.Play();
            yield return new WaitForSeconds(alarmInterval);
        }
    }

    private IEnumerator Countdown()
    {
        yield return new WaitForSeconds(alarmDuration);
        StopAlarm();
        InvokePlayerLostEvent();
    }

    public void StopAlarm()
    {
        if(timerId != -1) {
            GameObject.FindWithTag("GameManager").GetComponent<TimerDisplay>().StopTimer(timerId);
            timerId = -1;
        }
        alarmActive = false;
        alarmSound.Stop();
        Debug.Log("Alarm stopped!");
    }

    private void InvokePlayerLostEvent()
    {
        PlayerLost?.Invoke(this, new PlayerLostEvent("Alarm countdown is over"));
    }
}
