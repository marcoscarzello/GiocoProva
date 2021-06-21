using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerInputHandler : MonoBehaviour
{
    const int ncubes = 4;
    public int cube = -1;
    public bool beg = false;
    public bool clicked = false;
    public bool repeat = false;
    public GameObject buttons = null;

    //public GameObject press = null;
    //public GameObject rep = null;
    private SimonSaysCube[] children;

    private bool canType=true;
    private bool canClick=true;

    private Ray raycast;
    private RaycastHit raycastHit;

    private Camera mainCamera;

    private SimonSaysGameBoard simonGameBoard;
    private SimonSaysCube lastSimonCubeHit;
    public LayerMask CollisionMask;
    
    private void Awake()
    {
        //children = new GameObject[ncubes];
        mainCamera = Camera.main;
        simonGameBoard = GetComponent<SimonSaysGameBoard>();
        
        //if (String.Equals(gameObject.name, "Press"))
        //    for (int i=0; i<ncubes; i++)
        //        children[i]=press.transform.GetChild(i).gameObject;
        children=GetComponentsInChildren<SimonSaysCube>();
        Debug.Log("children[cube]", children[0]);
    }

    private void Update()
    {
        if (canClick)
            if (clicked/*Input.GetMouseButtonDown(0)*/)
            {
                clicked = false;
                //raycast = mainCamera.ScreenPointToRay(Input.mousePosition);

                //if (Physics.Raycast(raycast, out raycastHit, 10f, CollisionMask.value, QueryTriggerInteraction.Collide))
                {

                    //lastSimonCubeHit = children[cube].GetComponent<SimonSaysCube>();
                    lastSimonCubeHit = children[cube];

                    if (lastSimonCubeHit != null)
                    {
                        lastSimonCubeHit.PlayerSelect();
                    }
                }
            }
            else if (repeat)
            {
                Debug.Log("ripetiiii");
                canClick = false;
                simonGameBoard.setGenerate(false);
            }

        //if (canType && Input.GetKeyDown(KeyCode.Space))
        if (beg && canType)
        {
            simonGameBoard.StartNewGame();
        }
    }

    public void setCube(int c) { cube = c; }

    public void setClicked(bool c) { if(canClick) clicked = c; }

    public void setBeg(bool s) { beg = s; }

    public void setCanClick(bool c) { canClick = c; }

    public void setRepeat(bool r) { repeat = r; }
    //public void enRepeat() { rep.SetActive(true); }
    public bool getRepeat() { return repeat; }

    public void setCanType(bool setting) { canType = setting; }

    public bool getCanclick() { return canClick; }

    public void end() { buttons.SetActive(false); }
}
