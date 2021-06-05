using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;


public class EnemyFinder : MonoBehaviour
{
    public static string codiceNemico;
    public static string codiceSoluzione;

    public static int statoRicerca;

    public GameObject inputField;

    

    string sigla;
    string colore;
    string linea;
    string faccia;

    void Start()
    {
        codiceNemico = "";
        sigla = "";
        colore = "";
        linea = "";
        faccia = "";

        statoRicerca = 0;
        sigla = inputField.GetComponent<TMP_InputField>().text;
    }

    public void impostaColore(string color) {
        colore = color;
        Debug.Log(colore);
    }

    public void impostaSigla() {
        sigla = inputField.GetComponent<TMP_InputField>().text;
        Debug.Log(sigla);
    }

    public void impostaLinea(string line)
    {
        linea = line;
        Debug.Log(linea);
    }

    public void impostaFaccia(string face)
    {
        faccia = face;
        Debug.Log(faccia);
    }

    public void CalcolaSoluzione() {

        Debug.Log("Calcolo");
        codiceNemico = colore + sigla + linea + faccia;
    }

    public void GoOn() {

        if (statoRicerca < 3) 
            statoRicerca++;

        if (statoRicerca == 3)
        {
            statoRicerca = 0;
            codiceSoluzione = "";
            codiceNemico = "";
            sigla = "";
            colore = "";
            linea = "";
            faccia = "";
        }
    }

    public void VaiAllaSoluzione()
    {
        statoRicerca = 3;
    }


    void Update() {
        switch (statoRicerca)
        {
            case 0: //ricerca colori e sigla
                transform.GetChild(0).gameObject.SetActive(true);
                transform.GetChild(1).gameObject.SetActive(true);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(0).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(1).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(2).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(4).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(5).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(6).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(7).gameObject.SetActive(false);
                transform.GetChild(4).transform.GetChild(3).GetChild(8).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);

                break;
            case 1: //ricerca linee
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(true);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(false);

                break;
            case 2: //ricerca faccia
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(true);
                transform.GetChild(4).gameObject.SetActive(false);

                break;
            case 3: //mostra soluzione
                transform.GetChild(0).gameObject.SetActive(false);
                transform.GetChild(1).gameObject.SetActive(false);
                transform.GetChild(2).gameObject.SetActive(false);
                transform.GetChild(3).gameObject.SetActive(false);
                transform.GetChild(4).gameObject.SetActive(true);

                break;
            default:
                Debug.Log("errore");
                break;
        }
    }

}
