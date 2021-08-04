using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretBulletCtrl : MonoBehaviour
{
    private Rigidbody rid;
    private GameObject HitEffect;
    private float Speed;

    private void Start()
    {
        Speed = 200.0f;
        rid = GetComponent<Rigidbody>();
        rid.AddForce(transform.forward * Speed);
        HitEffect = Resources.Load<GameObject>("prefabs/Turret/TurretBulletHitEffect_Fire");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other == null) return;

        if (other.CompareTag("Monster"))
        {
            //Assets / Resources / Prefabs / Turret / TurretBulletHitEffect_Fire.prefab
            other.GetComponent<Monster>().Hit(10, Color.red);
            GameObject obj = Instantiate(HitEffect, transform);
            Destroy(this.gameObject);
        }
        else if (other.tag == "Obstacle" || other.tag == "Wall" || other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }
}