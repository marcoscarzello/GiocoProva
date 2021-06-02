using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class MovimentoSferetta : MonoBehaviour
{
    //Variables
    public float speed = 0.02f;

    public float xpos;
    public float zpos;

    void Start()            //non sarà la start ma la startMinigameX
    {
        Random rnd = new Random();
        xpos = rnd.Next(40);
        xpos /= 10f;
        zpos = rnd.Next(40);
        zpos /= 10f;

        Debug.Log(zpos + ", " + xpos);

        gameObject.transform.position = new Vector3(transform.position.x + xpos, transform.position.y, transform.position.z + zpos);
    }

    void Update()
    {
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        gameObject.transform.position = new Vector3(transform.position.x - (h * speed), transform.position.y,
           transform.position.z - (v * speed));
    }
}
