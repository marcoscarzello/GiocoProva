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
            GameObject weapon = (GameObject)Instantiate(Prefabs[tipo], new Vector3(rnd.Next(10, 46), 1.3F, rnd.Next(-18, 18)), Quaternion.identity);

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