using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class VitaEnergia : MonoBehaviour
{
    public float salute;
    public float energia;
    float _perHitLossBulletBlu;
    float _perHitLossBulletRosso;
    float _perHitLossBulletVerde;
    float _perHitLossBulletGiallo;

    public GameObject weaponHolder;

    public GameObject canvapuntatore;

    public float timer;
    public float waitingTime;

    public int gamestatus; //ricevitore di status game 0 = in game, 1 = vittoria, 2 = sconfitta

    void Start()
    {
        salute = 100f;
        energia = 90f;
        _perHitLossBulletBlu = 5f;
        _perHitLossBulletRosso = 8f;
        _perHitLossBulletVerde = 5f;
        _perHitLossBulletGiallo = 7f;


        waitingTime = 2f;
        timer = 0f;

        gamestatus = 0;

    }

    void Update()
    {

        if (gamestatus == 2)
        {

            Debug.Log("vado alla scena game over");
            SceneManager.LoadScene("GameOver");

        }
        if (gamestatus == 1)
        {

            Debug.Log("vado alla scena game over");
            SceneManager.LoadScene("Victory");

        }


        //aumento periodico energia
        timer += Time.deltaTime;

        if (timer > waitingTime)
        {
            if (energia <= 99)
                energia += 1;

            timer = 0f;
        }



        //Debug.Log("Health: " + salute);
    }


    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.GetComponent<Bullet>()!= null)
        {
            if(collision.tag == "Bullet")
            {
                salute -= _perHitLossBulletBlu;//gameObject.GetComponent<Bullet>()._perHitLoss;
                                               //Debug.Log("Health: " + salute);
                glitch();
            }
            if (collision.tag == "Bullet1")
            {
                salute -= _perHitLossBulletVerde;//gameObject.GetComponent<Bullet>()._perHitLoss;
                Debug.Log("DANNO LV1");                          //Debug.Log("Health: " + salute);
                glitch();
            }
            if (collision.tag == "Bullet2")
            {
                salute -= _perHitLossBulletGiallo;//gameObject.GetComponent<Bullet>()._perHitLoss;
                Debug.Log("DANNO LV2");                          //Debug.Log("Health: " + salute);
                glitch();
            }
            if (collision.tag == "Bullet3")
            {
                salute -= _perHitLossBulletRosso;//gameObject.GetComponent<Bullet>()._perHitLoss;
                Debug.Log("DANNO LV3");                          //Debug.Log("Health: " + salute);
                glitch();
            }
        }

        Debug.Log("Collisione con ammo");
        if (collision.gameObject.GetComponent<Munizioni>() != null)
        {
            weaponHolder.GetComponent<MunizioniManager>().raccoltaMunizioni();
            Destroy(collision.gameObject);
        }
    }

    public void Curato() {

        salute = 100f;
        //dimezzata
        energia -= 15f;
        Debug.Log("Sono il client. Salute ricaricata! Grazie");
    }

    public void Potenza()
    {
        Debug.Log("energia tolta per power up potenza");
        //questo toglie solo l'energia. Il power up scatta in munizionimanager
        //dimezzata
        energia -= 30f;
    }
    public void Munizze()
    {
        Debug.Log("energia tolta per power up munizze");
        //questo toglie solo l'energia. Il power up scatta in munizionimanager
        energia = 0f;
    }

    public void glitch() {

        canvapuntatore.GetComponent<CanvaPuntatoreManager>().subisciDanno();

    }

}
