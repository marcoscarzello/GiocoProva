using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunizioniManager : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void raccoltaMunizioni()
    {
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

        scortaPistola += 40;
        scortaAssalto += 100;
        scortaPompa += 20;
    }

    public void PowerUpPotenza()
    {
        //l'energia viene tolta in vitaenergia
        moltiplicatore = 3f;
        Debug.Log("Sono il client, potenza aumentata. Inizio coroutine");
        StartCoroutine(coroutinePotenza());
        Debug.Log("Fine Coroutine potenza. moltiplicatore riportato a 1");
        moltiplicatore = 1f;

    }

    IEnumerator coroutinePotenza() {
        yield return new WaitForSeconds(120);
    }
}
