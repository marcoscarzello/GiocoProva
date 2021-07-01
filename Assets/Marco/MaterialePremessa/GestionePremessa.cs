using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GestionePremessa : MonoBehaviour
{
    public GameObject blocco1;
    public GameObject blocco2;
    public GameObject blocco3;
    public GameObject bloccoIstruzioni;




    void Start()
    {
        blocco1.SetActive(true);
        blocco2.SetActive(false);
        blocco3.SetActive(false);
        bloccoIstruzioni.SetActive(false);


    }

    void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (blocco1.active)
            {
                blocco1.SetActive(false);
                blocco2.SetActive(true);
            }

            else if (blocco2.active)
            {
                blocco2.SetActive(false);
                blocco3.SetActive(true);
            }

            else if (blocco3.active)
            {
                blocco3.SetActive(false);
                bloccoIstruzioni.SetActive(true);
            }

            else if (bloccoIstruzioni.active)
            {
                //cambioscena
            }

        }
    }
}
