using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons_Trigger : MonoBehaviour
{

    public GameObject WeaponHolder;
    public GameObject WeaponsContainer;

    private Collider other;
    private bool trigger = false;

    private void Update()
    {
        if (trigger)
        {
            if (Input.GetKey(KeyCode.F))
            {
                for (int i = 0; i < WeaponHolder.transform.childCount; i++)
                {
                    if (other.tag == WeaponHolder.transform.GetChild(i).tag)
                    {
                        GameObject w = WeaponHolder.transform.GetChild(i).transform.gameObject;
                        w.SetActive(true);
                        w.transform.parent = WeaponsContainer.transform;
                        w.transform.position = new Vector3(this.gameObject.transform.position.x, 1.3F, this.gameObject.transform.position.z);
                        w.transform.rotation = new Quaternion(0, 0, 0, 1);
                        StartCoroutine(EnableCollider(w));
                        break;
                    }
                }

                if (other.tag == "ArmaRossa" || other.tag == "ArmaVerde" || other.tag == "ArmaBlu")
                {
                    for (int i = 0; i < WeaponHolder.transform.childCount; i++)
                    {
                        WeaponHolder.transform.GetChild(i).gameObject.SetActive(false);
                    }

                    other.transform.parent = WeaponHolder.transform;

                    switch (other.name)
                    {
                        case "pistola(Clone)":
                            other.transform.localPosition = new Vector3(0.685840011F, -0.750779986F, 1.26320004F);
                            other.transform.localRotation = new Quaternion(-0.5F, 0.5F, 0.5F, 0.5F);
                            other.GetComponent<BoxCollider>().enabled = false;
                            break;
                        case "pompa(Clone)":
                            other.transform.localPosition = new Vector3(0.540000021F, -0.720000029F, 0.743059993F);
                            other.transform.localRotation = new Quaternion(-0.5F, 0.5F, 0.5F, 0.5F);
                            other.GetComponent<BoxCollider>().enabled = false;
                            break;
                        case "assalto(Clone)":
                            other.transform.localPosition = new Vector3(0.664579988F, -0.477950007F, 2.46539998F);
                            other.transform.localRotation = new Quaternion(-0.5F, 0.5F, 0.5F, 0.5F);
                            other.GetComponent<BoxCollider>().enabled = false;
                            break;
                        default:
                            Debug.Log("Problema WeaponHolder - Switch in default");
                            break;
                    }
                }
                trigger = false;
            }
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        other = collider;
        trigger = true;
    }

    private void OnTriggerExit(Collider collider)
    {
        trigger = false;
    }

    IEnumerator EnableCollider(GameObject w)
    {
        yield return new WaitForSeconds(1);
        w.GetComponent<BoxCollider>().enabled = true;
    }
}
