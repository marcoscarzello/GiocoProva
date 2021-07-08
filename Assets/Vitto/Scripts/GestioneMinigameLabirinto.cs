using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneMinigameLabirinto : MonoBehaviour
{
    private AudioSource audio;

    public GameObject pallina;

    void Start() {

    }

   public void AvvioLabirinto() {


        pallina.GetComponent<MovimentoSferetta>().spawn();
        gameObject.SetActive(true);

        audio = GetComponent<AudioSource>();

        audio.Play();

        if (GameObject.Find("Simon") != null)
        GameObject.Find("Simon").SetActive(false);
        if (GameObject.Find("Simon2") != null)
            GameObject.Find("Simon2").SetActive(false);

    }
    public void FineLabirinto()
    {
        audio.Stop();

        gameObject.SetActive(false);

    }


}
