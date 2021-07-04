using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Questo script gestisce la vita del nemico e la sua distruzione se si spara nel punto giusto

public class NemicoManager : MonoBehaviour
{
    public GameObject Robot_LV1;

    public float vitaModuloSigla;

    public Material[] M1Distrutto = new Material[8];

    private string soluzione;
    private string enemycode;

    public GameObject DBScriptStarter;
    [SerializeField] private Munizioni ammo;
    [SerializeField] private Particle ExplosionParticle;

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
        Debug.Log("Soluzione nemico livello 1 "  + soluzione);

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

        //muore se arriva a zero
        if (vitaModuloSigla <= 0f)
        {
            //AAA muore
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
        Destroy(Robot_LV1);
    }
}
