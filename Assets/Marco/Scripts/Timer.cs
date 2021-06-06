using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public TextMeshProUGUI testo;
    public float seconds;
    public bool timeOut; //controlla se il tempo è scaduto
    public float totSeconds;
    public static bool reached; //controlla se la pallina è arrivata al centro

    void Start()
    {
        timeOut = false;
        reached = false;
        totSeconds = 20f;
        
    }
    void Update()
    {
        seconds = totSeconds - (int)(Time.time % 60f);
        if (seconds >= 0 && reached == false)
            testo.text = seconds.ToString("00");

        if (seconds == 0 && reached == false)
        {
            timeOut = true;
            testo.text = "TIMEOUT";

        }

        if (seconds > 0 && reached && !timeOut)
            testo.text = "DONE";
    }
}
