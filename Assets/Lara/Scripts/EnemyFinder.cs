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

    public Button ciano;        //ricorda di assegnare i pulsanti nel gameobj EnemyFinder
    public Button magenta;
    public Button giallo; 

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
        sigla = inputField.GetComponent<TMP_InputField>().text.ToUpper();

        switch (sigla) {

            case "EK-2":
                sigla = "a";
                break;
            case "FR-8":
                sigla = "b";
                break;
            case "PN-3":
                sigla = "c";
                break;
            case "KZ-1":
                sigla = "d";
                break;
            case "NN-9":
                sigla = "e";
                break;
            case "CP-3":
                sigla = "f";
                break;
            case "SM-1":
                sigla = "g";
                break;
            case "LS-2":
                sigla = "h";
                break;
            default:
                sigla = "0";    //nada ma importante avere 1 char sempre
                break;
        }

        Debug.Log(sigla);
    }

    public void azzeraSigla() {
        inputField.GetComponent<TMP_InputField>().text = "";

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

    public void GoBack()
    {
        if (statoRicerca > 0 && statoRicerca < 3)
            statoRicerca--;

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

        switch (colore)
        {
            case "":
                //change l'alpha di tutti e tre a 0.5
                ciano.image.color = new Color(ciano.image.color.r, ciano.image.color.g, ciano.image.color.b, 0.5f);
                magenta.image.color = new Color(magenta.image.color.r, magenta.image.color.g, magenta.image.color.b, 0.5f);
                giallo.image.color = new Color(giallo.image.color.r, giallo.image.color.g, giallo.image.color.b, 0.5f);
                break;

            case "1": //si è cliccato ciano
                ciano.image.color = new Color(ciano.image.color.r, ciano.image.color.g, ciano.image.color.b, 1f);   //change alpha del ciano a 1

                //change alpha di giallo e magenta a 0.5
                magenta.image.color = new Color(magenta.image.color.r, magenta.image.color.g, magenta.image.color.b, 0.5f);
                giallo.image.color = new Color(giallo.image.color.r, giallo.image.color.g, giallo.image.color.b, 0.5f);
                break;

            case "2"://si è cliccato magenta
                magenta.image.color = new Color(magenta.image.color.r, magenta.image.color.g, magenta.image.color.b, 1f);   //change alpha del magenta a 1

                //change alpha di giallo e ciano a 0.5
                ciano.image.color = new Color(ciano.image.color.r, ciano.image.color.g, ciano.image.color.b, 0.5f);
                giallo.image.color = new Color(giallo.image.color.r, giallo.image.color.g, giallo.image.color.b, 0.5f);
                break;

            case "3"://si è cliccato gialllo
                giallo.image.color = new Color(giallo.image.color.r, giallo.image.color.g, giallo.image.color.b, 1f); //change alpha del giallo a 1

                //change alpha di ciano e magenta a 0.5
                ciano.image.color = new Color(ciano.image.color.r, ciano.image.color.g, ciano.image.color.b, 0.5f);
                magenta.image.color = new Color(magenta.image.color.r, magenta.image.color.g, magenta.image.color.b, 0.5f);
                break;
        }
    }

}
