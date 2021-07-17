using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BearFire : Hero
{
    Animator myAnimator;
    Component myWeapon;

    protected override void Start()
    {
        base.Start();
        myAnimator = GetComponent<Animator>();
        myWeapon = GetComponentInChildren<Weapon>(); // �ٸ� ��ũ��Ʈ ����
    }

    public override void Attack()
    {
        if (Input.GetMouseButtonDown(0))
        {         
            myAnimator.SetTrigger("tBAttack");
            GetComponentInChildren<Weapon>().Shot();
        }

        if (Input.GetMouseButtonDown(1))
        {

        }
    } 
}
