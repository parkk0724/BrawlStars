using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JesterBullet : MonoBehaviour
{
    // Start is called before the first frame update
    Rigidbody rigid;
    JesterWeapon jwaepon;
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        jwaepon = FindObjectOfType<JesterWeapon>();
    }

    // Update is called once per frame
    void Update()
    {
        rigid.velocity = transform.forward * 30f;
        Destroy(this.gameObject, 2f);
    }
}
