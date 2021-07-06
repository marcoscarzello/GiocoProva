using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;
using UnityStandardAssets.CrossPlatformInput;
using UnityStandardAssets.Utility;
using UnityStandardAssets.Characters.FirstPerson;
using System;
using Random = UnityEngine.Random;



public class Player : NetworkBehaviour
{

    //
    //TODO: database trovato
    //TODO: non si può usare .Find su oggetti non attivi. 

    //ATTENZIONE: gli eventi per qualche motivo vengono chiamati due volte (nel caso di pressione del tasto spazio), potrebbe essere necessario dimezzare le variabili

    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public int munizioniPistola, munizioniPompa, munizioniMitra;

    public string ultimaSoluzione;

    //public DataTable DataBase;

    public List<Vector3> posizioniArmi;

    public float salute;
    public float energia;

    public int valoreProva;

    void Start()
    {
        posizioneShooter = new Vector3(0f, 0f, 0f);
        salute = 100f;
        energia = 10f;


        //solo il client attiva lo shooter, che parte disattivo 


        if (isServer)
            if (GameObject.Find("Shooter") != null)
            {
                GameObject.Find("Shooter").SetActive(false);

            }

        if (!isServer) {
            if (GameObject.Find("Shooter") != null)
            {
                GameObject.Find("Shooter").GetComponent<FirstPersonController>().enabled = true;

            }

        }
    }

    void OnEnable() {

        //Iscrizione di un metodo di prova ad un evento del cavolo
        if (!isServer)
        GestioneEventoDaServer.PremutoSpazio += MandaPressioneTastoAlClient;

        //iscrizione a evento di ricarica Salute. Viene mandato dal server, quindi mi iscrivo se non sono il server
        if (!isServer)
        {
            GestioneParamsInRete.SaluteRicaricata -= MandaAlClientCura;

            GestioneParamsInRete.SaluteRicaricata += MandaAlClientCura;
        }

        //iscrizione evento potenza
        if (!isServer)
        {
            GestioneParamsInRete.PotenzaAumentata -= MandaAlClientForza;
            GestioneParamsInRete.PotenzaAumentata += MandaAlClientForza;

        }

        //iscrizione evento munizioni
        if (!isServer)
            GestioneParamsInRete.MunizioniRicaricate += MandaAlClientAmmos;

        //iscrizione evento porte
        if (!isServer)
            GestionePorte.ApertaPorta += MandaAlClientPorta;

        //iscrizione evento attaccovirus: solo se sono il server
        //if (isServer)
            VirusFSM.PartitoAttacco += MandaAlServerAttaccoVirus;

        //iscrizione evento ricerca nuova soluzione
        //if (!isServer)
            EnemyFinder.NuovaSoluzione += MandaAlClientNuovoCodiceNemico;

        //iscrizione evento DBTrovato
        //if (isServer)
            DB_Trigger.DBTrovato += MandaAlServerDBTrovato;

    }

    //disiscrizioni dagli eventi
    void OnDisable()
    {
        if (!isServer)
            GestioneEventoDaServer.PremutoSpazio -= MandaPressioneTastoAlClient;

        if (!isServer)
            GestioneParamsInRete.SaluteRicaricata -= MandaAlClientCura;

        if (!isServer)
            GestioneParamsInRete.PotenzaAumentata -= MandaAlClientForza;

        if (!isServer)
            GestioneParamsInRete.MunizioniRicaricate -= MandaAlClientAmmos;

        if (!isServer)
            GestionePorte.ApertaPorta -= MandaAlClientPorta;

        if (isServer)
            VirusFSM.PartitoAttacco -= MandaAlServerAttaccoVirus;

        if (!isServer)
            EnemyFinder.NuovaSoluzione -= MandaAlClientNuovoCodiceNemico;

        if (isServer)
            DB_Trigger.DBTrovato -= MandaAlServerDBTrovato;
    }



