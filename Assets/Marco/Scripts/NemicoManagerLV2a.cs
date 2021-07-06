using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Questo script gestisce la vita del nemico e la sua distruzione se si spara nel punto giusto

public class NemicoManagerLV2a : MonoBehaviour
{
    public float vitaModuloSigla;
    public float vitaModuloLinea;
    private bool primoSchermoDistrutto;

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;
    [SerializeField] private Munizioni ammo;
    [SerializeField] private Particle ExplosionParticle;

    public Material[] M1Distrutti = new Material[8];
    public Material[] M2Distrutti = new Material[6];
    [SerializeField] private VitaEnergia aggiungi;
    private float energiaVal;
    void Start()
    {
        vitaModuloSigla = 40f;
        vitaModuloLinea = 40f;
        primoSchermoDistrutto = false;
        //prendo database
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = gameObject.GetComponent<LV2a_Enemy_Generator>().GetEnemyCode();
        //Debug.Log("EnemyCode 2a: " + enemycode);

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 2a " + soluzione);

    }

    public void Colpito(float damage, string tagArma, string schermoColpito)   //riceve danno e tag con colore arma e tag schermo 
    {
        //mappo tag arma -> lettera soluzione
        switch (tagArma)
        {
            case "ArmaRossa":
                tagArma = "u";
                break;
            case "ArmaVerde":
                tagArma = "v";
                break;
            case "ArmaBlu":
                tagArma = "z";
                break;
            default:
                break;
        }
        //mappo schermocolpito -> numero soluzione
        switch (schermoColpito)
        {
            case "Schermo1":
                schermoColpito = "1";
                break;
            case "Schermo2":
                schermoColpito = "2";
                break;
            default:
                break;
        }
        

        if (soluzione.Substring(1, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(0, 1))
            {
                if (schermoColpito == "1")
                {
                    if (vitaModuloSigla > 0)
                    vitaModuloSigla -= damage;
                }
                else
                {
                    if (vitaModuloLinea > 0)
                    vitaModuloLinea -= damage;
                }

                if (vitaModuloSigla <= 0)
                {
                    primoSchermoDistrutto = true;
                    //distruggi schermo sigla
                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo1 = gameObject.GetComponent<LV2a_Enemy_Generator>().DecodeM1(Convert.ToString(moduli[1]));
                    var rend1 = this.gameObject.transform.Find("BraccioSinistro").gameObject.GetComponent<Renderer>();
                    var materials1 = rend1.materials;
                    materials1[2] = M1Distrutti[materialmodulo1];
                    rend1.materials = materials1;
                    Debug.Log("LV2 - schermo 1 - distrutto");
                }
                if (vitaModuloLinea <= 0)
                {
                    primoSchermoDistrutto = true;
                    //distruggi schermo linea
                    char[] moduli = enemycode.ToCharArray();
                    int materialmodulo2 = gameObject.GetComponent<LV2a_Enemy_Generator>().DecodeM2(Convert.ToString(moduli[2]));
                    var rend2 = this.gameObject.transform.Find("BraccioDestro").gameObject.GetComponent<Renderer>();
                    var materials1 = rend2.materials;
                    materials1[2] = M2Distrutti[materialmodulo2];
                    rend2.materials = materials1;
                    Debug.Log("LV2 - schermo 1 - distrutto");
                }
            }
            else
            {
                //penalità per lo shooter
            }
        }

        if (soluzione.Substring(3, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(2, 1) && primoSchermoDistrutto)
            {

                if (schermoColpito == "1")
                {
                    if (vitaModuloSigla > 0)
                        vitaModuloSigla -= damage;
                }
                else if (schermoColpito == "2")
                {
                    if (vitaModuloLinea > 0)
                        vitaModuloLinea -= damage;
                }

                if (vitaModuloLinea <= 0 && vitaModuloSigla <= 0)
                {
                    //nemico morto
                }
            }
            else
            {
                //penalità per lo shooter
            }
        }


        //muore
        if (vitaModuloSigla <= 0f && vitaModuloLinea <= 0f)
        {
            Die();
        }

    }
    public void SpawnAmmo()
    {
        int rnd = UnityEngine.Random.Range(0, 99);
        if (rnd <= 45)
        {
            Debug.Log("Spawna");
            Instantiate(ammo, transform.position/*new Vector3(spawnPos.position.x, -1.3f , spawnPos.position.y )*/, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
        else Debug.Log("NonSpawna");
    }

    public void updateEnergy()
    {
        energiaVal = 20;
        if (energiaVal + aggiungi.energia > 100)
        {
            aggiungi.energia = 100;
        }
        else aggiungi.energia = aggiungi.energia + energiaVal;
    }

    void Die()
    {
        updateEnergy();
        Debug.Log("ENERGIA: " + aggiungi.energia);
        DBScriptStarter.GetComponent<Enemy_Spawner>().defeatedLV2_LV3 = true;
        SpawnAmmo();
        Vector3 temp = transform.position;
        temp.y = 0.7f;
        transform.position = temp;
        Instantiate(ExplosionParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
        Destroy(gameObject);
    }
}
