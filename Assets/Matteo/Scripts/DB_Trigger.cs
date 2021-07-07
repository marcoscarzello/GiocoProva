using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class DB_Trigger : MonoBehaviour
{
    public GameObject ScriptStarter;
    private Collider other;
    private bool trigger = false;

    public GameObject schermoDB;


    //evento trovato DB
    public delegate void TrovareDB();
    public static event TrovareDB DBTrovato;

    void Update()
    {
        if (trigger)
        {
            if (Input.GetKey(KeyCode.E) && !ScriptStarter.GetComponent<Enemy_Spawner>().collectedDB && other.tag == "DataBase")
            {
                Debug.Log("DataBase raccolto. Invio evento.");
                ScriptStarter.GetComponent<Enemy_Spawner>().collectedDB = true;

                //cambia monitor db
                schermoDB.GetComponent<ChangeMaterialRef>().DbFound();

                //lancio evento
                if (DBTrovato != null)
                    DBTrovato();
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        other = collider;
        trigger = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        trigger = false;
    }
}
