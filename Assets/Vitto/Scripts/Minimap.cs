using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Animator animator = null;
    private GameObject minimapborder = null;
    private GameObject footprint = null;
    public GameObject lv1 = null;
    public GameObject lv2 = null;
    public GameObject lv3 = null;
    public GameObject z1 = null;
    public GameObject z2 = null;
    public GameObject z3 = null;
    public GameObject z4 = null;
    public GameObject db = null;
    public GameObject gun = null;

    private GameManager gm = null;

    private bool open = false;
    public GameObject pg;

    //public Pause pause;

    private void Start()
    {
        //animator = GetComponent<Animator>();
        gm = FindObjectOfType<GameManager>();
        //m1 = GameObject.Find("marker_hs");
        //m2 = GameObject.Find("marker_gara");
        //m3 = GameObject.Find("marker_mom");
        //volpe = GameObject.Find("Volpe");
    }

    void Update()
    {

        /*if (Input.GetKeyDown(KeyCode.M) && FindObjectOfType<DialogueManager>().ended && !volpe.GetComponent<MovementController>().blocca)
        {
            if (!open)
                keyM();
            else
                keyEscape();
        }

        //if (Input.GetKeyDown(KeyCode.M) && open)
        //{
        //    keyEscape();
        //}*/
    }


    void setTime(int time)
    {
        Time.timeScale = time;
        /*if (time == 0)
            GameObject.Find("volpe").GetComponent<MovementController>().enabled = false;
        else
            GameObject.Find("volpe").GetComponent<MovementController>().enabled = true;*/
    }
}
