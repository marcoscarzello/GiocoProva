using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvaManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public bool flag = false;
    public float restartDelay = 1f;
    public int state = 0; /*1- db     2- mappa armi e nemico lv1      3- lv2      4- lv3     5- timer torna all'ascensore */

    //const int n_guns = 

    public GameObject completeLevelUI;

    //private GameObject sun;
    //private Animator sunAnimator;

    public void Start()
    {
        Cursor.visible = false;
    }

    //public void Update()
    //{
    //    if (flag)
    //    {
    //        flag = false;
    //        setSun();
    //    }
    //}

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