    void Update()
    {

        Debug.Log("va");

        
        //Cose che deve fare il player se è il client
        if (isLocalPlayer && !isServer)
        {

            //cosa di prova
            //valoreProva = GameObject.Find("oggettoProvaClient").GetComponent<scriptProva1>().valoreProva;
            //AggiornaServerProva(valoreProva);

            //inviare al server i parametri dello shooter. Mancano ancora salute e energia da creare in uno script dello shooter
            if (GameObject.Find("Shooter") != null) {
                posizioneShooter = GameObject.Find("Shooter").gameObject.transform.position;
                salute = GameObject.Find("Shooter").GetComponent<VitaEnergia>().salute;
                energia = GameObject.Find("Shooter").GetComponent<VitaEnergia>().energia;

            }
            AggiornaServerSuParamsShooter(posizioneShooter, salute, energia);

            //inviare al server posizioni dei robot nemici
            if (GameObject.Find("Robot_Lv1") != null)
                posizionelv1 = GameObject.Find("Robot_Lv1").gameObject.transform.position;
            else posizionelv1 = new Vector3(-1000f, 0, 0);
            if (GameObject.Find("Robot_Lv2_1") != null)
                posizionelv2_1 = GameObject.Find("Robot_Lv2_1").gameObject.transform.position;
            else posizionelv2_1 = new Vector3(-1000f, 0f, 0f);
            if (GameObject.Find("Robot_Lv2_2") != null)
                posizionelv2_2 = GameObject.Find("Robot_Lv2_2").gameObject.transform.position;
            else posizionelv2_2 = new Vector3(-1000f, 0f, 0f);
            if (GameObject.Find("Robot_Lv3") != null)
                posizionelv3 = GameObject.Find("Robot_Lv3").gameObject.transform.position;
            else posizionelv3 = new Vector3(-1000f, 0f, 0f);
            AggiornaServerSuPosizioneNemici(posizionelv1, posizionelv2_1, posizionelv2_2, posizionelv3);

            //inviare al server posizione armi
            if (GameObject.Find("Script_Starter") != null)
                posizioniArmi = GameObject.Find("Script_Starter").GetComponent<Weapons_Generator>().WeaponPositions();
            AggiornaServerSuPosizioneArmi(posizioniArmi);

            //inviare al server il database: non funziona
            //if (GameObject.Find("DBGeneratorProva") != null)
            //        DataBase = GameObject.Find("DBGeneratorProva").GetComponent<DB_Generator>().DataBase;
            //AggiornaServerSuDatabase(DataBase);

            //inviare munizioni al server
            if (GameObject.Find("WeaponHolder") != null)
            {
                munizioniPistola = GameObject.Find("WeaponHolder").GetComponent<MunizioniManager>().scortaPistola;
                munizioniMitra = GameObject.Find("WeaponHolder").GetComponent<MunizioniManager>().scortaAssalto;
                munizioniPompa = GameObject.Find("WeaponHolder").GetComponent<MunizioniManager>().scortaPompa;
            }
            AggiornaServerSuMunizioni(munizioniPistola, munizioniPompa, munizioniMitra);


            //inviare lastSolution al server
            if (GameObject.Find("Script_Starter") != null)
            {
                ultimaSoluzione = GameObject.Find("Script_Starter").GetComponent<DB_Generator>().lastSolution;

                Debug.Log("sono il client. invio ultima soluzione");
            }
            AggiornaServerSuUltimaSoluzione(ultimaSoluzione);
        }


        //Cose che deve fare il player se è il server
        if (isLocalPlayer && isServer) {

            //cosa di prova
            //valoreProva = GameObject.Find("oggettoProvaServer").GetComponent<scriptProva2>().valoreProva;
            //AggiornaClientProva(valoreProva);

        }



    }

    //Funzione di aggiornamento client -> server di posizione, vita, energia
    [Command]
    public void AggiornaServerSuParamsShooter(Vector3 posizioneShooter, float salute, float energia) {

        if (GameObject.Find("GestoreParamsInRete") != null)
        {
            GestioneParamsInRete MyScriptReference = GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>();
            MyScriptReference.posizioneShooter = posizioneShooter;

            Debug.Log("posizione shooter = " + posizioneShooter);
            MyScriptReference.salute = salute;
            MyScriptReference.energia = energia;
        }

    }

    //Funzione aggiornamento posizione nemici client -> server
    [Command]
    public void AggiornaServerSuPosizioneNemici(Vector3 posizionelv1, Vector3 posizionelv2_1, Vector3 posizionelv2_2, Vector3 posizionelv3)
    {
        if (GameObject.Find("GestoreParamsInRete") != null)
        {
            GestioneParamsInRete MyScriptReference = GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>();
            MyScriptReference.posizionelv1 = posizionelv1;
            MyScriptReference.posizionelv2_1 = posizionelv2_1;
            MyScriptReference.posizionelv2_2 = posizionelv2_2;
            MyScriptReference.posizionelv3 = posizionelv3;

        }
    }

    [Command]
    public void AggiornaServerSuPosizioneArmi(List<Vector3> posizioniArmi) {
        if (GameObject.Find("GestoreParamsInRete") != null)
        {
            GestioneParamsInRete MyScriptReference = GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>();
            MyScriptReference.posizioniArmi = posizioniArmi;

        }
    }

    [Command]
    public void AggiornaServerProva(int valoreProva)
    {
        if (GameObject.Find("oggettoProvaServer") != null)
            GameObject.Find("oggettoProvaServer").GetComponent<scriptProva2>().valoreProva = valoreProva;
    }

    //[Command]
    //public void AggiornaServerSuDatabase(DataTable DataBase)
    //{
    //    if (GameObject.Find("DBReceiver") != null)
    //    GameObject.Find("DBReceiver").GetComponent<ScriptDBReceiver>().DataBase = DataBase;
    //}

