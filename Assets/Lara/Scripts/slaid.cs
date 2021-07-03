using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class slaid : MonoBehaviour
{
    // Start is called before the first frame update
    public float volume = 1;
    public static slaid instance;

    void Start() //chiamata prima della start
    {
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }
}
