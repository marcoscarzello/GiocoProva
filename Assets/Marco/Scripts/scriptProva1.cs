using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptProva1 : MonoBehaviour
{
    public int valoreProva;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            valoreProva++;
        Debug.Log("Secondo il client il valore è attualmente " + valoreProva);
    }
}
