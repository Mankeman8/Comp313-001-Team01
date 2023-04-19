using Cinemachine;
using System;
using System.Diagnostics;
using UnityEngine;

public class Gun : MonoBehaviour
{
    public int damage = 10;
    public float range = 100f;
    public float fireRate = 15f;
    public float impactForce = 30f;

    public Camera fpscamera;
    public ParticleSystem muzzleflash;
    public GameObject impactEffect;
    public AudioSource gunShot;

    public bool isDead = false;

    private float nextTimeToFire = 0f;

    private void Start()
    {
        muzzleflash = FindObjectOfType<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isDead || FindObjectOfType<GameManager>().pausedGame)
        {
            return;
        }
        if (Input.GetButton("Fire1") && Time.time >= nextTimeToFire)
        {
            fpscamera = FindObjectOfType<Camera>();
            gunShot = GameObject.Find("PlayerCapsule").GetComponent<AudioSource>();
            nextTimeToFire = Time.time + 1f / fireRate;
            Shoot();
        }

    }

    void Shoot()
    {
        muzzleflash.Play();
        gunShot.Play();
        RaycastHit hit;
        if (Physics.Raycast(fpscamera.transform.position, fpscamera.transform.forward, out hit, range))
        {
            Target target = hit.transform.GetComponent<Target>();
            if (target != null)
            {
                target.TakeDamage(damage);
                FindObjectOfType<GameManager>().DamageDealt(damage);
            }

            if (hit.rigidbody != null)
            {
                hit.rigidbody.AddForce(-hit.normal * impactForce);
            }

            GameObject impactGO = Instantiate(impactEffect, hit.point, Quaternion.LookRotation(hit.normal));
            Destroy(impactGO, 0.2f);
        }
    }
}