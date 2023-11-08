using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    SoldierHealth soldier;
    SoldierHandControl firstSoldier;

    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject bullet;
    public float speed;
    private float lastShootTime;
    public float delay;

    public void Start()
    {
        soldier = GameObject.FindGameObjectWithTag("SoldierParent").GetComponent<SoldierHealth>();

        firstSoldier = GameObject.FindGameObjectWithTag("FirstSoldier").GetComponent<SoldierHandControl>();
        
        if(gameObject.CompareTag("Machine")) speed = firstSoldier.machine.GetComponent<GunController>().speed;

        if (gameObject.CompareTag("Rifle")) speed = firstSoldier.rifle.GetComponent<GunController>().speed;

        if (gameObject.CompareTag("Pistol")) speed = firstSoldier.pistol.GetComponent<GunController>().speed;
    }

    public void Update()
    {
        if (soldier.death) return;

        if (Input.GetButton("Fire1") && Time.time >= lastShootTime + delay)
        {
            Shoot();
        }   
    }

    private void Shoot()
    {
        GameObject bulletClone = Instantiate(bullet, shootPoint.transform.position, shootPoint.transform.rotation);

        bulletClone.GetComponent<Rigidbody>().AddForce(shootPoint.transform.forward * speed *50);

        Destroy(bulletClone, 1.5f);
        lastShootTime = Time.time;
    }
}
