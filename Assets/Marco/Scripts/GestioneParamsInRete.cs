using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneParamsInRete : MonoBehaviour
{
    //IMPORTANTISSIMO: CONTROLLARE SEMPRE CHE NON SIA NULL LA ROBA CHE SI PRENDE DA QUI, PRIMA DI USARLA
    //Perché se il client non è ancora collegato ma il server sì queste cose sono sicuramente null

   
    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public int munizioniPistola;
    public int munizioniPompa;
    public int munizioniMitra;

    public List<Vector3> posizioniArmi;

    public float salute;
    public float energia;

    void Start()
    {
        
    }

    void Update()
    {
        
    }


    public void RicaricaSalute() {

        Debug.Log("Ricarica salute");
        //fare evento ricarica salute: mandare al client la nuova salute e sarà esso a riaggiornare il server con la nuova salute
        //controllare prima di avere energia a sufficienza. Ricordarsi lato client di abbassare l'energia!!!!
    }
    public void AumentaPotenza()
    {

        Debug.Log("Aumenta potenza");
        //fare evento aumenta potenza
    }

    public void RicaricaMunizioni()
    {

        Debug.Log("Ricarica munizioni");
        //fare evento ricarica munizioni
    }
}
