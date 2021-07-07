using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Data;
using Random = System.Random;
using System;

public class LV3_Enemy_Generator : MonoBehaviour
{

    public GameObject DBScriptStarter;
    public Material[] M1 = new Material[8];
    public Material[] M2 = new Material[6];
    public Material[] M3 = new Material[4];
    public Material[] MColors = new Material[3];

    static string EnemyCode;

    void OnEnable()
    {
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;
        var rnd = DBScriptStarter.GetComponent<DB_Generator>().rnd;

        DataRow[] result = DataBase.Select("Level = 3");

        int dbnumber = rnd.Next(result.Length);
        EnemyCode = Convert.ToString(result[dbnumber][1]);

        char[] moduli = EnemyCode.ToCharArray();
        int enemycolor = Convert.ToInt32(Char.GetNumericValue(moduli[0])) - 1;
        int materialnumber1 = DecodeM1(Convert.ToString(moduli[1]));
        int materialnumber2 = DecodeM2(Convert.ToString(moduli[2]));
        int materialnumber3 = DecodeM3(Convert.ToString(moduli[3]));

        var rend1 = this.gameObject.transform.Find("SchermoSinistro").gameObject.GetComponent<Renderer>();
        var materials1 = rend1.materials;
        materials1[1] = M1[materialnumber1];
        rend1.materials = materials1;

        var rend5 = this.gameObject.transform.Find("SchermoDestra").gameObject.GetComponent<Renderer>();
        var materials5 = rend5.materials;
        materials5[1] = M2[materialnumber2];
        rend5.materials = materials5;
        
        var rend6 = this.gameObject.transform.Find("Corpo").gameObject.transform.Find("SchermoCentrale").gameObject.GetComponent<Renderer>();
        var materials6 = rend6.materials;
        materials6[1] = M3[materialnumber3];
        rend6.materials = materials6;

        var rend2 = this.gameObject.transform.Find("Corpo").gameObject.GetComponent<Renderer>();
        var materials2 = rend2.materials;
        materials2[1] = MColors[enemycolor];
        materials2[2] = MColors[enemycolor];
        rend2.materials = materials2;

        var rend3 = this.gameObject.transform.Find("SchermoSinistro").gameObject.GetComponent<Renderer>();
        var materials3 = rend3.materials;
        materials3[0] = MColors[enemycolor];
        rend3.materials = materials3;

        var rend4 = this.gameObject.transform.Find("SchermoDestra").gameObject.GetComponent<Renderer>();
        var materials4 = rend4.materials;
        materials4[0] = MColors[enemycolor];
        rend4.materials = materials4;

        var rend7 = this.gameObject.transform.Find("Corpo").gameObject.transform.Find("SchermoCentrale").gameObject.GetComponent<Renderer>();
        var materials7 = rend6.materials;
        materials6[2] = MColors[enemycolor];
        rend6.materials = materials6;

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

    public int DecodeM3(string letter)
    {
        switch (letter)
        {
            case "q":
                return 0;
            case "r":
                return 1;
            case "s":
                return 2;
            case "t":
                return 3;
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
