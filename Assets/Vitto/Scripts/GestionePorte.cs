using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GestionePorte : MonoBehaviour
{
    public int ultimaPortaSelezionata;
    public string password;

    public TMP_InputField inputField;

    //metodo chiamato dal clic su porta
    public void setUltimaPorta(int n)
    {
        ultimaPortaSelezionata = n;
    }


    public void setPassword(string pw)
    {
        password = pw;
        Debug.Log("nuova pw = " + pw);
    }

    public void apriPortaDaLabirinto() {

        Debug.Log("Aperta la " + ultimaPortaSelezionata);
    }

    public void apriPortaDaSimon()
    {

        Debug.Log("Aperta la " + ultimaPortaSelezionata);
    }

    public void checkPw() {

        

        switch (ultimaPortaSelezionata) {

            case 1:
                if (password == "muffin")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 2:
                if (password == "password")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 4:
                if (password == "capibara")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 5:
                if (password == "yolo")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 7:
                if (password == "open")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 8:
                if (password == "please")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 9:
                if (password == "knock")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            case 11:
                if (password == "close")
                {
                    Debug.Log("aperta la " + ultimaPortaSelezionata);
                }
                break;
            default:
                Debug.Log("nessuna porta seleionata");
                break;

        }
        //riporta al vuoto il campo di inserimento pw:
        inputField.text = "";
    }
 


}
