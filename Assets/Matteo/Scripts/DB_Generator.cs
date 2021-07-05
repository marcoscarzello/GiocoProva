using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using Random = System.Random;

public class DB_Generator : MonoBehaviour
{

    public int DatabaseNemiciLv1 = 20;
    public int DatabaseNemiciLv2 = 15;
    public int DatabaseNemiciLv3 = 10;

    public string lastSolution;

    public DataTable DataBase = new DataTable("DataBase");

    public Random rnd = new Random();

    void Start()
    {
        string enemycode;
        string enemyDB;

        DataBase.Clear();
        DataBase.Columns.Add("Level", typeof(int));
        DataBase.Columns.Add("EnemyCode", typeof(string));
        DataBase.Columns.Add("EnemyDB", typeof(string));
        DataBase.PrimaryKey = new DataColumn[] { DataBase.Columns["EnemyCode"] };

        // Info per cercare nella DataTable
        // Per trovare nemici da codice: DataBase.Rows.Find(enemycode)[index] dove index = 0, 1, 2
        // Per numero di righe: DataBase.Rows.Count
        // Per accedere ai campi DataBase.Rows[i][j]

        for ( int i = 0; i < DatabaseNemiciLv1; i++ )
        {
            enemycode = EnemyCodeGenerator(rnd, 1);
            enemyDB = EnemyDBGenerator(rnd, 1);
            if (DataBase.Rows.Find(enemycode) == null)
            {
                DataBase.Rows.Add(1, enemycode, enemyDB);
            }
        }

        for (int i = 0; i < DatabaseNemiciLv2; i++)
        {
            enemycode = EnemyCodeGenerator(rnd, 2);
            enemyDB = EnemyDBGenerator(rnd, 2);
            if (DataBase.Rows.Find(enemycode) == null)
            {
                DataBase.Rows.Add(2, enemycode, enemyDB);
            }
        }

        for (int i = 0; i < DatabaseNemiciLv3; i++)
        {
            enemycode = EnemyCodeGenerator(rnd, 3);
            enemyDB = EnemyDBGenerator(rnd, 3);
            if (DataBase.Rows.Find(enemycode) == null)
            {
                DataBase.Rows.Add(3, enemycode, enemyDB);
            }
        }

//Debug
        for (int i =0; i< DataBase.Rows.Count; i++)
        {
            Debug.Log(DataBase.Rows[i][0] + " - " + DataBase.Rows[i][1] + " - " + DataBase.Rows[i][2]);
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

    public void CercaSoluzione(string nemCode)
    {
        //if (!DataBase.Rows.Find(EnemyFinder.codiceNemico).IsNull(2))

        //{
        //if (DataBase.Rows.Find(EnemyFinder.codiceNemico)[2] != null)
        //EnemyFinder.codiceSoluzione = Convert.ToString(DataBase.Rows.Find(EnemyFinder.codiceNemico)[2]);
        //else
        //EnemyFinder.codiceSoluzione = "NoMatch";
        //}
        //else
        // EnemyFinder.codiceSoluzione = "NoMatch";





        //soluzione "all in one" deprecata:

        //EnemyFinder.codiceSoluzione = "No Match";
        //for (int a = 0; a < DataBase.Rows.Count; a++)
        //{

        //  if (Convert.ToString(DataBase.Rows[a][1]) == EnemyFinder.codiceNemico)
        //{
        //  EnemyFinder.codiceSoluzione = Convert.ToString(DataBase.Rows[a][2]);
        //}
        //}

        Debug.Log("Sono il DB_Generator: ricerco nuova soluzione per " + nemCode);

        lastSolution = "No Match";
        for (int a = 0; a < DataBase.Rows.Count; a++)
        {

          if (Convert.ToString(DataBase.Rows[a][1]) == nemCode)
             {
                lastSolution = Convert.ToString(DataBase.Rows[a][2]);
             }
        }

        




    }
}