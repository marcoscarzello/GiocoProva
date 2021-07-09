using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TimerEnd : MonoBehaviour
{
    public float timeRemaining = 120;
    public GameObject GestoreRete;
    public GameObject CanvaManager;

    private Vector3 posizioneShooter;
    private bool stopTimer = false;

    public GameObject testoConsole; 

    private void OnEnable()
    {
        timeRemaining = ComputeTimeRemaining();
        testoConsole.GetComponent<ConsoleManager>().aggiornaConsole("\n\n>All enemies destroyed! Go back to the elevator before some bad assault!");
    }

    void Update()
    {
        if (!stopTimer)
        {
            if (timeRemaining > 0.1)
            {
                timeRemaining -= Time.deltaTime;
                DisplayTime(timeRemaining);
            }
            else
            {
                gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("{0:00}:{1:00}", 0, 0);
                timeRemaining = 0;
                stopTimer = true;
                GameOver(); //GAME OVER
            }
        }

        if (posizioneShooter.z > 228)
        {
            stopTimer = true;
            Win(); //WIN
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        float milliseconds = (timeToDisplay - seconds) * 100;
        if (minutes > 0)
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("<color=orange>{0:00}:{1:00}</color>", minutes, seconds);
        } else
        {
            gameObject.GetComponent<TextMeshProUGUI>().text = string.Format("<color=red>{0:00}:{1:00}</color>", seconds, milliseconds);
        }
    }

    private int ComputeTimeRemaining()
    {
        posizioneShooter = GestoreRete.GetComponent<GestioneParamsInRete>().posizioneShooter;
        Debug.Log("posizione Shooter x: " + (int)posizioneShooter.x + " z: " + (int)posizioneShooter.z);
        float distance = Mathf.Sqrt((posizioneShooter.x * posizioneShooter.x) + ((230 - posizioneShooter.z) * (230 - posizioneShooter.z)));
        Debug.Log("Distanza: " + distance);
        int seconds = (int)(120 * distance / 500);
        if (seconds < 30) seconds = 30;
        if (seconds > 120) seconds = 120;
        Debug.Log("Seconds: " + seconds);
        return seconds;
    }

    private void Win()
    {
        Debug.Log("Win");
        CanvaManager.GetComponent<CanvaManager>().gamestatus = 1;
    }

    private void GameOver()
    {
        Debug.Log("Game Over");
        CanvaManager.GetComponent<CanvaManager>().gamestatus = 2;
    }
}