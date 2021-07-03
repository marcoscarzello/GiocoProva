using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionePorte : MonoBehaviour
{
    public int ultimaPortaSelezionata;
    public string password;

    public TMP_InputField inputField;

    public GameObject console;

    //metodo chiamato dal clic su porta
    public void setUltimaPorta(int n)
    {
        ultimaPortaSelezionata = n;
    }

    public void apriPorta() {

        Debug.Log("Aperta la " + ultimaPortaSelezionata);
        console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=yellow>Door " + ultimaPortaSelezionata + " opened!</color>");

    }

    public void setPassword(string pw)
    {
        password = pw;
        Debug.Log("nuova pw = " + pw);
    }

    public void apriPortaDaLabirinto() {

        apriPorta();
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
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 2:
                if (password == "password")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 4:
                if (password == "capibara")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 5:
                if (password == "yolo")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 7:
                if (password == "open")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 8:
                if (password == "please")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 9:
                if (password == "knock")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            case 11:
                if (password == "close")
                {
                    apriPorta();
                }
                else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>Wrong password.</color>");

                break;
            default:
                Debug.Log("nessuna porta selezionata");
                console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> You must first select a door.");

                break;

        }
        //riporta al vuoto il campo di inserimento pw:
        inputField.text = "";
    }
 


}
