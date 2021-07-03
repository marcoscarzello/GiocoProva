using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class Weapons_Generator : MonoBehaviour
{

    public int NumeroArmiPerColore = 3;

    public GameObject WeaponHolder;

    public GameObject[] Prefabs = new GameObject[3];

    public Material[] Colors = new Material[3];

    public DataTable DataBaseArmi = new DataTable("DataBaseArmi");

    public Random rnd = new Random();

    private static float altezzaArmi = -5.66F;
    private List<Vector3> notrWeapons = new List<Vector3> {
        new Vector3(-88.0699997F,altezzaArmi,-94.2600021F),
        new Vector3(35.5999985F,altezzaArmi,-140.889999F),
        new Vector3(157.710007F,altezzaArmi,-107.699997F),
        new Vector3(195.100006F,altezzaArmi,-31.5F),
        new Vector3(-17.1100006F,altezzaArmi,83.1299973F),
        new Vector3(-148.160004F,altezzaArmi,9.36999989F),
    };

    private List<Vector3> rWeapons = new List<Vector3> {
        new Vector3(-103.919998F,altezzaArmi,186.449997F),
        new Vector3(-195.889999F,altezzaArmi,45.8400002F),
        new Vector3(-195.139999F,altezzaArmi,-147.300003F),
        new Vector3(-65.8499985F,altezzaArmi,-162.899994F),
        new Vector3(112.410004F,altezzaArmi,-134.429993F),
        new Vector3(141.259995F,altezzaArmi,164.699997F),
    };

    public List<Vector3> spawnedPosition = new List<Vector3>();

    void Start()
    {
        DataBaseArmi.Clear();
        DataBaseArmi.Columns.Add("Code", typeof(string)); //numero incrementale
        DataBaseArmi.Columns.Add("Type", typeof(int)); //0 pistola, 1 pompa, 2 assalto
        DataBaseArmi.Columns.Add("Color", typeof(int)); //0 rosso, 1 verde, 2 blu
        DataBaseArmi.PrimaryKey = new DataColumn[] { DataBaseArmi.Columns["Code"] };

        int code = 0;

        for (int i = 0; i < NumeroArmiPerColore; i++)
        {
            int tipo = rnd.Next(3);
            DataBaseArmi.Rows.Add(code, tipo, 0);
            code++;
        }

        for (int i = 0; i < NumeroArmiPerColore; i++)
        {
            int tipo = rnd.Next(3);
            DataBaseArmi.Rows.Add(code, tipo, 1);
            code++;
        }

        for (int i = 0; i < NumeroArmiPerColore; i++)
        {
            int tipo = rnd.Next(3);
            DataBaseArmi.Rows.Add(code, tipo, 2);
            code++;
        }

        GenerateStartPistol();

        GameObject[] weapons = new GameObject[NumeroArmiPerColore*3];

        for (int i = 0; i < (NumeroArmiPerColore*3); i++)
        {
            int tipo = Convert.ToInt32(DataBaseArmi.Rows[i][1]);
            Vector3 positionWeapon = new Vector3();

            if (i == 0 || i == NumeroArmiPerColore || i == NumeroArmiPerColore * 2)
            {
                //armi raggiungibili da subito
                int pW = rnd.Next(rWeapons.Count);
                positionWeapon = rWeapons[pW];
                spawnedPosition.Add(positionWeapon);
                rWeapons.RemoveAt(pW);
            } else {
                //armi NON raggiungibili da subito
                int pW = rnd.Next(notrWeapons.Count);
                positionWeapon = notrWeapons[pW];
                spawnedPosition.Add(positionWeapon);
                notrWeapons.RemoveAt(pW);
            }

            GameObject weapon = (GameObject)Instantiate(Prefabs[tipo], positionWeapon, Quaternion.identity);  
            weapon.transform.parent = this.gameObject.transform.Find("WeaponsContainer").transform;

            int materialindex = 1;
            if (tipo == 2) //se assalto
            {
                materialindex = 0;
            }
            var rend = weapon.transform.GetChild(0).gameObject.GetComponent<Renderer>();
            var materials = rend.materials;
            materials[materialindex] = Colors[Convert.ToInt32(DataBaseArmi.Rows[i][2])];
            rend.materials = materials;

            switch (DataBaseArmi.Rows[i][2])
            {
                case 0:
                    weapon.tag = "ArmaRossa";
                    break;
                case 1:
                    weapon.tag = "ArmaVerde";
                    break;
                case 2:
                    weapon.tag = "ArmaBlu";
                    break;
                default:
                    Debug.Log("Problema generazione Tag Armi - Switch in default");
                    break;
            }

            weapons[i] = weapon;
        }

//Debug
        for (int i = 0; i < DataBaseArmi.Rows.Count; i++)
        {
            Debug.Log(DataBaseArmi.Rows[i][0] + " - " + DataBaseArmi.Rows[i][1] + " - " + DataBaseArmi.Rows[i][2]);
        }
        Debug.Log(spawnedPosition[0]);
    }

    private void GenerateStartPistol()
    {
        int coloreArmaIniziale = rnd.Next(3);
        GameObject startpistol = (GameObject)Instantiate(Prefabs[0], new Vector3(0, 0, 0), Quaternion.identity);
        startpistol.transform.parent = WeaponHolder.transform;
        startpistol.transform.localPosition = new Vector3(0.685840011F, -0.750779986F, 1.26320004F);
        startpistol.transform.localRotation = new Quaternion(-0.5F, 0.5F, 0.5F, 0.5F);
        startpistol.transform.localScale = new Vector3(6, 6, 6);
        startpistol.GetComponent<BoxCollider>().enabled = false;
        var rendstartpistol = startpistol.transform.GetChild(0).gameObject.GetComponent<Renderer>();
        var materialsstartpistol = rendstartpistol.materials;
        materialsstartpistol[1] = Colors[coloreArmaIniziale];
        rendstartpistol.materials = materialsstartpistol;
        switch (coloreArmaIniziale)
        {
            case 0:
                startpistol.tag = "ArmaRossa";
                break;
            case 1:
                startpistol.tag = "ArmaVerde";
                break;
            case 2:
                startpistol.tag = "ArmaBlu";
                break;
            default:
                Debug.Log("Problema generazione Tag Start Pistol - Switch in default");
                break;
        }
    }

    public List<Vector3> WeaponPositions()
    {
        return spawnedPosition;
    }
}