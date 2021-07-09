using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class WeaponSwitching : MonoBehaviour
{
    public int selectedWeapon = 0; 

    private int previousChildCount = 0;
    private float a;
    public Animator anim;

    public bool staCambiando;

    int previousSW;

    void Start()
    {
        SelectWeapon();
        staCambiando = false;
        anim = GetComponent<Animator>();
    }

    IEnumerator CambioArmaCoroutine(float a)
    {
        yield return new WaitForSeconds(0.42f);

        anim.SetBool("alzata", true);
        if (a > 0f)
        {
            Debug.Log("entra?: " + selectedWeapon);
            if (selectedWeapon >= transform.childCount - 1)
                selectedWeapon = 0;
            else
                selectedWeapon++;
        }
        if (a < 0f)
        {

            if (selectedWeapon <= 0)
                selectedWeapon = transform.childCount - 1;
            else
                selectedWeapon--;
        }

        if (previousSW != selectedWeapon)
        {
            SelectWeapon();
        }

        StartCoroutine(CambioArmaCoroutine2());

        

    }
    IEnumerator CambioArmaCoroutine2() {

        yield return new WaitForSeconds(0.42f);

        staCambiando = false;

        anim.SetBool("alzata", false);
        anim.SetBool("isChanging", false);

    }


    void Update()
    {
      

         previousSW = selectedWeapon;

        //fix bug

        if (transform.childCount != previousChildCount)
        {
            selectedWeapon = transform.childCount - 1;
        }

        previousChildCount = transform.childCount;

        //cambio arma con rotellina

        if (Input.GetAxis("Mouse ScrollWheel") > 0f)
        {
            staCambiando = true;

            a = 1f;
            anim.SetBool("isChanging", true);

            StartCoroutine(CambioArmaCoroutine(a));

            
        }
        if (Input.GetAxis("Mouse ScrollWheel") < 0f)
        {
            staCambiando = true;

            a = -1f;

            anim.SetBool("isChanging", true);
            StartCoroutine(CambioArmaCoroutine(a));

            
        }

        //cambio arma con numeri 

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            selectedWeapon = 0;
        }

        if (Input.GetKeyDown(KeyCode.Alpha2) && transform.childCount >= 2)
        {
            selectedWeapon = 1;
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && transform.childCount >= 3)
        {
            selectedWeapon = 2;
        }


        if (previousSW != selectedWeapon)
        {
            SelectWeapon();
        }
    }

    void SelectWeapon() 
    {
        int i = 0;
        foreach (Transform weapon in transform)
        {
            if (i == selectedWeapon)
                weapon.gameObject.SetActive(true);
            else
                weapon.gameObject.SetActive(false);
            i++;
        }
    }
}
