using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Bullet : MonoBehaviour
{
    [SerializeField] private int _destroyIntoPieces = 1;
    [SerializeField] private float _explosionForce = 20f;
    [SerializeField] private float _explosionRadius = 1f;
    [SerializeField] public float _perHitLoss = 10f;

    public bool IsBulletPiece = false;

    void OnTriggerEnter(Collider collision)
    {
       

        if(collision.gameObject.tag == "Wall" || collision.gameObject.tag== "Player")
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
