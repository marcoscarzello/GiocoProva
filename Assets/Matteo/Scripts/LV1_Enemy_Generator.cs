using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Random = System.Random;
using System;

public class LV1_Enemy_Generator : MonoBehaviour
{

    public GameObject DBScriptStarter;
    public Material[] M1 = new Material[8];
    public Material[] MColors = new Material[3];
    
    static string EnemyCode;

    void OnEnable()
    {
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;
        var rnd = DBScriptStarter.GetComponent<DB_Generator>().rnd;

        DataRow[] result = DataBase.Select("Level = 1");

        int dbnumber = rnd.Next(result.Length);
        EnemyCode = Convert.ToString(result[dbnumber][1]);

        char[] moduli = EnemyCode.ToCharArray();
        int enemycolor = Convert.ToInt32(Char.GetNumericValue(moduli[0])) - 1;
        int materialnumber = DecodeM1(Convert.ToString(moduli[1]));

        var rend1 = this.gameObject.transform.Find("Schermo").gameObject.GetComponent<Renderer>();
        var materials1 = rend1.materials;
        materials1[1] = M1[materialnumber];
        rend1.materials = materials1;

        var rend2 = this.gameObject.transform.Find("Corpo").gameObject.GetComponent<Renderer>();
        var materials2 = rend2.materials;
        materials2[2] = MColors[enemycolor];
        rend2.materials = materials2;

        var rend3 = this.gameObject.transform.Find("PistolaDestra").gameObject.GetComponent<Renderer>();
        var materials3 = rend3.materials;
        materials3[1] = MColors[enemycolor];
        rend3.materials = materials3;

        var rend4 = this.gameObject.transform.Find("PistolaSinistra").gameObject.GetComponent<Renderer>();
        var materials4 = rend4.materials;
        materials4[1] = MColors[enemycolor];
        rend4.materials = materials4;

        var rend5 = this.gameObject.transform.Find("Schermo").gameObject.GetComponent<Renderer>();
        var materials5 = rend1.materials;
        materials1[2] = MColors[enemycolor];
        rend1.materials = materials1;

        //Debug
        //Debug.Log("Spawnato EnemyCode LV.1 - " + EnemyCode);
    }

    int DecodeM1(string letter)
    {
        switch (letter)
        {
            case "a":
                return 0;
            case "b":
                return 1;
            case "c":
                return 2;
            case "d":
                return 3;
            case "e":
                return 4;
            case "f":
                return 5;
            case "g":
                return 6;
            case "h":
                return 7;
            default:
                Debug.Log("Problema generazione Nemico - Switch in default");
                return 0;
        }
    }

    public String GetEnemyCode()
    {
        return EnemyCode;
    }
}