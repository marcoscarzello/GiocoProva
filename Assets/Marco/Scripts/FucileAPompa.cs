using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FucileAPompa : MonoBehaviour
{

    public float damage;
    public float range;
    public float fireRate;

    //munizioni
    public int maxAmmo;
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;

    public Camera fpsCam;
    private ParticleSystem muzzleFlash;
    public GameObject impactEffect;
    public float impactForce;

    private float nextTimeToFire = 0f;

    public Animator animator;

    void Start()
    {
        currentAmmo = 0;
        muzzleFlash = GetComponent<ParticleSystem>();

    }

    void RicaricaAutomatica()
    {
        if (MunizioniManager.scortaPompa > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else
            currentAmmo = MunizioniManager.scortaPompa;

        MunizioniManager.scortaPompa -= maxAmmo;
    }

    //impedire che il cambio arma blocchi lo sparo
    void OnEnable()
    {
        isReloading = false;
        animator.SetBool("isReloading", false);
    }

    void Update()
    {
        if (isReloading) return;

        

        if (transform.parent.name == "WeaponHolder" && gameObject.activeSelf)
        {

            if (currentAmmo <= 0 && MunizioniManager.scortaPompa > 0)
            {
                StartCoroutine(Reload());
                return;
            }

            if (Input.GetButtonDown("Fire1") && Time.time >= nextTimeToFire)
            {
                nextTimeToFire = Time.time + 1f / fireRate;         //inutile per la pistola

                if (currentAmmo > 0) Shoot();
            }
        }
    }

    IEnumerator Reload()
    {
        isReloading = true;

        animator.SetBool("isReloading", true);

        yield return new WaitForSeconds(reloadTime);

        RicaricaAutomatica();

        isReloading = false;
        animator.SetBool("isReloading", false);
    }


    void Shoot()
    {
        ParticleSystem.MainModule main = muzzleFlash.main;

        //setto il colore dei muzzleflah in base al colore dell'arma
        switch (tag)
        {

            case "ArmaVerde":
                main.startColor = new Color(0f, 1f, 0f, 1f);
                break;
            case "ArmaRossa":
                main.startColor = new Color(1f, 0f, 0f, 1f);
                break;
            case "ArmaBlu":
                main.startColor = new Color(0f, 0f, 1f, 1f);
                break;
            default:
                break;
        }

        muzzleFlash.Play();
        animator.SetBool("isShooting", true);
        animator.SetBool("isShooting", false);

        currentAmmo--;

        RaycastHit hit; //grazie ad out hit, hit contiene tutte le info sul colpo
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) //ritorna true se colpisce 
        {
            //se il nemico � un nemico grosso di LV1
            NemicoManager nemico = hit.transform.GetComponent<NemicoManager>();
            if (nemico != null)
            {
                nemico.Colpito(damage, tag); //invio danno e colore arma
                                             //Debug.Log(hit.collider.gameObject.tag);             
            }



            //se il nemico � un nemico grosso di LV2

            if (hit.collider != null)
            {
                if (hit.collider.transform != null)
                {
                    if (hit.collider.transform.parent != null)
                    {

                        NemicoManagerLV2 nemicoLV2 = hit.collider.transform.parent.gameObject.GetComponent<NemicoManagerLV2>();
                        if (nemicoLV2 != null)
                        {
                            Debug.Log(hit.transform.gameObject.tag);
                            nemicoLV2.Colpito(damage, tag, hit.transform.gameObject.tag); //invio danno e colore arma
                        }
                    }
                }
            }


            //se il nemico � un nemico grosso di LV3

            if (hit.collider != null)
            {
                if (hit.collider.transform != null)
                {
                    if (hit.collider.transform.parent != null)
                    {

                        NemicoManagerLV3 nemicoLV3 = hit.collider.transform.parent.gameObject.GetComponent<NemicoManagerLV3>();
                        if (nemicoLV3 != null)
                        {
                            Debug.Log(hit.transform.gameObject.tag);
                            nemicoLV3.Colpito(damage, tag, hit.transform.gameObject.tag);
                        }
                    }
                }
            }




            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 2f);
        }
    }
}