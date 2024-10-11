using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 25f;
    [SerializeField] bool IsBullet = false;

    // Start is called before the first frame update
    void Start()
    {
        Items.OnSpeedPickup += Addedspeed;
        Items.OnRotatePickup += Addedrotate;
    }

    // Update is called once per frame
    void Update()
    {
        if (IsBullet)
        {
            move(1);
        }
        else
        {
            move(Input.GetAxis("Vertical"));
            Rotate();
        }
    }
    public void Addedspeed(int amount)
    {
        moveSpeed += amount;
    }
    public void Addedrotate(int amount)
    {
        rotationSpeed += amount;
    }
    void Rotate()
    {
        transform.Rotate(transform.up * rotationSpeed * Time.deltaTime * Input.GetAxis("Horizontal"));
    }
    void move(float axis)
    {
        transform.position = transform.position + transform.forward * moveSpeed * axis * Time.deltaTime;
    }
}
