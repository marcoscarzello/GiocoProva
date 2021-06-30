using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptProva2 : MonoBehaviour
{
    public int valoreProva;
    // Start is called before the first frame update
    void Start()
    {
        valoreProva = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("space"))
            valoreProva++;

        Debug.Log("Secondo il server il valore è attualmente " + valoreProva);
    }
}
