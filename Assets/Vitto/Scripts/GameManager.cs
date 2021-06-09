using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    bool gameHasEnded = false;
    public bool flag = false;
    public float restartDelay = 1f;
    public int state = 0; /*1- db     2- mappa armi e nemico lv1      3- lv2      4- lv3     5- timer torna all'ascensore */

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

    //public void setSun()
    //{
    //    int daytime = sunAnimator.GetInteger("daytime") + 1;
    //    sunAnimator.SetInteger("daytime", daytime);
    //    //FindObjectOfType<GameManager>().setSun();
    //}
}
