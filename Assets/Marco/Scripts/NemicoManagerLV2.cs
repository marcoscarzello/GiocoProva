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

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;


    void Start()
    {
        vitaModuloSigla = 40f;
        vitaModuloLinea = 40f;

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
        
        //manca ancora il controllo dell'ordine di esecuzione

        if (soluzione.Substring(1, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(0, 1))
            {
                if (schermoColpito == "1") vitaModuloSigla -= damage;
                else vitaModuloLinea -= damage;

            }
            else
            {
                //penalità per lo shooter
            }
        }

        if (soluzione.Substring(3, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(2, 1))
            {
                if (schermoColpito == "1") vitaModuloSigla -= damage;
                else vitaModuloLinea -= damage;
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


    void Die()
    {
        Destroy(gameObject);
    }

    void Update()
    {

    }
}
