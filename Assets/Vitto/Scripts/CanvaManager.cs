using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CanvaManager : MonoBehaviour
{
    private bool gameHasEnded = false;
    public bool gameStarted = false;
    public float restartDelay = 1f;
    public int state = 1; /*1- db     2- mappa armi e nemico lv1      3- lv2 , lv3      4 timer torna all'ascensore */

    private float timer = 0f;
    private float waitingTime = 5.0f;
    public GameObject completeLevelUI;
    public GameObject gameOverUI;
    private Slider health = null;
    private Slider energy = null;
    private Slider integrity = null;

    //public GameObject barH = null;
    //public GameObject barE = null;
    //private Color fill;
    //private GameObject sun;
    //private Animator sunAnimator;

    private GestioneParamsInRete mirror = null;

    public void Start()
    {
        gameHasEnded = false;
        Cursor.visible = true;

        mirror = FindObjectOfType<GestioneParamsInRete>().GetComponent<GestioneParamsInRete>();
        health = FindObjectOfType<HealthBar>().GetComponent<Slider>();
        energy = FindObjectOfType<EnergyBar>().GetComponent<Slider>();
        integrity = FindObjectOfType< UIntegrityBar> ().GetComponent<Slider>();
        health.value = mirror.salute;
        energy.value = mirror.energia;
    }

    public void Update()
    {
        Cursor.visible = true;

        if (integrity.value <= 0 || health.value <= 0)
            EndGame();
        timer += Time.deltaTime;
        if (timer > waitingTime)
        {
            timer = 0f;
            barsFill();
        }
        //Invoke("barsFill", waitingTime);
    }

    private void barsFill()
    {
        health.value= mirror.salute;
        energy.value = mirror.energia;

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
        //Invoke("nextScene", restartDelay); //fine livello torna al menu o schermata finale
    }

    public void EndGame ()
    {
        if (!gameHasEnded)
        {
            gameHasEnded = true;
            Debug.Log("GAME OVER");
            //Invoke("Restart",restartDelay);
        }
    }
    public void setState(int s) { state = s; }
    public void accState() { state++; }
    public int getState() { return state; }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    //public Vector3[] getGunsPosition()
    //{
    //    Vector3[] v = new Vector3[2];
    //    return v;
    //}
    //public Vector3[] getDoorsPosition()
    //{
    //    Vector3[] v = new Vector3[2];
    //    return v;
    //}
    //public Vector3 getPgPosition() { return mirror.posizioneShooter; }
}
