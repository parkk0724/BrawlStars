using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawFire : MonoBehaviour
{
    public GameObject BulletObject;
    protected int nBulletDamage = 0;
    protected float fBulletSpeed = 0.0f;

    void Start()
    {
        nBulletDamage = 100;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(0))
        {

        }
    }

    private void OnTriggerEnter(Collider other) //������ٵ� �ִ� �� ������ ��
    {
        if (other.tag.Equals("Player"))
        {
            other.GetComponentInChildren<Hero>().Hit(nBulletDamage);
            Destroy(this.gameObject);
        }
    }
}
