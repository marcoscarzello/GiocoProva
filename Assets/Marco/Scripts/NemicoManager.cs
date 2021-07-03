using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Questo script gestisce la vita del nemico e la sua distruzione se si spara nel punto giusto

public class NemicoManager : MonoBehaviour
{
    public float vitaModuloSigla;

    public Material[] M1Distrutto = new Material[8];

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;


    void Start()
    {
        vitaModuloSigla = 40f;

        //prendo database
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = transform.parent.gameObject.GetComponent<LV1_Enemy_Generator>().GetEnemyCode();
        //Debug.Log(enemycode);

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 1 : "  + soluzione);

    }

    public void Colpito(float damage, string tagArma)   //riceve danno e tag con colore arma
    {
        //mappo tag arma -> lettera soluzione
        switch (tagArma) {
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

        //subisce danno se l'arma è giusta rispetto alla soluzione
        if (tagArma == soluzione.Substring(0, 1))
        {
            vitaModuloSigla -= damage;
        }
        else
        {
            //penalità per lo shooter
        }

        //muore il modulo se arriva a zero
        if (vitaModuloSigla <= 0f)
        {
            transform.parent.gameObject.GetComponent<LV1_Enemy_Generator>().Distrutto();

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
