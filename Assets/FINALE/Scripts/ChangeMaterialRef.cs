using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeMaterialRef : MonoBehaviour
{

    public Material NewMatRef;
    public int _thisEnum;
  

    private Material _actualMaterial;
    private bool _dbFound = false;
    private bool _prepareToChange = false;
    private int _counter;
 


    public void DbFound()
    {
        _prepareToChange = true;

    }


    void Start()
    {
        _thisEnum = 1;
        _actualMaterial = GetComponent<Renderer>().material;

    }



    void Update()
    {

        if (_prepareToChange)
        {

            _counter = Mathf.RoundToInt(Time.time);
            _dbFound = true;
            _prepareToChange = false;

        }

        if (_dbFound)
            if (Mathf.RoundToInt(Time.time) - _counter == _thisEnum)
                this.GetComponent<Renderer>().material = NewMatRef;


        //if (Mathf.RoundToInt(Time.time) == 10)
        //    _prepareToChange = true;

    }
}
