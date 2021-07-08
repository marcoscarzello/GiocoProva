using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Events;


public class GestionePorte : MonoBehaviour
{
    public int ultimaPortaSelezionata;
    public string password;

    public TMP_InputField inputField;

    public GameObject console;
    private Transform[] doors = null;


    private AudioSource[] audios;

    //evento porta
    public delegate void AperturaPorta(int a);
    public static event AperturaPorta ApertaPorta;

    private void Start()
    {
        audios = GetComponents<AudioSource>();

        doors = GetComponentsInChildren<Transform>();
    }

    //metodo chiamato dal clic su porta
    public void setUltimaPorta(int n)
    {
        ultimaPortaSelezionata = n;
    }

    public void apriPorta() {

        audios[1].Play();

        Debug.Log("Aperta la " + ultimaPortaSelezionata);
        console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=yellow>Door " + ultimaPortaSelezionata + " opened!</color>");
        doors[ultimaPortaSelezionata].gameObject.SetActive(false);
        //lancio evento
        if (ApertaPorta != null)
        {
            ApertaPorta(ultimaPortaSelezionata);
            Debug.Log("Evento porta lanciato da server");
        }
        
    }

    public void setPassword(string pw)
    {
        password = pw;
        Debug.Log("nuova pw = " + pw);
    }

    public void apriPortaDaLabirinto() {

        apriPorta();
    }

    public void persoLabirinto() {

        console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Door hacking failed. Try again.</color>");
    }

    public void apriPortaDaSimon()
    {

        apriPorta();
    }

    public void checkPw() {

        

        switch (ultimaPortaSelezionata) {

            case 1:
                if (password == "muffin")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }

                break;
            case 2:
                if (password == "password")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 4:
                if (password == "capibara")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 5:
                if (password == "yolo")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 7:
                if (password == "open")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 8:
                if (password == "please")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 9:
                if (password == "knock")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            case 11:
                if (password == "close")
                {
                    apriPorta();
                }
                else
                {
                    console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");
                    error();
                }
                break;
            default:
                Debug.Log("nessuna porta selezionata");
                { console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> You must first select a door.");

                    error();
                }

                break;

        }
        //riporta al vuoto il campo di inserimento pw:
        inputField.text = "";
    }

    public void error() {

        audios[0].Play();
    }

}
