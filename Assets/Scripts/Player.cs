using Mirror;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.Events;


public class Player : NetworkBehaviour
{
    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public List<Vector3> posizioniArmi;

    public float salute;
    public float energia;

    public int valoreProva;

    void Start()
    {
        

    }

    void OnEnable() {

        //Iscrizione di un metodo di prova ad un evento del cavolo
        if (!isServer)
        GestioneEventoDaServer.PremutoSpazio += MandaPressioneTastoAlClient;
    }

    void OnDisable()
    {
        if (!isServer)
            GestioneEventoDaServer.PremutoSpazio -= MandaPressioneTastoAlClient;
    }

    void Update()
    {
        //Cose che deve fare il player se è il client
        if (isLocalPlayer && !isServer)
        {

            //cosa di prova
            //valoreProva = GameObject.Find("oggettoProvaClient").GetComponent<scriptProva1>().valoreProva;
            //AggiornaServerProva(valoreProva);

            //inviare al server i parametri dello shooter
            posizioneShooter = GameObject.Find("Shooter").gameObject.transform.position;
            AggiornaServerSuParamsShooter(posizioneShooter, salute, energia);

            //inviare al server posizioni dei robot nemici
            if (GameObject.Find("Robot_Lv1") != null)
                posizionelv1 = GameObject.Find("Robot_Lv1").gameObject.transform.position;
            if (GameObject.Find("Robot_Lv2_1") != null)
                posizionelv2_1 = GameObject.Find("Robot_Lv2_1").gameObject.transform.position;
            if (GameObject.Find("Robot_Lv2_2") != null)
                posizionelv2_2 = GameObject.Find("Robot_Lv2_2").gameObject.transform.position;
            if (GameObject.Find("Robot_Lv3") != null)
                posizionelv3 = GameObject.Find("Robot_Lv3").gameObject.transform.position;
            AggiornaServerSuPosizioneNemici(posizionelv1, posizionelv2_1, posizionelv2_2, posizionelv3);

            //inviare al server posizione armi
            if (GameObject.Find("Script_Starter") != null)
                posizioniArmi = GameObject.Find("Script_Starter").GetComponent<Weapons_Generator>().WeaponPositions();
            AggiornaServerSuPosizioneArmi(posizioniArmi);
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

        //prova da cancellare
        GameObject.Find("Canvas_map").gameObject.GetComponent<ProvaPosizioneCanvaeccecc>().posizionePG = posizioneShooter;


        if (GameObject.Find("GestoreParamsInRete") != null)
        {
            GestioneParamsInRete MyScriptReference = GameObject.Find("GestoreParamsInRete").GetComponent<GestioneParamsInRete>();
            MyScriptReference.posizioneShooter = posizioneShooter;
            MyScriptReference.salute = salute;
            MyScriptReference.energia = energia;
            MyScriptReference.posizionelv3 = posizionelv3;

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
        GameObject.Find("oggettoProvaServer").GetComponent<scriptProva2>().valoreProva = valoreProva;
    }

    [ClientRpc]
    public void AggiornaClientProva(int valoreProva)
    {
        if (!isServer)
        GameObject.Find("oggettoProvaClient").GetComponent<scriptProva1>().valoreProva = valoreProva;
    }

    //Funzione di prova che si manda quando si ascolta un evento e si passa un parametro
    [ClientRpc]
    public void MandaPressioneTastoAlClient(int a) {

        if (!isServer)
            GameObject.Find("OggettoProvaRiceveEventoDaClient").GetComponent<RicezioneEventoDaClient>().stampa(a);

    }


}
