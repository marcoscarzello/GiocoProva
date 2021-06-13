using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


public class PlayerInputHandler : MonoBehaviour
{
    const int ncubes = 4;
    public int cube = -1;
    public GameObject press = null;
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
        if (canClick && Input.GetMouseButtonDown(0))
        {
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

        if (canType && Input.GetKeyDown(KeyCode.Space))
        {
            simonGameBoard.StartNewGame();
        }
    }

    public void setCube(int c) { cube = c; }

    public void setCanClick(bool setting){canClick = setting;}

    public void setCanType(bool setting){canType = setting;}
}
