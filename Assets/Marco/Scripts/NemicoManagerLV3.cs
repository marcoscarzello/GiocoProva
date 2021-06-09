using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;


public class NemicoManagerLV3 : MonoBehaviour
{
    public float vitaModuloSigla;
    public float vitaModuloLinea;
    public float vitaModuloFaccia;

    private bool primoSchermoDistrutto;
    private bool secondoSchermoDistrutto;
    private string qualeIlPrimoDistrutto; //contiene il nome del primo schermo distrutto 1, 2, 3

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;


    void Start()
    {
        vitaModuloSigla = 40f;
        vitaModuloLinea = 40f;
        vitaModuloFaccia = 40f;

        primoSchermoDistrutto = false;
        secondoSchermoDistrutto = false;
        qualeIlPrimoDistrutto = "0";

        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = gameObject.GetComponent<LV3_Enemy_Generator>().GetEnemyCode();

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 3 " + soluzione);

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
            case "Schermo3":
                schermoColpito = "3";
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
                else if (schermoColpito == "2")
                {
                    if (vitaModuloLinea > 0)
                        vitaModuloLinea -= damage;
                }
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }

                if (vitaModuloLinea <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "1";
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);
                }
                if (vitaModuloSigla <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "2";
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);

                }
                if (vitaModuloFaccia <= 0)
                {
                    primoSchermoDistrutto = true;
                    qualeIlPrimoDistrutto = "3";
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);

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
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }


                if (vitaModuloLinea <= 0 && qualeIlPrimoDistrutto != "1")
                {
                    secondoSchermoDistrutto = true;
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);
                }
                if (vitaModuloSigla <= 0 && qualeIlPrimoDistrutto != "2")
                {
                    secondoSchermoDistrutto = true;
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);

                }
                if (vitaModuloFaccia <= 0 && qualeIlPrimoDistrutto != "3")
                {
                    secondoSchermoDistrutto = true;
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito);

                }
            }
            else
            {
                //penalità per lo shooter
            }
        }


        if (soluzione.Substring(5, 1) == schermoColpito)
        {
            if (tagArma == soluzione.Substring(4, 1) && primoSchermoDistrutto && secondoSchermoDistrutto)
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
                else if (schermoColpito == "3")
                {
                    if (vitaModuloFaccia > 0)
                        vitaModuloFaccia -= damage;
                }

                if (vitaModuloLinea <= 0 && vitaModuloSigla <= 0 && vitaModuloFaccia <= 0)
                {
                    gameObject.GetComponent<LV3_Enemy_Generator>().DistruttoModulo(schermoColpito); 
                }
            }
            else
            {
                //penalità per lo shooter
            }
        }


        //muore
        if (vitaModuloSigla <= 0f && vitaModuloLinea <= 0f && vitaModuloFaccia <= 0f)
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
