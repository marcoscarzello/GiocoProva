using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToSemiGridPos : MonoBehaviour
{

    public void AdjustPosition()
    {
        bool flagx = false;
        bool flagz = false;

        if(Mathf.Abs(transform.position.x - Mathf.RoundToInt(this.transform.position.x)) >= 0.25)
            flagx = true;

        if(Mathf.Abs(transform.position.z - Mathf.RoundToInt(this.transform.position.z)) >= 0.25)
            flagz = true;

        if (flagx)
        {
            var position = new Vector3(
            Mathf.Ceil(this.transform.position.x) - 0.5f,
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.RoundToInt(this.transform.position.z)
            );

            Debug.Log(position.x.GetType()==typeof(float));

            

            this.transform.position = position;
        }
        else if (flagz)
        {
            var position = new Vector3(
            Mathf.RoundToInt(this.transform.position.x),
            Mathf.RoundToInt(this.transform.position.y),
            Mathf.Ceil(this.transform.position.z) - 0.5f
            );

            Debug.Log(position.z);

           

            this.transform.position = position;
        }
        else
        {
            var position = new Vector3(
                Mathf.RoundToInt(this.transform.position.x),
                Mathf.RoundToInt(this.transform.position.y),
                Mathf.RoundToInt(this.transform.position.z)
                );

            this.transform.position = position;
        }
    }

    void Start()
    {
        AdjustPosition();
    }
}