    [Command]
    public void AggiornaServerSuMunizioni(int pis, int pom, int mit) {

        if (GameObject.Find("GestoreParamsInRete") != null)
        {
            GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>().munizioniPistola = pis;
            GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>().munizioniPompa = pom;
            GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>().munizioniMitra = mit;

        }
    }

    [Command]
    public void AggiornaServerSuUltimaSoluzione(string ultSol)
    {

        if (GameObject.Find("Enemy_finder") != null)
        {
            GameObject.Find("Enemy_finder").GetComponent<EnemyFinder>().ultimoCodiceSoluzione = ultSol;

        }
    }


    [ClientRpc]
    public void AggiornaClientProva(int valoreProva)
    {
        if (!isServer)
        {
            if (GameObject.Find("oggettoProvaClient") != null)
                GameObject.Find("oggettoProvaClient").GetComponent<scriptProva1>().valoreProva = valoreProva;
        }
    }


    //Funzione di prova che si manda quando si ascolta un evento e si passa un parametro
    [ClientRpc]
    public void MandaPressioneTastoAlClient(int a) {

        if (!isServer)
        {
            if (GameObject.Find("OggettoProvaRiceveEventoDaClient") != null)
            GameObject.Find("OggettoProvaRiceveEventoDaClient").GetComponent<RicezioneEventoDaClient>().stampa(a);
        }

    }

    [ClientRpc]
    public void MandaAlClientCura() {
        if (!isServer)
        {
            if (GameObject.Find("Shooter") != null)
                GameObject.Find("Shooter").GetComponent<VitaEnergia>().Curato();
        }
    }

    [ClientRpc]
    public void MandaAlClientForza()
    {
        if (!isServer)
        {
            Debug.Log("Player: mando al client forza");
            //a munizionimanager aumento il moltiplicatore, a vitaenergia tolgo l'energia usata
            if (GameObject.Find("WeaponHolder") != null)
                GameObject.Find("WeaponHolder").GetComponent<MunizioniManager>().PowerUpPotenza();
            if (GameObject.Find("Shooter") != null)
                GameObject.Find("Shooter").GetComponent<VitaEnergia>().Potenza();
        }
    }


    [ClientRpc]
    public void MandaAlClientAmmos()
    {
        if (!isServer)
        {
            if (GameObject.Find("WeaponHolder") != null)
                GameObject.Find("WeaponHolder").GetComponent<MunizioniManager>().PiuMunizioniGrazie();
            if (GameObject.Find("Shooter") != null)
                GameObject.Find("Shooter").GetComponent<VitaEnergia>().Munizze();
        }
    }

    [ClientRpc]
    public void MandaAlClientPorta(int porta)
    {

        if (!isServer)
        {
            Debug.Log("Player: apertura porta inviata al client");
            if (GameObject.Find("door 1") != null)
            {
                GameObject.Find("door 1").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 2") != null)
            {
                GameObject.Find("door 2").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 3") != null)
            {
                GameObject.Find("door 3").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 4") != null)
            {
                GameObject.Find("door 4").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 5") != null)
            {
                GameObject.Find("door 5").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 6") != null)
            {
                GameObject.Find("door 6").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 7") != null)
            {
                GameObject.Find("door 7").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 8") != null)
            {
                GameObject.Find("door 8").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 9") != null)
            {
                GameObject.Find("door 9").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 10") != null)
            {
                GameObject.Find("door 10").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 11") != null)
            {
                GameObject.Find("door 11").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
            if (GameObject.Find("door 12") != null)
            {
                GameObject.Find("door 12").GetComponent<OpenDoor>().OpenDoorNumber(porta);
            }
        }

    }

    [ClientRpc]
    public void MandaAlClientNuovoCodiceNemico(string stringaNemico) {

        if (!isServer)
        {
            if (GameObject.Find("Script_Starter") != null)
            {
                GameObject.Find("Script_Starter").GetComponent<DB_Generator>().CercaSoluzione(stringaNemico);
                Debug.Log("sono il Player del client: ricevuto evento ricerca nuova soluzione. Invocata funzione su DB_generator.");
            }
        }
    }


    //eventi client to server: attacco virus
    [Command]
    public void MandaAlServerAttaccoVirus()
    {
        if (isServer)
        {
            if (GameObject.Find("VirusAttack") != null)
            {
                GameObject.Find("Canvas").GetComponent<virusHelper>().startVirusAttack();
                Debug.Log("Player: attacco virus inoltrato al server");
            }
        }
    }

    [Command]
    public void MandaAlServerDBTrovato()
    {
        Debug.Log("qua entra");
        if (isServer)
        {
            if (GameObject.Find("Panels") != null)
            {
                GameObject.Find("Panels").GetComponent<DBmanager>().Trovato();
                Debug.Log("Player: sentito eEvnto db trovato, mando al server");
            }
        }
    }



}
