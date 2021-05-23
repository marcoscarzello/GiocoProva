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
    public int totAmmo; //in realtà è la scorta (totale munizioni escluse quelle in canna)
    private int currentAmmo;
    public float reloadTime;
    private bool isReloading = false;

    public Camera fpsCam;
    public ParticleSystem muzzleFlash = null;
    public GameObject impactEffect;
    public float impactForce;

    private float nextTimeToFire = 0f;

    public Animator animator;

    void Start()
    {
        RicaricaAutomatica();
    }

    void RicaricaAutomatica()
    {
        if (totAmmo > maxAmmo)
        {
            currentAmmo = maxAmmo;
        }
        else
            currentAmmo = totAmmo;

        totAmmo -= maxAmmo;
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

        if (currentAmmo <= 0 && totAmmo > 0)
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

        muzzleFlash.Play();
        animator.SetBool("isShooting", true);
        animator.SetBool("isShooting", false);

        currentAmmo--;

        RaycastHit hit; //grazie ad out hit, hit contiene tutte le info sul colpo
        if (Physics.Raycast(fpsCam.transform.position, fpsCam.transform.forward, out hit, range)) //ritorna true se colpisce 
        {
            Nemico nemico = hit.transform.GetComponent<Nemico>();
            if (nemico != null)
            {
                nemico.TakeDamage(damage);
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