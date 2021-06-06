using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Labirinto : MonoBehaviour
{
    //Vector3 rot = new Vector3(0 ,360, 0);

    void Start()
    {
        //transform.DORotate(rot, 2f, RotateMode.Fast).SetLoops(-1);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, 5 * Time.deltaTime);
    }
}
