using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Minimap : MonoBehaviour
{
    private Animator animator = null;
    private GameObject minimapborder = null;
    private GameObject footprint = null;
    public GameObject m1 = null;
    public GameObject m2 = null;
    public GameObject m3 = null;

    private bool open = false;
    public GameObject pg;

    //public Pause pause;

    private void Start()
    {
        animator = GetComponent<Animator>();
        minimapborder = GameObject.Find("MinimapBorder");
        footprint = GameObject.Find("Footprint");
        //m1 = GameObject.Find("marker_hs");
        //m2 = GameObject.Find("marker_gara");
        //m3 = GameObject.Find("marker_mom");
        //volpe = GameObject.Find("Volpe");
        keyEscape();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M) /*&& FindObjectOfType<DialogueManager>().ended && !volpe.GetComponent<MovementController>().blocca*/)
        {
            if (!open)
                keyM();
            else
                keyEscape();
        }

        //if (Input.GetKeyDown(KeyCode.M) && open)
        //{
        //    keyEscape();
        //}
    }

    void keyM()
    {
        if (animator.GetBool("esc") /*!pause.open*/)
        {
            //FindObjectOfType<AudioManager>().Play("slide_dialogue");
            open = true;
            minimapborder.SetActive(true);
            footprint.SetActive(true);
            if (m1)
                m1.SetActive(true);
            if (m2)
                m2.SetActive(true);
            if (m1)
                m3.SetActive(true);
            animator.SetBool("esc", false);
            //Time.timeScale = 0;
        }
    }

    public void keyEscape()
    {
        if (!animator.GetBool("esc"))
        {
            //FindObjectOfType<AudioManager>().Play("slide_dialogue");
            setTime(1);
            open = false;
            animator.SetBool("esc", true);
            minimapborder.SetActive(false);
            footprint.SetActive(false);
            m1.SetActive(false);
            m2.SetActive(false);
            m3.SetActive(false);
        }
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
