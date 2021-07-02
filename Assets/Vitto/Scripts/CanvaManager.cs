using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvaManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public bool flag = false;
    public float restartDelay = 1f;
    public int state = 0; /*1- db     2- mappa armi e nemico lv1      3- lv2      4- lv3     5- timer torna all'ascensore */

    //const int n_guns = 
    private float timer = 0f;
    private float waitingTime = 5.0f;
    public GameObject completeLevelUI;
    //public GameObject barH = null;
    //public GameObject barE = null;
    private Slider health = null;
    private Slider energy = null;
    private Color fill;
    //private GameObject sun;
    //private Animator sunAnimator;

    private GestioneParamsInRete mirror = null;

    public void Start()
    {
        mirror = FindObjectOfType<GestioneParamsInRete>().GetComponent<GestioneParamsInRete>();
        health = FindObjectOfType<HealthBar>().GetComponent<Slider>();
        energy = FindObjectOfType<EnergyBar>().GetComponent<Slider>();
        Cursor.visible = false;
    }

    public void Update()
    {
        //timer += Time.deltaTime;
        //if (timer > waitingTime)
        //{
        //    timer = 0f;
        //    bars();
        //}
        Invoke("barsFill", waitingTime);
    }

    private void barsFill()
    {
        health.value+= mirror.salute;
        energy.value += mirror.energia;
        //fill = health.color;
        //fill.a += mirror.salute;
        //health.color = fill;
        //fill = energy.color;
        //fill.a += mirror.energia;
        //energy.color = fill;
    }

    public void CompleteLevel()
    {
        completeLevelUI.SetActive(true);
    }

    public void EndGame ()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            Invoke("Restart",restartDelay);
        }
    }
    public void setState(int s) { state = s; }

    public void accState() { state++; }

    public int getState() { return state; }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public Vector3[] getGunsPosition()
    {
        Vector3[] v = new Vector3[2];
        return v;
    }

    public Vector3[] getDoorsPosition()
    {
        Vector3[] v = new Vector3[2];
        return v;
    }

    public Vector3 getPgPosition() { return new Vector3(0f, 0f, 0f); }
}
