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
    //public GameObject lv2 = null;
    public GameObject lv3 = null;
    public GameObject z1 = null;
    public GameObject z2 = null;
    public GameObject z3 = null;
    public GameObject z4 = null;
    public GameObject db = null;

    public GameObject doors = null;
    private Transform[] door = null;
    //private Vector3[] doors_positions = null;
    public GameObject guns = null;
    private Transform[] gun = null;
    //private Vector3[] guns_positions = null;
    public GameObject countdown = null;

    private CanvaManager cm = null;

    private bool open = false;
    public GameObject pg;

    private float timer = 0f;
    private float waitingTime = 5.0f; //tempo di refresh mappa
    private Vector3 random;
    const uint randomRange = 30; //di quanto si sposta

    private GestioneParamsInRete mirror =null;

    private void Start()
    {
        cm = FindObjectOfType<CanvaManager>();
        mirror = FindObjectOfType<GestioneParamsInRete>().GetComponent<GestioneParamsInRete>(); 
        //guns_positions = cm.getGunsPosition(); //array guns positions da mirror
        //doors_positions = cm.getDoorsPosition();
        random = new Vector3(0f, 0f, 0f);
    }

    void Update()
    {
        if(state!= cm.getState()) //così icons lavora una volta sola per stato, nel default fa in modo di non essere richiamato
            icons(cm.getState());
        Invoke("refreshMap", waitingTime);
        //timer += Time.deltaTime;
        //if (timer > waitingTime)
        //{
        //    timer = 0f;
        //    refreshMap();
        //}
    }

    private void refreshMap()
    {
        random.x = Random.Range(-randomRange, randomRange);
        random.y = Random.Range(-randomRange, randomRange);
        pg.transform.position = mirror.posizioneShooter + random; //posizione shooter randomica
        switch (state)
        {
            case 2:
                lv1.transform.position = mirror.posizionelv1;
                break;
            case 3:
                lv2_1.transform.position = mirror.posizionelv2_1;
                lv2_2.transform.position = mirror.posizionelv2_2;
                lv3.transform.position = mirror.posizionelv3;
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

                gun = guns.GetComponentsInChildren<Transform>();
                foreach (Transform g in gun)
                {
                    g.gameObject.SetActive(true);
                }
                gunsIcons(-1); //place guns
                break;

            case 3:
                lv1.SetActive(false);
                lv2_1.SetActive(true);
                lv2_2.SetActive(true);
                lv3.SetActive(true);

                door = doors.GetComponentsInChildren<Transform>();
                foreach (Transform d in door)
                {
                    d.gameObject.SetActive(true);
                }
                doorsIcons(-1); //place doors
                break;

            case 4:
                countdown.SetActive(true); //torna all'ascensore
                break;

            default:
                break;
        }
        state = cmstate;
    }

    private void gunsIcons(int i) //place and destroy
    {
        if (i > -1)
            Destroy(gun[i].gameObject, .5f);
        else
        {
            int j = 0;
            foreach (Transform g in gun)
            {
                g.position = mirror.posizioniArmi[j++];
            }
        }
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