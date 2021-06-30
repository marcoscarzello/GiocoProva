using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class GestioneEventoDaServer : MonoBehaviour
{
    public delegate void PressioneSpazio(int a);
    public static event PressioneSpazio PremutoSpazio;

    private int a;
    
    void Start()
    {
        a = 5;
    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {

            if (PremutoSpazio != null)
                PremutoSpazio(a);
        }
    }
}
