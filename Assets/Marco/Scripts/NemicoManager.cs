using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

//Questo script gestisce la vita del nemico e la sua distruzione se si spara nel punto giusto

public class NemicoManager : MonoBehaviour
{
    private AudioSource[] audios;


    public GameObject Robot_LV1;
    public float vitaModuloSigla;

    private string soluzione;
    private string enemycode;

    private float energiaVal;
    private bool attivati;
    private int count = 0;

    public GameObject DBScriptStarter;
    [SerializeField] private Munizioni ammo;
    [SerializeField] private Particle ExplosionParticle;
    [SerializeField] private Particle AT_Field;
    [SerializeField] private VitaEnergia aggiungi;
    [SerializeField] private GameObject spawn_Point;
 
    void Start()
    {
        audios = GetComponents<AudioSource>();


        vitaModuloSigla = 40f;
        attivati = false;
        //prendo database
        var DataBase = DBScriptStarter.GetComponent<DB_Generator>().DataBase;

        //prendo codice nemico
        enemycode = transform.parent.gameObject.GetComponent<LV1_Enemy_Generator>().GetEnemyCode();
        //Debug.Log(enemycode);

        //prendo soluzione
        soluzione = Convert.ToString(DataBase.Rows.Find(enemycode)[2]);
        Debug.Log("Soluzione nemico livello 1 "  + soluzione);

    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.M))
        {
            Die();
        }

      
    }

    private IEnumerator disattiva()
    {
       
        if (attivati == true && count == 1)
        {
            yield return new WaitForSeconds(4);
            attivati = false;
            AT_Field.gameObject.SetActive(false);
            count = 0;
        }

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
        if (tagArma == soluzione.Substring(0, 1) && attivati == false)
        {
            vitaModuloSigla -= damage;
        }
        else
        {
            Debug.Log("PENALITA'");
            AT_Field.gameObject.SetActive(true);
            attivati = true;
            count++;
            StartCoroutine(disattiva());
            //Instantiate(AT_Field, AT_Field.transform.position, Quaternion.identity);
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
        if (rnd <= 99)
        {
            Debug.Log("Spawna");
            Instantiate(ammo, spawn_Point.transform.position/*new Vector3(spawnPos.position.x, -1.3f , spawnPos.position.y )*/, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
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
        audios[0].Play();

        updateEnergy();
        Debug.Log("ENERGIA: " + aggiungi.energia);
        DBScriptStarter.GetComponent<Enemy_Spawner>().defeatedLV1 = true;
        SpawnAmmo();
        Vector3 temp = transform.position;
        temp.y = 0.0f;
        transform.position = temp;
        Instantiate(ExplosionParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
        Destroy(Robot_LV1);
    }
}
