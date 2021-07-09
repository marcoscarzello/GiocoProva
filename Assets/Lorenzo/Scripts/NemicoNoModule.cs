using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoNoModule : MonoBehaviour
{
    private AudioSource[] audios;


    [SerializeField] private float vitaEnemy;
    [SerializeField] private Munizioni ammo;
    //[SerializeField] private Transform spawnPos;
    [SerializeField] private Particle ExplosionParticle;
    [SerializeField] private VitaEnergia aggiungi;
    [SerializeField] private GameObject spawn_point;
    private float energiaVal;


    // Start is called before the first frame update
    void Start()
    {
        audios = GetComponents<AudioSource>();


        //vitaEnemy = 20f;

        aggiungi = GameObject.Find("Shooter").GetComponent<VitaEnergia>();

    }
    public void Colpito(float damage)
    {
        vitaEnemy -= damage;

        if(vitaEnemy <= 0f)
        {
            Die();
        }

    }

    public void updateEnergy()
    {
        energiaVal = 10;
        if (energiaVal + aggiungi.energia > 100)
        {
            aggiungi.energia = 100;
        }
        else aggiungi.energia = aggiungi.energia + energiaVal;
    }



    public void SpawnAmmo()
    {
        int rnd = Random.Range(0, 99);
        if (rnd <= 45)
        {
            Debug.Log("Spawna");
            Instantiate(ammo, spawn_point.transform.position/*new Vector3(spawnPos.position.x, -1.3f , spawnPos.position.y )*/, Quaternion.Euler(-90.0f, 0.0f, 0.0f));
        }
        else Debug.Log("NonSpawna");
    }
    
    void Die()
    {
        audios[0].Play();


        updateEnergy();
        Debug.Log("ENERGIA: " + aggiungi.energia);

        SpawnAmmo();
        Vector3 temp = transform.position;
        temp.y = -0.7f;
        transform.position = temp;
        Instantiate(ExplosionParticle, transform.position /*new Vector3(spawnPos.position.x, 0.0f, spawnPos.position.y)*/, Quaternion.identity);
        Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {

        //SOLO PER I TEST ANDRA' ELIMINATO

        //
    }
}
