using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class Codes_generator : MonoBehaviour
{

    public int NumeroNemiciLV1 = 5;
    public int NumeroNemiciLV2 = 3;
    public int NumeroNemiciLV3 = 2;

    void Start()
    {
        string enemycode;
        string enemyDB;

        Random rnd = new Random();

        for ( int i = 0; i < NumeroNemiciLV1; i++ )
        {
            enemycode = EnemyCodeGenerator(rnd, 1);
            enemyDB = EnemyDBGenerator(rnd, 1);
            Debug.Log("LV.1 -> enemycode: " + enemycode + ", enemyDB: " + enemyDB);
        }

        for (int i = 0; i < NumeroNemiciLV2; i++)
        {
            enemycode = EnemyCodeGenerator(rnd, 2);
            enemyDB = EnemyDBGenerator(rnd, 2);
            Debug.Log("LV.2 -> enemycode: " + enemycode + ", enemyDB: " + enemyDB);
        }

        for (int i = 0; i < NumeroNemiciLV3; i++)
        {
            enemycode = EnemyCodeGenerator(rnd, 3);
            enemyDB = EnemyDBGenerator(rnd, 3);
            Debug.Log("LV.3 -> enemycode: " + enemycode + ", enemyDB: " + enemyDB);
        }
    }

    String EnemyCodeGenerator(Random rnd, int livello)
    {
        string[] modulo1 = { "a", "b", "c", "d", "e", "f", "g", "h" };
        string[] modulo2 = { "i", "l", "m", "n", "o", "p" };
        string[] modulo3 = { "q", "r", "s", "t" };
        string[] colori = { "1", "2", "3" }; //1 Ciano, 2 Magenta, 3 Giallo

        string enemycode = "";

        switch (livello)
        {
            case 1:
                int c1 = rnd.Next(3);
                int l1m1 = rnd.Next(8);
                enemycode = colori[c1] + modulo1[l1m1];
                break;
            case 2:
                int c2 = rnd.Next(3);
                int l2m1 = rnd.Next(8);
                int l2m2 = rnd.Next(6);
                enemycode = colori[c2] + modulo1[l2m1] + modulo2[l2m2];
                break;
            case 3:
                int c3 = rnd.Next(3);
                int l3m1 = rnd.Next(8);
                int l3m2 = rnd.Next(6);
                int l3m3 = rnd.Next(4);
                enemycode = colori[c3] + modulo1[l3m1] + modulo2[l3m2] + modulo3[l3m3];
                break;
            default:
                Debug.Log("Problema generazione codici nemici - Switch in default");
                break;
        }

        return enemycode;
    }

    String EnemyDBGenerator(Random rnd, int livello)
    {
        string[] armi = { "u", "v", "z" }; //u Rosso, v Verde, z Blu

        string enemyDB = "";

        switch (livello)
        {
            case 1:
                int c1 = rnd.Next(3);
                enemyDB = armi[c1] + "1";
                break;
            case 2:
                var moduli2 = new List<string>() { "1", "2" };
                int c2mA = rnd.Next(3);
                int c2mB = rnd.Next(3);
                int n2mA = rnd.Next(2);
                string m2A = moduli2[n2mA];
                moduli2.RemoveAt(n2mA);
                enemyDB = armi[c2mA] + m2A + armi[c2mB] + moduli2[0];
                break;
            case 3:
                var moduli3 = new List<string>() { "1", "2", "3" };
                int c3mA = rnd.Next(3);
                int c3mB = rnd.Next(3);
                int c3mC = rnd.Next(3);
                int n3mA = rnd.Next(3);
                string m3A = moduli3[n3mA];
                moduli3.RemoveAt(n3mA);
                int n3mB = rnd.Next(2);
                string m3B = moduli3[n3mB];
                moduli3.RemoveAt(n3mB);
                enemyDB = armi[c3mA] + m3A + armi[c3mB] + m3B + armi[c3mC] + moduli3[0];
                break;
            default:
                Debug.Log("Problema generazione codici DB - Switch in default");
                break;
        }

        return enemyDB;
    }
}