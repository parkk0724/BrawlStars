using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFire : Hero
{
    Animator myAnimator;
    public Weapon myWeapon;

    protected override void Start()
    {
        base.Start();
        myAnimator = GetComponent<Animator>();

        // 여기서 Weapon 이라는 스크립트를 담아와서 쓰려고 하는데
        myWeapon = GetComponentInChildren<Weapon>(); // 다른 스크립트 저장
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {         
            myAnimator.SetTrigger("tBAttack");
            // GetComponentInChildren<Weapon>().Shot();
            myWeapon.Shot();
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
    } 
}
