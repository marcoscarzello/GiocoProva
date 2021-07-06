using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DBmanager : MonoBehaviour
{
    public bool DBtrovato;
    public GameObject waitingText;
    public GameObject DBpanel;
    public GameObject DBtab;
    public bool flag;
    public GameObject console;


    void Start()
    {
        DBtrovato = false;
        DBpanel.SetActive(false);
        flag = true;
    }

    void Update()
    {
        if (DBtrovato && flag)
        {
            Trovato();
        }
    }

    public void Trovato()
    {
        //DBtrovato = true;
        waitingText.SetActive(false);

        flag = false;


        console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=yellow>DataBase found!</color>\n\n> <color=yellow>You can now use the tab to insert enemies features and check the correct solution to kill 'em all.</color>");
        //DBtab.SetActive(true);

        DBpanel.SetActive(true);


    }

}
