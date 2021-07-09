using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;


public class MovimentoSferetta : MonoBehaviour
{
    //Variables
    public float speed;

    public float xpos;
    public float zpos;

    void Start()            //non sarà la start ma la startMinigameX
    {
        spawn();
        xpos = 8f;
        zpos = 8f;
        speed = 3f;
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.G)) spawn();

        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        gameObject.transform.position = new Vector3(transform.position.x - (h * speed * Time.deltaTime), transform.position.y,
           transform.position.z - (v * speed * Time.deltaTime));
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name == "Core")
        {
            Debug.Log("arrivato al core");
            Timer.reached = true;
        }

    }

    public void spawn() {

        speed = 3f;
        Random rnd = new Random();

        while (xpos < 20f)
            xpos = rnd.Next(80);
        xpos /= 10f;
        while (zpos < 20f)
            zpos = rnd.Next(80);
        zpos /= 10f;

        Debug.Log(zpos + ", " + xpos);

        gameObject.transform.localPosition = new Vector3(xpos, 1.35f,  zpos);

    }




}
