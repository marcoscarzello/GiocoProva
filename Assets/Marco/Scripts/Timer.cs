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

    public GameObject emptydoor;
    public GameObject emptylab;

    GestionePorte myscript;
    GestioneMinigameLabirinto myscript2;


    public float mytimetime;

    void Start()
    {
        myscript = emptydoor.GetComponent<GestionePorte>();
        myscript2 = emptylab.GetComponent<GestioneMinigameLabirinto>();

        timeOut = false;
        reached = false;
        totSeconds = 20f;
        
    }
    void Update()
    {

        seconds = totSeconds - (int)((Time.time - mytimetime) % 60f);
        if (seconds >= 0 && reached == false)
            testo.text = seconds.ToString("00");

        if (seconds == 0 && reached == false)
        {
            timeOut = true;
            testo.text = "TIMEOUT";
            myscript2.FineLabirinto();
            myscript.persoLabirinto();


        }

        if (seconds > 0 && reached && !timeOut)
        {
            reached = false;
            testo.text = "DONE";
            myscript.apriPortaDaLabirinto();
            myscript2.FineLabirinto();
        }


    }

    public void resetTimer() {
        mytimetime = Time.time;
        timeOut = false;
    }
}
