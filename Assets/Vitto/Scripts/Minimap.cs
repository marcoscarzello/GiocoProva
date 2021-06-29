using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private int state=0; /*1- db     2- mappa armi e nemico lv1      3- lv2 , lv3      4- lv3     5- timer torna all'ascensore */
    private int power = 0; //stati powerup

    private Animator animator = null;
    private GameObject minimapborder = null;
    private GameObject footprint = null;
    public GameObject doors = null;
    public GameObject lv1 = null;
    public GameObject lv2 = null;
    public GameObject lv3 = null;
    public GameObject z1 = null;
    public GameObject z2 = null;
    public GameObject z3 = null;
    public GameObject z4 = null;
    public GameObject db = null;
    public GameObject gun = null;
    public GameObject countdown = null;

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
        if (state == 0)
            icons(gm.getState());
    }

    private void icons(int state)
    {
        switch (state)
        {
            case 1:
                db.SetActive(true);
                gun.SetActive(true);
                //gun2.SetActive(true);
                gunsIcons(-1);
                break;
            case 2:
                db.SetActive(false);
                doors.SetActive(true);
                lv1.SetActive(true);
                break;
            case 3:
                lv1.SetActive(false);
                lv2.SetActive(true);
                lv3.SetActive(true);
                break;
            case 4:
                lv2.SetActive(false);
                lv3.SetActive(true);
                break;
            case 5:
                countdown.SetActive(true);
                break;
                //default: break;
        }
    }

    private void gunsIcons(int i)
    {
        //Transform t = getGunsTransform();
        if(i>-1)
            switch (i)
            {
                case 1:
                    gun.SetActive(false);
                    break;
                case 2:
                    //gun2.SetActive(false);
                    break;
                case 3:
                    //gun3.SetActive(false);
                    break;
                case 4:
                    //gun4.SetActive(false);
                    break;
            }
        else
        {
            //gun1.transform.position = t.position;
            //gun2.transform.position = t.position;
            //gun3.transform.position = t.position;
            //gun4.transform.position = t.position;
        }
    }

    private void setTime(int time)
    {
        Time.timeScale = time;
        /*if (time == 0)
            GameObject.Find("volpe").GetComponent<MovementController>().enabled = false;
        else
            GameObject.Find("volpe").GetComponent<MovementController>().enabled = true;*/
    }
}