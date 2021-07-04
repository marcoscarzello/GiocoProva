using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Questo script gestisce la vita del nemico e la sua distruzione se si spara nel punto giusto

public class NemicoManagerLV2 : MonoBehaviour
{
    public float vitaModuloSigla;
    public float vitaModuloLinea;
    private bool primoSchermoDistrutto;

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;
    [SerializeField] private Munizioni ammo;
    [SerializeField] private Particle ExplosionParticle;



    void Start()
    {
        vitaModuloSigla = 40f;
        vitaModuloLinea = 40f;
        primoSchermoDistrutto = false;
        //prendo database
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = gameObject.GetComponent<LV2_Enemy_Generator>().GetEnemyCode();
        //Debug.Log(enemycode);

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 2 " + soluzione);

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


                if (vitaModuloLinea <= 0)
                {
                    primoSchermoDistrutto = true;
                    gameObject.GetComponent<LV2_Enemy_Generator>().DistruttoModulo(schermoColpito);
                }
                if (vitaModuloSigla <= 0)
                {
                    primoSchermoDistrutto = true;
                    gameObject.GetComponent<LV2_Enemy_Generator>().DistruttoModulo(schermoColpito);

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
                    gameObject.GetComponent<LV2_Enemy_Generator>().DistruttoModulo(schermoColpito); //distrutto anche l'altro
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

    void Die()
    {
        SpawnAmmo();
        Vector3 temp = transform.position;
        temp.y = 0.7f;
        transform.position = temp;
        Instantiate(ExplosionParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
        Destroy(gameObject);
    }

    void Update()
    {
      
    }
}
