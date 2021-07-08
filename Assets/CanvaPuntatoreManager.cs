using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaPuntatoreManager : MonoBehaviour
{

    public GameObject danno;
    public GameObject potenza;
    public GameObject menuHUD;

    void Start()
    {
        OpenMenuHUD();
    }

    public void OpenMenuHUD()
    {
        Cursor.visible = true;

        menuHUD.SetActive(true);
    }

    public void CloseMenuHUD()
    {
        Cursor.visible = false;

        menuHUD.SetActive(false);
    }


    public void inizioPotenza() {
        Debug.Log("inizio canva puntatore potenza");
        potenza.SetActive(true);
    }

    public void finePotenza()
    {
        Debug.Log("fine canva puntatore potenza");

        potenza.SetActive(false);
    }

    public void subisciDanno() {

        danno.SetActive(true);
        StartCoroutine(coroutineGlitch());

    }

    IEnumerator coroutineGlitch()
    {
        yield return new WaitForSeconds(0.15f);
        danno.SetActive(false);

    }
}
