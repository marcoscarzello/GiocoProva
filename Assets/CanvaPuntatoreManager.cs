using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaPuntatoreManager : MonoBehaviour
{

    public GameObject danno;
    public GameObject potenza;

    void Start()
    {
        
    }

    public void inizioPotenza() {

        potenza.SetActive(true);
    }

    public void finePotenza()
    {

        potenza.SetActive(false);
    }

    public void subisciDanno() {

        danno.SetActive(true);
        StartCoroutine(coroutineGlitch());

    }

    IEnumerator coroutineGlitch()
    {
        yield return new WaitForSeconds(0.3f);
        danno.SetActive(false);




    }
}
