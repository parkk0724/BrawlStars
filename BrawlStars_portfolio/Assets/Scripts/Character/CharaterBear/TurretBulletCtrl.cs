﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletCtrl : MonoBehaviour
{
    private Rigidbody rid;
    private float Speed;

    private void Start()
    {
        Speed = 200.0f;
        rid = GetComponent<Rigidbody>();
        rid.AddForce(transform.forward * Speed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Monster"))
        {
            other.GetComponent<Monster>().Hit(10, Color.red);
            Destroy(this.gameObject);
        }
        //else
        //    Destroy(gameObject);
    }
}