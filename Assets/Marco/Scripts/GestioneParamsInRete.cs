using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestioneParamsInRete : MonoBehaviour
{
    //IMPORTANTISSIMO: CONTROLLARE SEMPRE CHE NON SIA NULL LA ROBA CHE SI PRENDE DA QUI, PRIMA DI USARLA
    //Perch� se il client non � ancora collegato ma il server s� queste cose sono sicuramente null

   
    public Vector3 posizioneShooter;

    public Vector3 posizionelv1;
    public Vector3 posizionelv2_1;
    public Vector3 posizionelv2_2;
    public Vector3 posizionelv3;

    public List<Vector3> posizioniArmi;

    public float salute;
    public float energia;

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}
