using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorManager : MonoBehaviour
{
    
    [SerializeField] private float _velocity = 2f;
    [SerializeField] private int _timer = 5;
   
    private float _opened = 9f;
    private float _closed = 0f;
    private int _counter;

    private bool _toOpen = false;
    private bool _toClose = false;
    private bool _prepareToClose = false;

    public void CloseDoor()
    {
        _prepareToClose = true;
    }


    void Start()
    {
        //_toOpen = true;
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
            _toOpen = true;


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


        if(_prepareToClose)
        {
            Debug.Log("Prepare Closing");

            _counter = Mathf.RoundToInt(Time.time);
            _toClose = true;
            _prepareToClose = false;

        }

        Debug.Log("Counter:" + _counter);

        if (Mathf.RoundToInt(Time.time) - _counter == _timer * 60)
        {
            Debug.Log("TIMER!");

            _toClose = true;
        }


        if (_toClose)     
        {
            Debug.Log("Closing door");

            var _closedPos = new Vector3(
                    transform.position.x,
                    _closed,
                    transform.position.z);

            transform.position = Vector3.MoveTowards(transform.position, _closedPos, _velocity * Time.deltaTime);

            if (transform.position == _closedPos)
                _toClose = false;
        }
        


        //if (Mathf.RoundToInt(Time.time) == _timer)
        //    CloseDoor();

    }
}
