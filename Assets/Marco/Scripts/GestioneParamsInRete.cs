using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GestioneParamsInRete : MonoBehaviour
{
    //IMPORTANTISSIMO: CONTROLLARE SEMPRE CHE NON SIA NULL LA ROBA CHE SI PRENDE DA QUI, PRIMA DI USARLA
    //Perché se il client non è ancora collegato ma il server sì queste cose sono sicuramente null

    //evento di ricarica salute 
    public delegate void Cura();
    public static event Cura SaluteRicaricata;

    //evento aumenta potenza: al client ricordarsi di ritornare alla potenza normale dopo tot secondi
    public delegate void Forza();
    public static event Forza PotenzaAumentata;

    //Evento ricarica munizioni
    public delegate void Ammos();
    public static event Ammos MunizioniRicaricate;

    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public int munizioniPistola;
    public int munizioniPompa;
    public int munizioniMitra;

    public List<Vector3> posizioniArmi;

    public GameObject console;

    //parametri aggiornati continuamente dal client
    public float salute;
    public float energia;
    
    void Start()
    {
        salute = 100f;
        energia = 10f;

        munizioniMitra = 0;
        munizioniPistola = 0;
        munizioniPompa = 0;
        posizioneShooter = new Vector3(250f, 250f, 250f);
    }

    void Update()
    {
        
    }


    public void RicaricaSalute() {

        Debug.Log("Sono il server. Ricarico salute");
        //fare evento ricarica salute: mandare al client l'info e sarà esso a riaggiornare il server con la nuova salute e la nuova energia
        //controllare prima di avere energia a sufficienza. 

        if (energia >= 30f)
        {
            if (SaluteRicaricata != null)
            {
                SaluteRicaricata();

                //aggiornamento console:
                console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=yellow>Health charged.</color>");
            }
        }
        else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>You don't have enough energy to restore health.</color>");


    }
    public void AumentaPotenza()
    {

        Debug.Log("sono il server, Aumenta potenza");
        if (energia >= 60f)
        {
            if (PotenzaAumentata != null)
            {
                PotenzaAumentata();

            }
        }
    }

    public void RicaricaMunizioni()
    {

        Debug.Log("sono il server, Ricarico munizioni");
        if (energia >= 99f)
        {
            if (MunizioniRicaricate != null)
            {
                MunizioniRicaricate();

                console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=yellow>Ammo charged.</color>");
            }
        }
        else console.GetComponent<ConsoleManager>().aggiornaConsole("\n\n> <color=red>You don't have enough energy to charge ammos.</color>");

    }
}
