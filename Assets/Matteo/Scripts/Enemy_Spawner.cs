using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Enemy_Spawner : MonoBehaviour
{
    public GameObject Shooter;

    public GameObject NanoBot;
    public GameObject Enemy_LV1;
    public GameObject Enemy_LV2a;
    public GameObject Enemy_LV2b;
    public GameObject Enemy_LV3;

    public bool collectedDB = false;
    public bool defeatedLV1 = false;
    public bool defeatedLV2_LV3 = false;

    private bool control1 = true;
    private bool control2 = true;

    public bool allDefeated = false;
    private int countDefeated = 0;

    public Random rnd = new Random();

    void Update()
    {
        if (collectedDB && control1)
        {
            Enemy_LV1.SetActive(true);
            control1 = false;
        }

        if (defeatedLV1 && control2)
        {
            SpawnNanoBots(WhichQuadrant());
            Enemy_LV2a.SetActive(true);
            Enemy_LV2b.SetActive(true);
            Enemy_LV3.SetActive(true);
            control2 = false;
            countDefeated++;
        }

        if (defeatedLV2_LV3)
        {
            SpawnNanoBots(WhichQuadrant());
            defeatedLV2_LV3 = false;
            countDefeated++;
        }

        if (countDefeated == 4)
        {
            allDefeated = true;
        }
    }

    private int WhichQuadrant()
    {
        if (Shooter.transform.position.x > 0 && Shooter.transform.position.z > 0)
        {
            return 1;
        } else if (Shooter.transform.position.x > 0 && Shooter.transform.position.z < 0)
        {
            return 2;
        } else if (Shooter.transform.position.x < 0 && Shooter.transform.position.z < 0)
        {
            return 3;
        } else if (Shooter.transform.position.x < 0 && Shooter.transform.position.z > 0)
        {
            return 4;
        }
        return 0;
    }

    private void SpawnNanoBots(int quadrant)
    {

        List<Vector3> spawn1 = new List<Vector3> {
            new Vector3(143.410004F,-5.72100019F,159.240005F),
            new Vector3(128.179993F,-5.72100019F,98.8700027F),
            new Vector3(170.800003F,-5.72100019F,69.9000015F),
            new Vector3(60.7000008F,-5.72100019F,37.2000008F) };
        List<Vector3> spawn2 = new List<Vector3> {
            new Vector3(180.699997F,-5.72100019F,-38F),
            new Vector3(90.0100021F,-5.72100019F,-139.350006F),
            new Vector3(192.160004F,-5.72100019F,-125.080002F),
            new Vector3(191.940002F,-5.72100019F,-163.880005F) };
        List<Vector3> spawn3 = new List<Vector3> {
            new Vector3(-37.0999985F,-5.72100019F,-125.400002F),
            new Vector3(-48.0999985F,-5.72100019F,-36.9000015F),
            new Vector3(-163.190002F,-5.72100019F,-151.970001F),
            new Vector3(-193.699997F,-5.72100019F,-72.2699966F) };
        List<Vector3> spawn4 = new List<Vector3> {
            new Vector3(-72.7600021F,-5.72100019F,43.7799988F),
            new Vector3(-116.739998F,-5.72100019F,191.919998F),
            new Vector3(-173.929993F,-5.72100019F,181.779999F),
            new Vector3(-130.380005F,-5.72100019F,45.3699989F) };

        if (this.gameObject.transform.Find("NanoBotContainer").transform.childCount > 12 || quadrant == 0) return;
        int toSpawn = 12 - this.gameObject.transform.Find("NanoBotContainer").transform.childCount;
        int spawned = 0;

        Debug.Log("Quadrante Shooter: " + quadrant);

        for (int i = 0; i < 12; i++)
        {
            if (quadrant != 1 && spawned < toSpawn)
            {
                int s = rnd.Next(spawn1.Count);
                GameObject nanobot = (GameObject)Instantiate(NanoBot, spawn1[s], Quaternion.identity);
                nanobot.transform.parent = this.gameObject.transform.Find("NanoBotContainer").transform;
                spawn1.RemoveAt(s);
                nanobot.SetActive(true);
                spawned++;
                Debug.Log("NanoBot spawnato da 1");
            }

            if (quadrant != 2 && spawned < toSpawn)
            {
                int s = rnd.Next(spawn2.Count);
                GameObject nanobot = (GameObject)Instantiate(NanoBot, spawn2[s], Quaternion.identity);
                nanobot.transform.parent = this.gameObject.transform.Find("NanoBotContainer").transform;
                spawn2.RemoveAt(s);
                nanobot.SetActive(true);
                spawned++;
                Debug.Log("NanoBot spawnato da 2");
            }

            if (quadrant != 3 && spawned < toSpawn)
            {
                int s = rnd.Next(spawn3.Count);
                GameObject nanobot = (GameObject)Instantiate(NanoBot, spawn3[s], Quaternion.identity);
                nanobot.transform.parent = this.gameObject.transform.Find("NanoBotContainer").transform;
                spawn3.RemoveAt(s);
                nanobot.SetActive(true);
                spawned++;
                Debug.Log("NanoBot spawnato da 3");
            }

            if (quadrant != 4 && spawned < toSpawn)
            {
                int s = rnd.Next(spawn4.Count);
                GameObject nanobot = (GameObject)Instantiate(NanoBot, spawn4[s], Quaternion.identity);
                nanobot.transform.parent = this.gameObject.transform.Find("NanoBotContainer").transform;
                spawn4.RemoveAt(s);
                nanobot.SetActive(true);
                spawned++;
                Debug.Log("NanoBot spawnato da 4");
            }
        }
    }
}