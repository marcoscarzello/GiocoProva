using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunizioniManager : MonoBehaviour
{

    private AudioSource[] audios;

    public GameObject canvaspuntatore;

    public int scortaPistola;
    public int scortaAssalto;
    public int scortaPompa;

    public float moltiplicatore;

    // Start is called before the first frame update
    void Start()
    {
        scortaPistola = 20;
        scortaAssalto = 40;
        scortaPompa = 10;
        moltiplicatore = 1f;

        audios = GetComponents<AudioSource>();

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void raccoltaMunizioni()
    {

        audios[0].Play();

        int rnd = Random.Range(0, 99);
        if (rnd <= 32)
        {
            scortaPistola += 20;
            Debug.Log("Ricarica MunizPistola: " + scortaPistola);
        }

        if (rnd >= 66)
        {
            scortaAssalto += 50;
            Debug.Log("Ricarica MunizAssalto: " + scortaAssalto);
        }

        if (rnd >= 33 && rnd <= 65)
        {
            scortaPompa += 10;
            Debug.Log("Ricarica MunizPompa: " + scortaPompa);
        }
    }

    public void PiuMunizioniGrazie() {

        scortaPistola += 25;
        scortaAssalto += 50;
        scortaPompa += 10;
    }

    public void PowerUpPotenza()
    {
        audios[1].Play();

        canvaspuntatore.GetComponent<CanvaPuntatoreManager>().inizioPotenza();
        //l'energia viene tolta in vitaenergia
        moltiplicatore = 3f;
        Debug.Log("Sono il client, potenza aumentata. Inizio coroutine");
        StartCoroutine(coroutinePotenza());
       

    }

    IEnumerator coroutinePotenza() {
        yield return new WaitForSeconds(60);

        Debug.Log("Fine Coroutine potenza. moltiplicatore riportato a 1");
        moltiplicatore = 1f;
        canvaspuntatore.GetComponent<CanvaPuntatoreManager>().finePotenza();



    }
}
