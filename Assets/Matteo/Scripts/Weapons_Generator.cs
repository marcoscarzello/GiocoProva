using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class Weapons_Generator : MonoBehaviour
{

    public int NumeroArmiPerColore = 3;

    public GameObject[] Prefabs = new GameObject[3];

    public Material[] Colors = new Material[3];

    public DataTable DataBaseArmi = new DataTable("DataBaseArmi");

    public Random rnd = new Random();

    private static float altezzaArmi = -5.66F;
    private List<Vector3> positionWeapons = new List<Vector3> {
        new Vector3(-103.919998F,altezzaArmi,186.449997F),
        new Vector3(-195.889999F,altezzaArmi,45.8400002F),
        new Vector3(-195.139999F,altezzaArmi,-147.300003F),
        new Vector3(-88.0699997F,altezzaArmi,-94.2600021F),
        new Vector3(-65.8499985F,altezzaArmi,-162.899994F),
        new Vector3(35.5999985F,altezzaArmi,-140.889999F),
        new Vector3(112.410004F,altezzaArmi,-134.429993F),
        new Vector3(157.710007F,altezzaArmi,-107.699997F),
        new Vector3(195.100006F,altezzaArmi,-31.5F),
        new Vector3(141.259995F,altezzaArmi,164.699997F),
        new Vector3(-17.1100006F,altezzaArmi,83.1299973F),
        new Vector3(-148.160004F,altezzaArmi,9.36999989F),
    };

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

        GameObject[] weapons = new GameObject[NumeroArmiPerColore*3];

        for (int i = 0; i < (NumeroArmiPerColore*3); i++)
        {
            int tipo = Convert.ToInt32(DataBaseArmi.Rows[i][1]);
            int pW = rnd.Next(positionWeapons.Count);
            GameObject weapon = (GameObject)Instantiate(Prefabs[tipo], positionWeapons[pW], Quaternion.identity);
            positionWeapons.RemoveAt(pW);
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
    }
}