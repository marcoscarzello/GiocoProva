using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class VirusCounter : MonoBehaviour
{
    public TextMeshProUGUI testo;
    private int killed; 
    private void Start()
    {
        testo.text =(30 - killed).ToString(); 
    }

    private void Update()
    {
        testo.text =(30- killed).ToString(); 
    }

    public void OneMoreKilled()
    {
        killed += 1;
    }

    public void ResetKilledCounter()
    {
        killed = 0; 
    }

    public int GetKilledNum()
    {
        return killed; 
    }
}
