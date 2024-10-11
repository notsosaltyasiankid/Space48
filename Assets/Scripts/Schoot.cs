using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Schoot : MonoBehaviour
{
    [SerializeField] private float cooldownTime = 3f;
    [SerializeField] private GameObject laserPrefab;
    private float cooldownCounter = 0f;
    private void Start()
    {
        Items.OncooldownPickup += addedCooldown;
    }
    private void Update()
    {
        Shoot();
    }
    void addedCooldown(float amount)
    {
        cooldownTime -= amount;
    }
    void Shoot()
    {
        cooldownCounter += Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.Space) && cooldownCounter > cooldownTime)
        {
            GameObject laser = Instantiate(laserPrefab);
            laser.transform.position = transform.position;
            laser.transform.rotation = transform.rotation;
            Destroy(laser, 3f);
            cooldownCounter = 0f;
        }
    }
}
