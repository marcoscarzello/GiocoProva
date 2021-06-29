using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    
    [SerializeField] private float _velocity = 2f;
    [SerializeField] private int _timer = 10;
   
    private float _opened = 9f;
    private float _closed = 0f;

    private bool _toOpen = false;
    private bool _toClose = false;

    public void CloseDoor()
    {
        _toClose = true;
    }


    void Start()
    {
        _toOpen = true;
    }


    void Update()
    {

        if (_toOpen)
        {
            var _openedPos = new Vector3(
                   transform.position.x,
                   _opened,
                   transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, _openedPos, _velocity * Time.deltaTime);

            if (transform.position == _openedPos)
                _toOpen = false;
        }


        if (_toClose)
        {
            var _closedPos = new Vector3(
                   transform.position.x,
                   _closed,
                   transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, _closedPos, _velocity * Time.deltaTime);

            if (transform.position == _closedPos)
                _toClose = false;
        }


        if (Mathf.RoundToInt(Time.time) == _timer*60)
            CloseDoor();

    }
}
