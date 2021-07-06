using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VitaEnergia : MonoBehaviour
{
    public float salute;
    public float energia;
    public float _perHitLossBulletBlu;
    public GameObject weaponHolder;


    public float timer;
    public float waitingTime;

    void Start()
    {
        salute = 100f;
        energia = 50f;
        _perHitLossBulletBlu = 5f;


        waitingTime = 2f;
        timer = 0f;
    }

    void Update()
    {
        //aumento periodico energia
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            if (energia <=99)
            energia += 1;

            timer = 0f;
        }



        //Debug.Log("Health: " + salute);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.gameObject.GetComponent<Bullet>() != null)
        {
            salute -= _perHitLossBulletBlu;//gameObject.GetComponent<Bullet>()._perHitLoss;
            //Debug.Log("Health: " + salute);
        }
            

    }

    void OnTriggerEnter(Collider collisionAmmo)
    {
        Debug.Log("Collisione con ammo");
        if (collisionAmmo.gameObject.GetComponent<Munizioni>() != null)
        {
            weaponHolder.GetComponent<MunizioniManager>().raccoltaMunizioni();
            Destroy(collisionAmmo.gameObject);
        }
    }

    public void Curato() {

        salute = 100f;
        energia -= 30f;
        Debug.Log("Sono il client. Salute ricaricata! Grazie");
    }

    public void Potenza()
    {
        Debug.Log("energia tolta per power up potenza");
        //questo toglie solo l'energia. Il power up scatta in munizionimanager
        energia -= 60f;
    }
    public void Munizze()
    {
        Debug.Log("energia tolta per power up munizze");
        //questo toglie solo l'energia. Il power up scatta in munizionimanager
        energia = 0f;
    }
}
