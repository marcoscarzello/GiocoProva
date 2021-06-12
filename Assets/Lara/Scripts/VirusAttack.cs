using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class VirusAttack : MonoBehaviour
{
    public Image virus;
    public static bool spawnAllowed;
    public GameObject panel;

    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating(nameof(SpawnVirus), Random.Range(0,1), 1f);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnVirus()
    {
        if (spawnAllowed)
        {
            //float xPos = Random.Range(0, Screen.width);
            //float yPos = Random.Range(0, Screen.height);
            float xPos = Random.Range(0, 1920);
            float yPos = Random.Range(0, 1080);
            Vector3 spawnPosition = new Vector3(xPos, yPos, 0f);
            Image imgSpawned = Instantiate(virus, spawnPosition, Quaternion.identity);
            imgSpawned.transform.SetParent(panel.transform);
            imgSpawned.transform.position = spawnPosition;
        }
    }


}
