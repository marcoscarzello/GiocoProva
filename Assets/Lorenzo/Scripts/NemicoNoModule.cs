using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NemicoNoModule : MonoBehaviour
{

    public float vitaEnemy;
    [SerializeField] private GameObject ammo;
    [SerializeField] private Transform spawnPos;



    // Start is called before the first frame update
    void Start()
    {
        vitaEnemy = 40f;

    }
    public void Colpito(float damage)
    {
        vitaEnemy -= damage;

        if(vitaEnemy <= 0f)
        {
            Die();
        }

    }

    void Die()
    {
        SpawnAmmo();
        Destroy(gameObject);
    }

    public void SpawnAmmo()
    {
        int rnd = Random.Range(0, 99);
        if (rnd <= 40)
        {
            Debug.Log("Spawna");
            Instantiate(ammo, new Vector3(spawnPos.position.x, 0, spawnPos.position.y ), Quaternion.identity);
        }
        else Debug.Log("NonSpawna");
    }
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Colpito(10f);
        }

    }
}
