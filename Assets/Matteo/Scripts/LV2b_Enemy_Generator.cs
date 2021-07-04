using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Random = System.Random;
using System;

public class LV2b_Enemy_Generator : MonoBehaviour
{

    public GameObject DBScriptStarter;
    public Material[] M1 = new Material[8];
    public Material[] M2 = new Material[6];
    public Material[] MColors = new Material[3];

    static string EnemyCode;

    void OnEnable()
    {
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;
        var rnd = DBScriptStarter.GetComponent<DB_Generator>().rnd;

        DataRow[] result = DataBase.Select("Level = 2");

        int dbnumber = rnd.Next(result.Length);
        EnemyCode = Convert.ToString(result[dbnumber][1]);

        char[] moduli = EnemyCode.ToCharArray();
        int enemycolor = Convert.ToInt32(Char.GetNumericValue(moduli[0])) - 1;
        int materialnumber1 = DecodeM1(Convert.ToString(moduli[1]));
        int materialnumber2 = DecodeM2(Convert.ToString(moduli[2]));

        var rend1 = this.gameObject.transform.Find("BraccioSinistro").gameObject.GetComponent<Renderer>();
        var materials1 = rend1.materials;
        materials1[2] = M1[materialnumber1];
        rend1.materials = materials1;

        var rend5 = this.gameObject.transform.Find("BraccioDestro").gameObject.GetComponent<Renderer>();
        var materials5 = rend5.materials;
        materials5[2] = M2[materialnumber2];
        rend5.materials = materials5;

        var rend2 = this.gameObject.transform.Find("Corpo").gameObject.GetComponent<Renderer>();
        var materials2 = rend2.materials;
        materials2[3] = MColors[enemycolor];
        rend2.materials = materials2;

        var rend3 = this.gameObject.transform.Find("BraccioDestro").gameObject.GetComponent<Renderer>();
        var materials3 = rend3.materials;
        materials3[1] = MColors[enemycolor];
        rend3.materials = materials3;

        var rend4 = this.gameObject.transform.Find("BraccioSinistro").gameObject.GetComponent<Renderer>();
        var materials4 = rend4.materials;
        materials4[1] = MColors[enemycolor];
        rend4.materials = materials4;

        //Debug
        //Debug.Log("Spawnato EnemyCode LV.2 - " + EnemyCode);
    }

    public int DecodeM1(string letter)
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

    public int DecodeM2(string letter)
    {
        switch (letter)
        {
            case "i":
                return 0;
            case "l":
                return 1;
            case "m":
                return 2;
            case "n":
                return 3;
            case "o":
                return 4;
            case "p":
                return 5;
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