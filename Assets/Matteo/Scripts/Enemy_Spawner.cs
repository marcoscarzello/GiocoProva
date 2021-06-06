using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Spawner : MonoBehaviour
{

    public GameObject Enemy_LV1;
    public GameObject Enemy_LV2;
    public GameObject Enemy_LV3;

    public bool UnlockLV1 = false;
    public bool UnlockLV2 = false;
    public bool UnlockLV3 = false;

    private bool Phase1 = false;
    private bool Phase2 = false;
    private bool Phase3 = false;
    
    void Update()
    {
        if (UnlockLV1 && !Phase1)
        {
            Enemy_LV1.SetActive(true);
            Phase1 = true;
        }

        if (UnlockLV2 && !Phase2)
        {
            Enemy_LV2.SetActive(true);
            Phase2 = true;
        }

        if (UnlockLV3 && !Phase3)
        {
            Enemy_LV3.SetActive(true);
            Phase3 = true;
        }
    }
}
