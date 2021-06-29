using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    public GameObject Enemy_LV1;
    public GameObject Enemy_LV2_1;
    public GameObject Enemy_LV2_2;
    public GameObject Enemy_LV3;

    public bool collectedDB = false;
    public bool defeatedLV1 = false;
    
    void Update()
    {
        if (collectedDB)
        {
            Enemy_LV1.SetActive(true);
        }

        if (defeatedLV1)
        {
            Enemy_LV2_1.SetActive(true);
            Enemy_LV2_2.SetActive(true);
            Enemy_LV3.SetActive(true);
        }
    }
}
