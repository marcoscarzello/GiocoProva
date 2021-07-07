using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HudPlayer : MonoBehaviour
{
    public GameObject PowerVignette;
    public GameObject DannoSubito; 


    public void SetPowerVignette(bool set)
    {
        PowerVignette.SetActive(set);
    }

    public void SetDannoSubito(bool set)
    {
        PowerVignette.SetActive(false);
    }
}
