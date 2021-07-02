using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneMinigameSimon : MonoBehaviour
{
    public void AvvioSimon()
    {

        gameObject.SetActive(true);
        if (GameObject.Find("MinigameLabirinto") != null)
            GameObject.Find("MinigameLabirinto").SetActive(false);


    }
    public void FineSimon()
    {

        gameObject.SetActive(false);

    }
}
