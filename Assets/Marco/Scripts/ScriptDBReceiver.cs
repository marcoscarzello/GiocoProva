using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ScriptDBReceiver : MonoBehaviour
{
    public DataTable DataBase;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(DataBase);
    }
}
