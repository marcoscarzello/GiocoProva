using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private int state=0; /*1- db     2- mappa armi e nemico lv1      3- lv2 , lv3      4 timer torna all'ascensore */

    private Animator animator = null;
    private GameObject minimapborder = null;
    private GameObject footprint = null;
    public GameObject lv1 = null;
    public GameObject lv2_1 = null;
    public GameObject lv2_2 = null;
    public GameObject lv3 = null;
    private RectTransform r1 = null;
    private RectTransform r2_1 = null;
    private RectTransform r2_2 = null;
    private RectTransform r3 = null;
    //public GameObject z1 = null;
    //public GameObject z2 = null;
    //public GameObject z3 = null;
    //public GameObject z4 = null;
    public GameObject db = null;

    public GameObject doors = null;
    private Transform[] door = null;
    //private Vector3[] doors_positions = null;
    public GameObject guns = null;
    private RectTransform[] gun = null;
    //private Vector3[] guns_positions = null;
    public GameObject countdown = null;

    private CanvaManager cm = null;

    private bool open = false;
    private RectTransform pg;

    private float timer = 0f;
    private float timer2 = 0f;
    private float waitingTime = 2.0f; //tempo di refresh mappa
    private Vector3 random;
    private Vector3 inv; //vettore con y e z invertiti
    const uint randomRange = 30; //di quanto si sposta
    private int kill = 1;
    //private bool[] uccisi= { false, false, false, false };

    private GestioneParamsInRete mirror =null;
    private DBmanager dbm = null;

    private void Start()
    {
        cm = FindObjectOfType<CanvaManager>();
        mirror = FindObjectOfType<GestioneParamsInRete>().GetComponent<GestioneParamsInRete>();
        //guns_positions = cm.getGunsPosition(); //array guns positions da mirror
        //doors_positions = cm.getDoorsPosition();
        random = new Vector3(0f, 0f, 0f);
        inv = new Vector3(0f, 0f, 0f);
        dbm = GameObject.Find("Panels").GetComponent<DBmanager>();
        pg = GameObject.Find("Pg").GetComponent<RectTransform>();
        r1 = lv1.GetComponent<RectTransform>();
        r2_1 = lv2_1.GetComponent<RectTransform>();
        r2_2 = lv2_2.GetComponent<RectTransform>();
        r3 = lv3.GetComponent<RectTransform>();

        gun = guns.GetComponentsInChildren<RectTransform>();
        foreach (RectTransform g in gun)
            g.gameObject.SetActive(false);
    }

    void Update()
    {
        if(state!= cm.getState()) //così icons lavora una volta sola per stato, nel default fa in modo di non essere richiamato
            icons(cm.getState());
        //Invoke("refreshMap", waitingTime);
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            timer = 0f;
            refreshMap();
            Debug.Log("dentro" + cm.getState());
        }
        Debug.Log("fuori" + cm.getState());
        //Debug.Log(pg.rectra);
    }

    private void refreshMap()
    {
        random.x = Random.Range(-randomRange, randomRange);
        random.y = Random.Range(-randomRange, randomRange);
        pg.anchoredPosition = inverti(mirror.posizioneShooter) + random; //posizione shooter randomica
        
        //gunsIcons();

        Debug.Log("state: " + cm.getState());
        switch (cm.getState())
        {
            case 1:
                if (dbm.DBtrovato)
                {
                    //timer2 += Time.deltaTime;
                    //if (timer2 > waitingTime)
                    //{
                        cm.setState(2);
                        db.SetActive(false);
                    //}
                }
                break;
            case 2:
                r1.anchoredPosition = inverti(mirror.posizionelv1);
                if (r1.anchoredPosition.x < -900f)
                {
                    cm.setState(3);
                    lv1.SetActive(false);
                }
                break;
            case 3:
                Debug.Log("mirror.posizionelv2_1" + mirror.posizionelv2_1);
                r2_1.anchoredPosition = inverti(mirror.posizionelv2_1);
                r2_2.anchoredPosition = inverti(mirror.posizionelv2_2);
                r3.anchoredPosition = inverti(mirror.posizionelv3);
                if (r2_1.anchoredPosition.x < -900f)
                {
                    //uccisi[1] = true;
                    kill++;
                    lv2_1.SetActive(false);
                    Debug.Log("KILL " + kill);
                } /*
                else
                {
                    //uccisi[1] = true;
                    kill--;
                    lv2_1.SetActive(true);
                } */
                if (r2_2.anchoredPosition.x < -900f)
                {
                    //uccisi[2] = true;
                    kill++;
                    lv2_2.SetActive(false);

                    Debug.Log("KILL " + kill);
                } /*
                else
                {
                    //uccisi[2] = true;
                    kill--;
                    lv2_2.SetActive(true);
                } */
                if (r3.anchoredPosition.x < -900f)
                {
                    //uccisi[3] = true;
                    kill++;
                    lv3.SetActive(false);
                    Debug.Log("KILL " + kill);
                } /*
                else
                if (r3.anchoredPosition.x < -900f)
                {
                    //uccisi[3] = true;
                    kill--;
                    lv3.SetActive(true);
                } */
                if (kill == 4)
                {
                    cm.setState(4);
                    countdown.SetActive(true);
                    Debug.Log("KILL " + kill);
                }
                    break;
            case 4:
                if (!countdown.activeSelf) countdown.SetActive(true);
                break;
            default:
                break;
        }
    }

    private void icons(int cmstate)
    {
        switch (cmstate)
        {
            case 1:
                db.SetActive(true);
                //gun2.SetActive(true);
                //gunsIcons(-1);
                break;

            case 2:
                db.SetActive(false);
                lv1.SetActive(true);
                guns.SetActive(true);
                gunsIcons();
                refreshMap();
                break;

            case 3:
                doors.SetActive(true);
                lv1.SetActive(false);
                lv2_1.SetActive(true);
                lv2_2.SetActive(true);
                lv3.SetActive(true);

                door = doors.GetComponentsInChildren<Transform>();
                foreach (Transform d in door)
                {
                    d.gameObject.SetActive(true);
                }
                refreshMap();
                break;

            case 4:
                countdown.SetActive(true); //torna all'ascensore
                refreshMap();
                break;

            default:
                break;
        }
        state = cmstate;
    }

    private void gunsIcons() //place and destroy
    {
        Debug.Log("gunnnnnnnnnnnnnnnnnnnnnnnnnnnn " + gun.Length);
        foreach (int i in mirror.posizioniArmi)
        {
            gun[i].gameObject.SetActive(true);
        }

        //if (i > -1)
        //    Destroy(gun[i].gameObject, .5f);
        //else
        //{
        //int j = 0;
        //bool skip = true; 
        //foreach (RectTransform g in gun)
        //{
        //    //Debug.Log("Gun lenght" + gun.Length);
        //    //Debug.Log("Mirror lenght" + mirror.posizioniArmi.Count);
        //    if (skip)
        //        skip = false;
        //    else if (g.gameObject.active)
        //    {
        //        Debug.Log("Gun POSITION" + mirror.posizioniArmi[j]);

            //        if (mirror.posizioniArmi[j].y > -5.65f)
            //        {
            //            Debug.Log("mirror.posizioniArmi[j].y" + mirror.posizioniArmi[j].y);
            //            g.gameObject.SetActive(false);
            //            j++;
            //        }
            //        else
            //        {
            //            g.anchoredPosition = inverti(mirror.posizioniArmi[j]);
            //            j++;
            //        }

            //    }
            //}
    }

    private Vector3 inverti(Vector3 vec)
    {
        inv.x=vec.x;
        inv.y = vec.z;
        return inv;
    }

    private void doorsIcons(int i) //place and destroy
    {
        Destroy(door[i].gameObject, .5f);
        //if (i > -1)
        //    Destroy(door[i].gameObject, .5f);
        //else
        //{
        //    int j = 0;
        //    foreach (Transform d in door)
        //    {
        //        d.position = mirror.posizioniArmi[j++];
        //    }
        //}
    }
}