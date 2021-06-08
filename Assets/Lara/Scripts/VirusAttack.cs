using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

public class VirusAttack : MonoBehaviour
{

    public RectTransform[] spawnPoints;
    public Image virus;
    int randomSpawnPoint; 
    public static bool spawnAllowed; 


    void Start()
    {
        spawnAllowed = true;
        InvokeRepeating("SpawnVirus", 0f, 1f);
    }

    // Update is called once per frame
    void Update()
    {

        /*
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = Physics2D.GetRayIntersection(ray, Mathf.Infinity);

            if (hit.collider != null)
            {
                // do whatever you want to do here
            }
        }

        */
    }

    void SpawnVirus()
    {
        if (spawnAllowed)
        {
            randomSpawnPoint = Random.Range(0, spawnPoints.Length);
            Image img = Instantiate(virus, spawnPoints[randomSpawnPoint].position, Quaternion.identity);
            img.transform.SetParent(gameObject.transform);
        }
    }


}
