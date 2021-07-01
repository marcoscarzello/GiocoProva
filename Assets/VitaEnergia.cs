using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaEnergia : MonoBehaviour
{
    public float salute;
    public float energia;

    void Start()
    {
        salute = 100f;
        energia = 10f;
    }

    void Update()
    {
        
    }

    public void Curato() {

        salute = 100;
        energia -= 30;
        Debug.Log("Sono il client. Salute ricaricata! Grazie");
    }
}
