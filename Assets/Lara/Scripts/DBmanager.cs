using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBmanager : MonoBehaviour
{
    public bool DBtrovato;
    public GameObject waitingText;
    public GameObject DBpanel;
    public GameObject DBtab;


    void Start()
    {
        DBtrovato = false;
        DBpanel.SetActive(false);
    }

    void Update()
    {
        if (DBtrovato)
            Trovato();
    }

    public void Trovato()
    {
        DBtrovato = true;
        waitingText.SetActive(false);
        //DBtab.SetActive(true);

        DBpanel.SetActive(true);


    }

}
