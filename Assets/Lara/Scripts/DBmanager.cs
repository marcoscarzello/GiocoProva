using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBmanager : MonoBehaviour
{
    public bool DBtrovato;
    public GameObject waitingText;
    public GameObject DBpanel;


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
        waitingText.SetActive(false);
        DBpanel.SetActive(true);

    }

}
