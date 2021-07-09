using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletLv1 : MonoBehaviour
{

    void OnTriggerEnter(Collider collision)
    {


        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Player")
        {
            Debug.Log("DISTRUTTA PALLOTTOLA");
            Destroy(gameObject);
        }

    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }
}

