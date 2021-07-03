using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ValoriMunizioni : MonoBehaviour
{
    public GameObject gestore;

    public TextMeshProUGUI testoPistola;
    public TextMeshProUGUI testoPompa;
    public TextMeshProUGUI testoMitra;


    void Start()
    {
    }

    void Update()
    {
        testoPompa.text = gestore.GetComponent<GestioneParamsInRete>().munizioniPistola.ToString();
        testoPistola.text = gestore.GetComponent<GestioneParamsInRete>().munizioniPompa.ToString();

        testoMitra.text = gestore.GetComponent<GestioneParamsInRete>().munizioniMitra.ToString();

    }
}
