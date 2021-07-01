using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MunizioniManager : MonoBehaviour
{
    public int scortaPistola;
    public int scortaAssalto;
    public int scortaPompa;

    // Start is called before the first frame update
    void Start()
    {
        scortaPistola = 20;
        scortaAssalto = 40;
        scortaPompa = 10;
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PiuMunizioniGrazie() {

        scortaPistola = 40;
        scortaAssalto = 100;
        scortaPompa = 20;
    }
}
