using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToGridPos : MonoBehaviour
{

    public void AdjustPosition()
    {

        var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x),
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.RoundToInt(this.transform.position.z)
            );

        this.transform.position = position;
    }

    void Start()
    {
        AdjustPosition();
    }


}
